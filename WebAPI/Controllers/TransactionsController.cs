using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    public class TransactionsController : ApiController
    {
        
        readonly DataAccess db = new DataAccess();

        // GET: api/Transactions
        public IEnumerable<Transaction> Get()
        {
            return db.GetAllTransactions();
        }

        // GET: api/Transactions/5
        public Transaction Get(int id)
        {
            return db.GetTransaction(id);
        }

        // POST: api/Transactions
        public void Post(Transaction transaction)
        {
            if (transaction.Id == -1)
            {
                db.InsertTransactionBulk(transaction);
            }
            else
            {
                db.InsertTransaction(transaction);
            }            
        }

        // PUT: api/Transactions
        public void Put(Transaction transaction )
        {
            db.UpdateTransaction(transaction);
        }

    }

}
