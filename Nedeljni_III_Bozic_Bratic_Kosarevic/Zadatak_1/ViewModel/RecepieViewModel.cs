using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Zadatak_1.Models;
using Zadatak_1.View;

namespace Zadatak_1.ViewModel
{
    public class RecepieViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Recept> Recepies { get; set; }
        public ObservableCollection<Components> UserHaveComponents { get; set; }
        public ObservableCollection<Components> MissingComponents { get; set; }

        public RecepieViewModel()
        {
            Recepie = new Recept();
            TypeName = new List<string>();
            Recepies = new ObservableCollection<Recept>();
            UserHaveComponents = new ObservableCollection<Components>();
            MissingComponents = new ObservableCollection<Components>();
            RecepieName = "";
            Components = "";
            FillList();
        }

        private Recept recepie;

        public Recept Recepie
        {
            get { return recepie; }
            set
            {
                if (recepie != value)
                {
                    recepie = value;
                    OnPropertyChanged("Recepie");
                }
            }
        }

        private string recepieName;

        public string RecepieName
        {
            get { return recepieName; }
            set
            {
                if (recepieName != value)
                {
                    recepieName = value;
                    OnPropertyChanged("RecepieName");
                }
            }
        }

        private string selectedType;

        public string SelectedType
        {
            get { return selectedType; }
            set
            {
                if (selectedType != value)
                {
                    selectedType = value;
                    OnPropertyChanged("SelectedType");
                }
            }
        }

        private List<string> typeName;

        public List<string> TypeName
        {
            get { return typeName; }
            set
            {
                if (typeName != value)
                {
                    typeName = value;
                    OnPropertyChanged("TypeName");
                }
            }
        }

        private string components;

        public string Components
        {
            get { return components; }
            set
            {
                if (components != value)
                {
                    components = value;
                    OnPropertyChanged("Components");
                }
            }
        }

