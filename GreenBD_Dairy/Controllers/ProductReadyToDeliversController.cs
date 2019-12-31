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
    [Authorize(Roles = "Owner,Worker")]
    public class ProductReadyToDeliversController : Controller
    {
        private readonly GreenBD_DairyContext _context;

        public ProductReadyToDeliversController(GreenBD_DairyContext context)
        {
            _context = context;
        }

        // GET: ProductReadyToDelivers
        public async Task<IActionResult> Index(string srctext, int page = 1)
        {
            IQueryable<ProductReadyToDeliver> greenBD_DairyContext = _context.ProductReadyToDeliver.Include(p => p.NumberSign).Include(p => p.ProductQuality).Include(p => p.ProductType).OrderByDescending(e =>e.Id);

            if (!string.IsNullOrEmpty(srctext))
            {
                srctext = srctext.ToUpper();

                greenBD_DairyContext = _context.ProductReadyToDeliver.Include(p => p.NumberSign).Include(p => p.ProductQuality).Include(p => p.ProductType).Where(e =>e.Id.ToString().Contains(srctext)|| e.ProductName.Contains(srctext)||e.ProductQuality.ProductQualityName.Contains(srctext)||e.ProductType.ProductTypeName.Contains(srctext)).OrderByDescending(e =>e.Id);
            }

            ViewBag.TotalCount = greenBD_DairyContext.Count();
            ViewBag.srctext = srctext;

            if (page <= 0) { page = 1; }
            int pageSize = 10;

            return View(await PaginatedList<ProductReadyToDeliver>.CreateAsync(greenBD_DairyContext, page, pageSize));
        }

        // GET: ProductReadyToDelivers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productReadyToDeliver = await _context.ProductReadyToDeliver
                .Include(p => p.NumberSign)
                .Include(p => p.ProductQuality)
                .Include(p => p.ProductType)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (productReadyToDeliver == null)
            {
                return NotFound();
            }

            return View(productReadyToDeliver);
        }

        // GET: ProductReadyToDelivers/Create
        public IActionResult Create()
        {
            ViewData["NumberSignId"] = new SelectList(_context.NumberSign, "NumberSignId", "NumberSignName");
            ViewData["ProductQualityId"] = new SelectList(_context.ProductQuality, "ProductQualityId", "ProductQualityName");
            ViewData["ProductTypeId"] = new SelectList(_context.Set<ProductType>(), "ProductTypeId", "ProductTypeName");
            return View();
        }

        // POST: ProductReadyToDelivers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ProductTypeId,ProductName,ProductQualityId,NumberOfProduct,NumberSignId,WorkerSignature,EntryDate")] ProductReadyToDeliver productReadyToDeliver)
        {
            if (ModelState.IsValid)
            {

                productReadyToDeliver.WorkerSignature = HttpContext.Session.GetString("username");

                productReadyToDeliver.EntryDate = DateTime.Now;

                _context.Add(productReadyToDeliver);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["NumberSignId"] = new SelectList(_context.NumberSign, "NumberSignId", "NumberSignName", productReadyToDeliver.NumberSignId);
            ViewData["ProductQualityId"] = new SelectList(_context.ProductQuality, "ProductQualityId", "ProductQualityName", productReadyToDeliver.ProductQualityId);
            ViewData["ProductTypeId"] = new SelectList(_context.Set<ProductType>(), "ProductTypeId", "ProductTypeName", productReadyToDeliver.ProductTypeId);
            return View(productReadyToDeliver);
        }

        // GET: ProductReadyToDelivers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productReadyToDeliver = await _context.ProductReadyToDeliver.SingleOrDefaultAsync(m => m.Id == id);
            if (productReadyToDeliver == null)
            {
                return NotFound();
            }
            ViewData["NumberSignId"] = new SelectList(_context.NumberSign, "NumberSignId", "NumberSignName", productReadyToDeliver.NumberSignId);
            ViewData["ProductQualityId"] = new SelectList(_context.ProductQuality, "ProductQualityId", "ProductQualityName", productReadyToDeliver.ProductQualityId);
            ViewData["ProductTypeId"] = new SelectList(_context.Set<ProductType>(), "ProductTypeId", "ProductTypeName", productReadyToDeliver.ProductTypeId);
            return View(productReadyToDeliver);
        }

        // POST: ProductReadyToDelivers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ProductTypeId,ProductName,ProductQualityId,NumberOfProduct,NumberSignId,WorkerSignature,EntryDate")] ProductReadyToDeliver productReadyToDeliver)
        {
            if (id != productReadyToDeliver.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {

                    productReadyToDeliver.WorkerSignature = HttpContext.Session.GetString("username");

                    productReadyToDeliver.EntryDate = DateTime.Now;

                    _context.Update(productReadyToDeliver);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductReadyToDeliverExists(productReadyToDeliver.Id))
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
            ViewData["NumberSignId"] = new SelectList(_context.NumberSign, "NumberSignId", "NumberSignName", productReadyToDeliver.NumberSignId);
            ViewData["ProductQualityId"] = new SelectList(_context.ProductQuality, "ProductQualityId", "ProductQualityName", productReadyToDeliver.ProductQualityId);
            ViewData["ProductTypeId"] = new SelectList(_context.Set<ProductType>(), "ProductTypeId", "ProductTypeName", productReadyToDeliver.ProductTypeId);
            return View(productReadyToDeliver);
        }

        // GET: ProductReadyToDelivers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productReadyToDeliver = await _context.ProductReadyToDeliver
                .Include(p => p.NumberSign)
                .Include(p => p.ProductQuality)
                .Include(p => p.ProductType)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (productReadyToDeliver == null)
            {
                return NotFound();
            }

            return View(productReadyToDeliver);
        }

        // POST: ProductReadyToDelivers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var productReadyToDeliver = await _context.ProductReadyToDeliver.SingleOrDefaultAsync(m => m.Id == id);
            _context.ProductReadyToDeliver.Remove(productReadyToDeliver);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductReadyToDeliverExists(int id)
        {
            return _context.ProductReadyToDeliver.Any(e => e.Id == id);
        }
    }
}
