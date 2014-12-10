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
        Machine selectedMachine;

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
           
            

            foreach (string day in weekDays) {
                Day.Items.Add(day);
            }
            selectedMachine = machines[0];
            MachineList.SelectedIndex = 0;
            Day.SelectedIndex = (int)DateTime.Now.DayOfWeek - 1;
            Week.SelectedIndex = Helper.GetWeekNumber(DateTime.Now) - 1;
        }

        private void MachineList_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            selectedMachine = machines[MachineList.SelectedIndex];
            GetBookings();
        }

        private void Week_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            GetBookings();
        }

        private void GetBookings() {
            if (Week.SelectedIndex > -1 && Day.SelectedIndex > -1) {
                int inet = int.Parse(Week.SelectedItem.ToString().Replace("Uge ", ""));
                int tner = Day.SelectedIndex + 1;
                DateTime dateWeek = Helper.DateFromWeek(DateTime.Today.Year, inet, tner);
                MachineBookingList.Items.Clear();
                foreach (Dictionary<string, string> booking in _controller.getMachineBookings(dateWeek, selectedMachine.ID)) {
                    MachineBookingList.Items.Add(" Start: " + booking["StartTime"] + " Tid: " + booking["Duration"] + " minutter " + " Produkt: " + booking["ProductName"] + " Kunde: " + booking["OrderName"]);
                }
            }
        }

        private void Day_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            GetBookings();
        }
    }
}
