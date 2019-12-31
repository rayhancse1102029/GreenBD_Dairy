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
    public class AmountSignsController : Controller
    {
        private readonly GreenBD_DairyContext _context;

        public AmountSignsController(GreenBD_DairyContext context)
        {
            _context = context;
        }

        // GET: AmountSigns
        public async Task<IActionResult> Index(string srctext, int page = 1)
        {
            IQueryable<AmountSign> db = _context.AmountSign.OrderByDescending(e => e.AmountSignId);

            if (!string.IsNullOrEmpty(srctext))
            {
                srctext = srctext.ToUpper();

                db = from item in db
                    orderby item.AmountSignId descending 
                    where (item.AmountSignId.ToString().Contains(srctext) ||item.AmountSignName.ToUpper().Contains(srctext))
                    select item ;
            }

            ViewBag.TotalCount = db.Count();
            ViewBag.srctext = srctext;

            if (page <= 0) { page = 1; }
            int pageSize = 10;


            return View(await PaginatedList<AmountSign>.CreateAsync(db, page, pageSize));
        }

        // GET: AmountSigns/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var amountSign = await _context.AmountSign
                .SingleOrDefaultAsync(m => m.AmountSignId == id);
            if (amountSign == null)
            {
                return NotFound();
            }

            return View(amountSign);
        }

        // GET: AmountSigns/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AmountSigns/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AmountSignId,AmountSignName,OwnerSignature,EntryDate")] AmountSign amountSign)
        {
            if (ModelState.IsValid)
            {

                amountSign.OwnerSignature = HttpContext.Session.GetString("username");

                amountSign.EntryDate = DateTime.Now;
                

                _context.Add(amountSign);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(amountSign);
        }

        // GET: AmountSigns/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var amountSign = await _context.AmountSign.SingleOrDefaultAsync(m => m.AmountSignId == id);
            if (amountSign == null)
            {
                return NotFound();
            }
            return View(amountSign);
        }

        // POST: AmountSigns/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AmountSignId,AmountSignName,OwnerSignature,EntryDate")] AmountSign amountSign)
        {
            if (id != amountSign.AmountSignId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {

                    amountSign.OwnerSignature = HttpContext.Session.GetString("username");

                    amountSign.EntryDate = DateTime.Now;

                    _context.Update(amountSign);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AmountSignExists(amountSign.AmountSignId))
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
            return View(amountSign);
        }

        // GET: AmountSigns/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var amountSign = await _context.AmountSign
                .SingleOrDefaultAsync(m => m.AmountSignId == id);
            if (amountSign == null)
            {
                return NotFound();
            }

            return View(amountSign);
        }

        // POST: AmountSigns/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var amountSign = await _context.AmountSign.SingleOrDefaultAsync(m => m.AmountSignId == id);
            _context.AmountSign.Remove(amountSign);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AmountSignExists(int id)
        {
            return _context.AmountSign.Any(e => e.AmountSignId == id);
        }
    }
}
