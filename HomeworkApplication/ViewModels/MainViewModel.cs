using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using Providers;
using System.Collections.ObjectModel;

using Microsoft.Practices.Unity;
using System.Windows.Controls;

namespace HomeworkApplication.ViewModels
{
    class MainViewModel : ViewModelBase
    {
        public ICoinValuesProvider CoinsProvider { get; set; }
        public IDrinksProvider DrinksProvider { get; set; }

        private int _balance = 0;

        public int Balance
        {
            get { return _balance; }
            set
            {
                _balance = value;
                OnPropertyChanged("Balance");
            }
        }

        public event EventHandler Click;

        public ICommand ClickRefillCommand { get; set; }

        public ICommand ClickChangeCommand { get; set; }
        public ICommand ClickBuyCommand { get; set; }

        public ICommand OnPropertyBalance { get; set; }
           
        //var varDrinkList = from t in teachers orderby t.name select t;
        //List<Teacher> teachersSorted = varDrinkList.ToList<DrinkViewModel>();  
        
        public ObservableCollection<DrinkViewModel> DrinkList { get; set; }
        public ObservableCollection<CoinViewModel> CoinList { get; set; }

        //static public ObservableCollection<CoinViewModel> FilterCoinList {
        //    get 
        //    {
        //        CoinViewModel filteredPerson = (from p in CoinList where p.Blocked == false select p).FirstOrDefault();
        //        CoinList.Any(s => s.Blocked == false);
        //        return filteredPerson; 
                    
        //    }
        //    set; }
        //CoinViewModel filteredPerson = (from p in CoinList where p.Blocked == false select p).FirstOrDefault();

        public ObservableCollection<CoinViewModel> FilterCoinList { get; set;}

        private CoinViewModel _selectedCoin;
        public CoinViewModel SelectedCoin
        {
            get { return _selectedCoin; }
            set
            {
                _selectedCoin = value;
                OnPropertyChanged("SelectedCoin");
            }
        }

        private DrinkViewModel _selectedDrink;
        public DrinkViewModel SelectedDrink
        {
            get { return _selectedDrink; }
            set
            {
                _selectedDrink = value;
                OnPropertyChanged("SelectedDrink");
            }
        }

        

        public void ButtonBuy_OnClick(object sender, RoutedEventArgs e)
        {
            MakeBuy();
        }
        private void AddBalance()
        {
            Balance = Balance + SelectedCoin.CoinValue;
        }

        private void MakeBuy()
        {
            if (SelectedDrink.Count==0)
            {
                MessageBox.Show("Данный напиток закончился!");
                return;
            }

            if (Balance >= SelectedDrink.Price)
            { 
                Balance = Balance - SelectedDrink.Price;
            }
            else
            {
                MessageBox.Show("Недостаточно денег для покупки!");
                return;
            }

            SelectedDrink.Count = SelectedDrink.Count - 1;
        }

        private void GetChange()
        {
             Balance = 0;   
        }

        public void OnWindowClosing(object sender, CancelEventArgs e)
        {
            List<Drink> DrinkListBase = new List<Drink>();
            foreach (DrinkViewModel drink in DrinkList)
            {
                DrinkListBase.Add(new Drink(drink.Name,drink.Price,drink.Count));
            }
            RecordDrinksInXMLFile.Record(DrinkListBase);
        }

        private void ChangeBackgroundDrinkList()
        {
            //Balance = Balance + SelectedCoin.CoinValue;
        }

        public MainViewModel()//List<Drink> drink)
        {

            DrinkList = new ObservableCollection<DrinkViewModel>();
            CoinList = new ObservableCollection<CoinViewModel>();

            ClickRefillCommand = new RelayCommand(arg => AddBalance());
            ClickBuyCommand = new RelayCommand(arg => MakeBuy());
            ClickChangeCommand = new RelayCommand(arg => GetChange());
            OnPropertyBalance = new RelayCommand(arg => ChangeBackgroundDrinkList());

            IUnityContainer container = DependenciesInjection.CreateContainer();
            CoinsProvider = container.Resolve<ICoinValuesProvider>();
            DrinksProvider = container.Resolve<IDrinksProvider>();

            using (ICoinValuesProvider coinsProvider = container.Resolve<ICoinValuesProvider>())
            {
                foreach (Coin coin in coinsProvider.GetCoins())
                {
                    CoinList.Add(new CoinViewModel(coin));
                }
            }
            CoinList = new ObservableCollection<CoinViewModel>(CoinList.OrderBy(i => i.CoinValue));

            using (IDrinksProvider drinksProvider = container.Resolve<IDrinksProvider>())
            {
                foreach (Drink drink in drinksProvider.GetDrinks())
                {
                    DrinkList.Add(new DrinkViewModel(drink));
                }
            }
            DrinkList = new ObservableCollection<DrinkViewModel>(DrinkList.OrderBy(i => i.Price));
            //DrinkList = (from c in DrinkList where c.Price == 8 select c).FirstOrDefault();

            var selectedTeams = from t in DrinkList // определяем каждый объект из teams как t
                    where t.Price>=Balance //фильтрация по критерию
                    orderby t.Price  // упорядочиваем по возрастанию
                    select t; // выбираем объект

            DrinkList = new ObservableCollection<DrinkViewModel>(selectedTeams);
                
            // using(new x) { } то же самое, что try { new c } finnaly { x.Dispose(); }

            // Console.ReadKey();
            ////

            //Drink _drink = new Drink("Кофе",10, 1 );
            // DrinkList.Add(new DrinkViewModel(_drink));

            // DrinkList = new ObservableCollection<DrinkViewModel>(drink.Select(b => new DrinkViewModel(b)));

        }



    }

    public abstract class ButtonBase : ContentControl
{
   // Определение события
   public static readonly RoutedEvent ClickEvent;
   
   // Регистрация события
   static ButtonBase()
   {
      ButtonBase.ClickEvent = EventManager.RegisterRoutedEvent(
         "Click", RoutingStrategy.Bubble,
         typeof(RoutedEventHandler), typeof(ButtonBase));
   }
   
   // Традиционная оболочка события
   public event RoutedEventHandler Click
   {
      add
      {
         base.AddHandler(ButtonBase.ClickEvent, value);
      }
      remove
      {
         base.RemoveHandler(ButtonBase.ClickEvent, value);
      }
   }
}
}
