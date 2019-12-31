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
    [Authorize(Roles = "Owner,Manager")]
    public class LandManagementsController : Controller
    {
        private readonly GreenBD_DairyContext _context;

        public LandManagementsController(GreenBD_DairyContext context)
        {
            _context = context;
        }

        // GET: LandManagements
        public async Task<IActionResult> Index(string srctext, int page = 1)
        {
            IQueryable<LandManagement> greenBD_DairyContext = _context.LandManagement.Include(l => l.AmountSign).Include(l => l.Gender).Include(l => l.NumberSign).OrderByDescending(e => e.Id);

            if (!string.IsNullOrEmpty(srctext))
            {
                srctext = srctext.ToUpper();

                greenBD_DairyContext = _context.LandManagement.Include(l => l.AmountSign).Include(l => l.Gender)
                    .Include(l => l.NumberSign).Where(e =>e.Id.ToString().Contains(srctext) || e.Location.ToString().ToUpper().Contains(srctext)||e.SellerName.ToUpper().Contains(srctext)|| e.SellerAddress.ToUpper().Contains(srctext)||e.ManagerSignature.ToUpper().Contains(srctext)).OrderByDescending(e => e.Id);
            }

            ViewBag.TotalCount = greenBD_DairyContext.Count();
            ViewBag.srctext = srctext;

            if (page <= 0) { page = 1; }
            int pageSize = 10;

            return View(await PaginatedList<LandManagement>.CreateAsync(greenBD_DairyContext, page, pageSize));
        }

        // GET: LandManagements/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var landManagement = await _context.LandManagement
                .Include(l => l.AmountSign)
                .Include(l => l.Gender)
                .Include(l => l.NumberSign)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (landManagement == null)
            {
                return NotFound();
            }

            return View(landManagement);
        }

        // GET: LandManagements/Create
        public IActionResult Create()
        {
            ViewData["AmountSignId"] = new SelectList(_context.AmountSign, "AmountSignId", "AmountSignName");
            ViewData["GenderId"] = new SelectList(_context.Gender, "GenderId", "GenderName");
            ViewData["NumberSignId"] = new SelectList(_context.Set<NumberSign>(), "NumberSignId", "NumberSignName");
            return View();
        }

        // POST: LandManagements/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,SellerName,GenderId,SellerAddress,Location,LandArea,NumberSignId,Price,AmountSignId,TotalPrice,Description,ManagerSignature,EntryDate")] LandManagement landManagement)
        {
            if (ModelState.IsValid)
            {

                landManagement.ManagerSignature = HttpContext.Session.GetString("username");

                landManagement.EntryDate = DateTime.Now;

                _context.Add(landManagement);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AmountSignId"] = new SelectList(_context.AmountSign, "AmountSignId", "AmountSignName", landManagement.AmountSignId);
            ViewData["GenderId"] = new SelectList(_context.Gender, "GenderId", "GenderName", landManagement.GenderId);
            ViewData["NumberSignId"] = new SelectList(_context.Set<NumberSign>(), "NumberSignId", "NumberSignName", landManagement.NumberSignId);
            return View(landManagement);
        }

        // GET: LandManagements/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var landManagement = await _context.LandManagement.SingleOrDefaultAsync(m => m.Id == id);
            if (landManagement == null)
            {
                return NotFound();
            }
            ViewData["AmountSignId"] = new SelectList(_context.AmountSign, "AmountSignId", "AmountSignName", landManagement.AmountSignId);
            ViewData["GenderId"] = new SelectList(_context.Gender, "GenderId", "GenderName", landManagement.GenderId);
            ViewData["NumberSignId"] = new SelectList(_context.Set<NumberSign>(), "NumberSignId", "NumberSignName", landManagement.NumberSignId);
            return View(landManagement);
        }

        // POST: LandManagements/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,SellerName,GenderId,SellerAddress,Location,LandArea,NumberSignId,Price,AmountSignId,TotalPrice,Description,ManagerSignature,EntryDate")] LandManagement landManagement)
        {
            if (id != landManagement.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {

                    landManagement.ManagerSignature = HttpContext.Session.GetString("username");

                    landManagement.EntryDate = DateTime.Now;

                    _context.Update(landManagement);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LandManagementExists(landManagement.Id))
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
            ViewData["AmountSignId"] = new SelectList(_context.AmountSign, "AmountSignId", "AmountSignName", landManagement.AmountSignId);
            ViewData["GenderId"] = new SelectList(_context.Gender, "GenderId", "GenderName", landManagement.GenderId);
            ViewData["NumberSignId"] = new SelectList(_context.Set<NumberSign>(), "NumberSignId", "NumberSignName", landManagement.NumberSignId);
            return View(landManagement);
        }

        // GET: LandManagements/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var landManagement = await _context.LandManagement
                .Include(l => l.AmountSign)
                .Include(l => l.Gender)
                .Include(l => l.NumberSign)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (landManagement == null)
            {
                return NotFound();
            }

            return View(landManagement);
        }

        // POST: LandManagements/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var landManagement = await _context.LandManagement.SingleOrDefaultAsync(m => m.Id == id);
            _context.LandManagement.Remove(landManagement);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LandManagementExists(int id)
        {
            return _context.LandManagement.Any(e => e.Id == id);
        }
    }
}
