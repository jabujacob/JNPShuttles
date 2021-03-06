﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebAPI.Models;
using System.Data;
using Dapper;

namespace WebAPI
{
    public class DataAccess
    {
                     
        #region User
        public List<User> GetAllUsers()
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.CnnVal("JNPDB")))
            {
                var output= connection.Query<User>("[dbo].[GetUsers]").ToList();
                return output;                
            }
        }
        public User GetUser(int id)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.CnnVal("JNPDB")))
            {
                var output = connection.Query<User>("[dbo].[GetUserByID]  @Id", new { Id = id }).FirstOrDefault();
                return output;
            }
        }


        public User GetUser(string username, string password)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.CnnVal("JNPDB")))
            {
                var output = connection.Query<User>("[dbo].[GetUserByUserNamePassword]  @Username, @Password", new { Username = username,Password=password }).FirstOrDefault();
                return output;
            }
        }

        public void UpdateUser(User user)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.CnnVal("JNPDB")))
            {

                connection.Execute("[dbo].[UpdateUser] @Id,@username,@DriverId,@Password,@Admin,@FirstName,@LastName", user);
            }
        }
        public void InsertUser(User user)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.CnnVal("JNPDB")))
            {

                connection.Execute("[dbo].[InsertUser] @username,@DriverId,@Password,@Admin,@FirstName,@LastName", user);
            }
        }
        #endregion

        #region TripSheet
        public List<TripSheet> GetAllTripSheets(int userId)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.CnnVal("JNPDB")))
            {
                var output = connection.Query<TripSheet>("[dbo].[GetTripSheetByUserId] @UserId", new { UserId = userId }).ToList();
                return output;
            }
        }
        public TripSheet GetTripSheet(int id)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.CnnVal("JNPDB")))
            {
                var output = connection.Query<TripSheet>("[dbo].[GetTripSheetById]  @Id", new { Id = id }).FirstOrDefault();
                return output;
            }
        }

        public void UpdateTripSheet(TripSheet tripSheet)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.CnnVal("JNPDB")))
            {

                connection.Execute("[dbo].[UpdateTripSheet] @Id,@DriverId,@VanId,@StartKM, @EndKM, @StartTime, @EndTime, @Swipes, @Trips, @TaxiChit, @CCEFTPOS, @PrePaid, @Cash", tripSheet);
            }
        }
        public void InsertTripSheet(TripSheet tripSheet)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.CnnVal("JNPDB")))
            {
                connection.Execute("[dbo].[InsertTripSheet] @DriverId,@VanId,@StartKM, @EndKM, @StartTime, @EndTime, @Swipes, @Trips, @TaxiChit, @CCEFTPOS, @PrePaid, @Cash", tripSheet);
            }
        }
        #endregion

        #region Tasks

        public List<Task> GetTasks (DateTime startDate, DateTime endDate)        {

            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.CnnVal("JNPDB")))
            {
                var output = connection.Query<Task>("[dbo].[Get_Tasks] @StartDate, @EndDate", new { StartDate = startDate, EndDate=endDate }).ToList();
                return output;
            }
        }

        public void TruncateTask()
        {
            
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.CnnVal("JNPDB")))
            {
                connection.Execute("[dbo].[TruncateTask]");
            }
        }

        public void TransferNewTaskToTaskMaster()
        {

            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.CnnVal("JNPDB")))
            {
                connection.Execute("[dbo].[TransferNewTaskToTaskMaster]");
            }
        }

        

        public DateTime GetLastImportTime()
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.CnnVal("JNPDB")))
            {
                var output = connection.Query<DateTime>("[dbo].[Get_TaskUpdatedLastOnTime]").FirstOrDefault();
                return output;
            }
        }

        public void InsertTask(Task task)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.CnnVal("JNPDB")))
            {
                connection.Execute("[dbo].[InsertTask] @AccountName ,@BookingId ,@TaskId ,@DispatchStatus ,@DropOffAddress ,@Pax  ,@FareType ,@PaymentType ,@PickupAddress ,@pickupTime  ,@RetailFare ,@ServiceTime ,@TravellerSurname ,@VehicleNumber ,@AirportFee ,@Bbucode ,@DriverId ,@Run ,@ServiceType ,@TotalFare ,@ImportDate,@Fee,@APIParameter", task);
            }
        }

        #endregion

        #region Driver
        public List<Driver> GetAllDrivers()
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.CnnVal("JNPDB")))
            {
                var output = connection.Query<Driver>("[dbo].[GetDrivers]").ToList();
                return output;
            }
        }
        public Driver GetDriver(string superShuttleId)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.CnnVal("JNPDB")))
            {
                var output = connection.Query<Driver>("[dbo].[GetDriverBySuperShuttleId]  @SuperShuttleId", new { SuperShuttleId = superShuttleId}).FirstOrDefault();
                return output;
            }
        }
        public Driver GetDriver(int id)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.CnnVal("JNPDB")))
            {
                var output = connection.Query<Driver>("[dbo].[GetDriverById]  @Id", new { Id = id }).FirstOrDefault();
                return output;
            }
        }
        public void InsertDriver(Driver driver)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.CnnVal("JNPDB")))
            {

                connection.Execute("[dbo].[InsertDriver] @FirstName,@LastName,@SuperShuttleId,@DefaultVanId,@DriverShare1,@DriverShare2", driver);
            }
        }
        public void UpdateDriver(Driver driver)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.CnnVal("JNPDB")))
            {

                connection.Execute("[dbo].[UpdateDriver] @Id,@FirstName,@LastName,@SuperShuttleId,@DefaultVanId,@DriverShare1,@DriverShare2", driver);
            }
        }

        #endregion

        #region Van
        public List<Van> GetAllVans()
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.CnnVal("JNPDB")))
            {
                var output = connection.Query<Van>("[dbo].[GetVans]").ToList();
                return output;
            }
        }
        public Van GetVan(int id)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.CnnVal("JNPDB")))
            {
                var output = connection.Query<Van>("[dbo].[GetVanById]  @Id", new { Id = id }).FirstOrDefault();
                return output;
            }
        }

        #endregion


        #region Account
        public List<Account> GetAllAccounts()
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.CnnVal("JNPDB")))
            {
                var output = connection.Query<Account>("[dbo].[GetAccounts]").ToList();
                return output;
            }
        }

        public Account GetAccount(int id)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.CnnVal("JNPDB")))
            {
                var output = connection.Query<Account>("[dbo].[GetAccountById]  @Id", new { Id = id }).FirstOrDefault();
                return output;
            }
        }
        public void InsertAccount(Account account)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.CnnVal("JNPDB")))
            {

                connection.Execute("[dbo].[InsertAccount] @AccountName, @GroupId, @GST, @AccountTypeId", account);
            }
        }
        public void UpdateAccount(Account account)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.CnnVal("JNPDB")))
            {

                connection.Execute("[dbo].[UpdateAccount] @Id,@AccountName, @GroupId, @GST, @AccountTypeId", account);
            }
        }

        public List<AccountType> GetAllAccountTypes()
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.CnnVal("JNPDB")))
            {
                var output = connection.Query<AccountType>("[dbo].[GetAccountTypes]").ToList();
                return output;
            }
        }
        public AccountType GetAccountType(int id)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.CnnVal("JNPDB")))
            {
                var output = connection.Query<AccountType>("[dbo].[GetAccountTypeById]  @Id", new { Id = id }).FirstOrDefault();
                return output;
            }
        }

        #endregion

        #region Transaction
        public List<Transaction> GetAllTransactions()
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.CnnVal("JNPDB")))
            {
                var output = connection.Query<Transaction>("[dbo].[GetTransactions]").ToList();
                return output;
            }
        }

        public Transaction GetTransaction(int id)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.CnnVal("JNPDB")))
            {
                var output = connection.Query<Transaction>("[dbo].[GetTransactionById]  @Id", new { Id = id }).FirstOrDefault();
                return output;
            }
        }
        public void InsertTransaction(Transaction transaction)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.CnnVal("JNPDB")))
            {

                connection.Execute("[dbo].[InsertTransaction] @AccountId, @Amount, @GST,@VanId,@TransactionBulkId,@Date,@ItemDescription,@InvoiceNumber,@Quantity", transaction);
            }
        }
        public void InsertTransactionBulk(Transaction transaction)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.CnnVal("JNPDB")))
            {

                connection.Execute("[dbo].[InsertTransactionBulk] @AccountId, @Amount, @GST,@VansId ,@Date,@ItemDescription,@InvoiceNumber,@Quantity", transaction);
            }
        }
        public void UpdateTransaction(Transaction transaction)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.CnnVal("JNPDB")))
            {

                connection.Execute("[dbo].[UpdateTransactionById] @Id,@AccountId, @Amount, @GST,@VanId, @Date,@ItemDescription,@InvoiceNumber,@Quantity", transaction);
            }
        }

        #endregion

        #region General
        public List<MenuPermission> GetMenuPermissions(int userId)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.CnnVal("JNPDB")))
            {
                var output = connection.Query<MenuPermission>("[dbo].[GetMenuPerssions] @UserID", new { UserId = userId }).ToList();
                return output;
            }
        }

        #endregion

        #region Reports

        public List<DriverAnalysis> GetDriverAnalysisDetails(DateTime startDate, DateTime endDate, int driverId)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.CnnVal("JNPDB")))
            {
                var output = connection.Query<DriverAnalysis>("[dbo].[Get_Driver_Analysis_Detail] @startDate, @endDate, @driverId", new { startDate = startDate, endDate=endDate, driverId=driverId }).ToList();
                return output;
            }
        }

        public List<DriverAnalysis> GetDriverAnalysisSummary(DateTime startDate, DateTime endDate, int driverId)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.CnnVal("JNPDB")))
            {
                var output = connection.Query<DriverAnalysis>("[dbo].[Get_Driver_Analysis_Summary] @startDate, @endDate, @driverId", new { startDate = startDate, endDate = endDate, driverId = driverId }).ToList();
                return output;
            }
        }

        public List<DriverAnalysis> GetDriverMonthlyAnalysisSummary(DateTime startDate, DateTime endDate)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.CnnVal("JNPDB")))
            {
                var output = connection.Query<DriverAnalysis>("[dbo].[Get_Driver_Monthly_Analysis_Summary] @startDate, @endDate", new { startDate = startDate, endDate = endDate}).ToList();
                return output;
            }
        }

        public List<TripSheet> GetTripSheetEntryStatus()
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.CnnVal("JNPDB")))
            {
                var output = connection.Query<TripSheet>("[dbo].[Get_TripSheet_Entry_Status]").ToList();
                return output;
            }
        }

        public List<TripSheet> GetTripSheetMultipleDrivers()
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.CnnVal("JNPDB")))
            {
                var output = connection.Query<TripSheet>("[dbo].[Get_TripSheet_Multiple_Drivers]").ToList();
                return output;
            }
        }

        #endregion
    }
}