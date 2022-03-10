using System;
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

        private void BTN_WhatShouldICook_Click(object sender, RoutedEventArgs e)
        {
            DataBaseHandler dbHandler = new DataBaseHandler();
            List<string> soups = dbHandler.Soups();
            List<string> linkOfSoups = dbHandler.LinksOfSoups();

            List<string> mainDishes = dbHandler.MainDishes();
            List<string> linkOfMainDishes = dbHandler.LinksOfMainDishes();

            List<string> dinners = dbHandler.Dinners();
            List<string> linkOfDinners = dbHandler.LinksOfDinners();

            WhatSoupShouldICook(soups, linkOfSoups, 3);
            WhatLunchShouldICook(mainDishes, linkOfMainDishes, 3);
            WhatDinnerShouldICook(dinners, linkOfDinners, 6);

        }

        private void WhatSoupShouldICook(List<string> ListOfSoups, List<string> listOfSoupLinks, int howMany)
        {
            Random rnd = new Random();

            List<string> listOfRaffleSoups = new List<string>();
            List<string> listOfRaffleSoupsLinks = new List<string>();

            int index = 0;

            while (index != howMany)
            {
                int indexOfItem = rnd.Next(0, ListOfSoups.Count);
                string raffleSoups = ListOfSoups[indexOfItem];
                string link = listOfSoupLinks[indexOfItem];

                if (!listOfRaffleSoups.Contains(raffleSoups))
                {
                    listOfRaffleSoups.Add(raffleSoups);
                    listOfRaffleSoupsLinks.Add(link);
                    index++;
                    
                }
            }
            
            ShowData(lbSoup1, hyperlinkSoup1, listOfRaffleSoups, listOfRaffleSoupsLinks, 0);
            ShowData(lbSoup2, hyperlinkSoup2, listOfRaffleSoups, listOfRaffleSoupsLinks, 1);
            ShowData(lbSoup3, hyperlinkSoup3, listOfRaffleSoups, listOfRaffleSoupsLinks, 2);

        }

        private void WhatLunchShouldICook(List<string> ListOfDinners, List<string> listOfDinnersLinks, int howMany)
        {
            Random rnd = new Random();

            List<string> listOfRaffleDinners = new List<string>();
            List<string> listOfRaffleDinnersLinks = new List<string>();

            int index = 0;

            while (index != howMany)
            {
                int indexOfItem = rnd.Next(0, ListOfDinners.Count);
                string raffleSoup = ListOfDinners[indexOfItem];
                string link = listOfDinnersLinks[indexOfItem];

                if (!listOfRaffleDinners.Contains(raffleSoup))
                {
  
                        listOfRaffleDinners.Add(raffleSoup);
                        listOfRaffleDinnersLinks.Add(link);
                        index++;
                }
            }

            ShowData(lbMainDish1, hyperlinkMainDish1, listOfRaffleDinners, listOfRaffleDinnersLinks, 0);
            ShowData(lbMainDish2, hyperlinkMainDish2, listOfRaffleDinners, listOfRaffleDinnersLinks, 1);
            ShowData(lbMainDish3, hyperlinkMainDish3, listOfRaffleDinners, listOfRaffleDinnersLinks, 2);

        }

        private void WhatDinnerShouldICook(List<string> ListOfDinners, List<string> listOfDinnersLinks, int howMany)
        {
            Random rnd = new Random();

            List<string> listOfRaffleDinners = new List<string>();
            List<string> listOfRaffleDinnersLinks = new List<string>();

            int index = 0;

            while (index != howMany)
            {
                int indexOfItem = rnd.Next(0, ListOfDinners.Count);
                string raffleMeal = ListOfDinners[indexOfItem];
                string link = listOfDinnersLinks[indexOfItem];

                if (!listOfRaffleDinners.Contains(raffleMeal))
                {

                        listOfRaffleDinners.Add(raffleMeal);
                        listOfRaffleDinnersLinks.Add(link);
                        index++;
                 }

            }

            ShowData(lbDinner1, hyperlinkDinner1, listOfRaffleDinners, listOfRaffleDinnersLinks, 0);
            ShowData(lbDinner2, hyperlinkDinner2, listOfRaffleDinners, listOfRaffleDinnersLinks, 1);
            ShowData(lbDinner3, hyperlinkDinner3, listOfRaffleDinners, listOfRaffleDinnersLinks, 2);
            ShowData(lbDinner4, hyperlinkDinner4, listOfRaffleDinners, listOfRaffleDinnersLinks, 3);
            ShowData(lbDinner5, hyperlinkDinner5, listOfRaffleDinners, listOfRaffleDinnersLinks, 4);
            ShowData(lbDinner6, hyperlinkDinner6, listOfRaffleDinners, listOfRaffleDinnersLinks, 5);
        }

        private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            try
            {
                Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri));
                e.Handled = true;
            }
            catch (Exception)
            {
                MessageBox.Show("Nem sikerült betölteni a linket");
            }
            
        } 

        private void BTN_NewFoods_Click(object sender, RoutedEventArgs e)
        {
            
            NewFoodWindow newFoodWindow = new NewFoodWindow();
            //newFoodWindow.tbInputText.SelectedText = "text";
            newFoodWindow.Show();
            this.Hide();
        }

        private void BTN_Print_Click(object sender, RoutedEventArgs e)
        {
            Print();
        }

        private void Window_Closed(object sender, EventArgs e) // Kilépés az alkalmazásból 
        {
            Environment.Exit(0);
        }

        private void ShowData(Label label,  Hyperlink link, List<string> listOfFoods, List<string> listOfLinks, int indexOfList) // Kisorsolt ételek és a hozzá tartozó linkek kiiratása
        {
            label.Content = listOfFoods[indexOfList];
            link.NavigateUri = new Uri(listOfLinks[indexOfList]);
        }

        private void Print() // A kisorsolt ételek kinyomtatása
        {
            PrintDialog printDialog = new PrintDialog();
            if (printDialog.ShowDialog().GetValueOrDefault(false))
            {
                printDialog.PrintVisual(this, this.Title);
            }
        }


    }
}
