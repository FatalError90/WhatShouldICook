﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using System.Windows.Documents;



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
            DataBaseHandler dbHandler = new DataBaseHandler();
            List<string> soups = dbHandler.Soups();
            List<string> linkOfSoups = dbHandler.LinksOfSoups();
            List<string> mainDishes = dbHandler.MainDishes();
            List<string> linkOfMainDishes = dbHandler.LinksOfMainDishes();
            List<string> dinners = dbHandler.Dinners();
            List<string> linkOfDinners = dbHandler.LinksOfDinners();

            WhatSoupShouldICook(soups, linkOfSoups, 3);
            WhatLunchhouldICook(mainDishes, linkOfMainDishes, 3);
            WhatDinnerShouldICook(dinners, linkOfDinners, 6);

        }

        public void WhatSoupShouldICook(List<string> ListOfSoups, List<string> listOfSoupLinks, int howMany)
        {
            Random rnd = new Random();

            List<string> listOfRaffleSoups = new List<string>();
            List<string> listOfRaffleSoupsLinks = new List<string>();

            int index = 0;

            while (index != howMany)
            {
                bool isGood = false;
                int indexOfItem = rnd.Next(0, ListOfSoups.Count);
                string raffleSoups = ListOfSoups[indexOfItem];
                string link = listOfSoupLinks[indexOfItem];

                if (!listOfRaffleSoups.Contains(raffleSoups))
                {
                    isGood = true;

                    if (isGood)
                    {
                        listOfRaffleSoups.Add(raffleSoups);
                        listOfRaffleSoupsLinks.Add(link);
                        index++;
                    }
                }
                else isGood = false;
            }
            
            ShowData(lbSoup1, hyperlinkSoup1, listOfRaffleSoups, listOfRaffleSoupsLinks, 0);
            ShowData(lbSoup2, hyperlinkSoup2, listOfRaffleSoups, listOfRaffleSoupsLinks, 1);
            ShowData(lbSoup3, hyperlinkSoup3, listOfRaffleSoups, listOfRaffleSoupsLinks, 2);

        }
        public void WhatLunchhouldICook(List<string> ListOfDinners, List<string> listOfDinnersLinks, int howMany)
        {
            Random rnd = new Random();

            List<string> listOfRaffleDinners = new List<string>();
            List<string> listOfRaffleDinnersLinks = new List<string>();

            int index = 0;

            while (index != howMany)
            {
                bool isGood = false;
                int indexOfItem = rnd.Next(0, ListOfDinners.Count);
                string raffleSoup = ListOfDinners[indexOfItem];
                string link = listOfDinnersLinks[indexOfItem];

                if (!listOfRaffleDinners.Contains(raffleSoup))
                {
                    isGood = true;

                    if (isGood)
                    {
                        listOfRaffleDinners.Add(raffleSoup);
                        listOfRaffleDinnersLinks.Add(link);
                        index++;
                    }
                }
                else isGood = false;
            }

            ShowData(lbMainDish1, hyperlinkMainDish1, listOfRaffleDinners, listOfRaffleDinnersLinks, 0);
            ShowData(lbMainDish2, hyperlinkMainDish2, listOfRaffleDinners, listOfRaffleDinnersLinks, 1);
            ShowData(lbMainDish3, hyperlinkMainDish3, listOfRaffleDinners, listOfRaffleDinnersLinks, 2);

        }
        public void WhatDinnerShouldICook(List<string> ListOfDinners, List<string> listOfDinnersLinks, int howMany)
        {
            Random rnd = new Random();

            List<string> listOfRaffleDinners = new List<string>();
            List<string> listOfRaffleDinnersLinks = new List<string>();

            int index = 0;

            while (index != howMany)
            {
                bool isGood = false;
                int indexOfItem = rnd.Next(0, ListOfDinners.Count);
                string raffleMeal = ListOfDinners[indexOfItem];
                string link = listOfDinnersLinks[indexOfItem];

                if (!listOfRaffleDinners.Contains(raffleMeal))
                {
                    isGood = true;

                    if (isGood)
                    {
                        listOfRaffleDinners.Add(raffleMeal);
                        listOfRaffleDinnersLinks.Add(link);
                        index++;
                    }
                }
                else isGood = false;
            }

            ShowData(lbDinner1, hyperlinkDinner1, listOfRaffleDinners, listOfRaffleDinnersLinks, 0);
            ShowData(lbDinner2, hyperlinkDinner2, listOfRaffleDinners, listOfRaffleDinnersLinks, 1);
            ShowData(lbDinner3, hyperlinkDinner3, listOfRaffleDinners, listOfRaffleDinnersLinks, 2);
            ShowData(lbDinner4, hyperlinkDinner4, listOfRaffleDinners, listOfRaffleDinnersLinks, 3);
            ShowData(lbDinner5, hyperlinkDinner5, listOfRaffleDinners, listOfRaffleDinnersLinks, 4);
            ShowData(lbDinner6, hyperlinkDinner6, listOfRaffleDinners, listOfRaffleDinnersLinks, 5);
        }
        private void hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            try
            {
                Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri));
                e.Handled = true;
            }
            catch (Exception)
            {
                
            }
            
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
        void ShowData(Label label,  Hyperlink link, List<string> listOfFoods, List<string> listOfLinks, int indexOfList)
        {
            label.Content = listOfFoods[indexOfList];
            link.NavigateUri = new Uri(listOfLinks[indexOfList]);
        }
        void Print()
        {
            PrintDialog printDialog = new PrintDialog();
            if (printDialog.ShowDialog().GetValueOrDefault(false))
            {
                printDialog.PrintVisual(this, this.Title);
            }
        }

    }
}
