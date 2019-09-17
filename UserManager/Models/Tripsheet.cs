using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JNPPortal.Models
{
    public class Tripsheet:IValidatableObject
    {
        [Required]        
        [DisplayName("No")]
        public int ID { get; set; }
        [Required]        
        [DisplayName("Driver")]
        public int DriverID { get; set; }
        [DisplayName("Driver")]
        public string Driver { get; set; }
        [DisplayName("Van")]
        public int VanID { get; set; }
        [DisplayName("Van")]
        public string Van { get; set; }
        [Required]
        [DisplayName("Start KM")]        
        public int StartKM { get; set; }
        [DisplayName("End KM")]
        public Nullable<int> EndKM { get; set; }
        [DisplayName("Start Time")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MMM/yyyy hh:mm tt}")]
        public System.DateTime StartTime { get; set; }

        [DisplayName("Start Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MMM/yyyy}")]
        public System.DateTime StartDate { get; set; }

        [DisplayName("End Time")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MMM/yyyy hh:mm tt}")]
        public Nullable<System.DateTime> EndTime { get; set; }


        public Nullable<byte> Swipes { get; set; }
        public Nullable<byte> Trips { get; set; }


        [DisplayName("Taxi Chit")]
        [DataType(DataType.Currency)]
        [DisplayFormat(ApplyFormatInEditMode = false, DataFormatString = "{0:c}")]
        public Nullable<decimal> TaxiChit { get; set; }

        [DataType(DataType.Currency)]
        [DisplayFormat(ApplyFormatInEditMode = false, DataFormatString = "{0:c}")]
        public Nullable<decimal> CCEFTPOS { get; set; }

        [DisplayName("Pre-paid")]
        [DataType(DataType.Currency)]
        [DisplayFormat(ApplyFormatInEditMode = false, DataFormatString = "{0:c}")]
        public Nullable<decimal> PrePaid { get; set; }

        [DataType(DataType.Currency)]
        [DisplayFormat(ApplyFormatInEditMode = false, DataFormatString = "{0:c}")]
        public Nullable<decimal> Cash { get; set; }

        [DisplayName("Trip Sheet Entry")]
        public string TripSheetEntered { get; set; }

        [DataType(DataType.Currency)]
        [DisplayFormat(ApplyFormatInEditMode = false, DataFormatString = "{0:c}")]
        public Nullable<decimal> Gross
        {
            get { return Convert.ToDecimal((PrePaid ?? 0) + (Cash ?? 0) + (CCEFTPOS ?? 0) + (TaxiChit ?? 0)); }
        }

        public String Period
        {
            get { return String.Concat(StartTime, " - ", EndTime); }
        }
        [DisplayName("KM Travelled")]
        public int KMTravelled
        {
            get { return Convert.ToInt32(EndKM - StartKM); }
        }

        [DisplayName("Duration")]
        public string Duration
        {

            get {
                TimeSpan span = Convert.ToDateTime(EndTime).Subtract(StartTime);
                return string.Concat(span.Hours," hrs and ",span.Minutes," mins");
            }            
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (EndTime != null)
            {
                if (EndTime < StartTime)
                {
                    yield return new ValidationResult("End time cannot be greater than Start time", new[] { "StartTime", "EndTime" });
                }
            }
        }
    }
}