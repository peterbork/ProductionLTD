using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace productionltd
{
    class OrderItem
    {
        private int ID;
        public int Amount { get; set; }
        public Product Product { get; set; }
        public List<MachineBooking> Bookings { get; set; }
    }
}
