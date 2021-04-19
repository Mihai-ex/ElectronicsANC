using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ElectronicsANC.Models
{
    public class ProductModel
    {
        public Guid IdProduct { get; set; }
        public Guid IdCategory { get; set; }
        [Required(ErrorMessage = "Mandatory field")]
        [StringLength(150, ErrorMessage = "String too long (max 150 characters)")]
        public string ProductName { get; set; }
        [Required(ErrorMessage = "Mandatory field")]
        [StringLength(150, ErrorMessage = "String too long (max 150 characters)")]
        public string Manufacturer { get; set; }
        [Required(ErrorMessage = "Mandatory field")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Mandatory field")]
        public decimal Price { get; set; }
        [Required(ErrorMessage = "Mandatory field")]
        public int Warranty { get; set; }
        public int? Rating { get; set; }
    }
}