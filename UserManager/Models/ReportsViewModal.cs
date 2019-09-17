using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JNPPortal.Models
{
    public class ReportsViewModal
    {
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "End Date")]
        public DateTime EndDate { get; set; }
        [Display(Name = "Driver")]
        public int DriverId { get; set; }


        //Tasks 
        [Display(Name = "Updated last on")]
        public DateTime UpdatedLastOnTime { get; set; }
    }
}