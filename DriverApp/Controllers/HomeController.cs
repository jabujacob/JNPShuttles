using JNPShuttle.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JNPShuttle.Controllers
{
    public class HomeController : Controller
    {
        private TripsEntities db = new TripsEntities();

        public ActionResult Index()
        {            
            return View();
        }

        public ActionResult Dashboard()
        {
            if (this.Session["AdminUser"].ToString() == "True")
            {
                return View(db.Get_Driver_Dashboard("-1"));
            }
            else
            {
                return View(db.Get_Driver_Dashboard(this.Session["SuperShuttleID"].ToString()));
            }
            
        }        
    }
}