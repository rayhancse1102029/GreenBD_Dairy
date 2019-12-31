using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GreenBD_Dairy.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace GreenBD_Dairy.Controllers
{
    [Authorize(Roles="Owner,Accounts")]
    public class ProductSellsController : Controller
    {
        private readonly GreenBD_DairyContext _context;

        public ProductSellsController(GreenBD_DairyContext context)
        {
            _context = context;
        }

        // GET: ProductSells
        public async Task<IActionResult> Index(string srctext, int page=1)
        {
            IQueryable<ProductSell> greenBD_DairyContext = _context.ProductSell.Include(p => p.AmountSign)
                .Include(p => p.NumberSign).Include(p => p.PaymentMethod).Include(p => p.ProductQuality)
                .Include(p => p.ProductType).OrderByDescending(e =>e.Id);

           

            if (!string.IsNullOrEmpty(srctext))
            {
                srctext = srctext.ToUpper();

                greenBD_DairyContext = _context.ProductSell.Include(p => p.AmountSign).Include(p => p.NumberSign)
                    .Include(p => p.PaymentMethod).Include(p => p.ProductQuality).Include(p => p.ProductType)
                    .Where(e => e.Id.ToString().Contains(srctext) ||e.ProductName.ToUpper().Contains(srctext)||e.ProductType.ToString().ToUpper().Contains(srctext)||e.ManagerSignature.ToUpper().Contains(srctext)).OrderByDescending(e =>e.Id);
            }

             ViewBag.TotalCount = greenBD_DairyContext.Count();
             ViewBag.srctext = srctext;

             if (page <= 0) { page = 1; }
             int pageSize = 10;


            return View(await PaginatedList<ProductSell>.CreateAsync(greenBD_DairyContext,page,pageSize));
        }


        // GET: ProductSells/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productSell = await _context.ProductSell
                .Include(p => p.AmountSign)
                .Include(p => p.NumberSign)
                .Include(p => p.PaymentMethod)
                .Include(p => p.ProductQuality)
                .Include(p => p.ProductType)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (productSell == null)
            {
                return NotFound();
            }

            return View(productSell);
        }

        // GET: ProductSells/Create
        public IActionResult Create()
        {
            ViewData["AmountSignId"] = new SelectList(_context.AmountSign, "AmountSignId", "AmountSignName");
            ViewData["NumberSignId"] = new SelectList(_context.NumberSign, "NumberSignId", "NumberSignName");
            ViewData["PaymentMethodId"] = new SelectList(_context.PaymentMethod, "PaymentMethodId", "PaymentMethodName");
            ViewData["ProductQualityId"] = new SelectList(_context.ProductQuality, "ProductQualityId", "ProductQualityName");
            ViewData["ProductTypeId"] = new SelectList(_context.Set<ProductType>(), "ProductTypeId", "ProductTypeName");
            return View();
        }

        // POST: ProductSells/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ProductTypeId,ProductName,ProductQualityId,Price,AmountSignId,NumberOfProduct,NumberSignId,TotalPrice,PaymentMethodId,Condition,ManagerSignature,EntryDate")] ProductSell productSell)
        {
            if (ModelState.IsValid)
            {
                
                productSell.TotalPrice = productSell.Price * productSell.NumberOfProduct;

                productSell.ManagerSignature = HttpContext.Session.GetString("username"); 

                productSell.EntryDate = DateTime.Now; 

                _context.Add(productSell);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AmountSignId"] = new SelectList(_context.AmountSign, "AmountSignId", "AmountSignName", productSell.AmountSignId);
            ViewData["NumberSignId"] = new SelectList(_context.NumberSign, "NumberSignId", "NumberSignName", productSell.NumberSignId);
            ViewData["PaymentMethodId"] = new SelectList(_context.PaymentMethod, "PaymentMethodId", "PaymentMethodName", productSell.PaymentMethodId);
            ViewData["ProductQualityId"] = new SelectList(_context.ProductQuality, "ProductQualityId", "ProductQualityName", productSell.ProductQualityId);
            ViewData["ProductTypeId"] = new SelectList(_context.Set<ProductType>(), "ProductTypeId", "ProductTypeName", productSell.ProductTypeId);
            return View(productSell);
        }

        // GET: ProductSells/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productSell = await _context.ProductSell.SingleOrDefaultAsync(m => m.Id == id);
            if (productSell == null)
            {
                return NotFound();
            }
            ViewData["AmountSignId"] = new SelectList(_context.AmountSign, "AmountSignId", "AmountSignName", productSell.AmountSignId);
            ViewData["NumberSignId"] = new SelectList(_context.NumberSign, "NumberSignId", "NumberSignName", productSell.NumberSignId);
            ViewData["PaymentMethodId"] = new SelectList(_context.PaymentMethod, "PaymentMethodId", "PaymentMethodName", productSell.PaymentMethodId);
            ViewData["ProductQualityId"] = new SelectList(_context.ProductQuality, "ProductQualityId", "ProductQualityName", productSell.ProductQualityId);
            ViewData["ProductTypeId"] = new SelectList(_context.Set<ProductType>(), "ProductTypeId", "ProductTypeName", productSell.ProductTypeId);
            return View(productSell);
        }

        // POST: ProductSells/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ProductTypeId,ProductName,ProductQualityId,Price,AmountSignId,NumberOfProduct,NumberSignId,TotalPrice,PaymentMethodId,Condition,ManagerSignature,EntryDate")] ProductSell productSell)
        {
            if (id != productSell.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    productSell.TotalPrice = productSell.Price * productSell.NumberOfProduct;

                    productSell.ManagerSignature = HttpContext.Session.GetString("username");


                    _context.Update(productSell);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductSellExists(productSell.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["AmountSignId"] = new SelectList(_context.AmountSign, "AmountSignId", "AmountSignName", productSell.AmountSignId);
            ViewData["NumberSignId"] = new SelectList(_context.NumberSign, "NumberSignId", "NumberSignName", productSell.NumberSignId);
            ViewData["PaymentMethodId"] = new SelectList(_context.PaymentMethod, "PaymentMethodId", "PaymentMethodName", productSell.PaymentMethodId);
            ViewData["ProductQualityId"] = new SelectList(_context.ProductQuality, "ProductQualityId", "ProductQualityName", productSell.ProductQualityId);
            ViewData["ProductTypeId"] = new SelectList(_context.Set<ProductType>(), "ProductTypeId", "ProductTypeName", productSell.ProductTypeId);
            return View(productSell);
        }

        // GET: ProductSells/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productSell = await _context.ProductSell
                .Include(p => p.AmountSign)
                .Include(p => p.NumberSign)
                .Include(p => p.PaymentMethod)
                .Include(p => p.ProductQuality)
                .Include(p => p.ProductType)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (productSell == null)
            {
                return NotFound();
            }

            return View(productSell);
        }

        // POST: ProductSells/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var productSell = await _context.ProductSell.SingleOrDefaultAsync(m => m.Id == id);
            _context.ProductSell.Remove(productSell);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductSellExists(int id)
        {
            return _context.ProductSell.Any(e => e.Id == id);
        }
    }
}
