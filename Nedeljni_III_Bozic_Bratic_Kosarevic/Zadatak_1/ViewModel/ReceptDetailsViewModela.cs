using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Zadatak_1.Model;
using Zadatak_1.Models;
using Zadatak_1.Service;
using Zadatak_1.View;

namespace Zadatak_1.ViewModel
{
    public class ReceptDetailsViewModela:ViewModelBase
    {
        ReceptDetailsView detailsView;
        ServiceCode service = new ServiceCode();
        
        #region Constructor
        public ReceptDetailsViewModela(Recept recept, ReceptDetailsView detailsViewOpen)
        {
            this.recept = recept;
            detailsView = detailsViewOpen;
            User = LoginWindow.CurrentUser;
            ReceptTypsList = new ObservableCollection<ReceptType>(service.GettAllTypes());
            SelectedReceptTyps = ReceptTypsList.FirstOrDefault(p => p.TypeID == recept.TypeId);
            TemporaryComponentList = new ObservableCollection<Components>(service.GettAllComponentsByReceptId(recept.ReceptId));
        }
        #endregion        
        #region Properties
        private ObservableCollection<ReceptType> receptTypsList;
        public ObservableCollection<ReceptType> ReceptTypsList
        {
            get
            {
                return receptTypsList;
            }
            set
            {
                receptTypsList = value;
                OnPropertyChanged("ReceptTypsList");
            }
        }

        private ReceptType selectedReceptTyps;
        public ReceptType SelectedReceptTyps
        {
            get
            {
                return selectedReceptTyps;
            }
            set
            {
                selectedReceptTyps = value;
                OnPropertyChanged("SelectedReceptTyps");
            }
        }

        private User user;
        public User User
        {
            get
            {
                return user;
            }
            set
            {
                user = value;
                OnPropertyChanged("User");
            }
        }

        //private Components selectedComponents;
        //public Components SelectedComponents
        //{
        //    get
        //    {
        //        return selectedComponents;
        //    }
        //    set
        //    {
        //        selectedComponents = value;
        //        OnPropertyChanged("SelectedComponents");
        //    }
        //}

        private Recept recept = new Recept();
        public Recept Recept
        {
            get
            {
                return recept;
            }
            set
            {
                recept = value;
                OnPropertyChanged("Recept");
            }
        }

        private Components components = new Components();
        public Components Components
        {
            get
            {
                return components;
            }
            set
            {
                components = value;
                OnPropertyChanged("Components");
            }
        }

        //private string componentName;
        //public string ComponentName
        //{
        //    get
        //    {
        //        return componentName;
        //    }
        //    set
        //    {
        //        componentName = value;
        //        OnPropertyChanged("ComponentName");
        //    }
        //}

        //private int componentAmount;
        //public int ComponentAmount
        //{
        //    get
        //    {
        //        return componentAmount;
        //    }
        //    set
        //    {
        //        componentAmount = value;
        //        OnPropertyChanged("ComponentAmount");
        //    }
        //}

        private ObservableCollection<Components> temporaryComponentList;
        public ObservableCollection<Components> TemporaryComponentList
        {
            get
            {
                return temporaryComponentList;
            }
            set
            {
                temporaryComponentList = value;
                OnPropertyChanged("TemporaryComponentList");
            }
        }
        #endregion
        #region Commands
        public void EditRecept()
        {
            try
            {
                Recept editRecept = new Recept();
                editRecept.ReceptId = recept.ReceptId;
                editRecept.UserId = recept.UserId;
                editRecept.TypeId = recept.TypeId;
                editRecept.ReceptName = recept.ReceptName;
                editRecept.PersonNumber = recept.PersonNumber;
                editRecept.Author = recept.Author;
                editRecept.ReceptText = recept.ReceptText;

                AddEditReceptView addEditReceptWindow = new AddEditReceptView(editRecept, true);
                addEditReceptWindow.Show();
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
                cmd.Parameters.AddWithValue("@RecepieID", recept.ReceptId);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
                MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Recepie successfully deleted.", "Notification");                
            }
        }

        #endregion
    }
}
