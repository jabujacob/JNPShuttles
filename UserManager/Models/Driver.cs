using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JNPPortal.Models
{
    public class Driver
    {
        public int ID { get; set; }

        [StringLength(50)]
        [DisplayName("First Name")]
        public string FirstName { get; set; }

        [StringLength(50)]
        [DisplayName("Last Name")]
        public string LastName { get; set; }
        [StringLength(50)]
        [DisplayName("Super Shuttle Id")]
        public string SuperShuttleID { get; set; }

        [DisplayName("Default Van")]
        public Nullable<int> DefaultVanId { get; set; }
        [DisplayName("Default Van")]
        public string DefaultVan { get; set; }

        [DisplayName("Driver Share 1")]
        public Nullable<decimal> DriverShare1 { get; set; }
        [DisplayName("Driver Share 2")]
        public Nullable<decimal> DriverShare2 { get; set; }

        public string DisplayName
        {
            get { return FirstName + " " + LastName + " - " + SuperShuttleID; }
        }

    }
}