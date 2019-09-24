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
    public class DriverAnalysisMonthlySummaryController : ApiController
    {

        readonly DataAccess db = new DataAccess();

        // GET: api/DriverAnalysisMonthlySummary
        [ResponseType(typeof(IEnumerable<DriverAnalysis>))]
        public IHttpActionResult Get(DateTime startDate, DateTime? endDate)
        {
            return Ok(db.GetDriverMonthlyAnalysisSummary(startDate, Convert.ToDateTime(endDate)));
        }
    }
}
