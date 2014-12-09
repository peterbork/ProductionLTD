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
    /// Interaction logic for SpecialOrder.xaml
    /// </summary>
    public partial class SpecialOrder : Window {
        Dictionary<Machine, int> machines = new Dictionary<Machine, int>();
        Controller _controller;
        MainWindow mainwindow;

        public SpecialOrder(MainWindow main) {
            mainwindow = main;
        }
        public SpecialOrder() {
            InitializeComponent();
            _controller = new Controller();
            foreach (Machine machine in _controller.getMachines()) {
                machines.Add(machine, 0);
                Machines.Items.Add(machine.Name);
            }
        }

        private void Machines_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            MachineTime.Text = machines.ElementAt(Machines.SelectedIndex).Value + "";
        }

        private void SaveTime(object sender, KeyEventArgs e) {
            try {
                machines[machines.ElementAt(Machines.SelectedIndex).Key] = int.Parse(MachineTime.Text);
            }
            catch (Exception) {

            }
        }

        public void Button_Click(object sender, RoutedEventArgs e) {
            List<Process> processes = new List<Process>();
            foreach (KeyValuePair<Machine, int> process in machines) {
                processes.Add(new Process(process.Value, process.Key));
            }
            string size = width.Text + " x " + height.Text + " x " + length.Text;
            Product product = _controller.addSpecialProduct(Name.Text, size, processes);
            Controller.mainwindow.AddToProductList(product);
            this.Close();
        }
    }
}
