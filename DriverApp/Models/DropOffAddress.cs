using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JNPShuttle.Models
{
    public class DropoffAddress
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
}


    