using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace productionltd {
    /// <summary>
    /// Interaction logic for MachineMenu.xaml
    /// </summary>
    public partial class MachineMenu : Window {
        Controller _controller;
        public MachineMenu() {
            InitializeComponent();
            _controller = new Controller();
            
                foreach (var machine in _controller.getMachines())
	            {
                    MachineList.Items.Add(machine);
	            }
            
        }

        private void MachineList_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            foreach (var machineBooking in _controller.getMachineBookings(int.Parse(MachineList.SelectedItem.ToString())))
	            {
                    MachineBookingList.Items.Add(machineBooking);
	            }
        }
    }
}
