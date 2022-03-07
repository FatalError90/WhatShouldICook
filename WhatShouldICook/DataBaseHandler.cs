using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace WhatShouldICook
{
    internal class DataBaseHandler
    {

        private DataClassesLINQDataContext _dc = new DataClassesLINQDataContext();

        public List<string> Soups() // Levesek kiolvasása
        {
            List<string> soupsList = (from item in _dc.Soups
                                      select item.Soup1.ToString()).ToList();

            return soupsList;
        }
        public List<string> LinksOfSoups() // Leves linkjeinek a kiolvasása
        {
            List<string> linkOfSoups = (from item in _dc.Soups
                                        select item.Link.ToString()).ToList();

            return linkOfSoups;
        }
        public List<string> MainDishes() // Második kiolvasása
        {
            List<string> mainDishesList = (from item in _dc.MainDishes
                                           select item.MainDish.ToString()).ToList();

            return mainDishesList;
        }
        public List<string> LinksOfMainDishes() // Második linkeinek a kiolvasása
        {
            try
            {
                List<string> linkOfMainsDishes = (from item in _dc.MainDishes
                                                  select item.Link.ToString()).ToList();

                return linkOfMainsDishes;
            }
            catch (Exception)
            {
                MessageBox.Show("Hiba a második linkjeinek kiolvasása során");
                return null;
            }
        }
        public List<string> Dinners() // Vacsorák kiolvasása
        {
            try
            {
                List<string> dinnersList = (from item in _dc.Dinners
                                            select item.Dinner1.ToString()).ToList();

                return dinnersList;
            }
            catch (Exception)
            {

                MessageBox.Show("Hiba a vacsorák kiolvasása során");
                return null;
            }
        }
        public List<string> LinksOfDinners() // Vacsorák linkjeinek a kiolvasása
        {
            try
            {
                List<string> linkOfDinners = (from item in _dc.Dinners
                                              select item.Link.ToString()).ToList();

                return linkOfDinners;
            }
            catch (Exception)
            {
                MessageBox.Show("Hiba a vacsora linkjeinek kiolvasása során");
                return null;
            }
        }
        public void Load (DataGrid dataGrid, ComboBoxItem soup, ComboBoxItem mainDish) // DataGrid betöltése az adatbázisból
        {
            try
            {
                if (soup.IsSelected)
                {
                    var SeleceAll =
                        from all in _dc.GetTable<Soup>()
                        select all;

                    dataGrid.ItemsSource = SeleceAll;
                }
                else if(mainDish.IsSelected)
                {
                    var SeleceAll =
                        from all in _dc.GetTable<MainDishe>()
                        select all;

                    dataGrid.ItemsSource = SeleceAll;
                }
                else
                {
                    var SeleceAll =
                        from all in _dc.GetTable<Dinner>()
                        select all;

                    dataGrid.ItemsSource = SeleceAll;
                }
                

            }
            catch (Exception)
            {
                MessageBox.Show("Nem sikerült kapcsolódni az adatbázishoz!");
            }
        }

        public void UpdateDataBase()
        {

        }

    }
}
