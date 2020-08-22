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
    /// Interaction logic for ReceptDetailsView.xaml
    /// </summary>
    public partial class ReceptDetailsView : Window
    {
        ReceptDetailsViewModela receptDetails;
        public ReceptDetailsView(Recept recept)
        {
            InitializeComponent();
            receptDetails = new ReceptDetailsViewModela(recept, this);
            this.DataContext = receptDetails;
        }

        private void btnQuit_Click(object sender, RoutedEventArgs e)
        {      
            RecepieWindow recepieWindowWindow = new RecepieWindow();
            recepieWindowWindow.Show();
            Close();
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            receptDetails.EditRecept();
            Close();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            receptDetails.DeleteRecepie();
            RecepieWindow recepie = new RecepieWindow();
            recepie.Show();
            Close();            
        }
    }
}
