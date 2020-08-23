using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Zadatak_1.Validations
{
    static class SearchValidation
    {

        public static bool Validate(string s)
        {
            if (s.Length < 3)
            {
                MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("You must input at least 3 characters, please try again.", "Notification");
                return false;
            }
            if (CheckSpecialCharacters(s))
            {
                MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Search input can not contain any special characters, please try again.", "Notification");
                return false;
            }

            return true;
        }

        public static bool CheckSpecialCharacters(string s)
        {
            string specialChar = @"\|!#$%&/()=?»«@£§€{}.-;'<>_,";
            foreach (var item in specialChar)
            {
                if (s.Contains(item))
                    return true;
            }

            return false;
        }
    }
}
