using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPI.Models
{
    public class Transaction
    {
        public int Id { get; set; }
        public int AccountId { get; set; }       
        public string Account { get; set; }
        public bool Expense { get; set; }
        public bool CAPEX { get; set; }
        public int GroupId { get; set; }
        public string AccountGroup { get; set; }
        public decimal Amount { get; set; }
        public decimal GST { get; set; }
        public decimal Net { get; set; }
        public int VanId { get; set; }
        public string Van { get; set; }
        public string VansId { get; set; } //Id for bulk transaction
        public int TransactionBulkId { get; set; }
        public DateTime Date { get; set; }
   
    }
}