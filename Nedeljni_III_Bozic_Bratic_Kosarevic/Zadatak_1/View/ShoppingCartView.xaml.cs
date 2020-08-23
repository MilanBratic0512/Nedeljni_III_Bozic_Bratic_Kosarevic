using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        public ShoppingCartView(ObservableCollection<Components> MissingComponents, Recept recepie)
        {
            InitializeComponent();
            scvm.AllComponents = MissingComponents;
            scvm.InitialComponets = MissingComponents;
            scvm.Recepie = recepie;
            DataContext = scvm;
            scvm.NumberOfPersonsInput = recepie.PersonNumber;
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

            MessageBox.Show("You have successfully purchased components.");
            RecepieWindow window = new RecepieWindow();
            window.Show();
            Close();
        }

        private void CancleBtn(object sender, RoutedEventArgs e)
        {
            RecepieWindow window = new RecepieWindow();
            window.Show();
            Close();
        }

        private void CalculateComponentsBtn(object sender, RoutedEventArgs e)
        {
            BtnOk.IsEnabled = false;
            NumOfPersons.IsEnabled = false;
            scvm.CalculateComponent(scvm.InitialComponets, scvm.Recepie.PersonNumber, scvm.NumberOfPersonsInput);
        }
    }
}
