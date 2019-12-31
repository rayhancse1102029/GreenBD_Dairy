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
    public class FoodTypesController : Controller
    {
        private readonly GreenBD_DairyContext _context;

        public FoodTypesController(GreenBD_DairyContext context)
        {
            _context = context;
        }

        // GET: FoodTypes
        public async Task<IActionResult> Index(string srctext, int page = 1)
        {

            IQueryable<FoodType> db = _context.FoodType.OrderByDescending(e =>e.FoodTypeId);

            if (!string.IsNullOrEmpty(srctext))
            {
                srctext = srctext.ToUpper();

                db = from item in db
                    orderby item.FoodTypeId descending 
                    where (item.FoodTypeId.ToString().Contains(srctext) || item.FoodTypeName.ToUpper().Contains(srctext)|| item.OwnerSignature.ToUpper().Contains(srctext))
                    select item;
            }


            ViewBag.TotalCount = db.Count();
            ViewBag.srctext = srctext;

            if (page <= 0) { page = 1; }
            int pageSize = 10;

            return View(await PaginatedList<FoodType>.CreateAsync(db, page, pageSize));
        }

        // GET: FoodTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var foodType = await _context.FoodType
                .SingleOrDefaultAsync(m => m.FoodTypeId == id);
            if (foodType == null)
            {
                return NotFound();
            }

            return View(foodType);
        }

        // GET: FoodTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: FoodTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FoodTypeId,FoodTypeName,Description,OwnerSignature,EntryDate")] FoodType foodType)
        {
            if (ModelState.IsValid)
            {

                foodType.OwnerSignature = HttpContext.Session.GetString("username");

                foodType.EntryDate = DateTime.Now;


                _context.Add(foodType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(foodType);
        }

        // GET: FoodTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var foodType = await _context.FoodType.SingleOrDefaultAsync(m => m.FoodTypeId == id);
            if (foodType == null)
            {
                return NotFound();
            }
            return View(foodType);
        }

        // POST: FoodTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FoodTypeId,FoodTypeName,Description,OwnerSignature,EntryDate")] FoodType foodType)
        {
            if (id != foodType.FoodTypeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {

                    foodType.OwnerSignature = HttpContext.Session.GetString("username");

                    foodType.EntryDate = DateTime.Now;

                    _context.Update(foodType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FoodTypeExists(foodType.FoodTypeId))
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
            return View(foodType);
        }

        // GET: FoodTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var foodType = await _context.FoodType
                .SingleOrDefaultAsync(m => m.FoodTypeId == id);
            if (foodType == null)
            {
                return NotFound();
            }

            return View(foodType);
        }

        // POST: FoodTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var foodType = await _context.FoodType.SingleOrDefaultAsync(m => m.FoodTypeId == id);
            _context.FoodType.Remove(foodType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FoodTypeExists(int id)
        {
            return _context.FoodType.Any(e => e.FoodTypeId == id);
        }
    }
}
