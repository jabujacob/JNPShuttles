using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    public class DriversController : ApiController

    {
        readonly DataAccess db = new DataAccess();

        // GET: api/Drivers
        public IEnumerable<Driver> Get()
        {

            return db.GetAllDrivers();
        }

        // GET: api/Drivers/5
        public Driver Get(int id)
        {
            return db.GetDriver(id);
        }

        // POST: api/Drivers
        public void Post(Driver driver)
        {
            db.InsertDriver(driver);
        }

        // PUT: api/Drivers
        public void Put(Driver driver)
        {
            db.UpdateDriver(driver);
        }

        // DELETE: api/Drivers/5
        public void Delete(int id)
        {
        }
    }
}
