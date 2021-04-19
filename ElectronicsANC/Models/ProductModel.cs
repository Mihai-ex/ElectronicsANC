using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ElectronicsANC.Models
{
    public class ProductModel
    {
        public Guid IdProduct { get; set; }
        public Guid IdCategory { get; set; }
        public string ProductName { get; set; }
        public string Manufacturer { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Warranty { get; set; }
        public int? Rating { get; set; }
    }
}