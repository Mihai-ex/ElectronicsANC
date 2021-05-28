using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ElectronicsANC.Models
{
    public class ShoppingCartModel
    {
        public Guid IdShoppingCart { get; set; }
        public Guid IdProduct { get; set; }
        [Required(ErrorMessage = "Mandatory field")]
        public int Quantity { get; set; }
        [Required(ErrorMessage = "Mandatory field")]
        public decimal Price { get; set; }
        [NotMapped]
        public string ProductName { get; set; }
    }
}