using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Zadatak_1.Model;
using Zadatak_1.Models;
using Zadatak_1.ViewModel;

namespace Zadatak_1.View
{
    /// <summary>
    /// Interaction logic for AddEditReceptView.xaml
    /// </summary>
    public partial class AddEditReceptView : Window
    {
        AddEditReceptViewModel addEditModel;
        public AddEditReceptView(Recept recept, bool isForEdit)
        {
            InitializeComponent();
            addEditModel = new AddEditReceptViewModel(recept, this, isForEdit);
            this.DataContext = addEditModel;
        }

        private bool isValidReseptName;
        private bool isValidNumbOfPerson;
        private bool isValidReceptType;
        private bool isValidReceptText;
        private bool isValidComponentName;
        private bool isValidComponentAmount;

        private void IsSaveReceptEnabled()
        {
            if (isValidReseptName &&
                isValidNumbOfPerson &&
                isValidReceptType &&
                isValidReceptText && addEditModel.TemporaryComponentList.Count>0)
            {
                btnSave.IsEnabled = true;
            }
            else
            {
                btnSave.IsEnabled = false;
            }
        }

        private void IsAddComponentEnabled()
        {
            if (isValidComponentName &&
                isValidComponentAmount)
            {                
                btnAdd.IsEnabled = true;                
            }
            else
            {
                btnAdd.IsEnabled = false;
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            addEditModel.AddAddComponentToList();
            IsSaveReceptEnabled();
        }

        private void btnQuit_Click(object sender, RoutedEventArgs e)
        {
            RecepieWindow recepieWindowWindow = new RecepieWindow();
            recepieWindowWindow.Show();
            Close();
        }

        private void btnDeleteComponent_Click(object sender, RoutedEventArgs e)
        {
            addEditModel.DeleteComponentExecute();
            IsSaveReceptEnabled();
        }

        private void txtReceptName_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtReceptName.Focus())
            {
                lblValidationReceptName.Visibility = Visibility.Visible;
                lblValidationReceptName.FontSize = 16;
                lblValidationReceptName.Foreground = new SolidColorBrush(Colors.Red);
                lblValidationReceptName.Content = "The name must contain \nat least 5 letters!";
            }

            string patternName = @"^([a-zA-Z0-9 ]{5,100})$";
            Match match = Regex.Match(txtReceptName.Text, patternName, RegexOptions.IgnoreCase);

            if (!match.Success)
            {
                txtReceptName.BorderBrush = new SolidColorBrush(Colors.Red);
                txtReceptName.Foreground = new SolidColorBrush(Colors.Red);
                isValidReseptName = false;
            }
            else
            {
                lblValidationReceptName.Visibility = Visibility.Hidden;
                txtReceptName.BorderBrush = new SolidColorBrush(Colors.Black);
                txtReceptName.Foreground = new SolidColorBrush(Colors.Black);
                isValidReseptName = true;
            }
            IsSaveReceptEnabled();
        }

