using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace OnlineBookReviews.Models
{
    public class BookEntities : DbContext
    {
        public BookEntities() : base()
        {

        }

        public DbSet<BookDetails> BookDetail { get; set; }
        public DbSet<BookRatings> BookRating { get; set; }
        public DbSet<UserDetails> UserDetail { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BookDetails>().MapToStoredProcedures();
            modelBuilder.Entity<BookRatings>().MapToStoredProcedures();
            modelBuilder.Entity<UserDetails>().MapToStoredProcedures();
        }
    }
}