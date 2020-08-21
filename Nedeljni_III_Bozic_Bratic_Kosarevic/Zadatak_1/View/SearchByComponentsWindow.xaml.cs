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
    /// Interaction logic for SearchByComponentsWindow.xaml
    /// </summary>
    public partial class SearchByComponentsWindow : Window
    {
        public RecepieViewModel rvm1;

        public SearchByComponentsWindow(RecepieViewModel rvm)
        {
            InitializeComponent();
            DataContext = rvm;
            rvm1 = rvm;
            rvm.Components = "";
        }

        private void BeginSearch(object sender, RoutedEventArgs e)
        {
            rvm1.SearchByRecepieComponent();

            foreach (Window window in Application.Current.Windows)
            {
                if (window.GetType() == typeof(RecepieWindow))
                {
                    (window as RecepieWindow).ResetBtn.IsEnabled = true;
                }
            }
            Close();
        }

        private void Cancel(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
