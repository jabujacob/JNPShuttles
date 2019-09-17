using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    public class VansController : ApiController
    {
        readonly DataAccess db = new DataAccess();

        // GET: api/Vans
        public IEnumerable<Van> Get()
        {

            return db.GetAllVans();
        }

        // GET: api/Vans/5
        public Van Get(int id)
        {
            return db.GetVan(id);
        }
        
    }
}
