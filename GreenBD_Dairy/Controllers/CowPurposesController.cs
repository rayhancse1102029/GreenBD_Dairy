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
    public class CowPurposesController : Controller
    {
        private readonly GreenBD_DairyContext _context;

        public CowPurposesController(GreenBD_DairyContext context)
        {
            _context = context;
        }

        // GET: CowPurposes
        public async Task<IActionResult> Index()
        {
            return View(await _context.CowPurpose.ToListAsync());
        }

        // GET: CowPurposes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cowPurpose = await _context.CowPurpose
                .SingleOrDefaultAsync(m => m.CowPurposeId == id);
            if (cowPurpose == null)
            {
                return NotFound();
            }

            return View(cowPurpose);
        }

        // GET: CowPurposes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CowPurposes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CowPurposeId,CowPurposeName,Description,OwnerSignature,EntryDate")] CowPurpose cowPurpose)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cowPurpose);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cowPurpose);
        }

        // GET: CowPurposes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cowPurpose = await _context.CowPurpose.SingleOrDefaultAsync(m => m.CowPurposeId == id);
            if (cowPurpose == null)
            {
                return NotFound();
            }
            return View(cowPurpose);
        }

        // POST: CowPurposes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CowPurposeId,CowPurposeName,Description,OwnerSignature,EntryDate")] CowPurpose cowPurpose)
        {
            if (id != cowPurpose.CowPurposeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cowPurpose);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CowPurposeExists(cowPurpose.CowPurposeId))
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
            return View(cowPurpose);
        }

        // GET: CowPurposes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cowPurpose = await _context.CowPurpose
                .SingleOrDefaultAsync(m => m.CowPurposeId == id);
            if (cowPurpose == null)
            {
                return NotFound();
            }

            return View(cowPurpose);
        }

        // POST: CowPurposes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cowPurpose = await _context.CowPurpose.SingleOrDefaultAsync(m => m.CowPurposeId == id);
            _context.CowPurpose.Remove(cowPurpose);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CowPurposeExists(int id)
        {
            return _context.CowPurpose.Any(e => e.CowPurposeId == id);
        }
    }
}
