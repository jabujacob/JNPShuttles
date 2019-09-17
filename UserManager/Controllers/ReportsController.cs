using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using JNPPortal.Models;

namespace JNPPortal.Controllers
{

 
    public class ReportsController : Controller
    {
        // GET: Reports

        [HttpGet]
        public ActionResult DriverAnalysisSummary(DateTime? startDate, DateTime? endDate, int? driverId)
        {
            if (startDate == null)
            {
                DateTime now = DateTime.Now;
                startDate = new DateTime(now.Year, now.Month, 1);
                endDate = Convert.ToDateTime(startDate).AddMonths(1).AddDays(-1);
                driverId = 9;
            }

         
            ReportsViewModal rvm = new ReportsViewModal();

            rvm.StartDate = Convert.ToDateTime(startDate);
            rvm.EndDate = Convert.ToDateTime(endDate);
            rvm.DriverId = Convert.ToInt32(driverId);
           


            ViewBag.StartDate = HttpUtility.UrlEncode(String.Format("{0:dd/MMM/yyyy}", startDate));
            ViewBag.EndDate = HttpUtility.UrlEncode(String.Format("{0:dd/MMM/yyyy}", endDate));
            ViewBag.DriverId = driverId;


            //Get Driver List
            IEnumerable<Driver> driverList;
            HttpResponseMessage teamListResponse = GlobalVariables.WebApiClient.GetAsync("api/Drivers").Result;
            driverList = teamListResponse.Content.ReadAsAsync<IEnumerable<Driver>>().Result;
            ViewBag.DriverList = new SelectList(driverList, "Id", "DisplayName", "Select One");


            return View(rvm);

        }
        [HttpGet]
        public ActionResult DriverAnalysisSummaryGetGrid(DateTime startDate, DateTime endDate, int driverId)
        {

            HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("api/DriverAnalysisSummary?startDate=" + HttpUtility.UrlEncode(String.Format("{0:dd/MMM/yyyy}", startDate)) + "&endDate=" + HttpUtility.UrlEncode(String.Format("{0:dd/MMM/yyyy}", endDate)) + "&driverId=" + driverId.ToString()).Result;
            IEnumerable<DriverAnalysis> driverAnalysisSummary = response.Content.ReadAsAsync<IEnumerable<DriverAnalysis>>().Result;

            return Json(new { data = driverAnalysisSummary }, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public ActionResult DriverAnalysisSummary(ReportsViewModal rvm)
        {


            ViewBag.StartDate = HttpUtility.UrlEncode(String.Format("{0:dd/MMM/yyyy}", rvm.StartDate));
            ViewBag.EndDate = HttpUtility.UrlEncode(String.Format("{0:dd/MMM/yyyy}", rvm.EndDate));
            ViewBag.DriverId = rvm.DriverId;


            //Get Driver List
            IEnumerable<Driver> driverList;
            HttpResponseMessage teamListResponse = GlobalVariables.WebApiClient.GetAsync("api/Drivers").Result;
            driverList = teamListResponse.Content.ReadAsAsync<IEnumerable<Driver>>().Result;
            ViewBag.DriverList = new SelectList(driverList, "Id", "DisplayName", "Select One");

            return View(rvm);
        }


        [HttpGet]
        public ActionResult DriverAnalysisDetail(DateTime? startDate, DateTime? endDate, int? driverId)
        {
            if (startDate == null)
            {
                DateTime now = DateTime.Now;
                startDate = new DateTime(now.Year, now.Month, 1);
                endDate = Convert.ToDateTime(startDate).AddMonths(1).AddDays(-1);
                driverId = 9;
            }


            ReportsViewModal davm = new ReportsViewModal();

            davm.StartDate = Convert.ToDateTime(startDate);
            davm.EndDate = Convert.ToDateTime(endDate);
            davm.DriverId = Convert.ToInt32(driverId);



            ViewBag.StartDate = HttpUtility.UrlEncode(String.Format("{0:dd/MMM/yyyy}", startDate));
            ViewBag.EndDate = HttpUtility.UrlEncode(String.Format("{0:dd/MMM/yyyy}", endDate));
            ViewBag.DriverId = driverId;


            //Get Driver List
            IEnumerable<Driver> driverList;
            HttpResponseMessage teamListResponse = GlobalVariables.WebApiClient.GetAsync("api/Drivers").Result;
            driverList = teamListResponse.Content.ReadAsAsync<IEnumerable<Driver>>().Result;
            ViewBag.DriverList = new SelectList(driverList, "Id", "DisplayName", "Select One");


            return View(davm);

        }

        [HttpGet]
        public ActionResult DriverAnalysisDetailGetGrid(DateTime startDate, DateTime endDate, int driverId)
        {

            HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("api/DriverAnalysisDetails?startDate=" + HttpUtility.UrlEncode(String.Format("{0:dd/MMM/yyyy}", startDate)) + "&endDate=" + HttpUtility.UrlEncode(String.Format("{0:dd/MMM/yyyy}", endDate)) + "&driverId=" + driverId.ToString()).Result;
            IEnumerable<DriverAnalysis> driverAnalysisSummary = response.Content.ReadAsAsync<IEnumerable<DriverAnalysis>>().Result;

            return Json(new { data = driverAnalysisSummary }, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public ActionResult DriverAnalysisDetail(ReportsViewModal rvm)
        {


            ViewBag.StartDate = HttpUtility.UrlEncode(String.Format("{0:dd/MMM/yyyy}", rvm.StartDate));
            ViewBag.EndDate = HttpUtility.UrlEncode(String.Format("{0:dd/MMM/yyyy}", rvm.EndDate));
            ViewBag.DriverId = rvm.DriverId;


            //Get Driver List
            IEnumerable<Driver> driverList;
            HttpResponseMessage teamListResponse = GlobalVariables.WebApiClient.GetAsync("api/Drivers").Result;
            driverList = teamListResponse.Content.ReadAsAsync<IEnumerable<Driver>>().Result;
            ViewBag.DriverList = new SelectList(driverList, "Id", "DisplayName", "Select One");

            return View(rvm);
        }


        [HttpGet]
        public ActionResult TripSheetEntryStatus()
        {
            if (@Env.GetUserInfo("userid") != null)
            {
                ReportsViewModal rvm = new ReportsViewModal();

                HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("api/Tasks/GetLastImportTime").Result;
                rvm.UpdatedLastOnTime = response.Content.ReadAsAsync<DateTime>().Result;
                return View(rvm);
            }

            return View();

        }

        [HttpPost]
        public ActionResult TripSheetEntryStatus(ReportsViewModal rvm)
        {
            HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("api/Tasks/ImportTasksFromSuperShuttle").Result;
            rvm.UpdatedLastOnTime = response.Content.ReadAsAsync<DateTime>().Result;

            return View(rvm);

        }

        [HttpGet]
       
        public ActionResult TripSheetEntryStatusGetGrid()
        {

            HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("api/TripSheetEntryStatus").Result;
            IEnumerable<Tripsheet> tripsheets = response.Content.ReadAsAsync<IEnumerable<Tripsheet>>().Result;

            return Json(new { data = tripsheets }, JsonRequestBehavior.AllowGet);

        }

        public ActionResult TripSheetEntryMultipleDriverPerVanGetGrid()
        {

            HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("api/TripSheetEntryStatus/GetMultipleDriversPerVanList").Result;
            IEnumerable<Tripsheet> tripsheets = response.Content.ReadAsAsync<IEnumerable<Tripsheet>>().Result;

            return Json(new { data = tripsheets }, JsonRequestBehavior.AllowGet);

        }



    }
}
