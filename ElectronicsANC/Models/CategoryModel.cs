using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ElectronicsANC.Models
{
    public class CategoryModel
    {
        public Guid IdCategory { get; set; }
        [Required(ErrorMessage = "Mandatory field")]
        [StringLength(150, ErrorMessage = "String too long (max 150 characters)")]
        public string CategoryName { get; set; }
    }
}