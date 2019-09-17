using JNPShuttle.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace JNPShuttle
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Application_Error()
        {
            var ex = Server.GetLastError();

            //log the error!           

            using (TripsEntities db = new TripsEntities())
            {

                if (this.Session["UserID"].ToString() != String.Empty)
                {
                    db.InsertErrorLog(ex.Message, this.Session["UserID"].ToString());
                }
                else
                {
                    db.InsertErrorLog(ex.Message, "");
                }
                
            }
        }
    }
}
