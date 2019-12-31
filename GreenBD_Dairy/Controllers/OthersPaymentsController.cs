using System;
using System.Collections.Generic;
using System.IO;
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
    [Authorize(Roles = "Owner,Accounts")]
    public class OthersPaymentsController : Controller
    {
        private readonly GreenBD_DairyContext _context;

        public OthersPaymentsController(GreenBD_DairyContext context)
        {
            _context = context;
        }

        // GET: OthersPayments
        public async Task<IActionResult> Index(string srctext, int page = 1)
        {
            IQueryable<OthersPayment> greenBD_DairyContext = _context.OthersPayment.Include(o => o.AmountSign).Include(o => o.Gender).Include(o => o.Rank).OrderByDescending(e => e.Id);

            if (!string.IsNullOrEmpty(srctext))
            {
                srctext = srctext.ToUpper();

                greenBD_DairyContext = _context.OthersPayment.Include(o => o.AmountSign).Include(o => o.Gender)
                    .Include(o => o.Rank).Where(e => e.Id.ToString().Contains(srctext)||e.Country.ToUpper().Contains(srctext)||e.Name.ToUpper().Contains(srctext)||e.ManagerSignature.ToUpper().Contains(srctext)).OrderByDescending(e => e.Id);
            }

            ViewBag.TotalCount = greenBD_DairyContext.Count();
            ViewBag.srctext = srctext;

            if (page <= 0) { page = 1; }
            int pageSize = 10;

            return View(await PaginatedList<OthersPayment>.CreateAsync(greenBD_DairyContext, page, pageSize));
        }

        // GET: OthersPayments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var othersPayment = await _context.OthersPayment
                .Include(o => o.AmountSign)
                .Include(o => o.Gender)
                .Include(o => o.Rank)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (othersPayment == null)
            {
                return NotFound();
            }

            ViewBag.nid = othersPayment.NID;

            return View(othersPayment);
        }

        // GET: OthersPayments/Create
        public IActionResult Create()
        {
            ViewData["AmountSignId"] = new SelectList(_context.AmountSign, "AmountSignId", "AmountSignName");
            ViewData["GenderId"] = new SelectList(_context.Gender, "GenderId", "GenderName");
            ViewData["RankId"] = new SelectList(_context.Set<Rank>(), "RankId", "RankName");
            return View();
        }

        // POST: OthersPayments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,GenderId,Country,NID,Passport,BithCertificate,Image,RankId,Amount,AmountSignId,Description,ManagerSignature,EntryDate")] OthersPayment othersPayment, IFormFile nid, IFormFile profile)
        {
            if (othersPayment.Name != null && othersPayment.GenderId != null && othersPayment.Country != null &&
                othersPayment.RankId != null && othersPayment.Amount != null && othersPayment.AmountSignId != null &&
                othersPayment.Description != null && othersPayment.ManagerSignature != null)
            {

                if (nid.Length > 0)
                {
                    byte[] p1 = null;

                    using (var fs1 = nid.OpenReadStream())
                    using (var ms1 = new MemoryStream())
                    {
                        fs1.CopyTo(ms1);
                        p1 = ms1.ToArray();

                        othersPayment.NID = p1;

                    }
                }
                if (profile.Length > 0)
                {
                    byte[] p1 = null;

                    using (var fs1 = profile.OpenReadStream())
                    using (var ms1 = new MemoryStream())
                    {
                        fs1.CopyTo(ms1);
                        p1 = ms1.ToArray();

                        othersPayment.Image = p1;

                    }
                }
              
                othersPayment.ManagerSignature = HttpContext.Session.GetString("username");

                _context.Add(othersPayment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["AmountSignId"] = new SelectList(_context.AmountSign, "AmountSignId", "AmountSignName", othersPayment.AmountSignId);
            ViewData["GenderId"] = new SelectList(_context.Gender, "GenderId", "GenderName", othersPayment.GenderId);
            ViewData["RankId"] = new SelectList(_context.Set<Rank>(), "RankId", "RankName", othersPayment.RankId);
            return View(othersPayment);
        }

        // GET: OthersPayments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var othersPayment = await _context.OthersPayment.SingleOrDefaultAsync(m => m.Id == id);
            if (othersPayment == null)
            {
                return NotFound();
            }
            ViewData["AmountSignId"] = new SelectList(_context.AmountSign, "AmountSignId", "AmountSignName", othersPayment.AmountSignId);
            ViewData["GenderId"] = new SelectList(_context.Gender, "GenderId", "GenderName", othersPayment.GenderId);
            ViewData["RankId"] = new SelectList(_context.Set<Rank>(), "RankId", "RankName", othersPayment.RankId);
            return View(othersPayment);
        }

        // POST: OthersPayments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,GenderId,Country,NID,Passport,BithCertificate,Image,RankId,Amount,AmountSignId,Description,ManagerSignature,EntryDate")] OthersPayment othersPayment)
        {
            if (id != othersPayment.Id)
            {
                return NotFound();
            }

            if (othersPayment.Name != null && othersPayment.GenderId != null && othersPayment.Country != null &&
                othersPayment.RankId != null && othersPayment.Amount != null && othersPayment.AmountSignId != null &&
                othersPayment.Description != null && othersPayment.ManagerSignature != null)
            {
                try
                {
                    othersPayment.ManagerSignature = HttpContext.Session.GetString("username");

                    _context.Update(othersPayment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OthersPaymentExists(othersPayment.Id))
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
            ViewData["AmountSignId"] = new SelectList(_context.AmountSign, "AmountSignId", "AmountSignName", othersPayment.AmountSignId);
            ViewData["GenderId"] = new SelectList(_context.Gender, "GenderId", "GenderName", othersPayment.GenderId);
            ViewData["RankId"] = new SelectList(_context.Set<Rank>(), "RankId", "RankName", othersPayment.RankId);
            return View(othersPayment);
        }

        // GET: OthersPayments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var othersPayment = await _context.OthersPayment
                .Include(o => o.AmountSign)
                .Include(o => o.Gender)
                .Include(o => o.Rank)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (othersPayment == null)
            {
                return NotFound();
            }

            return View(othersPayment);
        }

        // POST: OthersPayments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var othersPayment = await _context.OthersPayment.SingleOrDefaultAsync(m => m.Id == id);
            _context.OthersPayment.Remove(othersPayment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OthersPaymentExists(int id)
        {
            return _context.OthersPayment.Any(e => e.Id == id);
        }
    }
}
