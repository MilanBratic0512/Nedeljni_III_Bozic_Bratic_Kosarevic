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
using Zadatak_1.Models;
using Zadatak_1.Service;

namespace Zadatak_1.ViewModel
{
    class ShoppingCartViewModel : INotifyPropertyChanged
    {
        ServiceCode service = new ServiceCode();
        public ObservableCollection<Components> AllComponents { get; set; }
        public ObservableCollection<Components> InitialComponets { get; set; }
        public ObservableCollection<Components> TempList { get; set; }
        public ObservableCollection<Components> ShoppingCart { get; set; }
        private List<Components> ComponentsForRemove;

        public ShoppingCartViewModel()
        {
            ComponentsForRemove = new List<Components>();
            AllComponents = new ObservableCollection<Components>();
            ShoppingCart = new ObservableCollection<Components>();
        }

        private Component component;

        public Component Component
        {
            get { return component; }
            set
            {
                if (component != value)
                {
                    component = value;
                    OnPropertyChanged("Component");
                }
            }
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

        private int numberOfPersonsInput;

        public int NumberOfPersonsInput
        {
            get { return numberOfPersonsInput; }
            set
            {
                if (numberOfPersonsInput != value)
                {
                    numberOfPersonsInput = value;
                    OnPropertyChanged("NumberOfPersonsInput");
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
                ComponentsForRemove.Add(components);
            }

            OnPropertyChanged("AllComponents");

        }
        public void WriteToTheFile()
        {
            Components[] array = ShoppingCart.ToArray();
            string[] lines = new string[array.Length];
            for (int i = 0; i < lines.Length; i++)
            {
                lines[i] = "Component name: " + array[i].ComponentName + ". Amount: " + array[i].ComponentAmount;
            }

            using (StreamWriter file = new StreamWriter(@"..\\..\Files\ShoppingCart.txt"))
            {
                foreach (string line in lines)
                {
                    file.WriteLine(line);
                }
                file.WriteLine("Time: " + DateTime.Now.ToString());
            }
        }
        public void UpdateDatabase()
        {
            foreach (Components components in AllComponents)
            {
                service.UpdateComponent(components);
            }

            foreach (Components componentsToRemove in ComponentsForRemove)
            {
                service.DeleteComponent(componentsToRemove.ComponentId);
            }

        }

        public void CalculateComponent(ObservableCollection<Components> list, int numberOfPersonByRecept, int numberOfPersonsInput)
        {
            ObservableCollection<Components> result = new ObservableCollection<Components>();
            var temp = list;

            double numCalculation = (double)numberOfPersonsInput / (double)numberOfPersonByRecept;

            for (int i = 0; i < temp.Count; i++)
            {
                temp[i].ComponentAmount = (int)(temp[i].ComponentAmount * numCalculation);
                result.Add(temp[i]); 
            }

            AllComponents = result;
            OnPropertyChanged("AllComponents");
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
