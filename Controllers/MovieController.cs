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
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Project2.Controllers
{
    [Authorize]
    public class MovieController : Controller
    {
        private IdentityDb _idb = new IdentityDb();
        private MovieDb _db = new MovieDb();
        protected override void Dispose(bool disposing)
        {
            _db.Dispose();
            _idb.Dispose();
            base.Dispose(disposing);
        }
        // GET: Movie
        public ActionResult Index()
        {
            var userStore = new UserStore<ApplicationUser>(_idb);
            var userManager = new UserManager<ApplicationUser>(userStore);
            var user = userManager.FindById(User.Identity.GetUserId());
            if (User.IsInRole("Admin"))
                return RedirectToAction("Index", "Admin");

            var alreadyViewed = _db.ViewedMovies.Where(s => s.UserEmail.Equals(user.Email, StringComparison.InvariantCultureIgnoreCase)).Select(s => s.Id).ToList();
            var query = _db.Movies.Where(s => !alreadyViewed.Contains(s.Id)).ToList();
            return View(query);
        }

        public ActionResult Watch(string Id)
        {
            var userStore = new UserStore<ApplicationUser>(_idb);
            var userManager = new UserManager<ApplicationUser>(userStore);
            var user = userManager.FindById(User.Identity.GetUserId());

            var movie = _db.Movies.Where(s => s.Id.Equals(Id, StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault();
            var count = _db.Counters.Where(s => s.UserEmail.Equals(user.Email, StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault();

            count.MovieCount += 1;
            count.MoneySpent += movie.Price;

            _db.Entry(count).State = System.Data.Entity.EntityState.Modified;
            _db.SaveChanges();

            var viewedMovie = new ViewedMovie();
            viewedMovie.Id = movie.Id;
            viewedMovie.Title = movie.Title;
            viewedMovie.LengthInMinutes = movie.LengthInMinutes;
            viewedMovie.Year = movie.Year;
            viewedMovie.Price = movie.Price;
            viewedMovie.UserEmail = user.Email;
            viewedMovie.IMDBUrl = movie.IMDBUrl;

            _db.ViewedMovies.Add(viewedMovie);
            _db.SaveChanges();

            return Redirect(movie.IMDBUrl);
        }

        public ActionResult Details(string Id)
        {
            var query = _db.Movies.Where(s => s.Id.Equals(Id, StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault();
            return View(query);
        }

        public ActionResult PurchasedList()
        {
            var userStore = new UserStore<ApplicationUser>(_idb);
            var userManager = new UserManager<ApplicationUser>(userStore);
            var user = userManager.FindById(User.Identity.GetUserId());
            if (User.IsInRole("Admin"))
                return RedirectToAction("Index", "Admin");

            ViewBag.tags = _db.Tags.Where(s => s.UserEmail.Equals(user.Email, StringComparison.InvariantCultureIgnoreCase)).ToList();

            var query = _db.ViewedMovies.Where(s => s.UserEmail.Equals(user.Email, StringComparison.InvariantCultureIgnoreCase)).ToList();
            return View(query.ToList());
        }

        public ActionResult DetailsOwned(string Id)
        {
            var query = _db.Movies.Where(s => s.Id.Equals(Id, StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault();
            return View(query);
        }

        public ActionResult Tags(string Id)
        {
            var userStore = new UserStore<ApplicationUser>(_idb);
            var userManager = new UserManager<ApplicationUser>(userStore);
            var user = userManager.FindById(User.Identity.GetUserId());

            ViewBag.Id = Id;

            var query = _db.Tags.Where(s => s.MovieId.Equals(Id, StringComparison.InvariantCultureIgnoreCase) && s.UserEmail.Equals(user.Email, StringComparison.InvariantCultureIgnoreCase)).ToList();
            return View(query);
        }
        
        public ActionResult Create(string Id)
        {
            var userStore = new UserStore<ApplicationUser>(_idb);
            var userManager = new UserManager<ApplicationUser>(userStore);
            var user = userManager.FindById(User.Identity.GetUserId());
            var newId = 0;

            var count = _db.Tags.Count();
            if (count == 0)
                newId = 1;
            else
            {
                var tags = _db.Tags.ToList();
                var lastTagId = tags.Last().Id;
                newId = (int.Parse(lastTagId) + 1);
            }


            ViewBag.Id = newId.ToString();
            ViewBag.MovieId = Id;
            ViewBag.UserEmail = user.Email;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Tag tag)
        {
            if (ModelState.IsValid)
            {
                _db.Tags.Add(tag);
                _db.SaveChanges();
                return RedirectToAction("PurchasedList", "Movie");
            }
            return View(tag);
        }

        public ActionResult TagDetails(string Id)
        {
            var query = _db.Tags.Where(s => s.Id.Equals(Id, StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault();
            return View(query);
        }

        public ActionResult Edit(string Id)
        {
            var query = _db.Tags.Where(s => s.Id.Equals(Id, StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault();
            return View(query);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Tag tag)
        {
            if (ModelState.IsValid)
            {
                _db.Entry(tag).State = System.Data.Entity.EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("PurchasedList", "Movie");
            }
            return View(tag);
        }

        public ActionResult Delete(string Id)
        {
            var query = _db.Tags.Where(s => s.Id.Equals(Id, StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault();
            return View(query);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string Id)
        {
            Tag tag = _db.Tags.Find(Id);
            _db.Tags.Remove(tag);
            _db.SaveChanges();
            return RedirectToAction("PurchasedList", "Movie");
        }

        public ActionResult search(string Id)
        {
            var userStore = new UserStore<ApplicationUser>(_idb);
            var userManager = new UserManager<ApplicationUser>(userStore);
            var user = userManager.FindById(User.Identity.GetUserId());
            if (User.IsInRole("Admin"))
                return RedirectToAction("Index", "Admin");

            ViewBag.tags = _db.Tags.Where(s => s.UserEmail.Equals(user.Email, StringComparison.InvariantCultureIgnoreCase)).ToList();
            ViewBag.MovieTag = Id;

            var movieIds = _db.Tags.Where(s => s.MovieTag.Equals(Id, StringComparison.InvariantCultureIgnoreCase) && s.UserEmail.Equals(user.Email, StringComparison.InvariantCultureIgnoreCase)).Select(s => s.MovieId).ToList();
            var query = _db.ViewedMovies.Where(s => movieIds.Contains(s.Id)).ToList();
            return View(query);
        }

    }
}