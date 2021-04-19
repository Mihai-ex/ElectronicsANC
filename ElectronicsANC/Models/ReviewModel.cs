using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ElectronicsANC.Models
{
    public class ReviewModel
    {
        public Guid IdReview { get; set; }
        public Guid IdMember { get; set; }
        public Guid IdProduct { get; set; }
        public string ReviewDetails { get; set; }
        public DateTime ReviewDate { get; set; }
    }
}