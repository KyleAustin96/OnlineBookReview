using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using OnlineBookReviews.Models;

namespace OnlineBookReviews.Controllers
{
    public class BookRatingsController : Controller
    {
        private BookEntities db = new BookEntities();

        // GET: BookRatings
        public ActionResult Index()
        {
            return View(db.BookRating.ToList());
        }

        // GET: BookRatings/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BookRatings bookRatings = db.BookRating.Find(id);
            if (bookRatings == null)
            {
                return HttpNotFound();
            }
            return View(bookRatings);
        }

        // GET: BookRatings/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BookRatings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,UserID,ISBN,Rating")] BookRatings bookRatings)
        {
            if (ModelState.IsValid)
            {
                db.BookRating.Add(bookRatings);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(bookRatings);
        }

        // GET: BookRatings/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BookRatings bookRatings = db.BookRating.Find(id);
            if (bookRatings == null)
            {
                return HttpNotFound();
            }
            return View(bookRatings);
        }

        // POST: BookRatings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,UserID,ISBN,Rating")] BookRatings bookRatings)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bookRatings).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(bookRatings);
        }

        // GET: BookRatings/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BookRatings bookRatings = db.BookRating.Find(id);
            if (bookRatings == null)
            {
                return HttpNotFound();
            }
            return View(bookRatings);
        }

        // POST: BookRatings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BookRatings bookRatings = db.BookRating.Find(id);
            db.BookRating.Remove(bookRatings);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
