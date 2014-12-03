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
        List<Machine> machines = new List<Machine>();

        public MachineMenu() {
            InitializeComponent();
            _controller = new Controller();
            
                foreach (var machine in _controller.getMachines())
	            {
                    MachineList.Items.Add(machine.Name);
                    machines.Add(machine);
	            }
            
        }

        private void MachineList_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            MachineBookingList.Items.Clear();
            foreach (MachineBooking machineBooking in _controller.getMachineBookings(machines[MachineList.SelectedIndex].ID))
	            {
                    MachineBookingList.Items.Add(machineBooking.StartTime.ToString() + machineBooking.EndTime.ToString());
	            }
        }
    }
}
