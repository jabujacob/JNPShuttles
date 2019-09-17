using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [RoutePrefix("api/TripSheetEntryStatus")]
    public class TripSheetEntryStatusController : ApiController
    {
        readonly DataAccess db = new DataAccess();

        // GET: api/TripSheetEntryStatus
        [ResponseType(typeof(IEnumerable<TripSheet>))]
        public IHttpActionResult Get()
        {
            return Ok(db.GetTripSheetEntryStatus());
        }


        // GET: api/TripSheetEntryStatus/GetMultipleDriversPerVanList
        [Route("GetMultipleDriversPerVanList/")]
        [HttpGet]
        [ResponseType(typeof(IEnumerable<TripSheet>))]
        public IHttpActionResult GetMultipleDriversPerVanList()
        {
            return Ok(db.GetTripSheetMultipleDrivers());
        }
    }



}
