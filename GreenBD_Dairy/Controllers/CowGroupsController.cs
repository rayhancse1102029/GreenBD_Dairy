﻿using System;
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
    [Authorize(Roles = "Owner")]
    public class CowGroupsController : Controller
    {
        private readonly GreenBD_DairyContext _context;

        public CowGroupsController(GreenBD_DairyContext context)
        {
            _context = context;
        }

        // GET: CowGroups
        public async Task<IActionResult> Index()
        {
            return View(await _context.CowGroup.ToListAsync());
        }

        // GET: CowGroups/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cowGroup = await _context.CowGroup
                .SingleOrDefaultAsync(m => m.CowGroupId == id);
            if (cowGroup == null)
            {
                return NotFound();
            }

            return View(cowGroup);
        }

        // GET: CowGroups/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CowGroups/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CowGroupId,CowGroupName,Description,OwnerSignature,EntryDate")] CowGroup cowGroup)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cowGroup);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cowGroup);
        }

        // GET: CowGroups/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cowGroup = await _context.CowGroup.SingleOrDefaultAsync(m => m.CowGroupId == id);
            if (cowGroup == null)
            {
                return NotFound();
            }
            return View(cowGroup);
        }

        // POST: CowGroups/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CowGroupId,CowGroupName,Description,OwnerSignature,EntryDate")] CowGroup cowGroup)
        {
            if (id != cowGroup.CowGroupId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cowGroup);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CowGroupExists(cowGroup.CowGroupId))
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
            return View(cowGroup);
        }

        // GET: CowGroups/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cowGroup = await _context.CowGroup
                .SingleOrDefaultAsync(m => m.CowGroupId == id);
            if (cowGroup == null)
            {
                return NotFound();
            }

            return View(cowGroup);
        }

        // POST: CowGroups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cowGroup = await _context.CowGroup.SingleOrDefaultAsync(m => m.CowGroupId == id);
            _context.CowGroup.Remove(cowGroup);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CowGroupExists(int id)
        {
            return _context.CowGroup.Any(e => e.CowGroupId == id);
        }
    }
}
