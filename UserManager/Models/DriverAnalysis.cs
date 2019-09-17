using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JNPPortal.Models
{
    public class DriverAnalysis
    {
        public string SuperShuttleId { get; set; }
        public string OriginalSuperShuttleId { get; set; }
        public string BookingId { get; set; }
        public int TaskId { get; set; }
        public int VehicleNumber { get; set; }
        public DateTime PickupDate { get; set; }
        public DateTime PickUpTime { get; set; }
        public string PickUpAddress { get; set; }
        public string DropOffAddress { get; set; }
        public int Pax { get; set; }
        public decimal SurCharge { get; set; }
        public decimal TelephoneCharge { get; set; }
        public decimal DriverShare1 { get; set; }
        public decimal DriverShare2 { get; set; }      
        public string PaymentType { get; set; }
        public string FareType { get; set; }
        public string ServiceType { get; set; }
        public decimal RetailFare { get; set; }
        public decimal TotalFare { get; set; }
        public decimal TTLCash { get; set; }
        public decimal TTLPrePaid { get; set; }
        public decimal TTLGross { get; set; }
        public int Transfer { get; set; }
        public decimal AlternateDriverShareCalculationAmount { get; set; }
        public int ShiftStartKM { get; set; }
        public int ShiftEndKM { get; set; }
        public int ShiftKMRun { get; set; }
        public DateTime ShiftStartTime { get; set; }
        public DateTime ShiftEndTime { get; set; }
        public int ShiftTimeWorked { get; set; }
        public int Swipes { get; set; }
        public int Trips { get; set; }
        public decimal DriverPrePaid { get; set; }
        public decimal DriverCash { get; set; }
        public decimal DriverCCEFTPOS { get; set; }
        public decimal DriverTaxiChit { get; set; }
        public decimal DriverGross { get; set; }



        //Summary props
        public int Drops { get; set; }
        public decimal Net { get; set; }
        public decimal Drvr { get; set; }
        public decimal Optr { get; set; }
        public decimal DollarPerKMNet { get; set; }
        public decimal Contr { get; set; }
        public decimal DrvrNet { get; set; }
        public decimal VariancePrePaid { get; set; }
        public decimal VarianceCash { get; set; }
        public decimal VarianceGross { get; set; }
    }
}