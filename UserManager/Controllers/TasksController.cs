using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using JNPPortal.Models;

namespace JNPPortal.Controllers
{
    public class TasksController : Controller
    {
        // GET: Tasks
        public ActionResult Index()
        {
            ReportsViewModal rvm = new ReportsViewModal();

            rvm.EndDate = DateTime.Now;
            rvm.StartDate = Convert.ToDateTime(rvm.EndDate).AddDays(-15);

            HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("api/Tasks/GetLastImportTime").Result;
            rvm.UpdatedLastOnTime = response.Content.ReadAsAsync<DateTime>().Result;

            ViewBag.StartDate = HttpUtility.UrlEncode(String.Format("{0:dd/MMM/yyyy}", rvm.StartDate));
            ViewBag.EndDate = HttpUtility.UrlEncode(String.Format("{0:dd/MMM/yyyy}", rvm.EndDate));

            return View(rvm);
        }
        [HttpPost]
        public ActionResult Index(ReportsViewModal rvm,string submit)
        {
            ViewBag.StartDate = HttpUtility.UrlEncode(String.Format("{0:dd/MMM/yyyy}", rvm.StartDate));
            ViewBag.EndDate = HttpUtility.UrlEncode(String.Format("{0:dd/MMM/yyyy}", rvm.EndDate));

            switch(submit)
            {
                case "Import from Super Shuttle":
                    HttpResponseMessage response1 = GlobalVariables.WebApiClient.GetAsync("api/Tasks/ImportTasksFromSuperShuttle").Result;
                    rvm.UpdatedLastOnTime = response1.Content.ReadAsAsync<DateTime>().Result;
                    break;
                case "View TTL Tasks":
                    HttpResponseMessage response2 = GlobalVariables.WebApiClient.GetAsync("api/Tasks/GetLastImportTime").Result;
                    rvm.UpdatedLastOnTime = response2.Content.ReadAsAsync<DateTime>().Result;
                    break;
            }
            
            return View(rvm);
        }

        // GET: Tasks/Details/5
        public ActionResult Details()
        {

            

        
            return View();
        }

        [HttpGet]
        public ActionResult TasksGetGrid(DateTime startDate, DateTime endDate)
        {
            HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("api/tasks?startDate=" + HttpUtility.UrlEncode(String.Format("{0:dd/MMM/yyyy}", startDate)) + "&endDate=" + HttpUtility.UrlEncode(String.Format("{0:dd/MMM/yyyy}", endDate))).Result;
            IEnumerable<Task> tasks = response.Content.ReadAsAsync<IEnumerable<Task>>().Result;

            return Json(new { data = tasks }, JsonRequestBehavior.AllowGet);
        }



    }
}