        private void txtPersonNumber_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtPersonNumber.Focus())
            {
                lblValidationPersonNumber.Visibility = Visibility.Visible;
                lblValidationPersonNumber.FontSize = 16;
                lblValidationPersonNumber.Foreground = new SolidColorBrush(Colors.Red);
                lblValidationPersonNumber.Content = "The field only allows \nnumbers greater than 0!!";
            }

            string patternPersonNumber = @"^([0-9]{1,3})$";
            Match match = Regex.Match(txtPersonNumber.Text, patternPersonNumber, RegexOptions.IgnoreCase);

            if (!match.Success)
            {
                txtPersonNumber.BorderBrush = new SolidColorBrush(Colors.Red);
                txtPersonNumber.Foreground = new SolidColorBrush(Colors.Red);
                isValidNumbOfPerson = false;
            }
            else
            {
                lblValidationPersonNumber.Visibility = Visibility.Hidden;
                txtPersonNumber.BorderBrush = new SolidColorBrush(Colors.Black);
                txtPersonNumber.Foreground = new SolidColorBrush(Colors.Black);
                isValidNumbOfPerson = true;
            }
            IsSaveReceptEnabled();
        }

        private void cmbReceptTyps_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbReceptTyps.SelectedItem == null)
            {
                cmbReceptTyps.BorderBrush = new SolidColorBrush(Colors.Red);
                cmbReceptTyps.Foreground = new SolidColorBrush(Colors.Red);
                isValidReceptType = false;
            }
            else
            {
                cmbReceptTyps.BorderBrush = new SolidColorBrush(Colors.Black);
                cmbReceptTyps.Foreground = new SolidColorBrush(Colors.Black);
                isValidReceptType = true;
            }
            IsSaveReceptEnabled();
        }

        private void txtReceptText_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtReceptText.Focus())
            {
                lblValidationReceptText.Visibility = Visibility.Visible;
                lblValidationReceptText.FontSize = 16;
                lblValidationReceptText.Foreground = new SolidColorBrush(Colors.Red);
                lblValidationReceptText.Content = "The text must contain \nat least 10 letters!";
            }

            string patternName = @"^([a-zA-Z0-9 ]{10,400})$";
            Match match = Regex.Match(txtReceptText.Text, patternName, RegexOptions.IgnoreCase);

            if (!match.Success)
            {
                txtReceptText.BorderBrush = new SolidColorBrush(Colors.Red);
                txtReceptText.Foreground = new SolidColorBrush(Colors.Red);
                isValidReceptText = false;
            }
            else
            {
                lblValidationReceptText.Visibility = Visibility.Hidden;
                txtReceptText.BorderBrush = new SolidColorBrush(Colors.Black);
                txtReceptText.Foreground = new SolidColorBrush(Colors.Black);
                isValidReceptText = true;
            }
            IsSaveReceptEnabled();
        }

        private void txtComponentName_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtComponentName.Focus())
            {
                lblValidationComponentName.Visibility = Visibility.Visible;
                lblValidationComponentName.FontSize = 16;
                lblValidationComponentName.Foreground = new SolidColorBrush(Colors.Red);
                lblValidationComponentName.Content = "The name can contain \n2-20 letters!";
            }

            string patternName = @"^([a-zA-Z ]{2,20})$";
            Match match = Regex.Match(txtComponentName.Text, patternName, RegexOptions.IgnoreCase);

            if (!match.Success)
            {
                txtComponentName.BorderBrush = new SolidColorBrush(Colors.Red);
                txtComponentName.Foreground = new SolidColorBrush(Colors.Red);
                isValidComponentName = false;
            }
            else
            {
                lblValidationComponentName.Visibility = Visibility.Hidden;
                txtComponentName.BorderBrush = new SolidColorBrush(Colors.Black);
                txtComponentName.Foreground = new SolidColorBrush(Colors.Black);
                isValidComponentName = true;
            }
            IsAddComponentEnabled();
        }

        private void txtComponentAmount_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtComponentAmount.Focus())
            {
                lblValidationComponentAmount.Visibility = Visibility.Visible;
                lblValidationComponentAmount.FontSize = 16;
                lblValidationComponentAmount.Foreground = new SolidColorBrush(Colors.Red);
                lblValidationComponentAmount.Content = "The field only allows \nnumbers greater than 0!";
            }

            string patterntxtComponentAmount = @"^([0-9]{1,3})$";
            Match match = Regex.Match(txtComponentAmount.Text, patterntxtComponentAmount, RegexOptions.IgnoreCase);

            bool isValid = int.TryParse(txtComponentAmount.Text, out int value);

            if (!match.Success || value <= 0)
            {
                txtComponentAmount.BorderBrush = new SolidColorBrush(Colors.Red);
                txtComponentAmount.Foreground = new SolidColorBrush(Colors.Red);
                isValidComponentAmount = false;
            }
            else
            {
                lblValidationComponentAmount.Visibility = Visibility.Hidden;
                txtComponentAmount.BorderBrush = new SolidColorBrush(Colors.Black);
                txtComponentAmount.Foreground = new SolidColorBrush(Colors.Black);
                isValidComponentAmount = true;
            }
            IsAddComponentEnabled();
        }
    }
}
