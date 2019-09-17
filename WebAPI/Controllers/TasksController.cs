using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using WebAPI.Models;

namespace WebAPI.Controllers
{

    [RoutePrefix("api/Tasks")]
    public class TasksController : ApiController
    {

        readonly DataAccess db = new DataAccess();

        // GET: api/Tasks?startDate=&endDate=
        [HttpGet]
        public IHttpActionResult Get(DateTime startDate, DateTime endDate)
        {
            return Ok(db.GetTasks(startDate, endDate));
        }

        // GET: api/Tasks/ImportTasksFromSuperShuttle
        [HttpGet]
        public DateTime ImportTasksFromSuperShuttle()
        {
            
            DateTime importDateTime = DateTime.Now;

            //Truncate Tasks Table
            db.TruncateTask();

            //Get Last Import Date from DB

            DateTime dtStartTime = db.GetLastImportTime().AddDays(-1);
            DateTime dtEndTime = DateTime.Now.AddDays(1);


            int noOfDays = ((TimeSpan)(dtEndTime - dtStartTime)).Days;

            TaskCollection taskCollection = new TaskCollection();
            for (int i = 0; i < noOfDays; i++)           {
                
                if (i == 0)
                {
                    taskCollection = GetTaskListFromSuperShuttle(dtStartTime, dtStartTime.AddDays(i + 1).AddMilliseconds(-1));                  

                }
                else if (i > 0)
                {
                    TaskCollection taskCollection1 = GetTaskListFromSuperShuttle(dtStartTime.AddDays(i), dtStartTime.AddDays(i + 1).AddMilliseconds(-1));
                    if (taskCollection1.Tasks != null)
                    {
                        taskCollection.Tasks.AddRange(taskCollection1.Tasks.ToList());
                    }
                    
                }

            }

            int totalTasks = 0;

            if (taskCollection != null)
            {
                totalTasks = taskCollection.Tasks.Count();
            }

            for (int i = 0; i < totalTasks; i++)
            {
                //Pickup Time
                string strpickupTime = taskCollection.Tasks[i].PickupTime;
                DateTime pickupTime = DateTime.MinValue;
                if (!String.IsNullOrEmpty(strpickupTime))
                {
                    pickupTime = Helper.FromEpoch(Helper.ConvertToEpochLong(strpickupTime));
                }

                //Service Time
                string strServiceTime = taskCollection.Tasks[i].ServiceTime;
                DateTime ServiceTime = pickupTime;
                if (!String.IsNullOrEmpty(strServiceTime))
                {
                    ServiceTime = Helper.FromEpoch(Helper.ConvertToEpochLong(strServiceTime));
                }

                Task myDBTask = new Task
                {
                    AccountName = taskCollection.Tasks[i].AccountName,
                    BookingId = taskCollection.Tasks[i].BookingID.ReservationNumber.ToString(),
                    TaskId = taskCollection.Tasks[i].BookingID.TaskId.ToString(),
                    DispatchStatus = taskCollection.Tasks[i].DispatchStatus,
                    DropOffAddress = taskCollection.Tasks[i].DropOffAddress.Suburb,
                    Pax = taskCollection.Tasks[i].Pax,
                    FareType = taskCollection.Tasks[i].FareType,
                    PaymentType = taskCollection.Tasks[i].PaymentType,
                    PickupTime = pickupTime.ToString("dd/MMM/yyyy hh:mm tt"),
                    RetailFare = taskCollection.Tasks[i].RetailFare,
                    PickupAddress = taskCollection.Tasks[i].PickupAddress.Suburb,
                    ServiceTime = ServiceTime.ToString("dd/MMM/yyyy hh:mm tt"),
                    TravellerSurname = taskCollection.Tasks[i].TravellerSurname,
                    VehicleNumber = taskCollection.Tasks[i].VehicleNumber,
                    AirportFee = taskCollection.Tasks[i].AirportFee,
                    Fee = taskCollection.Tasks[i].Fee,
                    Bbucode = taskCollection.Tasks[i].Bucode,
                    DriverId = taskCollection.Tasks[i].DriverID,
                    Run = taskCollection.Tasks[i].Run,
                    ServiceType = taskCollection.Tasks[i].ServiceType,
                    TotalFare = taskCollection.Tasks[i].TotalFare,
                    ImportDate = importDateTime
                };

                db.InsertTask(myDBTask);
            }
            
            //Transfer the Newly Imported Tasks to Task Master
            db.TransferNewTaskToTaskMaster();

            return importDateTime;
        }


        private TaskCollection GetTaskListFromSuperShuttle(DateTime StartTime, DateTime endTime)
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

            string postData = String.Concat("{\"fromDateInclusive\":\"/Date(", strEpochStartTimeDayLightSaving, ")/\",\"toDateInclusive\":\"/Date(", strEpochEndTimeDayLightSaving, ")/\"}");
            //db1.InsertErrorLog(postData, string.Concat(StartTime.ToString(), " - ", endTime.ToString()));

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

                    TaskCollection taskList = JsonConvert.DeserializeObject<TaskCollection>(result);

                    return taskList;
                }
            }
        }

        // GET: api/Tasks/GetLastImporTime
        [Route("GetLastImportTime")]
        public DateTime GetLastImportTime()
        {
            return db.GetLastImportTime();
        }



    }
}
