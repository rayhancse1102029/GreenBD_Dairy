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
    public class TransportsController : Controller
    {
        private readonly GreenBD_DairyContext _context;

        public TransportsController(GreenBD_DairyContext context)
        {
            _context = context;
        }

        // GET: Transports
        public async Task<IActionResult> Index(string srctext, int page = 1)
        {

            IQueryable<Transport> greenBD_DairyContext = _context.Transport.Include(t => t.TransportType).OrderByDescending(e => e.Id);

            if (!string.IsNullOrEmpty(srctext))
            {
                srctext = srctext.ToUpper();

                greenBD_DairyContext = _context.Transport.Include(t => t.TransportType).Where(e =>
                    e.Id.ToString().Contains(srctext) ||
                    e.TransportType.TransportTypeName.ToUpper().Contains(srctext) ||
                    e.UsesFor.ToUpper().Contains(srctext) || e.ManagerSignature.ToUpper().Contains(srctext)).OrderByDescending(e => e.Id);
            }


            ViewBag.TotalCount = greenBD_DairyContext.Count();
            ViewBag.srctext = srctext;

            if (page <= 0) { page = 1; }
            int pageSize = 10;

            return View(await PaginatedList<Transport>.CreateAsync(greenBD_DairyContext, page, pageSize));
        }

        // GET: Transports/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transport = await _context.Transport
                .Include(t => t.TransportType)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (transport == null)
            {
                return NotFound();
            }

            return View(transport);
        }

        // GET: Transports/Create
        public IActionResult Create()
        {
            ViewData["TransportTypeId"] = new SelectList(_context.Set<TransportType>(), "TransportTypeId", "TransportTypeName");
            return View();
        }

        // POST: Transports/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TransportTypeId,UsesFor,Description,ManagerSignature,EntryDate")] Transport transport)
        {
            if (ModelState.IsValid)
            {
                transport.ManagerSignature = HttpContext.Session.GetString("username");

                transport.EntryDate = DateTime.Now;

                _context.Add(transport);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TransportTypeId"] = new SelectList(_context.Set<TransportType>(), "TransportTypeId", "TransportTypeName", transport.TransportTypeId);
            return View(transport);
        }

        // GET: Transports/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transport = await _context.Transport.SingleOrDefaultAsync(m => m.Id == id);
            if (transport == null)
            {
                return NotFound();
            }
            ViewData["TransportTypeId"] = new SelectList(_context.Set<TransportType>(), "TransportTypeId", "TransportTypeName", transport.TransportTypeId);
            return View(transport);
        }

        // POST: Transports/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TransportTypeId,UsesFor,Description,ManagerSignature,EntryDate")] Transport transport)
        {
            if (id != transport.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    transport.ManagerSignature = HttpContext.Session.GetString("username");

                    transport.EntryDate = DateTime.Now;

                    _context.Update(transport);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TransportExists(transport.Id))
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
            ViewData["TransportTypeId"] = new SelectList(_context.Set<TransportType>(), "TransportTypeId", "TransportTypeName", transport.TransportTypeId);
            return View(transport);
        }

        // GET: Transports/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transport = await _context.Transport
                .Include(t => t.TransportType)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (transport == null)
            {
                return NotFound();
            }

            return View(transport);
        }

        // POST: Transports/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var transport = await _context.Transport.SingleOrDefaultAsync(m => m.Id == id);
            _context.Transport.Remove(transport);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TransportExists(int id)
        {
            return _context.Transport.Any(e => e.Id == id);
        }
    }
}
