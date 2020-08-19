using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
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
using Zadatak_1.Model;
using Zadatak_1.Validations;

namespace Zadatak_1.View
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        public static User CurrentUser = new User();

        private void BtnLogin(object sender, RoutedEventArgs e)
        {

            CurrentUser = null;

            //Inserted value in password field is being converted into enrypted verson for latter matching with database version.
            byte[] data = System.Text.Encoding.ASCII.GetBytes(txtPassword.Password);
            data = new System.Security.Cryptography.SHA256Managed().ComputeHash(data);
            String hash = System.Text.Encoding.ASCII.GetString(data);

            SqlConnection sqlCon = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ToString());
            //User is extracted from the database matching inserted paramaters Username and Password.
            SqlCommand query = new SqlCommand("SELECT * FROM tblUser WHERE Username=@Username AND Pasword=@Password", sqlCon);
            query.CommandType = CommandType.Text;
            query.Parameters.AddWithValue("@Username", txtUsername.Text);
            query.Parameters.AddWithValue("@Password", hash);
            sqlCon.Open();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(query);
            DataTable dataTable = new DataTable();
            sqlDataAdapter.Fill(dataTable);

            foreach (DataRow row in dataTable.Rows)
            {
                CurrentUser = new User
                {
                    UserId = int.Parse(row[0].ToString()),
                    FullName = row[1].ToString(),
                    Username = row[2].ToString(),
                    Password = row[3].ToString()
                };
            }
            sqlCon.Close();

            if(CurrentUser != null)
            {
                RecepieWindow window = new RecepieWindow();
                window.Show();
                Close();
                return;
            }
            else
            {
                if (!UserValidation.Validate(txtUsername.Text, txtPassword.Password)) return;

                data = System.Text.Encoding.ASCII.GetBytes(txtPassword.Password);
                data = new System.Security.Cryptography.SHA256Managed().ComputeHash(data);
                hash = System.Text.Encoding.ASCII.GetString(data);

                using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ToString()))
                {
                    var cmd = new SqlCommand(@"insert into tblUser values (@FullName, @Username, @Password);", conn);
                    cmd.Parameters.AddWithValue("@FullName", txtUsername.Text);
                    cmd.Parameters.AddWithValue("@Username", txtUsername.Text);
                    cmd.Parameters.AddWithValue("@Password", hash);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    MessageBoxResult messageBoxResult1 = System.Windows.MessageBox.Show("New user successfully created.", "Notification");
                }

                sqlCon = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ToString());
                //User is extracted from the database matching inserted paramaters Username and Password.
                query = new SqlCommand("SELECT * FROM tblUser WHERE Username=@Username AND Pasword=@Password", sqlCon);
                query.CommandType = CommandType.Text;
                query.Parameters.AddWithValue("@Username", txtUsername.Text);
                query.Parameters.AddWithValue("@Password", hash);
                sqlCon.Open();
                sqlDataAdapter = new SqlDataAdapter(query);
                dataTable = new DataTable();
                sqlDataAdapter.Fill(dataTable);

                foreach (DataRow row in dataTable.Rows)
                {
                    CurrentUser = new User
                    {
                        UserId = int.Parse(row[0].ToString()),
                        FullName = row[1].ToString(),
                        Username = row[2].ToString(),
                        Password = row[3].ToString()
                    };
                }
                sqlCon.Close();

                if (CurrentUser != null)
                {
                    RecepieWindow window = new RecepieWindow();
                    window.Show();
                    Close();
                    return;
                }
            }
        }
    }
}
