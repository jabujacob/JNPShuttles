
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using JNPPortal;
using JNPPortal.Models;

namespace JNPPortal.Controllers
{
    public class TripSheetsController : Controller
    {
        // GET: TripSheets
        public ActionResult Index()
        {
            if (@Env.GetUserInfo("userid")!=null)
            {
                HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("api/tripsheets/byuser/" + @Env.GetUserInfo("userid").ToString()).Result;
                IEnumerable<Tripsheet> tripsheets = response.Content.ReadAsAsync<IEnumerable<Tripsheet>>().Result;
                return View(tripsheets.ToList());
            }

            return View();                        
        }

        // GET: TripSheets/AddOrEdit/5
        public ActionResult AddOrEdit(int? id)
        {

            if (@Env.GetUserInfo("userid") != null)

            {

                //Get driver list 
                HttpResponseMessage responseDrivers = GlobalVariables.WebApiClient.GetAsync("api/Drivers").Result;
                IEnumerable<Driver> drivers = responseDrivers.Content.ReadAsAsync<IEnumerable<Driver>>().Result;
                ViewBag.DriverList = new SelectList(drivers, "Id", "DisplayName", "Select One");

                //Get Van list
                HttpResponseMessage responseVans = GlobalVariables.WebApiClient.GetAsync("api/Vans").Result;
                IEnumerable<Van> vans = responseVans.Content.ReadAsAsync<IEnumerable<Van>>().Result;
                ViewBag.VanList = new SelectList(vans, "Id", "Name", "Select One");

                Tripsheet tripsheet;
                if (id > 0)
                {
                    HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("api/TripSheets/" + id.ToString()).Result;
                    tripsheet = response.Content.ReadAsAsync<Tripsheet>().Result;
                }
                else
                {
                    tripsheet = new Tripsheet();
                    tripsheet.StartDate = DateTime.Now;
                }
                              

                return View(tripsheet);

            }
            return View();
        }

        // POST: TripSheets/AddOrEdit/
        [HttpPost]
        public ActionResult AddOrEdit(Tripsheet tripsheet)
        {


            //Get driver list 
            HttpResponseMessage responseDrivers = GlobalVariables.WebApiClient.GetAsync("api/Drivers").Result;
            IEnumerable<Driver> drivers = responseDrivers.Content.ReadAsAsync<IEnumerable<Driver>>().Result;
            ViewBag.DriverList = new SelectList(drivers, "Id", "DisplayName", "Select One");

            //Get Van list
            HttpResponseMessage responseVans = GlobalVariables.WebApiClient.GetAsync("api/Vans").Result;
            IEnumerable<Van> vans = responseVans.Content.ReadAsAsync<IEnumerable<Van>>().Result;
            ViewBag.VanList = new SelectList(vans, "Id", "Name", "Select One");

            if (!ModelState.IsValid)
            {
                return View(tripsheet);
            }

            
            if (tripsheet.ID > 0)
            {
                //Update
                HttpResponseMessage response = GlobalVariables.WebApiClient.PutAsJsonAsync("api/TripSheets/" + tripsheet.ID, tripsheet).Result;

            }
            else
            {
                //Insert
                HttpResponseMessage response = GlobalVariables.WebApiClient.PostAsJsonAsync("api/TripSheets/", tripsheet).Result;
            }

            //Message to display                                
            if (tripsheet.ID == 0)
            {
                ViewBag.Message = "Tripsheet created Successfully";
            }
            else
            {
                ViewBag.Message = "Tripsheet updated Successfully";
            }


            return View(tripsheet);
        }


        // GET TripSheets/GetGrid
        public ActionResult GetGrid()
        {
            HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("api/TripSheets/ByUser/" + @Env.GetUserInfo("userid").ToString()).Result;
            IEnumerable<Tripsheet> tripsheets = response.Content.ReadAsAsync<IEnumerable<Tripsheet>>().Result;

            return Json(new { data = tripsheets }, JsonRequestBehavior.AllowGet);
        }

        // GET: TripSheets/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: TripSheets/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
