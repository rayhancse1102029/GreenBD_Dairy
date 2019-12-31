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
using Microsoft.Extensions.FileSystemGlobbing.Internal.PathSegments;

namespace GreenBD_Dairy.Controllers
{
    [Authorize(Roles = "Owner,Accounts")]
    public class WorkerSalariesController : Controller
    {
        private readonly GreenBD_DairyContext _context;

        public WorkerSalariesController(GreenBD_DairyContext context)
        {
            _context = context;
        }

        // GET: WorkerSalaries
        public async Task<IActionResult> Index(string srctext, int page=1)
        {

            IQueryable<WorkerSalary> greenBD_DairyContext = _context.WorkerSalary.Include(w => w.Month).Include(w => w.PaymentMethod).Include(w => w.Rank).OrderByDescending(e => e.Id);

            if (!string.IsNullOrEmpty(srctext))
            {
                srctext = srctext.ToUpper();

                greenBD_DairyContext = _context.WorkerSalary.Include(w => w.Month).Include(w => w.PaymentMethod)
                    .Include(w => w.Rank).Where(e => e.Id.ToString().Contains(srctext)||e.IdCardNumber.ToString().Contains(srctext)||e.Month.ToString().ToUpper().Contains(srctext)||e.ManagerSignature.ToUpper().Contains(srctext)||e.PaymentMethod.ToString().ToUpper().Contains(srctext)).OrderByDescending(e => e.Id);
            }

            ViewBag.TotalCount = greenBD_DairyContext.Count();
            ViewBag.srctext = srctext;

            if (page <= 0) { page = 1; }
            int pageSize = 10;

            return View(await PaginatedList<WorkerSalary>.CreateAsync(greenBD_DairyContext, page, pageSize));
        }

        // GET: WorkerSalaries/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workerSalary = await _context.WorkerSalary
                .Include(w => w.Month)
                .Include(w => w.PaymentMethod)
                .Include(w => w.Rank)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (workerSalary == null)
            {
                return NotFound();
            }

            return View(workerSalary);
        }

        // GET: WorkerSalaries/Create
        public IActionResult Create()
        {
            ViewData["MonthId"] = new SelectList(_context.Month, "MonthId", "MonthName");
            ViewData["PaymentMethodId"] = new SelectList(_context.PaymentMethod, "PaymentMethodId", "PaymentMethodName");
            ViewData["RankId"] = new SelectList(_context.Rank, "RankId", "RankName");
            return View();
        }

        // POST: WorkerSalaries/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,IdCardNumber,RankId,Salary,MonthId,PaymentMethodId,ManagerSignature,EntryDate")] WorkerSalary workerSalary)
        {
            if (ModelState.IsValid)
            {

                workerSalary.ManagerSignature = HttpContext.Session.GetString("username");

                workerSalary.EntryDate = DateTime.Now;


                _context.Add(workerSalary);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MonthId"] = new SelectList(_context.Month, "MonthId", "MonthName", workerSalary.MonthId);
            ViewData["PaymentMethodId"] = new SelectList(_context.PaymentMethod, "PaymentMethodId", "PaymentMethodName", workerSalary.PaymentMethodId);
            ViewData["RankId"] = new SelectList(_context.Rank, "RankId", "RankName", workerSalary.RankId);
            return View(workerSalary);
        }

        // GET: WorkerSalaries/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workerSalary = await _context.WorkerSalary.SingleOrDefaultAsync(m => m.Id == id);
            if (workerSalary == null)
            {
                return NotFound();
            }
            ViewData["MonthId"] = new SelectList(_context.Month, "MonthId", "MonthName", workerSalary.MonthId);
            ViewData["PaymentMethodId"] = new SelectList(_context.PaymentMethod, "PaymentMethodId", "PaymentMethodName", workerSalary.PaymentMethodId);
            ViewData["RankId"] = new SelectList(_context.Rank, "RankId", "RankName", workerSalary.RankId);
            return View(workerSalary);
        }

        // POST: WorkerSalaries/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,IdCardNumber,RankId,Salary,MonthId,PaymentMethodId,ManagerSignature,EntryDate")] WorkerSalary workerSalary)
        {
            if (id != workerSalary.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {

                    workerSalary.ManagerSignature = HttpContext.Session.GetString("username");

                    _context.Update(workerSalary);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WorkerSalaryExists(workerSalary.Id))
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
            ViewData["MonthId"] = new SelectList(_context.Month, "MonthId", "MonthName", workerSalary.MonthId);
            ViewData["PaymentMethodId"] = new SelectList(_context.PaymentMethod, "PaymentMethodId", "PaymentMethodName", workerSalary.PaymentMethodId);
            ViewData["RankId"] = new SelectList(_context.Rank, "RankId", "RankName", workerSalary.RankId);
            return View(workerSalary);
        }

        // GET: WorkerSalaries/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workerSalary = await _context.WorkerSalary
                .Include(w => w.Month)
                .Include(w => w.PaymentMethod)
                .Include(w => w.Rank)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (workerSalary == null)
            {
                return NotFound();
            }

            return View(workerSalary);
        }

        // POST: WorkerSalaries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var workerSalary = await _context.WorkerSalary.SingleOrDefaultAsync(m => m.Id == id);
            _context.WorkerSalary.Remove(workerSalary);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WorkerSalaryExists(int id)
        {
            return _context.WorkerSalary.Any(e => e.Id == id);
        }
    }
}
