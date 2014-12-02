using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace productionltd
{
    class MachineBooking
    {
        public int ID;
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public Machine machine { get; set; }
        public MachineBooking(int id, DateTime startTime, DateTime endTime) {
        }
    }

}
