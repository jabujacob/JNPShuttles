using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JNPShuttle.Models


{
    public class BookingID
    {

        int taskId;
        int reservationNumber;

        public int TaskId { get => taskId; set => taskId = value; }
        public int ReservationNumber { get => reservationNumber; set => reservationNumber = value; }
    }
}

   