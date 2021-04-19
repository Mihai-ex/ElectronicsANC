using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ElectronicsANC.Models
{
    public class OrderModel
    {
        public Guid IdOrder { get; set; }
        public Guid IdProduct { get; set; }
        [Required(ErrorMessage = "Mandatory field")]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy HH:MM:SS}", ApplyFormatInEditMode = true)]
        public DateTime DateOrder { get; set; }
        [Required(ErrorMessage = "Mandatory field")]
        public int Quantity { get; set; }
        [Required(ErrorMessage = "Mandatory field")]
        public decimal Price { get; set; }
    }
}