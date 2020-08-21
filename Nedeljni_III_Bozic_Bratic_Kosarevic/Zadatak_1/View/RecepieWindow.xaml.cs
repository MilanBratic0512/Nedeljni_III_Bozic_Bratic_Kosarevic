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
    /// Interaction logic for RecepieWindow.xaml
    /// </summary>
    public partial class RecepieWindow : Window
    {
        RecepieViewModel rvm = new RecepieViewModel();

        public RecepieWindow()
        {
            InitializeComponent();
            DataContext = rvm;
        }

        private void AddNewRecepie(object sender, RoutedEventArgs e)
        {
            AddEditReceptView addEditReceptWindow = new AddEditReceptView( new Recept(), false);
            addEditReceptWindow.Show();
            Close();
        }

        private void Logout(object sender, RoutedEventArgs e)
        {
            LoginWindow window = new LoginWindow();
            window.Show();
            Close();
        }

        private void EditRecepie(object sender, RoutedEventArgs e)
        {           
            rvm.EditRecept();
            Close();
        }

        private void DeleteRecepie(object sender, RoutedEventArgs e)
        {
            rvm.DeleteRecepie();
        }

        private void ShoppingCart(object sender, RoutedEventArgs e)
        {
            ShoppingCartView shoppingCartView = new ShoppingCartView();
            shoppingCartView.Show();
        }

    }
}
