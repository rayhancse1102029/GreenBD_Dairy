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
    [Authorize(Roles = "Owner")]
    public class DaysController : Controller
    {
        private readonly GreenBD_DairyContext _context;

        public DaysController(GreenBD_DairyContext context)
        {
            _context = context;
        }

        // GET: Days
        public async Task<IActionResult> Index(string srctext, int page = 1)
        {
            IQueryable<Days> db = _context.Days;

            if (!string.IsNullOrEmpty(srctext))
            {
                srctext = srctext.ToUpper();

                db = from item in db
                    where (item.DayId.ToString().Contains(srctext) || item.DayName.ToUpper().Contains(srctext) || item.OwnerSignature.ToUpper().Contains(srctext))
                    select item;
            }

            ViewBag.TotalCount = db.Count();
            ViewBag.srctext = srctext;

            if (page <= 0) { page = 1; }
            int pageSize = 10;


            return View(await PaginatedList<Days>.CreateAsync(db, page, pageSize));
        }

        // GET: Days/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var days = await _context.Days
                .SingleOrDefaultAsync(m => m.DayId == id);
            if (days == null)
            {
                return NotFound();
            }

            return View(days);
        }

        // GET: Days/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Days/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DayId,DayName,OwnerSignature,EntryDate")] Days days)
        {
            if (ModelState.IsValid)
            {

                days.OwnerSignature = HttpContext.Session.GetString("username");

                days.EntryDate = DateTime.Now;

                _context.Add(days);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(days);
        }

        // GET: Days/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var days = await _context.Days.SingleOrDefaultAsync(m => m.DayId == id);
            if (days == null)
            {
                return NotFound();
            }
            return View(days);
        }

        // POST: Days/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DayId,DayName,OwnerSignature,EntryDate")] Days days)
        {
            if (id != days.DayId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(days);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DaysExists(days.DayId))
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
            return View(days);
        }

        // GET: Days/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var days = await _context.Days
                .SingleOrDefaultAsync(m => m.DayId == id);
            if (days == null)
            {
                return NotFound();
            }

            return View(days);
        }

        // POST: Days/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var days = await _context.Days.SingleOrDefaultAsync(m => m.DayId == id);
            _context.Days.Remove(days);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DaysExists(int id)
        {
            return _context.Days.Any(e => e.DayId == id);
        }
    }
}
