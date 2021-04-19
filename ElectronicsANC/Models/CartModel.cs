using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ElectronicsANC.Models
{
    public class CartModel
    {
        public Guid IdCart { get; set; }
        public Guid IdShoppingCart { get; set; }
        public Guid IdMember { get; set; }
        public DateTime Date { get; set; }
    }
}