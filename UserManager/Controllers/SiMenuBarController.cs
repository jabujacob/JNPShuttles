using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;
using JNPPortal.Models;
using System.Net.Http;

namespace JNPPortal.Controllers
{
    public class SiMenuBarController : Controller
    {
        //
        // GET: /SiMenuBar/ 
        public ActionResult Index()
        {
            ViewBag.bar = GetMenuBarPage();
            return PartialView();
        }


        public MvcHtmlString GetMenuBarPage()
        {
            StringBuilder sb = new StringBuilder();
            
            //get role id and role regarding to role bind this
            var userId = Convert.ToInt32(Env.GetUserInfo("userid"));            

            if (userId > 0)
            {
                sb.Append("<ul class=\"sidebar-menu\">");
                sb.Append("<li class=\"\"> <a href=\"/home\"><i class=\"fa fa-dashboard\"></i>Dashboard</a></li>");                
                sb.Append(GetMenuBar());                
            }
            sb.Append("</ul>");
            
            return MvcHtmlString.Create(sb.ToString());
        }




        public MvcHtmlString GetMenuBar()
        {
            StringBuilder sb = new StringBuilder();

            string x;

            //TripSheets Header
            x = "<li class=\"treeview\"> <a href=\"#\"> <i class=\"fa fa-folder\"></i> <span>Trip Sheets</span> <i class=\"fa fa-angle-left pull-right\"></i>  </a><ul class=\"treeview-menu\">";
            sb.Append(x);

            //Show TripSheets           
            x = "<li class=\"\"> <a href=\"/TripSheets\"><i class=\"fa fa-angle-double-right\"></i>View Trip Sheets</a></li>";
            sb.Append(x);


            //Create TripSheet            
            x = "<li class=\"\"> <a href=\"/TripSheets/AddOrEdit\"><i class=\"fa fa-angle-double-right\"></i>Create a New TripSheet</a></li>";
            sb.Append(x);


            sb.Append("</ul>");

            //Reports Header
            x = "<li class=\"treeview\"> <a href=\"#\"> <i class=\"fa fa-folder\"></i> <span>Reports</span> <i class=\"fa fa-angle-left pull-right\"></i>  </a><ul class=\"treeview-menu\">";
            sb.Append(x);

            //Tripsheets Entry Status
            x = "<li class=\"\"> <a href=\"/Reports/TripSheetEntryStatus\"><i class=\"fa fa-angle-double-right\"></i>Trip Sheets Entry Status</a></li>";
            sb.Append(x);

            //Monthly Driver Analysis
            x = "<li class=\"\"> <a href=\"/Reports/DriverAnalysisMonthlySummary\"><i class=\"fa fa-angle-double-right\"></i>Driver Analysis-Monthly</a></li>";
            sb.Append(x);

            //Daily Driver Analysis
            x = "<li class=\"\"> <a href=\"/Reports/DriverAnalysisSummary\"><i class=\"fa fa-angle-double-right\"></i>Driver Analysis-Daily Summary</a></li>";
            sb.Append(x);

            //Daily Driver Analysis Details
            x = "<li class=\"\"> <a href=\"/Reports/DriverAnalysisDetail\"><i class=\"fa fa-angle-double-right\"></i>Driver Analysis-Daily Details</a></li>";
            sb.Append(x);          

            sb.Append("</ul>");

            //TTL Management Header
            x = "<li class=\"treeview\"> <a href=\"#\"> <i class=\"fa fa-folder\"></i> <span>TTL Management</span> <i class=\"fa fa-angle-left pull-right\"></i>  </a><ul class=\"treeview-menu\">";
            sb.Append(x);

            //TTL Download
            x = "<li class=\"\"> <a href=\"/Tasks\"><i class=\"fa fa-angle-double-right\"></i>Show Tasks</a></li>";
            sb.Append(x);

            sb.Append("</ul>");

            //Accounts Management Header
            x = "<li class=\"treeview\"> <a href=\"#\"> <i class=\"fa fa-folder\"></i> <span>Accounts</span> <i class=\"fa fa-angle-left pull-right\"></i>  </a><ul class=\"treeview-menu\">";
            sb.Append(x);

            //Transactions
            x = "<li class=\"\"> <a href=\"/Transactions\"><i class=\"fa fa-angle-double-right\"></i>Show Transactions</a></li>";
            sb.Append(x);

            //Accounts
            x = "<li class=\"\"> <a href=\"/Accounts\"><i class=\"fa fa-angle-double-right\"></i>Show Accounts</a></li>";
            sb.Append(x);

            sb.Append("</ul>");



            //Organisation
            x = "<li class=\"treeview\"> <a href=\"#\"> <i class=\"fa fa-folder\"></i> <span>Organisation</span> <i class=\"fa fa-angle-left pull-right\"></i>  </a><ul class=\"treeview-menu\">";
            sb.Append(x);

            //User Management
            x = "<li class=\"\"> <a href=\"/User\"><i class=\"fa fa-angle-double-right\"></i>Users</a></li>";
            sb.Append(x);

            //Driver Management
            x = "<li class=\"\"> <a href=\"/Drivers\"><i class=\"fa fa-angle-double-right\"></i>Drivers</a></li>";
            sb.Append(x);


            sb.Append("</ul>");
         
            return MvcHtmlString.Create(sb.ToString());
        }
    }
}
