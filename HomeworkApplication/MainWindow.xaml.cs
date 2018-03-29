using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


using System.Collections;
using System.Threading;
using System.IO;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Xml;
using System.Xml.Serialization;

using HomeworkApplication.ViewModels;
using System.Text.RegularExpressions;


namespace HomeworkApplication
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        Regex inputRegex = new Regex(@"^[0-9]$");

        public static MainWindow Instance { get; private set; }

        public MainWindow()
        {
            Instance = this;
            InitializeComponent();
            MainViewModel _mainViewModel = new MainViewModel();
            DataContext = _mainViewModel;

            Closing += _mainViewModel.OnWindowClosing;
            ButtonBuy1.Click += _mainViewModel.ButtonBuy_OnClick;
        }


        private void Window_Initialized(object sender, EventArgs e)
        {
           
          
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
           
        }

        private void ListBoxCoin_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
                 //MessageBox.Show(string.Format("вы выбрали {0} строку", ListBoxCoin.SelectedIndex));
                 //CoinViewModel coin = (CoinViewModel)ListBoxCoin.Items[ListBoxCoin.SelectedIndex];
                 //Refill.IsEnabled = !coin.Blocked;
        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            //проверяем или подходит введенный символ нашему правилу
            Match match = inputRegex.Match(e.Text);
            //и проверяем или выполняется условие
            //если количество символов в строке больше или равно одному либо
            //если введенный символ не подходит нашему правилу
            if ((sender as TextBox).Text.Length >= 1 && !match.Success)
            {
                //то обработка события прекращается и ввода неправильного символа не происходит
                e.Handled = true;
            }
        }
    }

}