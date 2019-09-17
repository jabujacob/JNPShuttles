using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.IO;
using UserManager.Models;

namespace UserManager.Controllers
{
    public class RoleUserController : Controller
    { 
        // GET: /RoleUser/
        public ActionResult Index()
        {
            return ModelBind();
        }
        
        // GET RoleUser/GetGrid
        public ActionResult GetGrid()
        {
            var tak = db.RoleUsers.ToArray();
             
            var result = from c in tak select new string[] { c.Id.ToString(), Convert.ToString(c.Id), 
            Convert.ToString(c.Role_RoleId.RoleName), 
            Convert.ToString(c.User_UserId.UserName), 
             };
            return Json(new { aaData = result }, JsonRequestBehavior.AllowGet);
        }
        // GET: /RoleUser/ModelBindIndex
        public ActionResult ModelBindIndex()
        {
            return ModelBind();
        }
        // GET: /RoleUser/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RoleUser ObjRoleUser = db.RoleUsers.Find(id);
            if (ObjRoleUser == null)
            {
                return HttpNotFound();
            }
            return View(ObjRoleUser);
        }
        // GET: /RoleUser/Create
        public ActionResult Create()
        {
             ViewBag.RoleId = new SelectList(db.Roles, "Id", "RoleName");
ViewBag.UserId = new SelectList(db.Users, "Id", "UserName");

             return View();
        }

        // POST: /RoleUser/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create(RoleUser ObjRoleUser )
        { 
            System.Text.StringBuilder sb = new System.Text.StringBuilder(); 
            try
            {
                if (ModelState.IsValid)
                { 
                    

                    db.RoleUsers.Add(ObjRoleUser);
                    db.SaveChanges();

                    sb.Append("Sumitted");
                    return Content(sb.ToString());
                }
                else
                {
                    foreach (var key in this.ViewData.ModelState.Keys)
                    {
                        foreach (var err in this.ViewData.ModelState[key].Errors)
                        {
                            sb.Append(err.ErrorMessage + "<br/>");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                sb.Append("Error :" + ex.Message);
            }
 
            ViewBag.RoleId = new SelectList(db.Roles, "Id", "RoleName", ObjRoleUser.RoleId);
ViewBag.UserId = new SelectList(db.Users, "Id", "UserName", ObjRoleUser.UserId);

            return Content(sb.ToString());
             
        }
        // GET: /RoleUser/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RoleUser ObjRoleUser = db.RoleUsers.Find(id);
            if (ObjRoleUser == null)
            {
                return HttpNotFound();
            }
            ViewBag.RoleId = new SelectList(db.Roles, "Id", "RoleName", ObjRoleUser.RoleId);
ViewBag.UserId = new SelectList(db.Users, "Id", "UserName", ObjRoleUser.UserId);

            return View(ObjRoleUser);
        }

        // POST: /RoleUser/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit(RoleUser ObjRoleUser )
        { 
            System.Text.StringBuilder sb = new System.Text.StringBuilder(); 
            try
            {
                if (ModelState.IsValid)
                { 
                    

                    db.Entry(ObjRoleUser).State = EntityState.Modified;
                    db.SaveChanges();

                    sb.Append("Sumitted");
                    return Content(sb.ToString());
                }
                else
                {
                    foreach (var key in this.ViewData.ModelState.Keys)
                    {
                        foreach (var err in this.ViewData.ModelState[key].Errors)
                        {
                            sb.Append(err.ErrorMessage + "<br/>");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                sb.Append("Error :" + ex.Message);
            }
 
            ViewBag.RoleId = new SelectList(db.Roles, "Id", "RoleName", ObjRoleUser.RoleId);
ViewBag.UserId = new SelectList(db.Users, "Id", "UserName", ObjRoleUser.UserId);

            return Content(sb.ToString());

        }
        // GET: /RoleUser/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RoleUser ObjRoleUser = db.RoleUsers.Find(id);
            if (ObjRoleUser == null)
            {
                return HttpNotFound();
            }
            return View(ObjRoleUser);
        }

        // POST: /RoleUser/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        { 
            System.Text.StringBuilder sb = new System.Text.StringBuilder(); 
            try
            {
                  
                    RoleUser ObjRoleUser = db.RoleUsers.Find(id);
                    db.RoleUsers.Remove(ObjRoleUser);
                    db.SaveChanges();

                    sb.Append("Sumitted");
                    return Content(sb.ToString());
                 
            }
            catch (Exception ex)
            {
                sb.Append("Error :" + ex.Message);
            }
  
            return Content(sb.ToString());
  
        }
        // GET: /RoleUser/MultiViewIndex/5
        public ActionResult MultiViewIndex(int? id)
        { 
            RoleUser ObjRoleUser = db.RoleUsers.Find(id);
            ViewBag.IsWorking = 0;
            if (id > 0)
            {
                ViewBag.IsWorking = id;
                ViewBag.RoleId = new SelectList(db.Roles, "Id", "RoleName", ObjRoleUser.RoleId);
ViewBag.UserId = new SelectList(db.Users, "Id", "UserName", ObjRoleUser.UserId);

            }
            
            return View(ObjRoleUser);
        }

        // POST: /RoleUser/MultiViewIndex/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult MultiViewIndex(RoleUser ObjRoleUser )
        {  
            System.Text.StringBuilder sb = new System.Text.StringBuilder(); 
            try
            {
                if (ModelState.IsValid)
                { 
                    

                    db.Entry(ObjRoleUser).State = EntityState.Modified;
                    db.SaveChanges();

                    sb.Append("Sumitted");
                    return Content(sb.ToString());
                }
                else
                {
                    foreach (var key in this.ViewData.ModelState.Keys)
                    {
                        foreach (var err in this.ViewData.ModelState[key].Errors)
                        {
                            sb.Append(err.ErrorMessage + "<br/>");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                sb.Append("Error :" + ex.Message);
            }
 
           
            ViewBag.RoleId = new SelectList(db.Roles, "Id", "RoleName", ObjRoleUser.RoleId);
ViewBag.UserId = new SelectList(db.Users, "Id", "UserName", ObjRoleUser.UserId);

            return Content(sb.ToString());
 
        }

        private SIContext db = new SIContext();
		
		private ActionResult ModelBind()
        {
            var RoleUsers = db.RoleUsers;
            return View(RoleUsers.ToList());
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

