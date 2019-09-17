using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using JNPShuttle.Models;
using Newtonsoft.Json;



namespace JNPShuttle.Controllers
{
    public class TasksController : Controller
    {
        private TripsEntities db = new TripsEntities();
        private TripsEntities db1 = new TripsEntities();
        private TripsEntities db2 = new TripsEntities();
        private TripsEntities db3 = new TripsEntities();

        // GET: Tasks
        public ActionResult Index()
        {
                        
            return View();
        }
        // POST: Task Index


       [HttpPost]
        public ActionResult Index(bool isPost = true)
        {

            try
            {
                ImportTasksFromSuperShuttle();
            }
            catch(Exception ex)
            {
                db.InsertErrorLog(ex.Message, this.Session["UserID"].ToString());

                if(ex.InnerException != null)
                {
                    db.InsertErrorLog(ex.InnerException.Message, this.Session["UserID"].ToString());
                }

                if (ex.InnerException.InnerException != null)
                {
                    db.InsertErrorLog(ex.InnerException.InnerException.Message, this.Session["UserID"].ToString());
                }
            }

            return View();
        }
        
        #region WebAPI Client

        public TaskCollection GetTaskListFromSuperShuttle(DateTime StartTime, DateTime endTime)
        {

            string strURL = string.Format("https://www.tourismtransport.com:8443/v7/superservicerest.svc/v1/gettasksbytimerangeonaccounts");
            System.Net.WebRequest requestObject = WebRequest.Create(strURL);

            long epochStartTime = Helper.ToEpoch(StartTime);
            //db.InsertErrorLog("ToEpoch(StartTime): Completed", "1");
            
            long epochEndTime = Helper.ToEpoch(endTime);
            //db.InsertErrorLog("Helper.ToEpoch(endTime): Completed", "1");


            String strEpochStartTimeDayLightSaving;
            String strEpochEndTimeDayLightSaving;

            bool isDaylightStartTime = TimeZoneInfo.Local.IsDaylightSavingTime(StartTime);
            bool isDaylightEndTime = TimeZoneInfo.Local.IsDaylightSavingTime(endTime);

            if (isDaylightStartTime)
            {
                strEpochStartTimeDayLightSaving = String.Concat(epochStartTime.ToString(), "+1300");               
                
            }
            else
            {
                strEpochStartTimeDayLightSaving = String.Concat(epochStartTime.ToString(), "+1200");
            }

            if (isDaylightEndTime)
            {
                strEpochEndTimeDayLightSaving = String.Concat(epochEndTime.ToString(), "+1300");

            }
            else
            {
                strEpochEndTimeDayLightSaving = String.Concat(epochEndTime.ToString(), "+1200");
            }


            requestObject.ContentType = "application/json";


            // For Basic Authentication
            string authInfo = "philipkuruvilla@hotmail.com" + ":" + "philipkuruvillahasseveralvans";
            authInfo = Convert.ToBase64String(Encoding.Default.GetBytes(authInfo));
            requestObject.Headers["Authorization"] = "Basic " + authInfo;


            requestObject.Method = "POST";

            string postData = String.Concat("{\"fromDateInclusive\":\"/Date(",strEpochStartTimeDayLightSaving,")/\",\"toDateInclusive\":\"/Date(", strEpochEndTimeDayLightSaving, ")/\"}");
            db1.InsertErrorLog(postData, string.Concat(StartTime.ToString(), " - ", endTime.ToString()));

            using (var streamWriter = new StreamWriter(requestObject.GetRequestStream()))
            {

                streamWriter.Write(postData);
                streamWriter.Flush();
                streamWriter.Close();


                var httpResponse = (HttpWebResponse)requestObject.GetResponse();

                //db.InsertErrorLog("(HttpWebResponse)requestObject.GetResponse(): Completed", "1");

                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {

                    var result = streamReader.ReadToEnd();

                    TaskCollection taskList = new TaskCollection();

                    taskList = JsonConvert.DeserializeObject<TaskCollection>(result);

                    return taskList;
                }
            }
        }

        public void ImportTasksFromSuperShuttle()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();

            DateTime importDateTime = DateTime.Now;

            //Truncate Tasks Table
            db1.TruncateTask();

            //Get Task List 

