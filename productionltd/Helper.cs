using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;

namespace productionltd {
    static class Helper {

        public static DateTime GetNextMonday() {
            return GetNextWeekday(DateTime.Today, DayOfWeek.Monday).AddHours(8);
        }

        public static DateTime GetNextWeekday(DateTime start, DayOfWeek day) {
            // The (... + 7) % 7 ensures we end up with a value in the range [0, 6]
            int daysToAdd = ((int)day - (int)start.DayOfWeek + 7) % 7;
            if (daysToAdd == 0) daysToAdd = 7;
            return start.AddDays(daysToAdd);
        }

        public static int GetWeeksInYear(int year) {
            DateTimeFormatInfo dfi = DateTimeFormatInfo.CurrentInfo;
            DateTime date1 = new DateTime(year, 12, 31);
            Calendar cal = dfi.Calendar;
            return cal.GetWeekOfYear(date1, dfi.CalendarWeekRule,
                                                dfi.FirstDayOfWeek);
        }

        public static int GetWeekNumber(DateTime date) {
            var day = (int)CultureInfo.CurrentCulture.Calendar.GetDayOfWeek(date);
            return CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(date.AddDays(4 - (day == 0 ? 7 : day)), CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
        }

        public static DateTime GetLastBooking(int machine, DateTime nextWeek) {
            SqlConnection conn = new SqlConnection(DBConnectionString.Conn);

            conn.Open();

            SqlCommand cmd = new SqlCommand("getLastBooking", conn);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@Machine_FK", machine));
            cmd.Parameters.Add(new SqlParameter("@nextWeek", nextWeek));
            /*SqlDataReader reader = cmd.ExecuteReader();*/

             
            Object endtime = cmd.ExecuteScalar();

            /*while (reader.Read()) {
                endtime = reader["endtime"].ToString();
            }

            reader.Close();*/
            conn.Close();
            conn.Dispose();

            if (endtime != null)
                return Convert.ToDateTime(endtime);
            else
            return nextWeek;
        }
    }
}
