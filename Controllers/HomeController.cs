// ---------------------------------------------------------------------------
// File name:Project2.cs
// Project name:Movie Viewer
// ---------------------------------------------------------------------------
// Creator’s name and email: Stan Seiferth zsjs19@etsu.edu					
// Course-Section:CSCI-3110-001
//	Creation Date:	11/07/2016		
// ---------------------------------------------------------------------------

using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Project2.DataContexts;
using Project2.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Project2.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private IdentityDb _idb = new IdentityDb();
        private MovieDb _db = new MovieDb();
        protected override void Dispose(bool disposing)
        {
            _db.Dispose();
            _idb.Dispose();
            base.Dispose(disposing);
        }

        public ActionResult Index()
        {
            var userStore = new UserStore<ApplicationUser>(_idb);
            var userManager = new UserManager<ApplicationUser>(userStore);
            var user = userManager.FindById(User.Identity.GetUserId());
            if (User.IsInRole("Admin"))
                return RedirectToAction("Index", "Admin");
            
            var profileQuery = _db.Profiles.Where(s => s.Email.Equals(user.Email, StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault();
            if (profileQuery == null)
                return RedirectToAction("Create", "Profile");

            var Count = _db.Counters.Where(s => s.UserEmail.Equals(user.Email, StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault();
            ViewBag.MovieCount = Count.MovieCount;
            ViewBag.MoneySpent = Count.MoneySpent.ToString("C", CultureInfo.CurrentCulture);
            return View();
        }


        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
       
        
    }
}