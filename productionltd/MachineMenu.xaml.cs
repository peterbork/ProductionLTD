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
            string[] weekDays = new string[] { "Mandag", "Tirsdag", "Onsdag", "Torsdag", "Fredag"};
            
            foreach (var machine in _controller.getMachines())
	        {
                MachineList.Items.Add(machine.Name);
                machines.Add(machine);
	        }

            for (int i = 1; i <= Helper.GetWeeksInYear(DateTime.Now.Year); i++) {
                Week.Items.Add("Uge " + i);
            }
           
            Week.SelectedIndex = Helper.GetWeekNumber(DateTime.Now) - 1;

            foreach (string day in weekDays) {
                Day.Items.Add(day);
            }

            Day.SelectedIndex = (int)DateTime.Now.DayOfWeek - 1;
        }

        private void MachineList_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            MachineBookingList.Items.Clear();
            foreach (MachineBooking machineBooking in _controller.getMachineBookings(machines[MachineList.SelectedIndex]))
	            {
                    MachineBookingList.Items.Add(machineBooking.StartTime.ToString() + machineBooking.EndTime.ToString());
	            }
        }

        private void Week_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            _controller.getBookings(Helper.DateFromWeek(DateTime.Today.Year, int.Parse(Week.SelectedItem.ToString().Replace("Uge ", "")), int.Parse((string)Day.SelectedItem)));
        }
    }
}
