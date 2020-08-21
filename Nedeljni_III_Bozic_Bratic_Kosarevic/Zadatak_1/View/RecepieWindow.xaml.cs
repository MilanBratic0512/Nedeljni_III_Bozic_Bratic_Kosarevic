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
            AddEditReceptView addEditReceptWindow = new AddEditReceptView(false);
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
            AddEditReceptView addEditReceptWindow = new AddEditReceptView(true);
            addEditReceptWindow.Show();
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

        private void RecepieDetails(object sender, RoutedEventArgs e)
        {

        }

        private void Title_LostFocus(object sender, RoutedEventArgs e)
        {
            if (Type.Text == "" && Title.Text != "")
            {
                Type.IsEnabled = false;
            }
            else
            {
                Type.IsEnabled = true;
            }
        }

        private void Type_LostFocus(object sender, RoutedEventArgs e)
        {
            if (Title.Text == "" && Type.Text != "")
            {
                Title.IsEnabled = false;
            }
            else
            {
                Title.IsEnabled = true;
            }
        }

        private void ComponentsSearch(object sender, RoutedEventArgs e)
        {
            Title.Text = "";
            Type.Text = "";
            Title.IsEnabled = false;
            Type.IsEnabled = false;
            Components.IsEnabled = false;
            OkBtn.IsEnabled = false;
            ResetBtn.IsEnabled = false;
            SearchByComponentsWindow window = new SearchByComponentsWindow(rvm);
            window.Show();
        }

        private void BeginSearch(object sender, RoutedEventArgs e)
        {
            Title.IsEnabled = false;
            Type.IsEnabled = false;
            Components.IsEnabled = false;

            if (Title.Text != "")
            {
                rvm.SearchByRecepieTitle();
            }
            else
            {
                rvm.SearchByRecepieType();
            }
        }

        private void ResetSearch(object sender, RoutedEventArgs e)
        {
            Title.Text = "";
            Type.Text = "";
            Title.IsEnabled = true;
            Type.IsEnabled = true;
            Components.IsEnabled = true;
            OkBtn.IsEnabled = true;
            rvm.Recepies.Clear();
            rvm.FillList();
        }
    }
}
