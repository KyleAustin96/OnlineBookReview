using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace OnlineBookReviews.Models
{
    public class BookDetails
    {
        [Key]
        public string ISBN { get; set; }
        public string BookName { get; set; }
        public string Author { get; set; }
        public int AddedByUserID { get; set; }
        public DateTime AddedOn { get; set; }
        public string Description { get; set; }
        public decimal AverageRating { get; set; }
    }
}