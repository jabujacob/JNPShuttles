using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPI.Models
{
    public class TaskSuperShuttle

    {
               
        string accountName;
        public BookingID BookingID{ get; set; }
        string taskID;
        string dispatchStatus;
        public Address DropOffAddress { get; set; }         
        string fareType;
        string pax;
        string paymentType;
        public Address PickupAddress { get; set; }        
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
        
        public string TaskID { get => taskID; set => taskID = value; }
        public string DispatchStatus { get => dispatchStatus; set => dispatchStatus = value; }
        
        public string FareType { get => fareType; set => fareType = value; }
        public string Pax { get => pax; set => pax = value; }
        public string PaymentType { get => paymentType; set => paymentType = value; }
        
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


    public class BookingID
    {

        int taskId;
        int reservationNumber;

        public int TaskId { get => taskId; set => taskId = value; }
        public int ReservationNumber { get => reservationNumber; set => reservationNumber = value; }
    }

    public class Address
    {

        string district;
        string location;
        string number;
        string street;
        string suburb;

        public string District { get => district; set => district = value; }
        public string Location { get => location; set => location = value; }
        public string Number { get => number; set => number = value; }
        public string Street { get => street; set => street = value; }
        public string Suburb { get => suburb; set => suburb = value; }
    }
    public class TaskCollection
    {
        public List<TaskSuperShuttle> Tasks { get; set; }
    }

}

