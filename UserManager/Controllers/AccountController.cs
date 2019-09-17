using JNPPortal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security;
using Microsoft.AspNet.Identity;
using System.Threading;
using System.Net.Http;
using System.Web.Http.ModelBinding;

namespace JNPPortal.Controllers
{
    public class AccountController : Controller
    {
        
        // GET: /Account/
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        //[RequireHttps]
        public ActionResult Login(System.Web.Mvc.FormCollection frmCollection, string returnUrl)
        {
            string username = frmCollection["username"].ToString();
            string password = frmCollection["password"].ToString();

            //Validate User
            String uri = "api/users/?username=" + username +"&password=" + password ;
            HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync(uri).Result;
            User login = response.Content.ReadAsAsync<User>().Result;


            if (login != null && ModelState.IsValid)
            {
                var claims = new List<Claim>();
                claims.Add(new Claim(ClaimTypes.Name, login.Username.ToString())); // store email of user                
                claims.Add(new Claim(ClaimTypes.Sid, login.Id.ToString())); // store id of user

                var identity = new ClaimsIdentity(claims, DefaultAuthenticationTypes.ApplicationCookie);
                var authenticationManager = Request.GetOwinContext().Authentication;
                authenticationManager.SignIn(identity);

                var claimsPrincipal = new ClaimsPrincipal(identity);
                Thread.CurrentPrincipal = claimsPrincipal;

                if (returnUrl != null) {                    
                    return Redirect("~" + returnUrl);                }
                else
                {
                    return Redirect("~/Home");
                }
                
            }
            else
            {
                ViewBag.Msg = "!Invalid UserName and Password";
            }

            return View(frmCollection);
        }

        public ActionResult Register()
        {
            return View();
        }

        
        public ActionResult Signout()
        {
            AuthenticationManager.SignOut();
            HttpCookie c = new HttpCookie(".AspNet.ApplicationCookie")
            {
                Expires = DateTime.Now.AddDays(-1)
            };
            Response.Cookies.Add(c);

            HttpCookie d = new HttpCookie("__RequestVerificationToken")
            {
                Expires = DateTime.Now.AddDays(-1)
            };
            Response.Cookies.Add(d);

            return RedirectToAction("login", "Account");
        }
        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }
    }
}

