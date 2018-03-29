using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Globalization;
using System.Windows.Media;

namespace ViewModels.Converters
{
    /// <summary>
    /// Converts object reference checking result to boolean value
    /// </summary>
    public class NullToBooleanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value != null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }

    }

    public class BlockedToIsEnabledConverter : IValueConverter
    {
      
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value==null)
            {
                return false;
            }
            else
            {
                return !((HomeworkApplication.ViewModels.CoinViewModel)(value)).Blocked;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }

 
    }

    public class CoinValueToDisplayConverter : IValueConverter
    {
        
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return " " + value.ToString() + " р ";
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }

    }

    public class DrinkToDisplayConverter : IMultiValueConverter
    {

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {

            return " " + values[0].ToString() + " " + values[1] + " р (" + values[2] + ") ";
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

    }

    public class DrinkToBackgroundConverter : IMultiValueConverter
    {

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {

            String StringBalance = (string)values[1];
            if ((int)values[0] > int.Parse(StringBalance))
            { 
                return new SolidColorBrush(Colors.Red); 
            }
            else
            {
                return new SolidColorBrush(Colors.LightGreen);
            }
            
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

    }
    public class BuyToIsEnabledConverter : IMultiValueConverter
    {

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {

            if (values[0] == null)
            {
                return false;
            }
            else
            {
                String StringBalance = (string)values[1];
                return int.Parse(StringBalance)>=((HomeworkApplication.ViewModels.DrinkViewModel)(values[0])).Price;
            }
            
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

    }

}
