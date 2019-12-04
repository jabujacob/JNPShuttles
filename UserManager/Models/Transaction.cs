using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JNPPortal.Models
{
    public class Transaction
    {
        public int Id { get; set; }
        [DisplayName("Account")]
        public int AccountId { get; set; }
        public string Account { get; set; }
        public bool Expense { get; set; }
        public bool CAPEX { get; set; }
        public int GroupId { get; set; }
        [DisplayName("Account Group")]
        public string AccountGroup { get; set; }
        public decimal Amount { get; set; }
        public decimal GST { get; set; }
        public decimal Net { get; set; }

        [DisplayName("Van")]
        public int VanId { get; set; }
        public string Van { get; set; }
        [DisplayName("Van")]
        public string VansId { get; set; } //Id for bulk transaction
        public string[] SelectedVansId { get; set; } //Id for bulk transaction        

        [DisplayName("Transaction Bulk Id")]
        public int TransactionBulkId { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MMM/yyyy}")]
        public DateTime Date { get; set; }

     }
}