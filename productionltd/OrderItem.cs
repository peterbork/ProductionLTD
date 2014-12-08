using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace productionltd
{
    class OrderItem
    {
        private int ID;
        public int Amount { get; set; }
        public Product Product { get; set; }
        public List<MachineBooking> Bookings { get; set; }

        public OrderItem(int amount, Product product) {
            Amount = amount;
            Product = product;
        }
        public void Save(int order) {
            SqlConnection conn = new SqlConnection(DBConnectionString.Conn);

            try {
                conn.Open();
                SqlCommand cmd = new SqlCommand("addOrderItem", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter parameter = new SqlParameter();
                cmd.Parameters.Add(new SqlParameter("@amount", Amount));
                cmd.Parameters.Add(new SqlParameter("@order", order));
                cmd.Parameters.Add(new SqlParameter("@product", Product.ID));

                ID = (int)cmd.ExecuteScalar();

            }
            catch (Exception e) {
                new Alert(order + " " + e.Message);
            }
            finally {
                conn.Close();
                conn.Dispose();
            }

            MakeBooking();
        }

        private void MakeBooking() {
            Controller _controller = new Controller();
            _controller.getProcesses(Product);
            DateTime previousBooking = Helper.GetNextMonday();
            DateTime lastBooking;
            MachineBooking booking;

            foreach (Process process in Product.Processes) {
                lastBooking = Helper.GetLastBooking(process.Machine.ID, previousBooking);
                int time = (int)Math.Ceiling((double)process.Duration * (double)Amount / (double)process.Machine.Quantity);
                
                DateTime combinedTime = lastBooking.AddMinutes(time);
                if (combinedTime.Hour >= 16) {

                    lastBooking = lastBooking.AddDays(1);
                    if ((int)lastBooking.DayOfWeek < 6)
                        lastBooking = new DateTime(lastBooking.Year, lastBooking.Month, lastBooking.Day, 8, 0, 0, 0);
                    else
                        lastBooking = previousBooking;
                }

                previousBooking = lastBooking.AddMinutes(time);

                // Adding 5 minutes for time to change product
                booking = new MachineBooking(lastBooking.AddMinutes(5), previousBooking, process.Machine);
                booking.Save(ID);

            }
        }
    }
}
