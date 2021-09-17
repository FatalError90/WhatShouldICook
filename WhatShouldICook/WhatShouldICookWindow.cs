using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace WhatShouldICook
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class WhatShouldICookWindow : Window
    {
        public WhatShouldICookWindow()
        {
            InitializeComponent();
        }

        private void btnWhatShouldICook_Click(object sender, RoutedEventArgs e)
        {
            List<string> soups = WhatShouldICook(Soups(), 3);
            List<string> mainDishes = WhatShouldICook(MainDishes(), 3);
            List<string> dinners = WhatShouldICook(Dinners(), 6);
            

            ShowData(lbSoup1, soups, 0);
            ShowData(lbSoup2, soups, 1);
            ShowData(lbSoup3, soups, 2);

            ShowData(lbMainDish1, mainDishes, 0);
            ShowData(lbMainDish2, mainDishes, 1);
            ShowData(lbMainDish3, mainDishes, 2);

            ShowData(lbDinner1, dinners, 0);
            ShowData(lbDinner2, dinners, 1);
            ShowData(lbDinner3, dinners, 2);
            ShowData(lbDinner4, dinners, 3);
            ShowData(lbDinner5, dinners, 4);
            ShowData(lbDinner6, dinners, 5);

            
            ellenorzes();
        }

        private void hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri));
            e.Handled = true;
        }
        void ellenorzes()
        {
            List<string> leves = new List<string>();
            List<string> link = new List<string>();
            

            DataClassesLINQDataContext dc = new DataClassesLINQDataContext();

            leves = (from item in dc.Soups
                     select item.Soup1.ToString()).ToList();
            link = (from item in dc.Soups
                    select item.Link.ToString()).ToList();
            listak(leves, link, 3);

        }

        void listak (List<string> kaja, List<string> link, int mennyi)
        {
            Random rnd = new Random();

            List<string> sorsoltkaja = new List<string>();
            List<string> sorsoltlink = new List<string>();

            int index = 0;

            while (index != mennyi)
            {
                bool isGood = false;
                int IndexOfItems = rnd.Next(0, kaja.Count);
                string raffleMeal = kaja[IndexOfItems];
                string Link = link[IndexOfItems];

                if (!sorsoltkaja.Contains(raffleMeal))
                {
                    isGood = true;

                    if (isGood)
                    {
                        sorsoltkaja.Add(raffleMeal);
                        sorsoltlink.Add(Link);
                        index++;
                    }
                }
                else isGood = false;
            }

            lbSoup1.Content = sorsoltkaja[0];
            hyperlinkSoup1.NavigateUri = new Uri(sorsoltlink[0]);
            lbSoup2.Content = sorsoltkaja[1];
            hyperlinkSoup2.NavigateUri = new Uri(sorsoltlink[1]);
            lbSoup3.Content = sorsoltkaja[2];
            hyperlinkSoup3.NavigateUri = new Uri(sorsoltlink[2]);
        }
        private void btnNewFoods_Click(object sender, RoutedEventArgs e)
        {
            NewFoodWindow newFoodWindow = new NewFoodWindow();
            newFoodWindow.Show();
            this.Hide();
        }

        private void btnPrint_Click(object sender, RoutedEventArgs e)
        {
            Print();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        void ShowData(Label label, List<string> list, int indexOfList)
        {
            label.Content = list[indexOfList];
        }

        List<string> WhatShouldICook(List<string> list, int howMany)
        {
            Random rnd = new Random();

            List<string> listOfMeals = new List<string>();

            int index = 0;

            while (index != howMany)
            {
                bool isGood = false;
                int IndexOfItems = rnd.Next(0, list.Count);
                string raffleMeal = list[IndexOfItems];

                if (!listOfMeals.Contains(raffleMeal))
                {
                    isGood = true;

                    if (isGood)
                    {
                        listOfMeals.Add(raffleMeal);
                        index++;
                    }
                }
                else isGood = false;
            }
            return listOfMeals;
        }

        void Print()
        {
            PrintDialog printDialog = new PrintDialog();
            if (printDialog.ShowDialog().GetValueOrDefault(false))
            {
                printDialog.PrintVisual(this, this.Title);
            }
        }

        #region DB kiolvasás
        public static List<string> Soups()
        {

            DataClassesLINQDataContext dc = new DataClassesLINQDataContext();

            List<string> soupsList = (from item in dc.Soups
                                      select item.Soup1.ToString()).ToList();

            return soupsList;
        }

        public static List<string> MainDishes()
        {

            DataClassesLINQDataContext dc = new DataClassesLINQDataContext();

            List<string> mainDishesList = (from item in dc.MainDishes
                                           select item.MainDish.ToString()).ToList();

            return mainDishesList;
        }

        public static List<string> Dinners()
        {

            DataClassesLINQDataContext dc = new DataClassesLINQDataContext();

            try
            {
                List<string> dinnersList = (from item in dc.Dinners
                                            select item.Dinner1.ToString()).ToList();

                return dinnersList;
            }
            catch (Exception)
            {

                MessageBox.Show("Hiba a vacsorák kiolvasása során");
                return null;
            }

        }

        #endregion
    }
}
