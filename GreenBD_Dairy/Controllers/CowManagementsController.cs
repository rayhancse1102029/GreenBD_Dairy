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
    public class CowManagementsController : Controller
    {
        private readonly GreenBD_DairyContext _context;

        public CowManagementsController(GreenBD_DairyContext context)
        {
            _context = context;
        }

        // GET: CowManagements
        public async Task<IActionResult> Index(string srctext, int page = 1)
        {
            
            IQueryable<CowManagement> greenBD_DairyContext = _context.CowManagement.Include(c => c.CowCollection).Include(c => c.CowGroup).Include(c => c.CowPurpose).Include(c => c.Gender).Include(c => c.NationOfCow).OrderByDescending(e => e.Id);

            if (!string.IsNullOrEmpty(srctext))
            {
                srctext = srctext.ToUpper();

                greenBD_DairyContext = _context.CowManagement.Include(c => c.CowCollection).Include(c => c.CowGroup)
                    .Include(c => c.CowPurpose).Include(c => c.Gender).Include(c => c.NationOfCow)
                    .Where(e => e.Id.ToString().Contains(srctext) || e.CowName.ToUpper().Contains(srctext) || e.PreCodeNo.ToString().ToUpper().Contains(srctext) || e.OurCodeNo.ToString().ToUpper().Contains(srctext)).OrderByDescending(e => e.Id);
            }

            ViewBag.TotalCount = greenBD_DairyContext.Count();
            ViewBag.srctext = srctext;

            if (page <= 0) { page = 1; }
            int pageSize = 10;


            return View(await PaginatedList<CowManagement>.CreateAsync(greenBD_DairyContext, page, pageSize));
        }

        // GET: CowManagements/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cowManagement = await _context.CowManagement
                .Include(c => c.CowCollection)
                .Include(c => c.CowGroup)
                .Include(c => c.CowPurpose)
                .Include(c => c.Gender)
                .Include(c => c.NationOfCow)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (cowManagement == null)
            {
                return NotFound();
            }

            return View(cowManagement);
        }

        // GET: CowManagements/Create
        public IActionResult Create()
        {
            ViewData["CowCollectionId"] = new SelectList(_context.CowCollection, "CowCollectionId", "CowCollectionName");
            ViewData["CowGroupId"] = new SelectList(_context.CowGroup, "CowGroupId", "CowGroupName");
            ViewData["CowPurposeId"] = new SelectList(_context.Set<CowPurpose>(), "CowPurposeId", "CowPurposeName");
            ViewData["GenderId"] = new SelectList(_context.Set<Gender>(), "GenderId", "GenderName");
            ViewData["CowNationId"] = new SelectList(_context.Set<NationOfCow>(), "CowNationId", "CowNationName");
            return View();
        }

        // POST: CowManagements/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CowNationId,GenderId,CowName,PreCodeNo,OurCodeNo,CowGroupId,CowCollectionId,CowPurposeId,InitialPrice,Description,ManagerSignature,EntryDate")] CowManagement cowManagement)
        {
            if (ModelState.IsValid)
            {
                cowManagement.ManagerSignature = HttpContext.Session.GetString("username");

                _context.Add(cowManagement);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CowCollectionId"] = new SelectList(_context.CowCollection, "CowCollectionId", "CowCollectionName", cowManagement.CowCollectionId);
            ViewData["CowGroupId"] = new SelectList(_context.CowGroup, "CowGroupId", "CowGroupName", cowManagement.CowGroupId);
            ViewData["CowPurposeId"] = new SelectList(_context.Set<CowPurpose>(), "CowPurposeId", "CowPurposeName", cowManagement.CowPurposeId);
            ViewData["GenderId"] = new SelectList(_context.Set<Gender>(), "GenderId", "GenderName", cowManagement.GenderId);
            ViewData["CowNationId"] = new SelectList(_context.Set<NationOfCow>(), "CowNationId", "CowNationName", cowManagement.CowNationId);
            return View(cowManagement);
        }

        // GET: CowManagements/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cowManagement = await _context.CowManagement.SingleOrDefaultAsync(m => m.Id == id);
            if (cowManagement == null)
            {
                return NotFound();
            }
            ViewData["CowCollectionId"] = new SelectList(_context.CowCollection, "CowCollectionId", "CowCollectionName", cowManagement.CowCollectionId);
            ViewData["CowGroupId"] = new SelectList(_context.CowGroup, "CowGroupId", "CowGroupName", cowManagement.CowGroupId);
            ViewData["CowPurposeId"] = new SelectList(_context.Set<CowPurpose>(), "CowPurposeId", "CowPurposeName", cowManagement.CowPurposeId);
            ViewData["GenderId"] = new SelectList(_context.Set<Gender>(), "GenderId", "GenderName", cowManagement.GenderId);
            ViewData["CowNationId"] = new SelectList(_context.Set<NationOfCow>(), "CowNationId", "CowNationName", cowManagement.CowNationId);
            return View(cowManagement);
        }

        // POST: CowManagements/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CowNationId,GenderId,CowName,PreCodeNo,OurCodeNo,CowGroupId,CowCollectionId,CowPurposeId,InitialPrice,Description,ManagerSignature,EntryDate")] CowManagement cowManagement)
        {
            if (id != cowManagement.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    cowManagement.ManagerSignature = HttpContext.Session.GetString("username");

                    _context.Update(cowManagement);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CowManagementExists(cowManagement.Id))
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
            ViewData["CowCollectionId"] = new SelectList(_context.CowCollection, "CowCollectionId", "CowCollectionName", cowManagement.CowCollectionId);
            ViewData["CowGroupId"] = new SelectList(_context.CowGroup, "CowGroupId", "CowGroupName", cowManagement.CowGroupId);
            ViewData["CowPurposeId"] = new SelectList(_context.Set<CowPurpose>(), "CowPurposeId", "CowPurposeName", cowManagement.CowPurposeId);
            ViewData["GenderId"] = new SelectList(_context.Set<Gender>(), "GenderId", "GenderName", cowManagement.GenderId);
            ViewData["CowNationId"] = new SelectList(_context.Set<NationOfCow>(), "CowNationId", "CowNationName", cowManagement.CowNationId);
            return View(cowManagement);
        }

        // GET: CowManagements/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cowManagement = await _context.CowManagement
                .Include(c => c.CowCollection)
                .Include(c => c.CowGroup)
                .Include(c => c.CowPurpose)
                .Include(c => c.Gender)
                .Include(c => c.NationOfCow)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (cowManagement == null)
            {
                return NotFound();
            }

            return View(cowManagement);
        }

        // POST: CowManagements/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cowManagement = await _context.CowManagement.SingleOrDefaultAsync(m => m.Id == id);
            _context.CowManagement.Remove(cowManagement);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CowManagementExists(int id)
        {
            return _context.CowManagement.Any(e => e.Id == id);
        }
    }
}
