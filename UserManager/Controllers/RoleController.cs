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
    public class RoleController : Controller
    { 
        // GET: /Role/
        public ActionResult Index()
        {
            return ModelBind();
        }
        
        // GET Role/GetGrid
        public ActionResult GetGrid()
        {
            var tak = db.Roles.ToArray();
             
            var result = from c in tak select new string[] { c.Id.ToString(), Convert.ToString(c.Id), 
            Convert.ToString(c.RoleName), 
            Convert.ToString(c.IsActive), 
             };
            return Json(new { aaData = result }, JsonRequestBehavior.AllowGet);
        }
        // GET: /Role/ModelBindIndex
        public ActionResult ModelBindIndex()
        {
            return ModelBind();
        }
        // GET: /Role/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Role ObjRole = db.Roles.Find(id);
            if (ObjRole == null)
            {
                return HttpNotFound();
            }
            return View(ObjRole);
        }
        // GET: /Role/Create
        public ActionResult Create()
        {
             
             return View();
        }

        // POST: /Role/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create(Role ObjRole )
        { 
            System.Text.StringBuilder sb = new System.Text.StringBuilder(); 
            try
            {
                if (ModelState.IsValid)
                { 
                    

                    db.Roles.Add(ObjRole);
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
 
            
            return Content(sb.ToString());
             
        }
        // GET: /Role/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Role ObjRole = db.Roles.Find(id);
            if (ObjRole == null)
            {
                return HttpNotFound();
            }
            
            return View(ObjRole);
        }

        // POST: /Role/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit(Role ObjRole )
        { 
            System.Text.StringBuilder sb = new System.Text.StringBuilder(); 
            try
            {
                if (ModelState.IsValid)
                { 
                    

                    db.Entry(ObjRole).State = EntityState.Modified;
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
 
            
            return Content(sb.ToString());

        }
        // GET: /Role/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Role ObjRole = db.Roles.Find(id);
            if (ObjRole == null)
            {
                return HttpNotFound();
            }
            return View(ObjRole);
        }

        // POST: /Role/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        { 
            System.Text.StringBuilder sb = new System.Text.StringBuilder(); 
            try
            {
                  
                    Role ObjRole = db.Roles.Find(id);
                    db.Roles.Remove(ObjRole);
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
       

        // POST: /Role/MultiViewIndex/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult MultiViewIndex(Role ObjRole )
        {  
            System.Text.StringBuilder sb = new System.Text.StringBuilder(); 
            try
            {
                if (ModelState.IsValid)
                { 
                    

                    db.Entry(ObjRole).State = EntityState.Modified;
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
 
           
            
            return Content(sb.ToString());
 
        }
         public ActionResult RoleUserGetGrid(int id=0)
        {
            var tak = db.RoleUsers.Where(i=>i.RoleId==id).ToArray();
             
            var result = from c in tak select new string[] { Convert.ToString(c.Id),Convert.ToString(c.Id),
                Convert.ToString(c.RoleId),
                Convert.ToString(c.User_UserId.UserName), };
            return Json(new { aaData = result }, JsonRequestBehavior.AllowGet);
        }
         
        		
		private ActionResult ModelBind()
        {
            var Roles = db.Roles;
            return View(Roles.ToList());
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

