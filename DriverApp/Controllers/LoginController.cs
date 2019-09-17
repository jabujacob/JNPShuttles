using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JNPShuttle.Models;
namespace JNPShuttle.Controllers
{
   
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
          // throw new Exception("Its working");
            return View();            
        }

        public ActionResult Authenticate(LoginUser usrAccount)
        {

            
            using (TripsEntities db = new TripsEntities())

            {

                var userDetails = db.Users.Where(x => x.Username == usrAccount.Username && x.Password == usrAccount.Password).FirstOrDefault();
                if (userDetails == null)
                {

                    this.Session["Logon Error Message"] = "Wrong username or password. Please try again.";
                    return View("Index");
                }
                else
                {
                    this.Session["Logon Error Message"] = "";

                    //User details                    
                    this.Session["DriverID"] = userDetails.DriverID;
                    this.Session["UserID"] = userDetails.ID.ToString();
                    this.Session["GreetingName"] = userDetails.FirstName.ToString();
                    this.Session["AdminUser"] = userDetails.Admin.ToString();
                    this.Session["SuperShuttleID"] = userDetails.Driver.SuperShuttleID.ToString();

                    userDetails = null;
                    usrAccount = null;

                    return RedirectToAction("Index", "TripSheets");
                }

            }


        }
    }
}