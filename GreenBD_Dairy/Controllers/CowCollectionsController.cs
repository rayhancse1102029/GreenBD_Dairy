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
    //[Authorize(Roles = "Owner")]
    public class CowCollectionsController : Controller
    {
        private readonly GreenBD_DairyContext _context;

        public CowCollectionsController(GreenBD_DairyContext context)
        {
            _context = context;
        }

        // GET: CowCollections
        public async Task<IActionResult> Index()
        {
            return View(await _context.CowCollection.ToListAsync());
        }

        // GET: CowCollections/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cowCollection = await _context.CowCollection
                .SingleOrDefaultAsync(m => m.CowCollectionId == id);
            if (cowCollection == null)
            {
                return NotFound();
            }

            return View(cowCollection);
        }

        // GET: CowCollections/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CowCollections/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CowCollectionId,CowCollectionName,ManagerSignature,EntryDate")] CowCollection cowCollection)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cowCollection);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cowCollection);
        }

        // GET: CowCollections/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cowCollection = await _context.CowCollection.SingleOrDefaultAsync(m => m.CowCollectionId == id);
            if (cowCollection == null)
            {
                return NotFound();
            }
            return View(cowCollection);
        }

        // POST: CowCollections/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CowCollectionId,CowCollectionName,ManagerSignature,EntryDate")] CowCollection cowCollection)
        {
            if (id != cowCollection.CowCollectionId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cowCollection);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CowCollectionExists(cowCollection.CowCollectionId))
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
            return View(cowCollection);
        }

        // GET: CowCollections/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cowCollection = await _context.CowCollection
                .SingleOrDefaultAsync(m => m.CowCollectionId == id);
            if (cowCollection == null)
            {
                return NotFound();
            }

            return View(cowCollection);
        }

        // POST: CowCollections/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cowCollection = await _context.CowCollection.SingleOrDefaultAsync(m => m.CowCollectionId == id);
            _context.CowCollection.Remove(cowCollection);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CowCollectionExists(int id)
        {
            return _context.CowCollection.Any(e => e.CowCollectionId == id);
        }
    }
}
