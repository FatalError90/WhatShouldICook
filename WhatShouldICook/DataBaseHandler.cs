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
        public void Load (DataGrid dataGrid, ComboBoxItem soupItems, ComboBoxItem mainDishItems) // DataGrid betöltése az adatbázisból
        {
            try
            {
                if (soupItems.IsSelected)
                {
                    var SeleceAll =
                        from all in _dc.GetTable<Soup>()
                        select all;

                    dataGrid.ItemsSource = SeleceAll;
                }
                else if(mainDishItems.IsSelected)
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

        public void UpdateDatabase(TextBox textBox, ComboBoxItem soupItems, ComboBoxItem mainDishItems, DataGrid dataGrid) // Új ételek és linkek hozzáadása
        {
            try
            {
                List<string> list;
                
                if (soupItems.IsSelected)
                {
                    Soup soup = new Soup();
                    soup.Soup1 = textBox.Text;
                    list = Soups();

                    if (list.Contains(textBox.Text))
                    {
                        MessageBox.Show("Az adatbázis már tartalmazza ezt a levest");
                        textBox.Focus();
                    }
                    else
                    {
                        _dc.Soups.InsertOnSubmit(soup);
                        _dc.SubmitChanges();
                        Load(dataGrid, soupItems, mainDishItems);
                        textBox.Text = "";
                        textBox.Focus();
                    }
                }
                else if (mainDishItems.IsSelected)
                {
                    MainDishe mainDish =new MainDishe();
                    mainDish.MainDish = textBox.Text;
                    list = MainDishes();

                    if (list.Contains(textBox.Text))
                    {
                        MessageBox.Show("Az adatbázis már tartalmazza ezt a másodikat");
                        textBox.Focus();
                    }
                    else
                    {
                        _dc.MainDishes.InsertOnSubmit(mainDish);
                        _dc.SubmitChanges();
                        Load(dataGrid, soupItems, mainDishItems);
                        textBox.Text = "";
                        textBox.Focus();
                    }
                }
                else
                {
                    Dinner dinner = new Dinner();
                    dinner.Dinner1 = textBox.Text;
                    list = Dinners();

                    if (list.Contains(textBox.Text))
                    {
                        MessageBox.Show("Az adatbázis már tartalmazza ezt a vacsorát");
                        textBox.Focus();
                    }
                    else
                    {
                        _dc.Dinners.InsertOnSubmit(dinner);
                        _dc.SubmitChanges();
                        Load(dataGrid, soupItems, mainDishItems);
                        textBox.Text = "";
                        textBox.Focus();
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Nem sikerült kapcsolódni az adatbázishoz");
            }
        }
        public void ModifyDatabase(TextBox textBox, ComboBoxItem soupItems, ComboBoxItem mainDishItems, DataGrid dataGrid, int id) // Új ételek és linkek módosítása
        {
            try
            {
                if (soupItems.IsSelected)
                {
                    Soup soups = _dc.Soups.FirstOrDefault(soup => soup.ID.Equals(id));
                    soups.Soup1 = textBox.Text.ToString();
                    _dc.SubmitChanges();
                    Load(dataGrid, soupItems, mainDishItems);
                    textBox.Text = "";
                    textBox.Focus();
                }
                else if (mainDishItems.IsSelected)
                {
                    MainDishe dishes = _dc.MainDishes.FirstOrDefault(maindish => maindish.ID.Equals(id));
                    dishes.MainDish = textBox.Text.ToString();
                    _dc.SubmitChanges();
                    Load(dataGrid, soupItems, mainDishItems);
                    textBox.Text = "";
                    textBox.Focus();
                }
                else
                {
                    Dinner dinner = _dc.Dinners.FirstOrDefault(dinners => dinners.ID.Equals(id));
                    dinner.Dinner1 = textBox.Text.ToString();
                    _dc.SubmitChanges();
                    Load(dataGrid, soupItems, mainDishItems);
                    textBox.Text = "";
                    textBox.Focus();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Nem sikerült kapcsolódni az adatbázishoz");
            }
            
        }
        public void DeleteFromDatabase(TextBox textBox, ComboBoxItem soupItems, ComboBoxItem mainDishItems, DataGrid dataGrid, int id) // Ételek és linkjei törlése
        {
            try
            {
                if (soupItems.IsSelected)
                {
                    var deleteItem = from soup in _dc.Soups
                                     where soup.ID == id
                                     select soup;

                    foreach (var item in deleteItem)
                    {
                        _dc.Soups.DeleteOnSubmit(item);
                    }

                    _dc.SubmitChanges();

                    Load(dataGrid, soupItems, mainDishItems);
                    textBox.Text = "";
                    textBox.Focus();
                }
                else if (mainDishItems.IsSelected)
                {
                    var deleteItem = from MainDishe in _dc.MainDishes
                                     where MainDishe.ID == id
                                     select MainDishe;

                    foreach (var item in deleteItem)
                    {
                        _dc.MainDishes.DeleteOnSubmit(item);
                    }

                    _dc.SubmitChanges();

                    Load(dataGrid, soupItems, mainDishItems);
                    textBox.Text = "";
                    textBox.Focus();
                }
                else
                {
                    var deleteItem = from Dinner in _dc.Dinners
                                     where Dinner.ID == id
                                     select Dinner;

                    foreach (var item in deleteItem)
                    {
                        _dc.Dinners.DeleteOnSubmit(item);
                    }

                    _dc.SubmitChanges();

                    Load(dataGrid, soupItems, mainDishItems);
                    textBox.Text = "";
                    textBox.Focus();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Nem sikerült kapcsolódni az adatbázishoz");
            }
        }
    }
}
