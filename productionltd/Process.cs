using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace productionltd
{
    class Process
    {
        private int ID;
        public int Duration { get; set; }
        public Product Product { get; set; }
        public Machine Machine { get; set; }
    }
}
