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
    public class FoodManagementsController : Controller
    {
        private readonly GreenBD_DairyContext _context;

        public FoodManagementsController(GreenBD_DairyContext context)
        {
            _context = context;
        }

        // GET: FoodManagements
        public async Task<IActionResult> Index(string srctext, int page = 1)
        {
            IQueryable<FoodManagement> greenBD_DairyContext = _context.FoodManagement.Include(f => f.AmountSign).Include(f => f.FoodType).Include(f => f.NumberSign).OrderByDescending(e => e.Id);

            if (!string.IsNullOrEmpty(srctext))
            {
                srctext = srctext.ToUpper();

                greenBD_DairyContext = _context.FoodManagement.Include(f => f.AmountSign).Include(f => f.FoodType)
                    .Include(f => f.NumberSign).Where(e => e.Id.ToString().Contains(srctext) || e.ComOrBrndName.ToUpper().Contains(srctext) || e.FoodName.ToUpper().Contains(srctext) || e.ShopName.ToUpper().Contains(srctext)||e.ManagerSignature.ToUpper().Contains(srctext)).OrderByDescending(e => e.Id);
            }


            ViewBag.TotalCount = greenBD_DairyContext.Count();
            ViewBag.srctext = srctext;

            if (page <= 0) { page = 1; }
            int pageSize = 10;

            return View(await PaginatedList<FoodManagement>.CreateAsync(greenBD_DairyContext, page, pageSize));
        }

        // GET: FoodManagements/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var foodManagement = await _context.FoodManagement
                .Include(f => f.AmountSign)
                .Include(f => f.FoodType)
                .Include(f => f.NumberSign)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (foodManagement == null)
            {
                return NotFound();
            }

            return View(foodManagement);
        }

        // GET: FoodManagements/Create
        public IActionResult Create()
        {
            ViewData["AmountSignId"] = new SelectList(_context.AmountSign, "AmountSignId", "AmountSignName");
            ViewData["FoodTypeId"] = new SelectList(_context.Set<FoodType>(), "FoodTypeId", "FoodTypeName");
            ViewData["NumberSignId"] = new SelectList(_context.Set<NumberSign>(), "NumberSignId", "NumberSignName");
            return View();
        }

        // POST: FoodManagements/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FoodTypeId,FoodName,FoodForm,ComOrBrndName,ShopName,NumberOfProduct,NumberSignId,Price,AmountSignId,TotalPrice,Description,ManagerSignature,EntryDate")] FoodManagement foodManagement)
        {
            if (ModelState.IsValid)
            {

                foodManagement.ManagerSignature = HttpContext.Session.GetString("username");
                foodManagement.EntryDate = DateTime.Now;
                
           
                _context.Add(foodManagement);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AmountSignId"] = new SelectList(_context.AmountSign, "AmountSignId", "AmountSignName", foodManagement.AmountSignId);

            ViewData["FoodTypeId"] = new SelectList(_context.Set<FoodType>(), "FoodTypeId", "FoodRypeName", foodManagement.FoodTypeId);

            ViewData["NumberSignId"] = new SelectList(_context.Set<NumberSign>(), "NumberSignId", "NumberSignName", foodManagement.NumberSignId);
            return View(foodManagement);
        }

        // GET: FoodManagements/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var foodManagement = await _context.FoodManagement.SingleOrDefaultAsync(m => m.Id == id);
            if (foodManagement == null)
            {
                return NotFound();
            }
            ViewData["AmountSignId"] = new SelectList(_context.AmountSign, "AmountSignId", "AmountSignName", foodManagement.AmountSignId);

            ViewData["FoodTypeId"] = new SelectList(_context.Set<FoodType>(), "FoodTypeId", "FoodTypeName", foodManagement.FoodTypeId);

            ViewData["NumberSignId"] = new SelectList(_context.Set<NumberSign>(), "NumberSignId", "NumberSignName", foodManagement.NumberSignId);
            return View(foodManagement);
        }

        // POST: FoodManagements/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FoodTypeId,FoodName,FoodForm,ComOrBrndName,ShopName,NumberOfProduct,NumberSignId,Price,AmountSignId,TotalPrice,Description,ManagerSignature,EntryDate")] FoodManagement foodManagement)
        {
            if (id != foodManagement.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(foodManagement);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FoodManagementExists(foodManagement.Id))
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
            ViewData["AmountSignId"] = new SelectList(_context.AmountSign, "AmountSignId", "AmountSignName", foodManagement.AmountSignId);
            ViewData["FoodTypeId"] = new SelectList(_context.Set<FoodType>(), "FoodTypeId", "Description", foodManagement.FoodTypeId);
            ViewData["NumberSignId"] = new SelectList(_context.Set<NumberSign>(), "NumberSignId", "NumberSignName", foodManagement.NumberSignId);
            return View(foodManagement);
        }

        // GET: FoodManagements/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var foodManagement = await _context.FoodManagement
                .Include(f => f.AmountSign)
                .Include(f => f.FoodType)
                .Include(f => f.NumberSign)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (foodManagement == null)
            {
                return NotFound();
            }

            return View(foodManagement);
        }

        // POST: FoodManagements/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var foodManagement = await _context.FoodManagement.SingleOrDefaultAsync(m => m.Id == id);
            _context.FoodManagement.Remove(foodManagement);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FoodManagementExists(int id)
        {
            return _context.FoodManagement.Any(e => e.Id == id);
        }
    }
}
