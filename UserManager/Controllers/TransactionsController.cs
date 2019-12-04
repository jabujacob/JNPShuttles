
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
    public class TransactionsController : Controller
    {

        // GET: /Transactions/
        public ActionResult Index()
        {
            HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("api/transactions").Result;
            IEnumerable<Transaction> transactions = response.Content.ReadAsAsync<IEnumerable<Transaction>>().Result;

            return View(transactions.ToList());
        }

        // GET Transactions/GetGrid
        public ActionResult GetGrid()
        {
            HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("api/Transactions").Result;
            IEnumerable<Transaction> transactions = response.Content.ReadAsAsync<IEnumerable<Transaction>>().Result;

            return Json(new { data = transactions }, JsonRequestBehavior.AllowGet);
        }

        // GET: /Transaction/AddOrEdit
        public ActionResult AddOrEdit(int id = 0)
        {

            Transaction transaction = new Transaction();

            if (id == 0)
            {
                ViewBag.Title = "Create new Transaction";
                ViewBag.New = true;
                ViewBag.Multiple = false;
                transaction.Date = DateTime.Today;

            }
            else if(id == -1){
                ViewBag.Title = "Create new Transaction for multiple vans";
                ViewBag.New = true;
                ViewBag.Multiple = true;
                transaction.Date = DateTime.Today;
            }

            else
            {
                //Get Transaction details
                HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("api/Transactions/" + id.ToString()).Result;
                transaction = response.Content.ReadAsAsync<Transaction>().Result;

                ViewBag.Title = "Transaction - " + id.ToString();
                ViewBag.New = false;
                ViewBag.Multiple = false;
            }

            //Get Van List
            IEnumerable<Van> vanList;
            HttpResponseMessage teamListResponse = GlobalVariables.WebApiClient.GetAsync("api/Vans").Result;
            vanList = teamListResponse.Content.ReadAsAsync<IEnumerable<Van>>().Result;
            ViewBag.VanList = new SelectList(vanList, "Id", "Name", "Select One");

            //Get Accounts List
            IEnumerable<Account> accountsList;
            HttpResponseMessage accountsListResponse = GlobalVariables.WebApiClient.GetAsync("api/Accounts").Result;
            accountsList = accountsListResponse.Content.ReadAsAsync<IEnumerable<Account>>().Result;
            ViewBag.AccountsList = new SelectList(accountsList, "Id", "AccountName", "Select One");

            return View(transaction);
        }


        [HttpPost]
        public ActionResult AddOrEdit(Transaction transaction, string submit)
        {

            //Save changes      
            if (transaction.Id == -1) 
            {
                if (transaction.SelectedVansId != null)
                {

                    transaction.VansId = string.Join(",", transaction.SelectedVansId);
                }
                else
                {
                    transaction.VansId = "";
                }


                HttpResponseMessage response = GlobalVariables.WebApiClient.PostAsJsonAsync("api/Transactions", transaction).Result;
            }
            else if(transaction.Id == 0){
                HttpResponseMessage response = GlobalVariables.WebApiClient.PostAsJsonAsync("api/Transactions", transaction).Result;
            }
            else
            {
                HttpResponseMessage response = GlobalVariables.WebApiClient.PutAsJsonAsync("api/Transactions/" + transaction.Id, transaction).Result;
            }

            //Message to display                                
            if (transaction.Id == 0)
            {
                TempData["SuccessMessage"] = "Transaction created Successfully";
            }
            else
            {
                TempData["SuccessMessage"] = "Transaction updated Successfully";
            }

            return RedirectToAction("Index");
        }

    }
}
