using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace productionltd
{
    class Controller
    {
        private List<Order> orders;

        public void NewOrder(string name, string company)
        {
            Order p = new Order(name, company);
        }
        public void NewOrderItem()
        {

        }
        public void AddToOrderList()
        {

        } 
    }
}
