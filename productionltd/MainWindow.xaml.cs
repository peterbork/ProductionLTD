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
using System.Data;
using System.Data.SqlClient;

namespace productionltd {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {

        Controller _controller;
        List<Product> products = new List<Product>();
        Dictionary<Product, int> productList = new Dictionary<Product, int>();

        public MainWindow() {
            _controller = new Controller();
            InitializeComponent();

            products = _controller.getProducts();
            foreach (Product product in products) {
                Type.Items.Add(product.Name + " " + product.Size);
            }
            Type.SelectedIndex = 0;
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e) {
            try {
                productList.Add(products[Type.SelectedIndex], int.Parse(Count.Text));
                string orderItem = Type.SelectedValue.ToString() + " - " + Count.Text + " Stk";
                OrderPreview.Items.Add(orderItem);
                Count.Text = "Antal";
            }
            catch (Exception) {
                new Alert("Product is already added.");
            }
            //deadlineStatus.Content = Deadline.Text;
        }

        private void Count_TextChanged(object sender, TextChangedEventArgs e) {

        }

        private void Count_GotFocus(object sender, RoutedEventArgs e) {
            TextBox t = e.Source as TextBox; 
            
            if (t.Text == "Antal" || t.Text == "Navn" || t.Text == "Firma")
                t.Text = "";
        }

        private void Count_LostFocus(object sender, RoutedEventArgs e) {
            TextBox t = e.Source as TextBox;
            if (t.Text == "") {
                if (t.Name == "Count")
                    t.Text = "Antal";
                else if (t.Name == "Company")
                    t.Text = "Firma";
                else
                    t.Text = "Navn";
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //_controller.CheckOrder();
            _controller.NewOrder(Name.Text, Company.Text, productList);
        }

        private void OrderPreview_MouseUp(object sender, MouseButtonEventArgs e) {
        }

        private void MenuItemDelete_Click(object sender, RoutedEventArgs e) {
            productList.Remove(productList.ElementAt(OrderPreview.SelectedIndex).Key);
            OrderPreview.Items.RemoveAt(OrderPreview.SelectedIndex);
        }

        private void ToMachineWindow_Click(object sender, RoutedEventArgs e) {
            MachineMenu machine = new MachineMenu();
            machine.Show();
        }
    }
}
