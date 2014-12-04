using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace productionltd {
    class Alert {
        public Alert(string message) {
            MessageBox.Show(message);
        }

        public Alert(int number) {
            MessageBox.Show(number + "");
        }
    }
}
