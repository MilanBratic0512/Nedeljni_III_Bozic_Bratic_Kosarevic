﻿using System;
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
    class RecepieViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Recept> Recepies { get; set; }

        public RecepieViewModel()
        {
            FillList();
            Recepie = new Recept();
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

        public void FillList()
        {
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

                    Recepies.Add(r);
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
