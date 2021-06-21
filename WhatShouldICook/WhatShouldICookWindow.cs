using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

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
                }
                else isGood = false;

                if (isGood)
                {
                    listOfMeals.Add(raffleMeal);
                    index++;
                }
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
