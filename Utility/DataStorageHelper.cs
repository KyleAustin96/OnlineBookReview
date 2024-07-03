using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using OnlineBookReviews.Models;
using OnlineBookReviews.Controllers;

namespace OnlineBookReviews.Utility
{
    public class DataStorageHelper : System.Web.UI.Page
    {
        public void CreateData(string name, string value)
        {
            Session[name] = value;
            return;
        }

        public string RetrieveData(string name)
        {
            string value = null;
            if (System.Web.HttpContext.Current.Session[name] == null)
            {
                return value;
            }
            else
            {
                value = Session[name].ToString();
            }
            return value;
        }
        public bool CheckIfUserisLoggedIn()
        {
            string UserID = RetrieveData("UserID");
            if (UserID == null)
            {
                return false;
            }
            return true;
        }

        //Method to insert some data for testing//
        public void InsertData()
        {
            using (BookEntities db = new BookEntities())
            {
                var bookDetails = db.BookDetail.ToList();
                if(bookDetails.Count == 0)
                {
                    UserDetails user = new UserDetails();
                    BookDetails book = new BookDetails();

                    user.FirstName = "Kyle";
                    user.Surname = "Subramany";
                    user.EmailAddress = "kyleasubramany@gmail.com";
                    user.CreatedOn = DateTime.Now;
                    user.ContactNumber = 0788948875;
                    user.IDNumber = "9609085139086";
                    user.Password = "Kyle1234";
                    db.UserDetail.Add(user);
                    db.SaveChanges();

                    int userID = 0;
                    var userDetails = db.UserDetail.Where(x => x.FirstName == "Kyle").FirstOrDefault();
                    if(userDetails != null)
                    {
                        userID = userDetails.UserID;
                    }

                    book.ISBN = "9780399529207";
                    book.BookName = "Lord of the Flies";
                    book.Author = "William Golding";
                    book.AddedOn = DateTime.Now;
                    book.AddedByUserID = userID;
                    book.Description = "Children get abandoned on an island and have to learn how to survive on their own with no adults";

                    db.BookDetail.Add(book);
                    db.SaveChanges();
                }
            }
        }
    }
}