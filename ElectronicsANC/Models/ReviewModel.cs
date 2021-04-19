using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ElectronicsANC.Models
{
    public class ReviewModel
    {
        public Guid IdReview { get; set; }
        public Guid IdMember { get; set; }
        public Guid IdProduct { get; set; }
        [Required(ErrorMessage = "Mandatory field")]
        public string ReviewDetails { get; set; }
        [Required(ErrorMessage = "Mandatory field")]
        public DateTime ReviewDate { get; set; }
    }
}