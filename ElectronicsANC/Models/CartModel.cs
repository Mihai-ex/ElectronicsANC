using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ElectronicsANC.Models
{
    public class CartModel
    {
        public Guid IdCart { get; set; }
        public Guid IdShoppingCart { get; set; }
        public Guid IdMember { get; set; }
        [Required(ErrorMessage = "Mandatory field")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }
    }
}