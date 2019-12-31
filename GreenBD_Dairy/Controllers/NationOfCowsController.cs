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
    public class NationOfCowsController : Controller
    {
        private readonly GreenBD_DairyContext _context;

        public NationOfCowsController(GreenBD_DairyContext context)
        {
            _context = context;
        }

        // GET: NationOfCows
        public async Task<IActionResult> Index()
        {
            return View(await _context.NationOfCow.ToListAsync());
        }

        // GET: NationOfCows/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nationOfCow = await _context.NationOfCow
                .SingleOrDefaultAsync(m => m.CowNationId == id);
            if (nationOfCow == null)
            {
                return NotFound();
            }

            return View(nationOfCow);
        }

        // GET: NationOfCows/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: NationOfCows/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CowNationId,CowNationName,Description,OwnerSignature,EntryDate")] NationOfCow nationOfCow)
        {
            if (ModelState.IsValid)
            {
                _context.Add(nationOfCow);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(nationOfCow);
        }

        // GET: NationOfCows/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nationOfCow = await _context.NationOfCow.SingleOrDefaultAsync(m => m.CowNationId == id);
            if (nationOfCow == null)
            {
                return NotFound();
            }
            return View(nationOfCow);
        }

        // POST: NationOfCows/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CowNationId,CowNationName,Description,OwnerSignature,EntryDate")] NationOfCow nationOfCow)
        {
            if (id != nationOfCow.CowNationId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(nationOfCow);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NationOfCowExists(nationOfCow.CowNationId))
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
            return View(nationOfCow);
        }

        // GET: NationOfCows/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nationOfCow = await _context.NationOfCow
                .SingleOrDefaultAsync(m => m.CowNationId == id);
            if (nationOfCow == null)
            {
                return NotFound();
            }

            return View(nationOfCow);
        }

        // POST: NationOfCows/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var nationOfCow = await _context.NationOfCow.SingleOrDefaultAsync(m => m.CowNationId == id);
            _context.NationOfCow.Remove(nationOfCow);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NationOfCowExists(int id)
        {
            return _context.NationOfCow.Any(e => e.CowNationId == id);
        }
    }
}
