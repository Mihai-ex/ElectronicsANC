using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ElectronicsANC.Models
{
    public class OrderModel
    {
        public Guid IdOrder { get; set; }
        public Guid IdProduct { get; set; }
        [Required(ErrorMessage = "Mandatory field")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd", ApplyFormatInEditMode = true)]
        public DateTime DateOrder { get; set; }
        [Required(ErrorMessage = "Mandatory field")]
        public int Quantity { get; set; }
        [Required(ErrorMessage = "Mandatory field")]
        public decimal Price { get; set; }
        [NotMapped]
        public string ProductName { get; set; }
    }
}