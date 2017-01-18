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
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Project2.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        // GET: Admin
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
           
            var query = _db.Movies.ToList();
            return View(query.ToList());
          
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
        public ActionResult Edit(string Id)
        {
            var query = _db.Movies.Where(s => s.Id.Equals(Id, StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault();
            return View(query);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Movie movie)
        {
            if (ModelState.IsValid)
            {
                _db.Entry(movie).State = System.Data.Entity.EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("Index", "Admin");
            }
            return View(movie);
        }

        
        public ActionResult Details(string Id)
        {
            var query = _db.Movies.Where(s => s.Id.Equals(Id, StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault();
            return View(query);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Movie movie)
        {
            if (ModelState.IsValid)
            {
                var movies = _db.Movies.ToList();
                var lastMovieId = movies.Last().Id;
                movie.Id = (int.Parse(lastMovieId) + 1).ToString();
                _db.Movies.Add(movie);
                _db.SaveChanges();
                return RedirectToAction("Index", "Admin");
            }
            return View(movie);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Delete(string Id)
        {
            var query = _db.Movies.Where(s => s.Id.Equals(Id, StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault();
            return View(query);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string Id)
        {
            Movie movie = _db.Movies.Find(Id);
            _db.Movies.Remove(movie);
            _db.SaveChanges();
            return RedirectToAction("Index", "Admin");
        }
    }
}