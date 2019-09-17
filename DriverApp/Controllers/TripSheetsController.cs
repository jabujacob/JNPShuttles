using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using JNPShuttle.Models;

namespace JNPShuttle.Controllers
{
    [HandleError(ExceptionType = typeof(Exception), View = "Error")]
    public class TripSheetsController : Controller
    {
        private TripsEntities db = new TripsEntities();

        //GET: TripSheets
        
        public ActionResult Index()
        {                        
            if (Session["DriverID"].ToString() != String.Empty)
            {
                if (Session["AdminUser"].ToString() == "True")
                {
                    var tripSheets = db.GetTripSheetsByUserID(Convert.ToInt32(this.Session["UserID"].ToString()));
                    return View(tripSheets.ToList());
                }
                else
                {
                    var tripSheets = db.GetTripSheetsByUserID(Convert.ToInt32(this.Session["UserID"].ToString()));
                    return View(tripSheets.ToList());
                }
                
            }
            else
            {
                return RedirectToAction("Index","Logon");
            }            
        }



        // GET: TripSheets/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TripSheet tripSheet = db.TripSheets.Find(id);
            if (tripSheet == null)
            {
                return HttpNotFound();
            }
            return View(tripSheet);
        }

        // GET: TripSheets/Create
        public ActionResult Create()
        {
           
            TripSheet tripSheet = new TripSheet();

            tripSheet.DriverID = Int32.Parse(Session["DriverID"].ToString());
            tripSheet.VanID = Convert.ToInt32(db.Drivers.Find(tripSheet.DriverID).DefaultVanId);
            
            ViewBag.DriverID = new SelectList(db.Drivers.OrderBy(db => db.FirstName), "ID", "FirstName",tripSheet.DriverID);
            ViewBag.VanID = new SelectList(db.Vans.OrderBy(db => db.Name), "id", "Name", tripSheet.VanID);
                                 
            return View(tripSheet);
        }

        // POST: TripSheets/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Date,DriverID,VanID,StartKM,EndKM,StartTime,EndTime,Duration,TaxiChit,Swipes,CCEftpos,Trips,PrePaid,Cash")] TripSheet tripSheet)
        {

            try
            {
                //Validate the entry
                var validationResult = db.ValidateTripSheetEntry(tripSheet.StartTime, tripSheet.EndTime, tripSheet.VanID);

                var results = validationResult.ToList();
                            
                
                if (results[0] == "PASS")
                {
                    ViewBag.Message = "";
                    if (ModelState.IsValid)
                    {
                        tripSheet.DriverID = Int32.Parse(Session["DriverID"].ToString());                        
                        db.TripSheets.Add(tripSheet);                        
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }


                }

                ViewBag.DriverID = new SelectList(db.Drivers.OrderBy(db=>db.FirstName), "ID", "FirstName", tripSheet.DriverID);
                ViewBag.VanID = new SelectList(db.Vans.OrderBy(db=>db.Name), "id", "Name", tripSheet.VanID);
                ViewBag.Message = results[0];
                return View(tripSheet);

            }
            catch(Exception ex)
            {

                db.InsertErrorLog(ex.Message, this.Session["UserID"].ToString());

                ViewBag.DriverID = new SelectList(db.Drivers.OrderBy(db => db.FirstName), "ID", "FirstName", tripSheet.DriverID);
                ViewBag.VanID = new SelectList(db.Vans.OrderBy(db => db.Name), "id", "Name", tripSheet.VanID);
                //ViewBag.Message = "Unable to createa a new record. Trip summary for " + tripSheet..ToString("dd/MMM/yyyy") + " already exist.";
                return View(tripSheet);
            }                      
        }

        // GET: TripSheets/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TripSheet tripSheet = db.TripSheets.Find(id);
            if (tripSheet == null)
            {
                return HttpNotFound();
            }
            ViewBag.DriverID = new SelectList(db.Drivers.OrderBy(db => db.FirstName), "ID", "FirstName", tripSheet.DriverID);
            ViewBag.VanID = new SelectList(db.Vans.OrderBy(db => db.Name), "id", "Name", tripSheet.VanID);
            return View(tripSheet);
        }

        // POST: TripSheets/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Date,DriverID,VanID,StartKM,EndKM,StartTime,EndTime,Duration,TaxiChit,Swipes,CCEftpos,Trips,PrePaid,Cash")] TripSheet tripSheet)
        {    
            
            try
            {

                //Validate the entry
                var validationResult = db.ValidateTripSheetEntryBeforeSave(tripSheet.StartTime, tripSheet.EndTime, tripSheet.VanID,tripSheet.ID,tripSheet.Gross,tripSheet.StartKM,tripSheet.EndKM);
                var results = validationResult.ToList();

                if (results[0] == "PASS")
                {
                    ViewBag.Message = "";
                    if (ModelState.IsValid)
                    {
                        db.Entry(tripSheet).State = EntityState.Modified;
                        tripSheet.DriverID = Int32.Parse(Session["DriverID"].ToString());
                        db.SaveChanges();
                        return RedirectToAction("Index");

                    }
                }

                ViewBag.DriverID = new SelectList(db.Drivers.OrderBy(db => db.FirstName), "ID", "FirstName", tripSheet.DriverID);
                ViewBag.VanID = new SelectList(db.Vans.OrderBy(db => db.Name), "id", "Name", tripSheet.VanID);
                ViewBag.Message = results[0];
                return View(tripSheet);

            }
            catch (Exception ex)
            {

                db.InsertErrorLog(ex.Message, this.Session["UserID"].ToString());

                ViewBag.DriverID = new SelectList(db.Drivers.OrderBy(db => db.FirstName), "ID", "FirstName", tripSheet.DriverID);
                ViewBag.VanID = new SelectList(db.Vans.OrderBy(db => db.Name), "id", "Name", tripSheet.VanID);
                ViewBag.Message = "Unable to save changes. " + ex.Message.ToString() ;
                return View(tripSheet);
            }




        }

        // GET: TripSheets/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TripSheet tripSheet = db.TripSheets.Find(id);
            if (tripSheet == null)
            {
                return HttpNotFound();
            }
            return View(tripSheet);
        }

        // POST: TripSheets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TripSheet tripSheet = db.TripSheets.Find(id);
            db.TripSheets.Remove(tripSheet);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

    }
}
