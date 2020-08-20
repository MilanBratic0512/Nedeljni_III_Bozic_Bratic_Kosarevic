using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Zadatak_1.Model;

namespace Zadatak_1.Validations
{
    static class UserValidation
    {

        public static bool Validate(string username, string password)
        {
            if (username == null || username == "")
            {
                MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("incorrect username, try again.", "Notification");
                return false;
            }
            else
            {
                using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ToString()))
                {
                    var cmd = new SqlCommand(@"select Username from tblUser where Username = @Username", conn);
                    cmd.Parameters.AddWithValue("@Username", username);
                    conn.Open();
                    SqlDataReader reader1 = cmd.ExecuteReader();
                    while (reader1.Read())
                    {
                        if (reader1[0].ToString().ToLower() == username.ToLower())
                        {
                            MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Incorrect password, please try again.", "Notification");
                            return false;
                        }
                    }
                    reader1.Close();
                    conn.Close();
                }
            }
            if (password.Length < 5)
            {
                MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Password must contain at least 5 characters, try again.", "Notification");
                return false;
            }

            return true;
        }

    }
}
