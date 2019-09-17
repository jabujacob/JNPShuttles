using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [RoutePrefix("api/TripSheets")]
    public class TripSheetsController : ApiController
    {
        readonly DataAccess db = new DataAccess();

        //GET: TripSheets

        // GET: api/TripSheets
        [Route("ByUser/{id}")]
        [HttpGet]
        [ResponseType(typeof(IEnumerable<TripSheet>))]
        public IHttpActionResult GetTripSheetsByUser(int id)
        {
            return Ok(db.GetAllTripSheets(id));
        }

        // GET: api/TripSheets/5        
        [ResponseType(typeof(TripSheet))]
        [HttpGet]
        public IHttpActionResult Get(int id)        {
            TripSheet tripSheet = db.GetTripSheet(id);


            if (tripSheet == null)
            {
                return BadRequest();

            }

            return Ok(tripSheet);
        }

        // POST: api/Tripsheets
        [ResponseType(typeof(TripSheet))]
        public void Post(TripSheet tripSheet)
        {

            db.InsertTripSheet(tripSheet);

        }

        // PUT: api/tripsheets        
        public void Put(TripSheet tripSheet)
        {
            db.UpdateTripSheet(tripSheet);
        }

    }
}
