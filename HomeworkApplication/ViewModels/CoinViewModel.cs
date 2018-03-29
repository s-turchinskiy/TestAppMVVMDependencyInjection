using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;


using Providers;
//using SampleMVVM.Commands;
using System.Windows.Input;

namespace HomeworkApplication.ViewModels
{
 

    class CoinViewModel : ViewModelBase
    {
        public Coin Coin;

        public CoinViewModel(Coin coin)
        {
            this.Coin = coin;
        }

        public int CoinValue
        {
            get { return Coin.CoinValue; }
            set
            {
                Coin.CoinValue = value;
                OnPropertyChanged("CoinValue");
            }
        }

        public bool Blocked
        {
            get { return Coin.Blocked; }
            set
            {
                Coin.Blocked = value;
                OnPropertyChanged("Blocked");
            }
        }

    }
}