        public void FillList()
        {
            using (SqlConnection conn1 = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ToString()))
            {
                SqlCommand query1 = new SqlCommand(@"select * from tblType;", conn1);
                conn1.Open();
                SqlDataAdapter sqlDataAdapter1 = new SqlDataAdapter(query1);
                DataTable dataTable1 = new DataTable();
                sqlDataAdapter1.Fill(dataTable1);

                foreach (DataRow row1 in dataTable1.Rows)
                {
                    typeName.Add(row1[1].ToString());
                }
            }

            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ToString()))
            {
                SqlCommand query = new SqlCommand(@"exec Get_AllRecepts", conn);
                conn.Open();
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(query);
                DataTable dataTable = new DataTable();
                sqlDataAdapter.Fill(dataTable);

                if (Recepies == null)
                    Recepies = new ObservableCollection<Recept>();

                foreach (DataRow row in dataTable.Rows)
                {
                    Recept r = new Recept
                    {
                        ReceptId = int.Parse(row[0].ToString()),
                        UserId = int.Parse(row[1].ToString()),
                        TypeId = int.Parse(row[2].ToString()),
                        ReceptName = row[3].ToString(),
                        PersonNumber = int.Parse(row[4].ToString()),
                        Author = row[5].ToString(),
                        ReceptText = row[6].ToString(),
                        CreationDate = DateTime.Parse(row[7].ToString()),
                        ReceptType = row[9].ToString(),
                    };

                    if (LoginWindow.CurrentUser.UserId == r.UserId)
                    {
                        r.CanEdit = true;
                    }
                    if (LoginWindow.CurrentUser.Username == "Admin")
                    {
                        r.CanEdit = true;
                        r.CanDelete = true;
                    }

                    using (SqlConnection conn1 = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ToString()))
                    {
                        SqlCommand query1 = new SqlCommand(@"exec Get_AllReceptsComponentsNumber @ReceptID = @Id;", conn1);
                        query1.Parameters.AddWithValue("@Id", r.ReceptId);
                        conn1.Open();
                        SqlDataAdapter sqlDataAdapter1 = new SqlDataAdapter(query1);
                        DataTable dataTable1 = new DataTable();
                        sqlDataAdapter1.Fill(dataTable1);

                        foreach (DataRow row1 in dataTable1.Rows)
                        {
                            r.ComponentsNumber = int.Parse(row1[0].ToString());
                        }
                    }

                    Recepies.Add(r);
                }
            }
        }

        public void DetilsRecept()
        {
            try
            {
                Recept detailsRecept = new Recept();
                detailsRecept.ReceptId = recepie.ReceptId;
                detailsRecept.UserId = recepie.UserId;
                detailsRecept.TypeId = recepie.TypeId;
                detailsRecept.ReceptName = recepie.ReceptName;
                detailsRecept.PersonNumber = recepie.PersonNumber;
                detailsRecept.Author = recepie.Author;
                detailsRecept.ReceptText = recepie.ReceptText;

                ReceptDetailsView receptDetailsView = new ReceptDetailsView(detailsRecept);
                receptDetailsView.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public void DeleteRecepie()
        {
            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ToString()))
            {
                var cmd = new SqlCommand(@"Delete from tblComponents where ReceptID=@RecepieID; Delete from tblRecept where ReceptID=@RecepieID;", conn);
                cmd.Parameters.AddWithValue("@RecepieID", recepie.ReceptId);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
                MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Recepie successfully deleted.", "Notification");
                Recepies.Remove(recepie);
            }
        }

        public void SearchByRecepieTitle()
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ToString()))
            {
                SqlCommand query = new SqlCommand(@"exec Get_AllReceptsByName @RecepieName", conn);
                query.Parameters.AddWithValue("@RecepieName", recepieName);
                conn.Open();
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(query);
                DataTable dataTable = new DataTable();
                sqlDataAdapter.Fill(dataTable);

                Recepies.Clear();

                foreach (DataRow row in dataTable.Rows)
                {
                    Recept r = new Recept
                    {
                        ReceptId = int.Parse(row[0].ToString()),
                        UserId = int.Parse(row[1].ToString()),
                        TypeId = int.Parse(row[2].ToString()),
                        ReceptName = row[3].ToString(),
                        PersonNumber = int.Parse(row[4].ToString()),
                        Author = row[5].ToString(),
                        ReceptText = row[6].ToString(),
                        CreationDate = DateTime.Parse(row[7].ToString()),
                        ReceptType = row[9].ToString(),
                    };

                    if (LoginWindow.CurrentUser.UserId == r.UserId)
                    {
                        r.CanEdit = true;
                    }
                    if (LoginWindow.CurrentUser.Username == "Admin")
                    {
                        r.CanEdit = true;
                        r.CanDelete = true;
                    }

                    using (SqlConnection conn1 = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ToString()))
                    {
                        SqlCommand query1 = new SqlCommand(@"exec Get_AllReceptsComponentsNumber @ReceptID = @Id;", conn1);
                        query1.Parameters.AddWithValue("@Id", r.ReceptId);
                        conn1.Open();
                        SqlDataAdapter sqlDataAdapter1 = new SqlDataAdapter(query1);
                        DataTable dataTable1 = new DataTable();
                        sqlDataAdapter1.Fill(dataTable1);

                        foreach (DataRow row1 in dataTable1.Rows)
                        {
                            r.ComponentsNumber = int.Parse(row1[0].ToString());
                        }
                    }

                    Recepies.Add(r);
                }
            }
        }

        public void SearchByRecepieType()
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ToString()))
            {
                SqlCommand query = new SqlCommand(@"exec Get_AllReceptsByType @TypeName", conn);
                query.Parameters.AddWithValue("@TypeName", selectedType);
                conn.Open();
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(query);
                DataTable dataTable = new DataTable();
                sqlDataAdapter.Fill(dataTable);

                Recepies.Clear();

                foreach (DataRow row in dataTable.Rows)
                {
                    Recept r = new Recept
                    {
                        ReceptId = int.Parse(row[0].ToString()),
                        UserId = int.Parse(row[1].ToString()),
                        TypeId = int.Parse(row[2].ToString()),
                        ReceptName = row[3].ToString(),
                        PersonNumber = int.Parse(row[4].ToString()),
                        Author = row[5].ToString(),
                        ReceptText = row[6].ToString(),
                        CreationDate = DateTime.Parse(row[7].ToString()),
                        ReceptType = row[9].ToString(),
                    };

                    if (LoginWindow.CurrentUser.UserId == r.UserId)
                    {
                        r.CanEdit = true;
                    }
                    if (LoginWindow.CurrentUser.Username == "Admin")
                    {
                        r.CanEdit = true;
                        r.CanDelete = true;
                    }

                    using (SqlConnection conn1 = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ToString()))
                    {
                        SqlCommand query1 = new SqlCommand(@"exec Get_AllReceptsComponentsNumber @ReceptID = @Id;", conn1);
                        query1.Parameters.AddWithValue("@Id", r.ReceptId);
                        conn1.Open();
                        SqlDataAdapter sqlDataAdapter1 = new SqlDataAdapter(query1);
                        DataTable dataTable1 = new DataTable();
                        sqlDataAdapter1.Fill(dataTable1);

                        foreach (DataRow row1 in dataTable1.Rows)
                        {
                            r.ComponentsNumber = int.Parse(row1[0].ToString());
                        }
                    }

                    Recepies.Add(r);
                }
            }
        }

        public void SearchByRecepieComponent()
        {
            string[] words = components.Split(' ');

            Recepies.Clear();

            foreach (string s in words)
            {
                if (s == "") continue;

                using (SqlConnection conn1 = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ToString()))
                {
                    SqlCommand query1 = new SqlCommand(@"exec Get_AllComponentsByInput @ComponentName;", conn1);
                    query1.Parameters.AddWithValue("@ComponentName", s);
                    conn1.Open();
                    SqlDataAdapter sqlDataAdapter1 = new SqlDataAdapter(query1);
                    DataTable dataTable1 = new DataTable();
                    sqlDataAdapter1.Fill(dataTable1);

                    foreach (DataRow row1 in dataTable1.Rows)
                    {
                        Components c = new Components
                        {
                            ComponentId = int.Parse(row1[0].ToString()),
                            ReceptId = int.Parse(row1[1].ToString()),
                            ComponentName = row1[2].ToString(),
                            ComponentAmount = int.Parse(row1[3].ToString())
                        };

                        UserHaveComponents.Add(c);
                    }
                }

                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ToString()))
                {
                    SqlCommand query = new SqlCommand(@"exec Get_AllReceptsByComponents @Components", conn);
                    query.Parameters.AddWithValue("@Components", s);
                    conn.Open();
                    SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(query);
                    DataTable dataTable = new DataTable();
                    sqlDataAdapter.Fill(dataTable);

                    foreach (DataRow row in dataTable.Rows)
                    {
                        Recept r = new Recept
                        {
                            ReceptId = int.Parse(row[0].ToString()),
                            UserId = int.Parse(row[1].ToString()),
                            TypeId = int.Parse(row[2].ToString()),
                            ReceptName = row[3].ToString(),
                            PersonNumber = int.Parse(row[4].ToString()),
                            Author = row[5].ToString(),
                            ReceptText = row[6].ToString(),
                            CreationDate = DateTime.Parse(row[7].ToString()),
                            ReceptType = row[9].ToString(),
                        };

                        if (LoginWindow.CurrentUser.UserId == r.UserId)
                        {
                            r.CanEdit = true;
                        }
                        if (LoginWindow.CurrentUser.Username == "Admin")
                        {
                            r.CanEdit = true;
                            r.CanDelete = true;
                        }

                        using (SqlConnection conn1 = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ToString()))
                        {
                            SqlCommand query1 = new SqlCommand(@"exec Get_AllReceptsComponentsNumber @ReceptID = @Id;", conn1);
                            query1.Parameters.AddWithValue("@Id", r.ReceptId);
                            conn1.Open();
                            SqlDataAdapter sqlDataAdapter1 = new SqlDataAdapter(query1);
                            DataTable dataTable1 = new DataTable();
                            sqlDataAdapter1.Fill(dataTable1);

                            foreach (DataRow row1 in dataTable1.Rows)
                            {
                                r.ComponentsNumber = int.Parse(row1[0].ToString());
                            }
                        }

                        List<int> ids = new List<int>();

                        foreach (Recept i in Recepies)
                        {
                            ids.Add(i.ReceptId);
                        }

                        if (!ids.Contains(r.ReceptId))
                        {
                            Recepies.Add(r);
                        }
                    }
                }
            }
        }

        public void FilterRemainingComponets()
        {
            using (SqlConnection conn1 = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ToString()))
            {
                SqlCommand query1 = new SqlCommand(@"exec Get_AllComponentsByReceptId @ReceptID;", conn1);
                query1.Parameters.AddWithValue("@ReceptID", recepie.ReceptId);
                conn1.Open();
                SqlDataAdapter sqlDataAdapter1 = new SqlDataAdapter(query1);
                DataTable dataTable1 = new DataTable();
                sqlDataAdapter1.Fill(dataTable1);

                foreach (DataRow row1 in dataTable1.Rows)
                {
                    Components c = new Components
                    {
                        ComponentId = int.Parse(row1[0].ToString()),
                        ReceptId = int.Parse(row1[1].ToString()),
                        ComponentName = row1[2].ToString(),
                        ComponentAmount = int.Parse(row1[3].ToString())
                    };

                    bool contains = false;

                    foreach (Components item in UserHaveComponents)
                    {
                        if(item.ComponentId == c.ComponentId)
                        {
                            contains = true;
                            break;
                        }
                    }

                    if(!contains)
                    {
                        MissingComponents.Add(c);
                    }
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
