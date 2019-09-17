using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JNPShuttle.Models
{
    public class Dashboard
    {
        string period;
        decimal gross;
        decimal net;
        decimal driverShare;        

        public string Period { get => period; set => period = value; }
        public decimal Gross { get => gross; set => gross = value; }
        public decimal Net   { get => net; set => net = value; }
        public decimal DriverShare { get => driverShare; set => driverShare = value; }       
    }
}