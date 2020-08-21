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
        public AddEditReceptViewModel(Recept recept, AddEditReceptView addEditReceptViewOpen, bool isForEdit)
        {
            this.isForEdit = isForEdit;
            this.recept = recept;
            addEditReceptView = addEditReceptViewOpen;
            ReceptTypsList = new ObservableCollection<ReceptType>(service.GettAllTypes());
            SelectedReceptTyps = ReceptTypsList.FirstOrDefault(p => p.TypeID == recept.TypeId);
            TemporaryComponentList= new ObservableCollection<Components>(service.GettAllComponentsByReceptId(recept.ReceptId));
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

        private Components selectedComponents;
        public Components SelectedComponents
        {
            get
            {
                return selectedComponents;
            }
            set
            {
                selectedComponents = value;
                OnPropertyChanged("SelectedComponents");
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

        private string componentName;
        public string ComponentName
        {
            get
            {
                return componentName;
            }
            set
            {
                componentName = value;
                OnPropertyChanged("ComponentName");
            }
        }

        private int componentAmount;
        public int ComponentAmount
        {
            get
            {
                return componentAmount;
            }
            set
            {
                componentAmount = value;
                OnPropertyChanged("ComponentAmount");
            }
        }

        private ObservableCollection<Components> temporaryComponentList = new ObservableCollection<Components>();
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

        public void AddAddComponentToList()
        {
            Components component = new Components();
            component.ComponentName = ComponentName;
            component.ComponentAmount = ComponentAmount;
            try
            {
                temporaryComponentList.Add(component);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public void DeleteComponentExecute()
        {
            try
            {
                temporaryComponentList.Remove(selectedComponents);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

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
                        foreach (Components component in TemporaryComponentList)
                        {
                            component.ReceptId = receptId;
                        }

                        foreach(Components component in TemporaryComponentList)
                        {
                            service.AddComponent(component);
                        }
                        MessageBox.Show("You have successfully added new recept");
                        RecepieWindow recepieWindowWindow = new RecepieWindow();
                        recepieWindowWindow.Show();
                        addEditReceptView.Close();
                    }                    
                }
                else
                {
                    if (service.UpdateRecept(Recept))
                    {
                        foreach (Components component in TemporaryComponentList)
                        {
                            if (component.ReceptId == 0)
                            {
                                component.ReceptId = recept.ReceptId;
                            }
                        }

                        if(components.ComponentId==0)
                        {
                            foreach (Components component in TemporaryComponentList)
                            {
                                service.AddComponent(component);
                            }
                        }
                        else
                        {
                            foreach (Components component in TemporaryComponentList)
                            {
                                service.UpdateComponent(component);
                            }
                        }
                        

                        
                        MessageBox.Show("You have successfully changed recept");
                        RecepieWindow recepieWindowWindow = new RecepieWindow();
                        recepieWindowWindow.Show();
                        addEditReceptView.Close();
                    }
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
