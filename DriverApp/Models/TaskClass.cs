using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JNPShuttle.Models
{
    public class TaskClass
    {
               
        string accountName;
        BookingID bookingID;
        string taskID;
        string dispatchStatus;
        DropoffAddress dropOffAddress;
        string fareType;
        string pax;
        string paymentType;
        PickupAddress pickupAddress;
        string pickupTime;
        string retailFare;
        string serviceTime;
        string travellerSurname;
        string vehicleNumber;
        string airportFee;
        string fee;
        string bucode;
        string driverID;
        string run;
        string serviceType;
        string totalFare;

        public string AccountName { get => accountName; set => accountName = value; }
        public BookingID BookingID { get => bookingID; set => bookingID = value; }
        public string TaskID { get => taskID; set => taskID = value; }
        public string DispatchStatus { get => dispatchStatus; set => dispatchStatus = value; }
        public DropoffAddress DropOffAddress { get => dropOffAddress; set => dropOffAddress = value; }        
        public string FareType { get => fareType; set => fareType = value; }
        public string Pax { get => pax; set => pax = value; }
        public string PaymentType { get => paymentType; set => paymentType = value; }
        public PickupAddress PickupAddress { get => pickupAddress; set => pickupAddress = value; }
        public string PickupTime { get => pickupTime; set => pickupTime = value; }
        public string RetailFare { get => retailFare; set => retailFare = value; }
        public string ServiceTime { get => serviceTime; set => serviceTime = value; }
        public string TravellerSurname { get => travellerSurname; set => travellerSurname = value; }
        public string VehicleNumber { get => vehicleNumber; set => vehicleNumber = value; }
        public string AirportFee { get => airportFee; set => airportFee = value; }
        public string Fee { get => fee; set => fee = value; }
        public string Bucode { get => bucode; set => bucode = value; }
        public string ServiceType { get => serviceType; set => serviceType = value; }
        public string DriverID { get => driverID; set => driverID = value; }
        public string Run { get => run; set => run = value; }
        public string TotalFare { get => totalFare; set => totalFare = value; }
        
    }
}