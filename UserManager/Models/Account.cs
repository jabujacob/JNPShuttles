﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JNPPortal.Models
{
    public class Account
    {
        public int Id { get; set; }
        public string AccountName { get; set; }
        public int GroupId { get; set; }
        public string AccountGroup { get; set; }
        public decimal GST { get; set; }
        public bool Expense { get; set; }
        public bool CAPEX { get; set; }

    }
}