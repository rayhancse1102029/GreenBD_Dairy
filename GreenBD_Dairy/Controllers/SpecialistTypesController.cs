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
    public class SpecialistTypesController : Controller
    {
        private readonly GreenBD_DairyContext _context;

        public SpecialistTypesController(GreenBD_DairyContext context)
        {
            _context = context;
        }

        // GET: SpecialistTypes
        public async Task<IActionResult> Index()
        {
            return View(await _context.SpecialistType.ToListAsync());
        }

        // GET: SpecialistTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var specialistType = await _context.SpecialistType
                .SingleOrDefaultAsync(m => m.SpecialistTypeId == id);
            if (specialistType == null)
            {
                return NotFound();
            }

            return View(specialistType);
        }

        // GET: SpecialistTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: SpecialistTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SpecialistTypeId,SpecialistTypeName,OwnerSignature,EntryDate")] SpecialistType specialistType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(specialistType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(specialistType);
        }

        // GET: SpecialistTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var specialistType = await _context.SpecialistType.SingleOrDefaultAsync(m => m.SpecialistTypeId == id);
            if (specialistType == null)
            {
                return NotFound();
            }
            return View(specialistType);
        }

        // POST: SpecialistTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SpecialistTypeId,SpecialistTypeName,OwnerSignature,EntryDate")] SpecialistType specialistType)
        {
            if (id != specialistType.SpecialistTypeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(specialistType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SpecialistTypeExists(specialistType.SpecialistTypeId))
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
            return View(specialistType);
        }

        // GET: SpecialistTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var specialistType = await _context.SpecialistType
                .SingleOrDefaultAsync(m => m.SpecialistTypeId == id);
            if (specialistType == null)
            {
                return NotFound();
            }

            return View(specialistType);
        }

        // POST: SpecialistTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var specialistType = await _context.SpecialistType.SingleOrDefaultAsync(m => m.SpecialistTypeId == id);
            _context.SpecialistType.Remove(specialistType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SpecialistTypeExists(int id)
        {
            return _context.SpecialistType.Any(e => e.SpecialistTypeId == id);
        }
    }
}
