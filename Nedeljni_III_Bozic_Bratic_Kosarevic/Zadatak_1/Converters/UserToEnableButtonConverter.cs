using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using Zadatak_1.View;

namespace Zadatak_1.Converters
{
    public class UserToEnableButtonConverter: IMultiValueConverter
    {     
        public object Convert(object[] values, Type targetType,
            object parameter, System.Globalization.CultureInfo culture)
        {
            if(values==null || values.Length!=2)
            {
                return System.Windows.Visibility.Hidden;
            }

            string username = values[0].ToString();
            int userId =(int)values[1]; 

            if(username=="Admin" || userId==LoginWindow.CurrentUser.UserId)
            {
                return System.Windows.Visibility.Visible;
            }
            return System.Windows.Visibility.Hidden;
        }
        public object[] ConvertBack(object value, Type[] targetTypes,
            object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
        
    }
}
