using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ElectronicsANC.Models
{
    public class MemberModel
    {
        public Guid IdMember { get; set; }
        [Required(ErrorMessage ="Mandatory field")]
        [StringLength(250, ErrorMessage = "String too long (max 250 characters)")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Mandatory field")]
        [StringLength(250, ErrorMessage = "String too long (max 250 characters)")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Mandatory field")]
        [StringLength(500, ErrorMessage = "String too long (max 500 characters)")]
        public string Address { get; set; }
        [Required(ErrorMessage = "Mandatory field")]
        [StringLength(100, ErrorMessage = "String too long (max 100 characters)")]
        public string City { get; set; }
        [Required(ErrorMessage = "Mandatory field")]
        [StringLength(100, ErrorMessage = "String too long (max 100 characters)")]
        public string Country { get; set; }
        [StringLength(50, ErrorMessage = "String too long (max 50 characters)")]
        public string PostalCode { get; set; }
        [StringLength(20, ErrorMessage = "String too long (max 20 characters)")]
        public string Phone { get; set; }
        [Required(ErrorMessage = "Mandatory field")]
        [StringLength(100, ErrorMessage = "String too long (max 100 characters")]
        public string Email { get; set; }
    }
}