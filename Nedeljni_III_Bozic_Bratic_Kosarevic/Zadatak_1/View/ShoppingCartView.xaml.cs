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
using Zadatak_1.Models;
using Zadatak_1.ViewModel;

namespace Zadatak_1.View
{
    /// <summary>
    /// Interaction logic for ShoppingCartView.xaml
    /// </summary>
    public partial class ShoppingCartView : Window
    {
        ShoppingCartViewModel scvm = new ShoppingCartViewModel();

        public ShoppingCartView()
        {
            InitializeComponent();
            DataContext = scvm;
        }



        private void AddToCart(object sender, RoutedEventArgs e)
        {
            Components components = (sender as Button).DataContext as Components;
            scvm.AddToCart(components);
            try
            {
                datagrid2.Items.Refresh();
                datagrid3.Items.Refresh();
            }
            catch (Exception)
            {
                MessageBox.Show("Error");
            }

        }

        private void Buy(object sender, RoutedEventArgs e)
        {
            scvm.WriteToTheFile();
            scvm.UpdateDatabase();
            MessageBox.Show("You have successfully purchased components.");
            Close();
        }
    }
}
