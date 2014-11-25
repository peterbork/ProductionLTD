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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace productionltd {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();
            Type.Items.Add("Standard Order 1");
            Type.Items.Add("Standard Order 2");

        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e) {
            string orderItem = Type.SelectedValue.ToString() +" - "+ Count.Text + " Stk";
            OrderPreview.Items.Add(orderItem);
            deadlineStatus.Content = Deadline.Text;
        }
    }
}
