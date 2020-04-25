//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace JNPShuttle.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class TripSheet
    {
        public int ID { get; set; }
        public int DriverID { get; set; }
        public int VanID { get; set; }
        public int StartKM { get; set; }
        public Nullable<int> EndKM { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MMM/yyyy hh:mm tt}")]
        public Nullable<System.DateTime> StartTime { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MMM/yyyy hh:mm tt}")]
        public Nullable<System.DateTime> EndTime { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:HH:mm}")]
        public Nullable<System.DateTime> Duration { get; set; }
        public Nullable<byte> Swipes { get; set; }
        public Nullable<byte> Trips { get; set; }

        [DataType(DataType.Currency)]        
        public Nullable<decimal> TaxiChit { get; set; }

        [DataType(DataType.Currency)]
        public Nullable<decimal> CCEFTPOS { get; set; }

        [DataType(DataType.Currency)]
        public Nullable<decimal> PrePaid { get; set; }

        [DataType(DataType.Currency)]
        public Nullable<decimal> Cash { get; set; }

        [DataType(DataType.Currency)]
        public Nullable<decimal> Gross
        {
            get { return Convert.ToDecimal((PrePaid??0) + (Cash??0) + (CCEFTPOS?? 0) + (TaxiChit?? 0)); }
        }

        [DataType(DataType.Currency)]
        public Nullable<decimal> NetCash
        {
            get { return Convert.ToDecimal((Cash??0) - ((TaxiChit??0) + (CCEFTPOS??0))); }
        }

        public String Period
        {
            get { return String.Concat(StartTime, " - ", EndTime); }
        }
        public int KMTravelled
        {
            get { return Convert.ToInt32(EndKM - StartKM); }
        }

        public virtual Driver Driver { get; set; }
        public virtual Van Van { get; set; } 

    }
}