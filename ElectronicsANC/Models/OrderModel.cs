using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ElectronicsANC.Models
{
    public class OrderModel
    {
        public Guid IdOrder { get; set; }
        public Guid IdProduct { get; set; }
        public DateTime DateOrder { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}