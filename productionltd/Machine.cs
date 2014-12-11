using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace productionltd
{
    public class Machine
    {
        public int ID;
        public string Name { get; set; }
        public int Quantity { get; set; }
        public List<MachineBooking> Bookings { get; set; }


        public Machine(int id, string name, int quantity)
        {
            ID = id;
            Name = name;
            Quantity = quantity;
        }

        public Machine()
        {

        }

    }
}
