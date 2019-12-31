using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GreenBD_Dairy.Models;
using Microsoft.AspNetCore.Authorization;

namespace GreenBD_Dairy.Controllers
{
    [Authorize(Roles = "Owner")]
    public class ProductQualitiesController : Controller
    {
        private readonly GreenBD_DairyContext _context;

        public ProductQualitiesController(GreenBD_DairyContext context)
        {
            _context = context;
        }

        // GET: ProductQualities
        public async Task<IActionResult> Index()
        {
            return View(await _context.ProductQuality.ToListAsync());
        }

        // GET: ProductQualities/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productQuality = await _context.ProductQuality
                .SingleOrDefaultAsync(m => m.ProductQualityId == id);
            if (productQuality == null)
            {
                return NotFound();
            }

            return View(productQuality);
        }

        // GET: ProductQualities/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ProductQualities/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductQualityId,ProductQualityName,OwnerSignature,EntryDate")] ProductQuality productQuality)
        {
            if (ModelState.IsValid)
            {
                _context.Add(productQuality);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(productQuality);
        }

        // GET: ProductQualities/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productQuality = await _context.ProductQuality.SingleOrDefaultAsync(m => m.ProductQualityId == id);
            if (productQuality == null)
            {
                return NotFound();
            }
            return View(productQuality);
        }

        // POST: ProductQualities/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductQualityId,ProductQualityName,OwnerSignature,EntryDate")] ProductQuality productQuality)
        {
            if (id != productQuality.ProductQualityId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(productQuality);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductQualityExists(productQuality.ProductQualityId))
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
            return View(productQuality);
        }

        // GET: ProductQualities/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productQuality = await _context.ProductQuality
                .SingleOrDefaultAsync(m => m.ProductQualityId == id);
            if (productQuality == null)
            {
                return NotFound();
            }

            return View(productQuality);
        }

        // POST: ProductQualities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var productQuality = await _context.ProductQuality.SingleOrDefaultAsync(m => m.ProductQualityId == id);
            _context.ProductQuality.Remove(productQuality);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductQualityExists(int id)
        {
            return _context.ProductQuality.Any(e => e.ProductQualityId == id);
        }
    }
}
