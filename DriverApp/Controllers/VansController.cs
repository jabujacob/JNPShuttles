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
    public class VansController : Controller
    {
        private TripsEntities db = new TripsEntities();

        // GET: Vans
        public ActionResult Index()
        {
            return View(db.Vans.ToList());
        }

        // GET: Vans/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Van van = db.Vans.Find(id);
            if (van == null)
            {
                return HttpNotFound();
            }
            return View(van);
        }

        // GET: Vans/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Vans/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,Name")] Van van)
        {
            if (ModelState.IsValid)
            {
                db.Vans.Add(van);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(van);
        }

        // GET: Vans/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Van van = db.Vans.Find(id);
            if (van == null)
            {
                return HttpNotFound();
            }
            return View(van);
        }

        // POST: Vans/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,Name")] Van van)
        {
            if (ModelState.IsValid)
            {
                db.Entry(van).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(van);
        }

        // GET: Vans/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Van van = db.Vans.Find(id);
            if (van == null)
            {
                return HttpNotFound();
            }
            return View(van);
        }

        // POST: Vans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Van van = db.Vans.Find(id);
            db.Vans.Remove(van);
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
