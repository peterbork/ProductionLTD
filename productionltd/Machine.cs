using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace productionltd
{
    class Machine
    {
        private int ID;
        public string Name { get; set; }
        public int Quantity { get; set; }
        public List<MachineBooking> bookings { get; set; }

        public Machine(string name, int quantity)
        {
            Name = name;
            Quantity = quantity;
        }
        
    }
}
