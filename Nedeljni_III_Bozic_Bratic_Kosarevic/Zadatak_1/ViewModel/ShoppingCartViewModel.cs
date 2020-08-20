using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zadatak_1.Models;

namespace Zadatak_1.ViewModel
{
    class ShoppingCartViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Components> AllComponents { get; set; }
        public ObservableCollection<Components> ShoppingCart { get; set; }

        public ShoppingCartViewModel()
        {

            FillList();
            ShoppingCart = new ObservableCollection<Components>();
        }


        public void FillList()
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ToString()))
            {
                SqlCommand query = new SqlCommand(@"exec Get_AllComponents", conn);
                conn.Open();
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(query);
                DataTable dataTable = new DataTable();
                sqlDataAdapter.Fill(dataTable);

                if (AllComponents == null)
                    AllComponents = new ObservableCollection<Components>();

                foreach (DataRow row in dataTable.Rows)
                {
                    Components r = new Components
                    {

                        ComponentId = int.Parse(row[0].ToString()),
                        ReceptId = int.Parse(row[1].ToString()),
                        ComponentName = row[2].ToString(),
                        ComponentAmount = int.Parse(row[3].ToString()),
                    };

                    AllComponents.Add(r);
                }
            }
        }

        public void AddToCart(Components components)
        {
            var componentToCart = ShoppingCart.ToList().Find(c => c.ComponentId == components.ComponentId);

            if (componentToCart == null)
            {
                Components newComponent = new Components();
                newComponent.ComponentId = components.ComponentId;
                newComponent.ReceptId = components.ReceptId;
                newComponent.ComponentName = components.ComponentName;
                newComponent.ComponentAmount = 1;
                ShoppingCart.Add(newComponent);
            }
            else
            {
                componentToCart.ComponentAmount++;
            }

            components.ComponentAmount--;
            if (components.ComponentAmount == 0)
            {
                AllComponents.Remove(components);
            }

            OnPropertyChanged("AllComponents");

        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
