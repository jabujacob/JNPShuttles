using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    public class AccountTypesController : ApiController
    {
        readonly DataAccess db = new DataAccess();

        // GET: api/AccountTypes
        public IEnumerable<AccountType> Get()
        {

            return db.GetAllAccountTypes();
        }

        // GET: api/AccountTypes/5
        public AccountType Get(int id)
        {
            return db.GetAccountType(id);
        }
    }
}
