using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using JNPPortal.Models;

namespace JNPPortal.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {

            if (Env.GetUserInfo("name").Length > 0)
            {
               
                ReportsViewModal rvm = new ReportsViewModal();

                rvm.StartDate = Convert.ToDateTime(DateTime.Now.AddDays(-2));
                rvm.EndDate = Convert.ToDateTime(DateTime.Now.AddDays(1));
                rvm.DriverId = -1;

                ViewBag.StartDate = HttpUtility.UrlEncode(String.Format("{0:dd/MMM/yyyy}", rvm.StartDate));
                ViewBag.EndDate = HttpUtility.UrlEncode(String.Format("{0:dd/MMM/yyyy}", rvm.EndDate));
                ViewBag.DriverId = rvm.DriverId;

                HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("api/Tasks/GetLastImportTime").Result;
                rvm.UpdatedLastOnTime = response.Content.ReadAsAsync<DateTime>().Result;


                return View(rvm);
            }
            return RedirectToAction("login", "Account");

        }

               
        [HttpGet]
        public ActionResult HomeGetGrid(DateTime startDate, DateTime endDate)
        {

            HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("api/DriverAnalysisSummary?startDate=" + HttpUtility.UrlEncode(String.Format("{0:dd/MMM/yyyy}", startDate)) + "&endDate=" + HttpUtility.UrlEncode(String.Format("{0:dd/MMM/yyyy}", endDate)) + "&driverId=-1").Result;
            IEnumerable<DriverAnalysis> driverAnalysisSummary = response.Content.ReadAsAsync<IEnumerable<DriverAnalysis>>().Result;

            return Json(new { data = driverAnalysisSummary }, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public ActionResult Index(ReportsViewModal rvm)
        {
            ViewBag.StartDate = HttpUtility.UrlEncode(String.Format("{0:dd/MMM/yyyy}", rvm.StartDate));
            ViewBag.EndDate = HttpUtility.UrlEncode(String.Format("{0:dd/MMM/yyyy}", rvm.EndDate));
            ViewBag.DriverId = -1;

            HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("api/Tasks/ImportTasksFromSuperShuttle").Result;
            rvm.UpdatedLastOnTime = response.Content.ReadAsAsync<DateTime>().Result;

            return View(rvm);
        }

    }
}