            DateTime dtStartTime = Convert.ToDateTime(Request["dtStart"].ToString());            
            DateTime dtEndTime = Convert.ToDateTime(Request["dtEnd"].ToString());

        
                       

            TaskCollection taskCollection = new TaskCollection();
            int noOfDays = ((TimeSpan)(dtEndTime - dtStartTime)).Days;

            for (int i=0;i < noOfDays; i++)
            {
         
                if(i==0)
                {
                    taskCollection = GetTaskListFromSuperShuttle(dtStartTime, dtStartTime.AddDays(i+1).AddMilliseconds(-1));
                    db2.InsertErrorLog(taskCollection.tasks[taskCollection.tasks.Count-1].BookingID.ReservationNumber.ToString(),i.ToString());
                    
                }
                else if(i>0)
                {
                    taskCollection.tasks.AddRange(GetTaskListFromSuperShuttle(dtStartTime.AddDays(i), dtStartTime.AddDays(i+1).AddMilliseconds(-1)).tasks.ToList());
                    db2.InsertErrorLog(taskCollection.tasks[taskCollection.tasks.Count - 1].BookingID.ReservationNumber.ToString(), i.ToString());
                }
                
            }

            int totalTasks = 0;

            if (taskCollection.tasks != null)
            {
                totalTasks= taskCollection.tasks.Count();
            }
                        
            for (int i = 0; i < totalTasks; i++)
            {              
                //Pickup Time
                string strpickupTime = taskCollection.tasks[i].PickupTime;
                DateTime pickupTime=DateTime.MinValue;                
                if (!String.IsNullOrEmpty(strpickupTime))
                {
                    pickupTime = Helper.FromEpoch(Helper.ConvertToEpochLong(strpickupTime));
                }

                //Service Time
                string strServiceTime = taskCollection.tasks[i].ServiceTime;
                DateTime ServiceTime = pickupTime;
                if (!String.IsNullOrEmpty(strServiceTime))                
                {
                    ServiceTime = Helper.FromEpoch(Helper.ConvertToEpochLong(strServiceTime));
                }

                Task myDBTask = new Task
                {
                    AccountName = taskCollection.tasks[i].AccountName,                    
                    BookingId = taskCollection.tasks[i].BookingID.ReservationNumber.ToString(),
                    TaskId = taskCollection.tasks[i].BookingID.TaskId.ToString(),
                    DispatchStatus = taskCollection.tasks[i].DispatchStatus,
                    DropOffAddress = taskCollection.tasks[i].DropOffAddress.Suburb,
                    Pax = taskCollection.tasks[i].Pax,
                    FareType = taskCollection.tasks[i].FareType,
                    PaymentType = taskCollection.tasks[i].PaymentType,
                    pickupTime = pickupTime.ToString("dd/MMM/yyyy hh:mm tt"),
                    RetailFare = taskCollection.tasks[i].RetailFare,
                    PickupAddress = taskCollection.tasks[i].PickupAddress.Suburb,
                    ServiceTime = ServiceTime.ToString("dd/MMM/yyyy hh:mm tt"),
                    TravellerSurname =  taskCollection.tasks[i].TravellerSurname,
                    VehicleNumber = taskCollection.tasks[i].VehicleNumber,
                    AirportFee = taskCollection.tasks[i].AirportFee,
                    Fee = taskCollection.tasks[i].Fee,
                    Bbucode = taskCollection.tasks[i].Bucode,
                    DriverId = taskCollection.tasks[i].DriverID,
                    Run = taskCollection.tasks[i].Run,
                    ServiceType = taskCollection.tasks[i].ServiceType,
                    TotalFare = taskCollection.tasks[i].TotalFare,
                    ImportDate = importDateTime
                };

                db.Tasks.Add(myDBTask);
                         

            }
            db.SaveChanges();

            //Transfer the Newly Imported Tasks to Task Master
            db.TransferNewTaskToTaskMaster();            

            sw.Stop();
            
            ViewBag.Message =  "Imported tasks from " + dtStartTime.ToLongDateString() + " " + dtStartTime.ToLongTimeString() +"  to " + dtEndTime.ToLongDateString() +  " "  + dtEndTime.ToLongTimeString() + " Succesfully from SuperShuttle in " + string.Format("{0:hh\\:mm\\:ss}",sw.Elapsed) + " seconds.";
        }

        #endregion
    }
}