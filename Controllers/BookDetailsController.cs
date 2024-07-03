using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using OnlineBookReviews.Models;
using OnlineBookReviews.Utility;

namespace OnlineBookReviews.Controllers
{
    public class BookDetailsController : Controller
    {
        DataStorageHelper data = new DataStorageHelper();
        private BookEntities db = new BookEntities();

        // GET: BookDetails
        public ActionResult Index(string bookName)
        {
            data.InsertData();

            if(string.IsNullOrEmpty(bookName))
            {
                return View(db.BookDetail.ToList());
            }
            else
            {
                return View(db.BookDetail.Where(x => x.BookName.Contains(bookName)).ToList());
            }
        }

        [HttpPost]
        public ActionResult Search(string bookName)
        {
            return View(db.BookDetail.Where(x => x.BookName.Contains(bookName)).ToList());
        }

        [HttpPost]
        public ActionResult RateBook(string id, int rating)
        {
            bool loggedIn = data.CheckIfUserisLoggedIn();
            if (loggedIn == false)
            {
                return RedirectToAction("Login", "Login");
            }
            else
            {
                int userID = (Convert.ToInt32(data.RetrieveData("UserID")));
                var bookRate = db.BookRating.Where(x => x.ISBN == id && x.UserID == userID).ToList();
                if(bookRate.Count != 0)
                {
                    ViewBag.Message = "You have already rated this book";
                    ModelState.Clear();
                    return View();
                }
                else
                {
                    BookRatings bookRatings = new BookRatings();
                    bookRatings.ISBN = id;
                    bookRatings.UserID = userID;
                    bookRatings.Rating = rating;
                    db.BookRating.Add(bookRatings);
                    db.SaveChanges();

                    decimal avg = Convert.ToDecimal(CalcAvg(id));
                    BookDetails bookDetails = db.BookDetail.Find(id);
                    bookDetails.AverageRating = avg;
                    if (ModelState.IsValid)
                    {
                        db.Entry(bookDetails).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                    return RedirectToAction("Index", "BookDetails");
                }
            }
        }

        public double CalcAvg(string ISBN)
        {
            int count = 0;
            double total = 0;
            double avg = 0;
            var bookRatings = db.BookRating.Where(x => x.ISBN == ISBN).ToList();
            if(bookRatings != null)
            {
               count = bookRatings.Count();
               total = db.BookRating.Where(x => x.ISBN == ISBN).Sum(x => x.Rating);
                avg = total / count;
            }
            return avg;
        }

        // GET: BookDetails/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BookDetails bookDetails = db.BookDetail.Find(id);
            if (bookDetails == null)
            {
                return HttpNotFound();
            }
            return View(bookDetails);
        }

        public ActionResult EditUser()
        {
            int id = Convert.ToInt16(data.RetrieveData("UserID"));
            return RedirectToAction("Edit", "UserDetails", new { id = id});
        }

        // GET: BookDetails/Create
        public ActionResult Create()
        {
            bool loggedIn = data.CheckIfUserisLoggedIn();
            if(loggedIn == false)
            {
                return RedirectToAction("Login", "Login");
            }
            else
            {
                return View();
            }          
        }

        // POST: BookDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ISBN,BookName,Author,AddedByUserID,AddedOn,Description,AverageRating")] BookDetails bookDetails)
        {
            if (ModelState.IsValid)
            {
                bookDetails.AddedByUserID = Convert.ToInt32(data.RetrieveData("UserID"));
                bookDetails.AddedOn = DateTime.Now;
                db.BookDetail.Add(bookDetails);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(bookDetails);
        }

        // GET: BookDetails/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BookDetails bookDetails = db.BookDetail.Find(id);
            if (bookDetails == null)
            {
                return HttpNotFound();
            }
            return View(bookDetails);
        }

        // POST: BookDetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ISBN,BookName,Author,AddedByUserID,AddedOn,Description,AverageRating")] BookDetails bookDetails)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bookDetails).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(bookDetails);
        }

        // GET: BookDetails/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BookDetails bookDetails = db.BookDetail.Find(id);
            if (bookDetails == null)
            {
                return HttpNotFound();
            }
            return View(bookDetails);
        }

        // POST: BookDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            BookDetails bookDetails = db.BookDetail.Find(id);
            db.BookDetail.Remove(bookDetails);
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
