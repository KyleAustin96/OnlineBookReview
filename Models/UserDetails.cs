using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace OnlineBookReviews.Models
{
    public class UserDetails
    {
        [Key]
        public int UserID { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string EmailAddress { get; set; }
        public string IDNumber { get; set; }
        public int ContactNumber { get; set; }
        public DateTime CreatedOn { get; set; }
        public string Password { get; set; }
    }
}