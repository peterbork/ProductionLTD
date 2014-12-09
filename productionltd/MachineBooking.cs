using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace productionltd
{
    public class MachineBooking
    {
        public int ID;
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public Machine machine { get; set; }
        public MachineBooking(DateTime startTime, DateTime endTime, Machine machine) {
            StartTime = startTime;
            EndTime = endTime;
            this.machine = machine;
        }

        internal void Save(int orderItem) {
            SqlConnection conn = new SqlConnection(DBConnectionString.Conn);
            
            try {
                conn.Open();
                SqlCommand cmd = new SqlCommand("addMachineBooking", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter parameter = new SqlParameter();
                cmd.Parameters.Add(new SqlParameter("@startTime", StartTime));
                cmd.Parameters.Add(new SqlParameter("@endTime", EndTime));
                cmd.Parameters.Add(new SqlParameter("@machine", machine.ID));
                cmd.Parameters.Add(new SqlParameter("@orderItem", orderItem));

                ID = (int)cmd.ExecuteNonQuery();

            }
            catch (Exception e) {
                //new Alert(StartTime + " " + EndTime);
            }
            finally {
                conn.Close();
                conn.Dispose();
            }
        }
    }

}
