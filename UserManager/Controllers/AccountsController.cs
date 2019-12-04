using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using JNPPortal.Models;

namespace JNPPortal.Controllers
{
    public class AccountsController : Controller
    {
        // GET: Accounts
        public ActionResult Index()
        {
            HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("api/accounts").Result;
            IEnumerable<Account> accounts = response.Content.ReadAsAsync<IEnumerable<Account>>().Result;

            return View(accounts.ToList());
        }

        // GET Accounts/GetGrid
        public ActionResult GetGrid()
        {
            HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("api/accounts").Result;
            IEnumerable<Account> accounts = response.Content.ReadAsAsync<IEnumerable<Account>>().Result;

            return Json(new { data = accounts }, JsonRequestBehavior.AllowGet);
        }

        public decimal GST(int id = 0)
        {
            Account account = new Account();
            //Get Account details
            HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("api/accounts/" + id.ToString()).Result;
            account = response.Content.ReadAsAsync<Account>().Result;

            return account.GST;
        }

        // GET: /Accounts/AddOrEdit

        public ActionResult AddOrEdit(int id = 0)
        {

            Account account  = new Account();

            if (id == 0)
            {
                ViewBag.Title = "Create new Account";
                ViewBag.New = true;
                account.AccountTypeId = 1;
                account.GST = Convert.ToDecimal(0.15);
            }          

            else
            {
                //Get Account details
                HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("api/accounts/" + id.ToString()).Result;
                account = response.Content.ReadAsAsync<Account>().Result;

                ViewBag.Title = "Account - " + id.ToString();
                ViewBag.New = false;

                
            }

            //Get Account Types List
            IEnumerable<AccountType> accountTypesList;
            HttpResponseMessage accountsListResponse = GlobalVariables.WebApiClient.GetAsync("api/AccountTypes").Result;
            accountTypesList = accountsListResponse.Content.ReadAsAsync<IEnumerable<AccountType>>().Result;
            ViewBag.AccountTypesList = new SelectList(accountTypesList, "Id", "Value", "Select One");

            return View(account);
        }

        [HttpPost]
        public ActionResult AddOrEdit(Account account, string submit)
        {

            //Save changes       
            if (account.Id == 0)
            {
                HttpResponseMessage response = GlobalVariables.WebApiClient.PostAsJsonAsync("api/accounts", account).Result;
            }
            else
            {
                HttpResponseMessage response = GlobalVariables.WebApiClient.PutAsJsonAsync("api/accounts/" + account.Id, account).Result;
            }

            //Message to display                                
            if (account.Id == 0)
            {
                TempData["SuccessMessage"] = "Account created Successfully";
            }
            else
            {
                TempData["SuccessMessage"] = "Account updated Successfully";
            }

            return RedirectToAction("Index");
        }


        // GET: Accounts/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Accounts/Delete/5
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
