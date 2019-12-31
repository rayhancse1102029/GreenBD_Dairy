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
    [Authorize(Roles = "Owner,Manager")]
    public class DoctorsController : Controller
    {
        private readonly GreenBD_DairyContext _context;

        public DoctorsController(GreenBD_DairyContext context)
        {
            _context = context;
        }

        // GET: Doctors
        public async Task<IActionResult> Index(string srctext, int page = 1)
        {
            IQueryable<Doctors> greenBD_DairyContext = _context.Doctors.Include(d => d.SpecialistType).OrderByDescending(e => e.DoctorId);

            if (!string.IsNullOrEmpty(srctext))
            {
                srctext = srctext.ToUpper();

                greenBD_DairyContext = _context.Doctors.Include(d => d.SpecialistType).Where(e =>
                    e.DoctorId.ToString().Contains(srctext) || e.DoctorName.Contains(srctext) ||
                    e.SpecialistType.SpecialistTypeName.Contains(srctext)||e.ManagerSignature.ToUpper().Contains(srctext)).OrderByDescending(e => e.DoctorId);
            }

            ViewBag.TotalCount = greenBD_DairyContext.Count();
            ViewBag.srctext = srctext;

            if (page <= 0) { page = 1; }
            int pageSize = 10;

            return View(await PaginatedList<Doctors>.CreateAsync(greenBD_DairyContext, page, pageSize));
        }

        // GET: Doctors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var doctors = await _context.Doctors
                .Include(d => d.SpecialistType)
                .SingleOrDefaultAsync(m => m.DoctorId == id);
            if (doctors == null)
            {
                return NotFound();
            }

            return View(doctors);
        }

        // GET: Doctors/Create
        public IActionResult Create()
        {
            ViewData["SpecialistTypeId"] = new SelectList(_context.Set<SpecialistType>(), "SpecialistTypeId", "SpecialistTypeName");
            return View();
        }

        // POST: Doctors/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DoctorId,DoctorName,SpecialistTypeId,VisitFee,Profile,Description,ManagerSignature,EntryDate")] Doctors doctors, IFormFile profile)
        {
            if (ModelState.IsValid)
            {
                if (profile.Length > 0)
                {
                    byte[] p1 = null;

                    using (var fs1 = profile.OpenReadStream())
                    using (var ms1 = new MemoryStream())
                    {
                        fs1.CopyTo(ms1);
                        p1 = ms1.ToArray();

                        doctors.Profile = p1;

                        doctors.ManagerSignature = HttpContext.Session.GetString("username");


                        _context.Add(doctors);
                        await _context.SaveChangesAsync();
                        return RedirectToAction(nameof(Index));
                    }
                }
            }
            ViewData["SpecialistTypeId"] = new SelectList(_context.Set<SpecialistType>(), "SpecialistTypeId", "SpecialistTypeName", doctors.SpecialistTypeId);
            return View(doctors);
        }

        // GET: Doctors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var doctors = await _context.Doctors.SingleOrDefaultAsync(m => m.DoctorId == id);
            if (doctors == null)
            {
                return NotFound();
            }
            ViewData["SpecialistTypeId"] = new SelectList(_context.Set<SpecialistType>(), "SpecialistTypeId", "SpecialistTypeName", doctors.SpecialistTypeId);
            return View(doctors);
        }

        // POST: Doctors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DoctorId,DoctorName,SpecialistTypeId,VisitFee,Profile,Description,ManagerSignature,EntryDate")] Doctors doctors, IFormFile profile)
        {

            if (id != doctors.DoctorId)
            {
                return NotFound();
            }

            if (!string.IsNullOrEmpty(doctors.DoctorName) && !string.IsNullOrEmpty(doctors.Description) && doctors.SpecialistTypeId != 0 && doctors.VisitFee > 0)
            {
                try
                {
                    if (profile.Length > 0)
                    {
                        byte[] p1 = null;

                        using (var fs1 = profile.OpenReadStream())
                        using (var ms1 = new MemoryStream())
                        {
                            fs1.CopyTo(ms1);
                            p1 = ms1.ToArray();

                            doctors.Profile = p1;

                        }

                       
                        doctors.ManagerSignature = HttpContext.Session.GetString("username");

                        doctors.EntryDate = DateTime.Now;

                        _context.Update(doctors);
                        await _context.SaveChangesAsync();
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DoctorsExists(doctors.DoctorId))
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
            ViewData["SpecialistTypeId"] = new SelectList(_context.Set<SpecialistType>(), "SpecialistTypeId", "SpecialistTypeName", doctors.SpecialistTypeId);
            return View(doctors);
        }

        // GET: Doctors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var doctors = await _context.Doctors
                .Include(d => d.SpecialistType)
                .SingleOrDefaultAsync(m => m.DoctorId == id);
            if (doctors == null)
            {
                return NotFound();
            }

            return View(doctors);
        }

        // POST: Doctors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var doctors = await _context.Doctors.SingleOrDefaultAsync(m => m.DoctorId == id);
            _context.Doctors.Remove(doctors);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DoctorsExists(int id)
        {
            return _context.Doctors.Any(e => e.DoctorId == id);
        }
    }
}
