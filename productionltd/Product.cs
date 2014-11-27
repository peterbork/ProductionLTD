using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace productionltd
{
    class Product
    {
        private int ID;
        public string Name { get; set; }
        public bool Type { get; set; }
        public string Size { get; set; }
        public List<Process> Processes { get; set; }

        public Product(string name, bool type, string size)
        {
            Name = name;
            Type = type;
            Size = size;
        }
    }
}
