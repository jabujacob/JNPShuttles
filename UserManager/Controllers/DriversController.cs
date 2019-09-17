using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using JNPPortal.Models;

namespace JNPPortal.Controllers
{
    public class DriversController : Controller
    {
        // GET: Drivers
        public ActionResult Index()
        {
            HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("api/drivers").Result;
            IEnumerable<Driver> drivers = response.Content.ReadAsAsync<IEnumerable<Driver>>().Result;

            return View(drivers.ToList());
            
        }

        

       
        // GET: Drivers/AddOrEdit/5
        public ActionResult AddOrEdit(int id=0)
        {

            //Get Van list
            HttpResponseMessage responseVans = GlobalVariables.WebApiClient.GetAsync("api/Vans").Result;
            IEnumerable<Van> vans = responseVans.Content.ReadAsAsync<IEnumerable<Van>>().Result;
            ViewBag.VanList = new SelectList(vans, "Id", "Name", "Select One");

            if (id == 0)
            {
                ViewBag.Title = "Create new Driver";
                ViewBag.New = true;                

                //New user
                Driver driver = new Driver();

                return View(driver);
            }
            else
            {
                //Get Driver details
                HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("api/Drivers/" + id.ToString()).Result;
                Driver driver = response.Content.ReadAsAsync<Driver>().Result;

                ViewBag.Title = "Driver - " + driver.FirstName + " " + driver.LastName;
                ViewBag.New = false;

                return View(driver);
            }          
        }


        [HttpPost]
        // GET: Drivers/AddOrEdit/5
        public ActionResult AddOrEdit(Driver driver)
        {

            if (!ModelState.IsValid)
            {
                return View(driver);

            }

            //Get Van list
            HttpResponseMessage responseVans = GlobalVariables.WebApiClient.GetAsync("api/Vans").Result;
            IEnumerable<Van> vans = responseVans.Content.ReadAsAsync<IEnumerable<Van>>().Result;
            ViewBag.VanList = new SelectList(vans, "Id", "Name", "Select One");


            //Save changes      
            if (driver.ID == 0)
            {
                HttpResponseMessage response = GlobalVariables.WebApiClient.PostAsJsonAsync("api/Drivers", driver).Result;
            }
            else
            {
                HttpResponseMessage response = GlobalVariables.WebApiClient.PutAsJsonAsync("api/Drivers/", driver).Result;
            }

            //Message to display                                
            if (driver.ID == 0)
            {
                TempData["SuccessMessage"] = "Driver created Successfully";
            }
            else
            {
                TempData["SuccessMessage"] = "Driver updated Successfully";
            }

            return View(driver);
        }


        // GET: Drivers/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Drivers/Delete/5
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

        // GET Drivers/GetGrid
        public ActionResult GetGrid()
        {
            HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("api/drivers").Result;
            IEnumerable<Driver> drivers = response.Content.ReadAsAsync<IEnumerable<Driver>>().Result;

            return Json(new { data = drivers }, JsonRequestBehavior.AllowGet);
        }
    }
}
