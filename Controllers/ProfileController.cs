// ---------------------------------------------------------------------------
// File name:Project2.cs
// Project name:Movie Viewer
// ---------------------------------------------------------------------------
// Creator’s name and email: Stan Seiferth zsjs19@etsu.edu					
// Course-Section:CSCI-3110-001
//	Creation Date:	11/07/2016		
// ---------------------------------------------------------------------------

using Project2.DataContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Project2.Models;

namespace Project2.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        private IdentityDb _idb = new IdentityDb();
        private MovieDb _db = new MovieDb();
        protected override void Dispose(bool disposing)
        {
            _db.Dispose();
            _idb.Dispose();
            base.Dispose(disposing);
        }


        // GET: Profile
        public ActionResult Index()
        {
            var userStore = new UserStore<ApplicationUser>(_idb);
            var userManager = new UserManager<ApplicationUser>(userStore);
            var user = userManager.FindById(User.Identity.GetUserId());
            ViewBag.email = user.Email; 
            
                var query = _db.Profiles.Where(s => s.Email.Equals(user.Email, StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault();
                return View(query);
        }

        public ActionResult Create()
        {
            var userStore = new UserStore<ApplicationUser>(_idb);
            var userManager = new UserManager<ApplicationUser>(userStore);
            var user = userManager.FindById(User.Identity.GetUserId());
            ViewBag.Email = user.Email;

            return View();
        }

        [HttpPost]
        public ActionResult Create(Profile profile)
        {
            var userStore = new UserStore<ApplicationUser>(_idb);
            var userManager = new UserManager<ApplicationUser>(userStore);
            var user = userManager.FindById(User.Identity.GetUserId());

            var counter = new Counter();
            counter.UserEmail = user.Email;
            counter.MoneySpent = 0;
            counter.MovieCount = 0;
            profile.Email = user.Email;

            if (ModelState.IsValid)
            {
                _db.Counters.Add(counter);
                _db.Profiles.Add(profile);
                _db.SaveChanges();
                return RedirectToAction("Index", "Profile");
            }
            return View(profile);
        }

        public ActionResult Edit()
        {
            var userStore = new UserStore<ApplicationUser>(_idb);
            var userManager = new UserManager<ApplicationUser>(userStore);
            var user = userManager.FindById(User.Identity.GetUserId());

            var query = _db.Profiles.Where(s => s.Email.Equals(user.Email, StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault();
            return View(query);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Profile profile)
        {
            if (ModelState.IsValid)
            {
                _db.Entry(profile).State = System.Data.Entity.EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("Index", "Profile");
            }
            return View(profile);
        }

        public ActionResult Delete()
        {
            var userStore = new UserStore<ApplicationUser>(_idb);
            var userManager = new UserManager<ApplicationUser>(userStore);
            var user = userManager.FindById(User.Identity.GetUserId());

            var query = _db.Profiles.Where(s => s.Email.Equals(user.Email, StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault();
            return View(query);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string Id)
        {
            var userStore = new UserStore<ApplicationUser>(_idb);
            var userManager = new UserManager<ApplicationUser>(userStore);
            var user = userManager.FindById(User.Identity.GetUserId());

            Profile profile = _db.Profiles.Where(s => s.Email.Equals(user.Email, StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault();
            Counter counter = _db.Counters.Where(s => s.UserEmail.Equals(user.Email, StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault();

            _db.Profiles.Remove(profile);
            _db.Counters.Remove(counter);
            _db.SaveChanges();
            return RedirectToAction("Index", "Home");
        }

    }
}
