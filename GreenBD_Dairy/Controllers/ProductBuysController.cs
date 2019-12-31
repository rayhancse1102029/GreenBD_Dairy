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
    [Authorize(Roles = "Owner,Accounts")]
    public class ProductBuysController : Controller
    {
        private readonly GreenBD_DairyContext _context;

        public ProductBuysController(GreenBD_DairyContext context)
        {
            _context = context;
        }

        // GET: ProductBuys
        public async Task<IActionResult> Index(string srctext, int page = 1)
        {

            IQueryable<ProductBuy> greenBD_DairyContext = _context.ProductBuy.Include(p => p.AmountSign).Include(p => p.NumberSign).Include(p => p.PaymentMethod).Include(p => p.ProductQuality).Include(p => p.ProductType).OrderByDescending(e => e.Id);

            if (!string.IsNullOrEmpty(srctext))
            {
                srctext = srctext.ToUpper();

                greenBD_DairyContext = _context.ProductBuy.Include(p => p.AmountSign).Include(p => p.NumberSign)
                    .Include(p => p.PaymentMethod).Include(p => p.ProductQuality).Include(p => p.ProductType)
                    .Where(e => e.Id.ToString().Contains(srctext) || e.ProductName.ToUpper().Contains(srctext) || e.ProductType.ToString().ToUpper().Contains(srctext)||e.ManagerSignature.ToUpper().Contains(srctext)).OrderByDescending(e => e.Id);
            }


            ViewBag.TotalCount = greenBD_DairyContext.Count();
            ViewBag.srctext = srctext;

            if (page <= 0) { page = 1; }
            int pageSize = 10;

            return View(await PaginatedList<ProductBuy>.CreateAsync(greenBD_DairyContext, page, pageSize));
        }

        // GET: ProductBuys/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productBuy = await _context.ProductBuy
                .Include(p => p.AmountSign)
                .Include(p => p.NumberSign)
                .Include(p => p.PaymentMethod)
                .Include(p => p.ProductQuality)
                .Include(p => p.ProductType)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (productBuy == null)
            {
                return NotFound();
            }

            return View(productBuy);
        }

        // GET: ProductBuys/Create
        public IActionResult Create()
        {
            ViewData["AmountSignId"] = new SelectList(_context.AmountSign, "AmountSignId", "AmountSignName");
            ViewData["NumberSignId"] = new SelectList(_context.NumberSign, "NumberSignId", "NumberSignName");
            ViewData["PaymentMethodId"] = new SelectList(_context.PaymentMethod, "PaymentMethodId", "PaymentMethodName");
            ViewData["ProductQualityId"] = new SelectList(_context.Set<ProductQuality>(), "ProductQualityId", "ProductQualityName");
            ViewData["ProductTypeId"] = new SelectList(_context.Set<ProductType>(), "ProductTypeId", "ProductTypeName");
            return View();
        }

        // POST: ProductBuys/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ProductTypeId,ProductName,ProductQualityId,Price,AmountSignId,NumberOfProduct,NumberSignId,TotalPrice,PaymentMethodId,Condition,ManagerSignature,EntryDate")] ProductBuy productBuy)
        {
            if (ModelState.IsValid)
            {

                productBuy.TotalPrice = productBuy.Price * productBuy.NumberOfProduct;

                productBuy.ManagerSignature = HttpContext.Session.GetString("username");

                productBuy.EntryDate = DateTime.Now;

                _context.Add(productBuy);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AmountSignId"] = new SelectList(_context.AmountSign, "AmountSignId", "AmountSignName", productBuy.AmountSignId);
            ViewData["NumberSignId"] = new SelectList(_context.NumberSign, "NumberSignId", "NumberSignName", productBuy.NumberSignId);
            ViewData["PaymentMethodId"] = new SelectList(_context.PaymentMethod, "PaymentMethodId", "PaymentMethodName", productBuy.PaymentMethodId);
            ViewData["ProductQualityId"] = new SelectList(_context.Set<ProductQuality>(), "ProductQualityId", "ProductQualityName", productBuy.ProductQualityId);
            ViewData["ProductTypeId"] = new SelectList(_context.Set<ProductType>(), "ProductTypeId", "ProductTypeName", productBuy.ProductTypeId);
            return View(productBuy);
        }

        // GET: ProductBuys/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productBuy = await _context.ProductBuy.SingleOrDefaultAsync(m => m.Id == id);
            if (productBuy == null)
            {
                return NotFound();
            }
            ViewData["AmountSignId"] = new SelectList(_context.AmountSign, "AmountSignId", "AmountSignName", productBuy.AmountSignId);
            ViewData["NumberSignId"] = new SelectList(_context.NumberSign, "NumberSignId", "NumberSignName", productBuy.NumberSignId);
            ViewData["PaymentMethodId"] = new SelectList(_context.PaymentMethod, "PaymentMethodId", "PaymentMethodName", productBuy.PaymentMethodId);
            ViewData["ProductQualityId"] = new SelectList(_context.Set<ProductQuality>(), "ProductQualityId", "ProductQualityName", productBuy.ProductQualityId);
            ViewData["ProductTypeId"] = new SelectList(_context.Set<ProductType>(), "ProductTypeId", "ProductTypeName", productBuy.ProductTypeId);
            return View(productBuy);
        }

        // POST: ProductBuys/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ProductTypeId,ProductName,ProductQualityId,Price,AmountSignId,NumberOfProduct,NumberSignId,TotalPrice,PaymentMethodId,Condition,ManagerSignature,EntryDate")] ProductBuy productBuy)
        {
            if (id != productBuy.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {

                    productBuy.TotalPrice = productBuy.Price * productBuy.NumberOfProduct;

                    productBuy.ManagerSignature = HttpContext.Session.GetString("username");

                    _context.Update(productBuy);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductBuyExists(productBuy.Id))
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
            ViewData["AmountSignId"] = new SelectList(_context.AmountSign, "AmountSignId", "AmountSignName", productBuy.AmountSignId);
            ViewData["NumberSignId"] = new SelectList(_context.NumberSign, "NumberSignId", "NumberSignName", productBuy.NumberSignId);
            ViewData["PaymentMethodId"] = new SelectList(_context.PaymentMethod, "PaymentMethodId", "PaymentMethodName", productBuy.PaymentMethodId);
            ViewData["ProductQualityId"] = new SelectList(_context.Set<ProductQuality>(), "ProductQualityId", "ProductQualityName", productBuy.ProductQualityId);
            ViewData["ProductTypeId"] = new SelectList(_context.Set<ProductType>(), "ProductTypeId", "ProductTypeName", productBuy.ProductTypeId);
            return View(productBuy);
        }

        // GET: ProductBuys/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productBuy = await _context.ProductBuy
                .Include(p => p.AmountSign)
                .Include(p => p.NumberSign)
                .Include(p => p.PaymentMethod)
                .Include(p => p.ProductQuality)
                .Include(p => p.ProductType)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (productBuy == null)
            {
                return NotFound();
            }

            return View(productBuy);
        }

        // POST: ProductBuys/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var productBuy = await _context.ProductBuy.SingleOrDefaultAsync(m => m.Id == id);
            _context.ProductBuy.Remove(productBuy);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductBuyExists(int id)
        {
            return _context.ProductBuy.Any(e => e.Id == id);
        }
    }
}
