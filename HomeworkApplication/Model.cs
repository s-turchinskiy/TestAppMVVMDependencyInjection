using System;
using Microsoft.Practices.Unity;
using System.Collections.Generic;
using Providers;

using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Windows;
using Microsoft.Practices.Unity.Configuration;
using System.Configuration;
using System.Diagnostics;

namespace Providers
{
    public struct Coin
    {
        public int CoinValue { get; set; }
        public bool Blocked { get; set; }

        public Coin(int coinValue, bool blocked)
            : this()
        {
            CoinValue = coinValue;
            Blocked = blocked;
        }
    }

    public interface ICoinValuesProvider : IDisposable
    {
        List<Coin> GetCoins();
    }

    public abstract class CoinValuesProvider : ICoinValuesProvider
    {
        protected List<Coin> coins;
        public virtual List<Coin> GetCoins()
        {
            return coins;
        }

        public virtual void Dispose()
        {
        }
    }

    public sealed class HardCodedCoinProvider : CoinValuesProvider
    {
        public HardCodedCoinProvider()
        {
            coins = new List<Coin>
            {
                new Coin(5, false),
                new Coin(3, true),
                new Coin(15,true),
                new Coin(10, false),
                new Coin(2, false),
                new Coin(1, false)
            };
        }
        public override void Dispose()
        {
            base.Dispose();
            //TODO
        }
    }

    public sealed class XmlCoinProvider : CoinValuesProvider
    {
        public XmlCoinProvider()
        {
            coins = new List<Coin>();
            string filename = System.AppDomain.CurrentDomain.BaseDirectory + "Coins.xml";
            XmlSerializer serializer = new XmlSerializer(typeof(List<Coin>));
            FileStream fs = new FileStream(filename, FileMode.Open);
            coins = (List<Coin>)serializer.Deserialize(fs);
            fs.Close();
        }
        public override void Dispose()
        {
            base.Dispose();
        }
    }

    public struct Drink
    {
        public string Name { get; set; }
        public int Price { get; set; }
        public int Count { get; set; }

        public Drink(string name, int price, int count)
            : this()
        {
            Price = price;
            Count = count;
            Name = name;
        }
    }

    public interface IDrinksProvider : IDisposable
    {
        List<Drink> GetDrinks();
    }

    public abstract class DrinksProvider : IDrinksProvider
    {
        protected List<Drink> drinks;

        public List<Drink> GetDrinks()
        {
            return drinks;
        }
        public virtual void Dispose()
        {
        }
    }

    public sealed class HardCodedDrinksProvider : DrinksProvider
    {
        public HardCodedDrinksProvider()
        {
            drinks = new List<Drink>
            {
                new Drink("Чай",10, 7 ),
                new Drink("Кофе",15, 1),
                new Drink("Молоко",12, 5 ),
                new Drink("Какао",8, 5 ),
                new Drink("Латте",20, 2 ),
                new Drink("Каппучино",50, 3),
                new Drink("Горячий шоколад",30, 4),
                new Drink("Двойной шоколад", 40, 6),
                new Drink("Зеленый чай",15, 8),
                new Drink("Экспрессо",40, 2),
                new Drink("Горячий шоколад с молоком",30, 2)
                
            };
        }

        public override void Dispose()
        {
            base.Dispose();
            //TODO
        }
    }

    public sealed class XmlDrinksProvider : DrinksProvider
    {
        public XmlDrinksProvider()
        {

            drinks = new List<Drink>();
            string filename = System.AppDomain.CurrentDomain.BaseDirectory + "Drinks.xml";
            XmlSerializer serializer = new XmlSerializer(typeof(List<Drink>));
            FileStream fs = new FileStream(filename, FileMode.Open);
            drinks = (List<Drink>)serializer.Deserialize(fs);
            fs.Close();

            //запись позже
            //string filename = System.AppDomain.CurrentDomain.BaseDirectory + "Drinks.xml";
            //XmlSerializer formatter = new XmlSerializer(typeof(List<Drink>));
            //using (FileStream fs = new FileStream(filename, FileMode.OpenOrCreate))
            //{
            //    formatter.Serialize(fs, drinks);
            //}
        }
        public override void Dispose()
        {
            base.Dispose();
        }

    }



    public static class RecordDrinksInXMLFile
    {
        public static void Record(List<Drink> drinks)
        {
            string filename = System.AppDomain.CurrentDomain.BaseDirectory + "Drinks.xml";
            XmlSerializer formatter = new XmlSerializer(typeof(List<Drink>));
            using (FileStream fs = new FileStream(filename, FileMode.Create))
            {
                formatter.Serialize(fs, drinks);
            }
        }

    }

    public static class DependenciesInjection
    {
        public static IUnityContainer CreateContainer()
        {
            IUnityContainer container = new UnityContainer();

            //container.RegisterType<ICoinValuesProvider, HardCodedCoinProvider>();
            //container.RegisterType<IDrinksProvider, HardCodedDrinksProvider>();

            //container.LoadConfiguration("HardCoded");
            container.LoadConfiguration("Xml");
            //container.RegisterType<ICoinValuesProvider, XmlCoinProvider>();
            //container.RegisterType<IDrinksProvider, XmlDrinksProvider>();

            return container;
        }
    }

}