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
    public class NumberSignsController : Controller
    {
        private readonly GreenBD_DairyContext _context;

        public NumberSignsController(GreenBD_DairyContext context)
        {
            _context = context;
        }

        // GET: NumberSigns
        public async Task<IActionResult> Index(string srctext, int page = 1)
        {

            IQueryable<NumberSign> db = _context.NumberSign.OrderByDescending(e =>e.NumberSignId);

            if (!string.IsNullOrEmpty(srctext))
            {
                srctext = srctext.ToUpper();

                db = from item in db
                    orderby item.NumberSignId descending 
                    where (item.NumberSignId.ToString().Contains(srctext) || item.NumberSignName.ToUpper().Contains(srctext)|| item.OwnerSignature.ToUpper().Contains(srctext))
                    select item;
            }

            ViewBag.TotalCount = db.Count();
            ViewBag.srctext = srctext;

            if (page <= 0) { page = 1; }
            int pageSize = 10;

            return View(await PaginatedList<NumberSign>.CreateAsync(db, page, pageSize));
        }

        // GET: NumberSigns/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var numberSign = await _context.NumberSign
                .SingleOrDefaultAsync(m => m.NumberSignId == id);
            if (numberSign == null)
            {
                return NotFound();
            }

            return View(numberSign);
        }

        // GET: NumberSigns/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: NumberSigns/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NumberSignId,NumberSignName,OwnerSignature,EntryDate")] NumberSign numberSign)
        {
            if (ModelState.IsValid)
            {

                numberSign.OwnerSignature = HttpContext.Session.GetString("username");

                numberSign.EntryDate = DateTime.Now;

                _context.Add(numberSign);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(numberSign);
        }

        // GET: NumberSigns/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var numberSign = await _context.NumberSign.SingleOrDefaultAsync(m => m.NumberSignId == id);
            if (numberSign == null)
            {
                return NotFound();
            }
            return View(numberSign);
        }

        // POST: NumberSigns/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("NumberSignId,NumberSignName,OwnerSignature,EntryDate")] NumberSign numberSign)
        {
            if (id != numberSign.NumberSignId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {

                    numberSign.OwnerSignature = HttpContext.Session.GetString("username");

                    numberSign.EntryDate = DateTime.Now;

                    _context.Update(numberSign);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NumberSignExists(numberSign.NumberSignId))
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
            return View(numberSign);
        }

        // GET: NumberSigns/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var numberSign = await _context.NumberSign
                .SingleOrDefaultAsync(m => m.NumberSignId == id);
            if (numberSign == null)
            {
                return NotFound();
            }

            return View(numberSign);
        }

        // POST: NumberSigns/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var numberSign = await _context.NumberSign.SingleOrDefaultAsync(m => m.NumberSignId == id);
            _context.NumberSign.Remove(numberSign);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NumberSignExists(int id)
        {
            return _context.NumberSign.Any(e => e.NumberSignId == id);
        }
    }
}
