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
    public class DriverAnalysisSummaryController : ApiController
    {
        
        readonly DataAccess db = new DataAccess();

        // GET: api/DriverAnalysisSummary
        [ResponseType(typeof(IEnumerable<DriverAnalysis>))]
        public IHttpActionResult Get(int driverId,DateTime startDate, DateTime? endDate)
        {
            return Ok(db.GetDriverAnalysisSummary(startDate, Convert.ToDateTime(endDate), driverId));
        }

    }
}
