using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    public class AccountsController : ApiController
    {

        readonly DataAccess db = new DataAccess();

        // GET: api/Accounts
        public IEnumerable<Account> Get()
        {
            return db.GetAllAccounts();
        }

        // GET: api/Accounts/5
        public Account Get(int id)
        {
            return db.GetAccount(id);
        }

        // POST: api/Accounts
        public void Post(Account account)
        {
            db.InsertAccount(account);
        }

        // PUT: api/Accounts
        public void Put(Account account)
        {
            db.UpdateAccount(account);
        }

    }
}
