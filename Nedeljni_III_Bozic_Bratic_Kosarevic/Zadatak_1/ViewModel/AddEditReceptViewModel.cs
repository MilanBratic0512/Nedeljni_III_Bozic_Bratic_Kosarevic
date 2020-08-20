using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Ink;
using System.Windows.Input;
using Zadatak_1.Commands;
using Zadatak_1.Model;
using Zadatak_1.Models;
using Zadatak_1.Service;
using Zadatak_1.View;

namespace Zadatak_1.ViewModel
{
    public class AddEditReceptViewModel:ViewModelBase
    {
        AddEditReceptView addEditReceptView;
        ServiceCode service = new ServiceCode();
        #region Constructor
        public AddEditReceptViewModel(AddEditReceptView addEditReceptViewOpen, bool isForEdit)
        {
            this.isForEdit = isForEdit;
            addEditReceptView = addEditReceptViewOpen;
            ReceptTypsList = new ObservableCollection<ReceptType>(service.GettAllTypes());            
        }
        #endregion

        #region Properties
        private bool isForEdit;
        public bool IsForEdit
        {
            get
            {
                return isForEdit;
            }
        }
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
        #endregion

        #region Commands
        private ICommand save;

        public ICommand Save
        {
            get
            {
                if (save == null)
                {
                    save = new RelayCommand(param => SaveExecute());
                }
                return save;
            }
        }
        public void SaveExecute()
        {
            try
            {                
                Recept.TypeId = selectedReceptTyps.TypeID;
                Recept.UserId = LoginWindow.CurrentUser.UserId;
                Recept.Author = LoginWindow.CurrentUser.FullName;
                Recept.CreationDate = DateTime.Now;
                if (Recept.ReceptId == 0)
                {
                    int receptId = service.AddRecept(Recept);
                    if (receptId != 0)
                    {
                        MessageBox.Show("You have successfully added new recept");
                        components.ReceptId = receptId;                        
                    }                    
                }
                else
                {
                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        #endregion
    }
}
