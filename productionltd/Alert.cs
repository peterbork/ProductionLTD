using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace productionltd {
    class Alert {
        private Exception e;

        public Alert(string message) {
            MessageBox.Show(message);
        }

        public Alert(int number) {
            MessageBox.Show(number + "");
        }

        public Alert(DateTime date) {
            MessageBox.Show(date.ToString());
        }

        public Alert(Exception e) {
            MessageBox.Show(e.Message);
        }
    }
}
