using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GreenBD_Dairy.Data;
using GreenBD_Dairy.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GreenBD_Dairy.Controllers
{
    public class OthersLogicController : Controller
    {

        private readonly ApplicationDbContext _context;

        public OthersLogicController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Remote validation

        [HttpPost]
        public JsonResult VerifyUsername(string username)
        {
            return Json(IsUserAvailable(username));
        }


        public bool IsUserAvailable(string uname)
        {

            var RegEmailId = (from u in _context.Users
                where u.NormalizedUserName == uname.ToUpper()
                select new { uname }).FirstOrDefault();

            bool status;
            if (RegEmailId != null)
            {
                //Already registered  
                status = false;
            }
            else
            {
                //Available to use  
                status = true;
            }

            return status;
        }

        [HttpGet]

        [Authorize(Roles = "Owner,Manager,Accounts,Worker")]
        public IActionResult AdminIndex()
        {
            return View();
        }
        public IActionResult Contact()
        {
            return View();
        }

    }
}