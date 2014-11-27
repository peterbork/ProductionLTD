using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace productionltd
{
    class Order
    {
        private int ID;
        public string Name { get; set; }
        public string Company { get; set; }
        public DateTime Deadline { get; set; }
        public List<OrderItem> OrderItems { get; set; }

        public Order(string name, string company)
        {
            Name = name;
            Company = company;
        }

    }
}
