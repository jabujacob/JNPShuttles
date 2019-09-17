using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace WebAPI
{
    public static class Helper
    {

        public static string CnnVal(string name)
        {
            return ConfigurationManager.ConnectionStrings[name].ConnectionString;
        }

        /// <summary>
        /// Converts a DateTime to the long representation which is the number of seconds since the unix epoch.
        /// </summary>
        /// <param name="dateTime">A DateTime to convert to epoch time.</param>
        /// <returns>The long number of seconds since the unix epoch.</returns>
        public static long ToEpoch(DateTime dateTime) => (long)(dateTime - new DateTime(1970, 1, 1)).TotalMilliseconds;

        /// <summary>
        /// Converts a long representation of time since the unix epoch to a DateTime.
        /// </summary>
        /// <param name="epoch">The number of seconds since Jan 1, 1970.</param>
        /// <returns>A DateTime representing the time since the epoch.</returns>
        public static DateTime FromEpoch(long epoch)
        {

            DateTime dt = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc).AddMilliseconds(epoch);


            TimeZoneInfo cstZone = TimeZoneInfo.FindSystemTimeZoneById("New Zealand Standard Time");
            DateTime cstTime = TimeZoneInfo.ConvertTimeFromUtc(dt, cstZone);


            //if (cstZone.IsDaylightSavingTime(cstTime))

            //{
            //    cstTime = cstTime.AddHours(1);
            //}

            return cstTime;
        }


        /// <summary>
        /// Converts a DateTime? to the long? representation which is the number of seconds since the unix epoch.
        /// </summary>
        /// <param name="dateTime">A DateTime? to convert to epoch time.</param>
        /// <returns>The long? number of seconds since the unix epoch.</returns>
        public static long? ToEpoch(DateTime? dateTime) => dateTime.HasValue ? (long?)ToEpoch(dateTime.Value) : null;

        /// <summary>
        /// Converts a long? representation of time since the unix epoch to a DateTime?.
        /// </summary>
        /// <param name="epoch">The number of seconds since Jan 1, 1970.</param>
        /// <returns>A DateTime? representing the time since the epoch.</returns>
        public static DateTime? FromEpoch(long? epoch)
        {
            return epoch.HasValue ? (DateTime?)FromEpoch(epoch.Value) : null;
        }

        public static long ConvertToEpochLong(string epoch)
        {
            if (epoch != null)
            {
                string strSubstring = epoch.Substring(0, 19);

                strSubstring = strSubstring.Substring(6);

                return Convert.ToInt64(strSubstring);
            }
            else
            {
                return 0;
            }

        }


    }
}