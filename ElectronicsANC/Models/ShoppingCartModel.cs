using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ElectronicsANC.Models
{
    public class ShoppingCartModel
    {
        public Guid IdShoppingCart { get; set; }
        public Guid IdProduct { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}