using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.IO;
using JNPPortal.Models;
using System.Net.Http;

namespace JNPPortal.Controllers
{
    public class UserController : Controller
    {
        // GET: /User/
        public ActionResult Index()
        {
            HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("api/users").Result;
            IEnumerable<User>  users = response.Content.ReadAsAsync<IEnumerable<User>>().Result;

            return View(users.ToList());
        }

        // GET User/GetGrid
        public ActionResult GetGrid()
        {
            HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("api/users").Result;
            IEnumerable<User>  users = response.Content.ReadAsAsync<IEnumerable<User>>().Result;
            
            return Json(new { data = users }, JsonRequestBehavior.AllowGet);
        }

        
        // GET: /User/AddOrEdit
        public ActionResult AddOrEdit(int id = 0)
        {
            if (id == 0)
            {
                ViewBag.Title = "Create new User";
                ViewBag.New = true;
               
                //New user
                User user = new User();

                //Get Driver List
                IEnumerable<Driver> driverList;
                HttpResponseMessage teamListResponse = GlobalVariables.WebApiClient.GetAsync("api/Drivers").Result;
                driverList = teamListResponse.Content.ReadAsAsync<IEnumerable<Driver>>().Result;
                ViewBag.DriverList = new SelectList(driverList, "Id", "DisplayName", "Select One");

                return View(user);
            }

            else
            {                
                //Get User details
                HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("api/Users/" + id.ToString()).Result;
                User user = response.Content.ReadAsAsync<User>().Result;
                
                ViewBag.Title = "User - " + user.FirstName + " " +user.LastName;
                ViewBag.New = false;

                //Get Driver List
                IEnumerable<Driver> driverList;
                HttpResponseMessage teamListResponse = GlobalVariables.WebApiClient.GetAsync("api/Drivers").Result;
                driverList = teamListResponse.Content.ReadAsAsync<IEnumerable<Driver>>().Result;
                ViewBag.DriverList = new SelectList(driverList, "Id", "DisplayName", "Select One");

                return View(user);
            }
        }


        [HttpPost]
        public ActionResult AddOrEdit(User user, string submit)
        {

            //Save changes      
            if (user.Id == 0)
            {
                HttpResponseMessage response = GlobalVariables.WebApiClient.PostAsJsonAsync("api/Users", user).Result;
            }
            else
            {
                HttpResponseMessage response = GlobalVariables.WebApiClient.PutAsJsonAsync("api/Users/" + user.Id, user).Result;
            }

            //Message to display                                
            if (user.Id == 0)
            {
                TempData["SuccessMessage"] = "User created Successfully";
            }
            else
            {
                TempData["SuccessMessage"] = "User updated Successfully";
            }

            return RedirectToAction("Index");
        }


        // GET: /User/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("api/users/" + id).Result;
            User user = response.Content.ReadAsAsync<User>().Result;

            if (user == null)
            {
                return HttpNotFound();
            }

            return View(user);
        }

    }
}