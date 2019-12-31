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
    [Authorize(Roles = "Owner,Manager")]
    public class WorkerManagementsController : Controller
    {
        private readonly GreenBD_DairyContext _context;

        public WorkerManagementsController(GreenBD_DairyContext context)
        {
            _context = context;
        }

        // GET: WorkerManagements
        public async Task<IActionResult> Index()
        {
            var greenBD_DairyContext = _context.WorkerManagement.Include(w => w.AmountSign).Include(w => w.Gender).Include(w => w.Rank);
            return View(await greenBD_DairyContext.ToListAsync());
        }

        // GET: WorkerManagements/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workerManagement = await _context.WorkerManagement
                .Include(w => w.AmountSign)
                .Include(w => w.Gender)
                .Include(w => w.Rank)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (workerManagement == null)
            {
                return NotFound();
            }

            return View(workerManagement);
        }

        // GET: WorkerManagements/Create
        public IActionResult Create()
        {
            ViewData["AmountSignId"] = new SelectList(_context.AmountSign, "AmountSignId", "AmountSignName");
            ViewData["GenderId"] = new SelectList(_context.Gender, "GenderId", "GenderName");
            ViewData["RankId"] = new SelectList(_context.Rank, "RankId", "RankName");
            return View();
        }

        // POST: WorkerManagements/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,GenderId,Country,NID,Passport,BithCertificate,Profile,Qualification,RankId,Salary,AmountSignId,JoinDate,ManagerSignature,EntryDate")] WorkerManagement workerManagement)
        {
            if (ModelState.IsValid)
            {
                _context.Add(workerManagement);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AmountSignId"] = new SelectList(_context.AmountSign, "AmountSignId", "AmountSignName", workerManagement.AmountSignId);
            ViewData["GenderId"] = new SelectList(_context.Gender, "GenderId", "GenderName", workerManagement.GenderId);
            ViewData["RankId"] = new SelectList(_context.Rank, "RankId", "RankName", workerManagement.RankId);
            return View(workerManagement);
        }

        // GET: WorkerManagements/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workerManagement = await _context.WorkerManagement.SingleOrDefaultAsync(m => m.Id == id);
            if (workerManagement == null)
            {
                return NotFound();
            }
            ViewData["AmountSignId"] = new SelectList(_context.AmountSign, "AmountSignId", "AmountSignName", workerManagement.AmountSignId);
            ViewData["GenderId"] = new SelectList(_context.Gender, "GenderId", "GenderName", workerManagement.GenderId);
            ViewData["RankId"] = new SelectList(_context.Rank, "RankId", "RankName", workerManagement.RankId);
            return View(workerManagement);
        }

        // POST: WorkerManagements/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,GenderId,Country,NID,Passport,BithCertificate,Profile,Qualification,RankId,Salary,AmountSignId,JoinDate,ManagerSignature,EntryDate")] WorkerManagement workerManagement)
        {
            if (id != workerManagement.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(workerManagement);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WorkerManagementExists(workerManagement.Id))
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
            ViewData["AmountSignId"] = new SelectList(_context.AmountSign, "AmountSignId", "AmountSignName", workerManagement.AmountSignId);
            ViewData["GenderId"] = new SelectList(_context.Gender, "GenderId", "GenderName", workerManagement.GenderId);
            ViewData["RankId"] = new SelectList(_context.Rank, "RankId", "RankName", workerManagement.RankId);
            return View(workerManagement);
        }

        // GET: WorkerManagements/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workerManagement = await _context.WorkerManagement
                .Include(w => w.AmountSign)
                .Include(w => w.Gender)
                .Include(w => w.Rank)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (workerManagement == null)
            {
                return NotFound();
            }

            return View(workerManagement);
        }

        // POST: WorkerManagements/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var workerManagement = await _context.WorkerManagement.SingleOrDefaultAsync(m => m.Id == id);
            _context.WorkerManagement.Remove(workerManagement);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WorkerManagementExists(int id)
        {
            return _context.WorkerManagement.Any(e => e.Id == id);
        }
    }
}
