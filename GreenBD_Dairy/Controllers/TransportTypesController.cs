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
    public class TransportTypesController : Controller
    {
        private readonly GreenBD_DairyContext _context;

        public TransportTypesController(GreenBD_DairyContext context)
        {
            _context = context;
        }

        // GET: TransportTypes
        public async Task<IActionResult> Index()
        {
            return View(await _context.TransportType.ToListAsync());
        }

        // GET: TransportTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transportType = await _context.TransportType
                .SingleOrDefaultAsync(m => m.TransportTypeId == id);
            if (transportType == null)
            {
                return NotFound();
            }

            return View(transportType);
        }

        // GET: TransportTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TransportTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TransportTypeId,TransportTypeName,OwnerSignature,EntryDate")] TransportType transportType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(transportType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(transportType);
        }

        // GET: TransportTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transportType = await _context.TransportType.SingleOrDefaultAsync(m => m.TransportTypeId == id);
            if (transportType == null)
            {
                return NotFound();
            }
            return View(transportType);
        }

        // POST: TransportTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TransportTypeId,TransportTypeName,OwnerSignature,EntryDate")] TransportType transportType)
        {
            if (id != transportType.TransportTypeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(transportType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TransportTypeExists(transportType.TransportTypeId))
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
            return View(transportType);
        }

        // GET: TransportTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transportType = await _context.TransportType
                .SingleOrDefaultAsync(m => m.TransportTypeId == id);
            if (transportType == null)
            {
                return NotFound();
            }

            return View(transportType);
        }

        // POST: TransportTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var transportType = await _context.TransportType.SingleOrDefaultAsync(m => m.TransportTypeId == id);
            _context.TransportType.Remove(transportType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TransportTypeExists(int id)
        {
            return _context.TransportType.Any(e => e.TransportTypeId == id);
        }
    }
}
