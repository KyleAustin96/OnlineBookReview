﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace OnlineBookReviews.Models
{
    public class BookRatings
    {
        [Key]
        public int ID { get; set; }
        public int UserID { get; set; }
        public string ISBN { get; set; }
        public int Rating { get; set; }
    }
}