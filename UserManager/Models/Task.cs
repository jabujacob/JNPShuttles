﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JNPPortal.Models
{
    public class Task
    {
        public int ID { get; set; }
        public string AccountName { get; set; }
        public string BookingId { get; set; }
        public string TaskId { get; set; }
        public string DispatchStatus { get; set; }
        public string DropOffAddress { get; set; }
        public string Pax { get; set; }
        public string FareType { get; set; }
        public string PaymentType { get; set; }
        public string PickupAddress { get; set; }
        public string PickupTime { get; set; }
        public DateTime DTPickupTime { get; set; }
        public string RetailFare { get; set; }
        public decimal DCRetailFare { get; set; }
        public string ServiceTime { get; set; }
        public DateTime DTServiceTime { get; set; }
        public string TravellerSurname { get; set; }
        public string VehicleNumber { get; set; }
        public string AirportFee { get; set; }
        public decimal DCAirportFee { get; set; }
        public string Fee { get; set; }
        public decimal DCFee { get; set; }
        public string Bbucode { get; set; }
        public string DriverId { get; set; }
        public string Run { get; set; }
        public string ServiceType { get; set; }
        public string TotalFare { get; set; }
        public decimal DCTotalFare { get; set; }
        public DateTime ImportDate { get; set; }
    }
}