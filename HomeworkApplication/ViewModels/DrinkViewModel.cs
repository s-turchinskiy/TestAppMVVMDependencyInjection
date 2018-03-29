using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Providers;

namespace HomeworkApplication.ViewModels
{
    class DrinkViewModel: ViewModelBase
    {
        public Drink Drink;

        public DrinkViewModel(Drink drink)
        {
            this.Drink = drink;
        }

        public string Name
        {
            get { return Drink.Name; }
            set
            {
                Drink.Name = value;
                OnPropertyChanged("Name");
            }
        }

        public int Price
        {
            get { return Drink.Price; }
            set
            {
                Drink.Price = value;
                OnPropertyChanged("Price");
            }
        }

        public int Count
        {
            get { return Drink.Count; }
            set
            {
                Drink.Count = value;
                OnPropertyChanged("Count");
            }
        }
    }
}
