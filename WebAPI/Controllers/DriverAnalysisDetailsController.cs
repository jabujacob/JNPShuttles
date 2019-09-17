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
    public class DriverAnalysisDetailsController : ApiController
    {

        readonly DataAccess db = new DataAccess();

        // GET: api/DriverAnalysisDetails
        [ResponseType(typeof(IEnumerable<DriverAnalysis>))]
        public IHttpActionResult Get(DateTime startDate,DateTime endDate,int driverId)
        {
            return Ok(db.GetDriverAnalysisDetails(startDate,endDate,driverId));
        }
    }
}
