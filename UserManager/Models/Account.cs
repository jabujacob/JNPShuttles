using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace JNPPortal.Models
{
    public class Account
    {
        public int Id { get; set; }
        [DisplayName("Account Head")]
        public string AccountName { get; set; }
        public int GroupId { get; set; }
        public string AccountGroup { get; set; }
        public decimal GST { get; set; }
        [DisplayName("Account Type")]
        public int AccountTypeId { get; set; }
        [DisplayName("Account Type")]
        public string AccountType { get; set; }

    }
}