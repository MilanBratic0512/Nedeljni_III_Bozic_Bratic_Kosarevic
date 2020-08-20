using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zadatak_1.Model;
using Zadatak_1.Models;
using Zadatak_1.View;

namespace Zadatak_1.ViewModel
{
    public class AddEditReceptViewModel:ViewModelBase
    {
        AddEditReceptView addEditReceptView;

        #region Constructor
        public AddEditReceptViewModel(AddEditReceptView addEditReceptViewOpen, bool isForEdit)
        {
            this.isForEdit = isForEdit;
            addEditReceptView = addEditReceptViewOpen;            
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

        private Recept recept;
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

        private Components components;
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
    }
}
