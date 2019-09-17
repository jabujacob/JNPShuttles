using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebAPI.Models

{
    public class User
    {
        
        public int Id { get; set; }        
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

        [Required]
        [StringLength(50)]
        [DisplayName("Username")]
        public string Username { get; set; }

        [Required]
        [DisplayName("Is Active")]
        public bool IsActive { get; set; }

        [Required]
        [DisplayName("Admin")]
        public bool Admin { get; set; }

        [Required]
        [DisplayName("Driver")]
        public int DriverId { get; set; }

        public string Driver { get; set; }

    }
}