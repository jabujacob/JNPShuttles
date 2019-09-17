using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace JNPPortal.Models
{
    public class User
    {
        [DisplayName("No")]
        public int Id { get; set; }

        [StringLength(50)]
        [DisplayName("User Name")]
        public string Username { get; set; }
        [Required]
        [StringLength(50)]
        [DisplayName("Password")]
        public string Password { get; set; }

        [Required]
        [StringLength(50)]
        [DisplayName("First Name")]
        public string FirstName { get; set; }
       
        [Required]
        [StringLength(50)]
        [DisplayName("Last Name")]
        public string LastName { get; set; }        
        
        [RegularExpression(@"[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}", ErrorMessage = "Please enter correct email")]
        [StringLength(50)]
        [DisplayName("Email")]
        public string Email { get; set; }

        [DisplayName("Driver")]
        public int DriverId { get; set; }

        [DisplayName("Driver")]
        public string Driver { get; set; }

        [Required]
        [DisplayName("Admin")]
        public bool Admin { get; set; }        
      
        public string DisplayName
        {
            get { return FirstName + " " +LastName; }
        }        
    }
}
