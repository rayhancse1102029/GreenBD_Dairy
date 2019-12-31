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
    public class DoctorsSchedulesController : Controller
    {
        private readonly GreenBD_DairyContext _context;

        public DoctorsSchedulesController(GreenBD_DairyContext context)
        {
            _context = context;
        }

        // GET: DoctorsSchedules
        public async Task<IActionResult> Index(string srctext, int page = 1)
        {
            IQueryable<DoctorsSchedule> greenBD_DairyContext = _context.DoctorsSchedule.Include(d => d.Days).Include(d => d.Doctors).OrderByDescending(e =>e.ScheduleTypeId);

            if (!string.IsNullOrEmpty(srctext))
            {
                srctext = srctext.ToUpper();

                greenBD_DairyContext = _context.DoctorsSchedule.Include(d => d.Days).Include(d => d.Doctors).Where(e =>
                    e.ScheduleTypeId.ToString().Contains(srctext) || e.DoctorId.ToString().Contains(srctext) ||
                    e.Days.DayName.ToUpper().Contains(srctext) || e.Time.ToString().ToUpper().Contains(srctext)||e.ManagerSignature.ToUpper().Contains(srctext)).OrderByDescending(e => e.ScheduleTypeId);
            }

            ViewBag.TotalCount = greenBD_DairyContext.Count();
            ViewBag.srctext = srctext;

            if (page <= 0) { page = 1; }
            int pageSize = 10;

            return View(await PaginatedList<DoctorsSchedule>.CreateAsync(greenBD_DairyContext, page, pageSize));
        }

        // GET: DoctorsSchedules/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var doctorsSchedule = await _context.DoctorsSchedule
                .Include(d => d.Days)
                .Include(d => d.Doctors)
                .SingleOrDefaultAsync(m => m.ScheduleTypeId == id);
            if (doctorsSchedule == null)
            {
                return NotFound();
            }

            return View(doctorsSchedule);
        }

        // GET: DoctorsSchedules/Create
        public IActionResult Create()
        {
            ViewData["DayId"] = new SelectList(_context.Days, "DayId", "DayName");
            ViewData["DoctorId"] = new SelectList(_context.Doctors, "DoctorId", "DoctorName");
            return View();
        }

        // POST: DoctorsSchedules/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ScheduleTypeId,DoctorId,DayId,Time,ManagerSignature,EntryDate")] DoctorsSchedule doctorsSchedule)
        {
            if (ModelState.IsValid)
            {
                _context.Add(doctorsSchedule);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DayId"] = new SelectList(_context.Days, "DayId", "DayName", doctorsSchedule.DayId);
            ViewData["DoctorId"] = new SelectList(_context.Doctors, "DoctorId", "Description", doctorsSchedule.DoctorId);
            return View(doctorsSchedule);
        }

        // GET: DoctorsSchedules/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var doctorsSchedule = await _context.DoctorsSchedule.SingleOrDefaultAsync(m => m.ScheduleTypeId == id);
            if (doctorsSchedule == null)
            {
                return NotFound();
            }
            ViewData["DayId"] = new SelectList(_context.Days, "DayId", "DayName", doctorsSchedule.DayId);
            ViewData["DoctorId"] = new SelectList(_context.Doctors, "DoctorId", "DoctorName", doctorsSchedule.DoctorId);
            return View(doctorsSchedule);
        }

        // POST: DoctorsSchedules/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ScheduleTypeId,DoctorId,DayId,Time,ManagerSignature,EntryDate")] DoctorsSchedule doctorsSchedule)
        {
            if (id != doctorsSchedule.ScheduleTypeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(doctorsSchedule);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DoctorsScheduleExists(doctorsSchedule.ScheduleTypeId))
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
            ViewData["DayId"] = new SelectList(_context.Days, "DayId", "DayName", doctorsSchedule.DayId);
            ViewData["DoctorId"] = new SelectList(_context.Doctors, "DoctorId", "Description", doctorsSchedule.DoctorId);
            return View(doctorsSchedule);
        }

        // GET: DoctorsSchedules/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var doctorsSchedule = await _context.DoctorsSchedule
                .Include(d => d.Days)
                .Include(d => d.Doctors)
                .SingleOrDefaultAsync(m => m.ScheduleTypeId == id);
            if (doctorsSchedule == null)
            {
                return NotFound();
            }

            return View(doctorsSchedule);
        }

        // POST: DoctorsSchedules/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var doctorsSchedule = await _context.DoctorsSchedule.SingleOrDefaultAsync(m => m.ScheduleTypeId == id);
            _context.DoctorsSchedule.Remove(doctorsSchedule);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DoctorsScheduleExists(int id)
        {
            return _context.DoctorsSchedule.Any(e => e.ScheduleTypeId == id);
        }
    }
}
