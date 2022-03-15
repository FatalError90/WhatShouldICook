using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace WhatShouldICook
{
    internal class DataBaseHandler
    {

        private readonly DataClassesLINQDataContext _dc = new DataClassesLINQDataContext();

        #region Táblákból lista
        public List<string> Soups() // Levesek kiolvasása
        {
            try
            {
                List<string> soupsList = (from item in _dc.Soups
                                          select item.Soup1).ToList();

                return soupsList;
            }
            catch (Exception)
            {
                MessageBox.Show("Hiba a levesek kiolvasása során");
                return null;
            }
        }
        public List<string> LinksOfSoups() // Leves linkjeinek a kiolvasása
        {
            try
            {
                List<string> linkOfSoups = (from item in _dc.Soups
                                            select item.Link).ToList();

                return linkOfSoups;
            }
            catch (Exception)
            {
                MessageBox.Show("Hiba a levesek linkjeinek kiolvasása során");
                return null;
            } 
        }
        public List<string> MainDishes() // Második kiolvasása
        {
            try
            {
                List<string> mainDishesList = (from item in _dc.MainDishes
                                               select item.MainDish).ToList();

                return mainDishesList;
            }
            catch (Exception)
            {
                MessageBox.Show("Hiba a második linkjeinek kiolvasása során");
                return null;
            }
        }
        public List<string> LinksOfMainDishes() // Második linkeinek a kiolvasása
        {
            try
            {
                List<string> linkOfMainsDishes = (from item in _dc.MainDishes
                                                  select item.Link).ToList();

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
                                            select item.Dinner1).ToList();

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
                                              select item.Link).ToList();

                return linkOfDinners;
            }
            catch (Exception)
            {
                MessageBox.Show("Hiba a vacsora linkjeinek kiolvasása során");
                return null;
            }
        }
        #endregion

        #region Adataok betöltése
        public void Load(DataGrid dataGrid, ComboBoxItem soupItems, ComboBoxItem mainDishItems, TextBox textboxFood, TextBox textboxLink) // DataGrid betöltése az adatbázisból
        {
            try
            {
                if (soupItems.IsSelected)
                {
                    var SeleceAll =
                    from all in _dc.GetTable<Soup>()
                    select all;

                    dataGrid.ItemsSource = SeleceAll;

                    if (textboxFood == null & textboxLink == null)
                    {
                        //Muszáj volt így megcsinálni, mert a függvény első és csak is az első meghívásánál a textBox.Text = string.Empty null reference error-t dob
                    }
                    else
                    {
                        textboxFood.Text = textboxLink.Text = string.Empty;
                    }
                }
                else if (mainDishItems.IsSelected)
                {
                    var SeleceAll =
                    from all in _dc.GetTable<MainDishe>()
                    select all;

                    dataGrid.ItemsSource = SeleceAll;

                    textboxFood.Text = textboxLink.Text = string.Empty;
                }
                else
                {
                    var SeleceAll =
                    from all in _dc.GetTable<Dinner>()
                    select all;

                    dataGrid.ItemsSource = SeleceAll;

                    textboxFood.Text = textboxLink.Text = string.Empty;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Nem sikerült kapcsolódni az adatbázishoz!");
            }
        }
        #endregion

        #region Tábla adatok módosítása
        public void UpdateDatabase(TextBox textboxFood, TextBox textboxLink, ComboBoxItem soupItems, ComboBoxItem mainDishItems, DataGrid dataGrid) // Új ételek és linkek hozzáadása
        {
            try
            {
                List<string> list;

                if (soupItems.IsSelected)
                {
                    list = Soups();

                    if (list.Contains(textboxFood.Text))
                    {
                        MessageBox.Show("Az adatbázis már tartalmazza ezt a levest");
                        textboxFood.Focus();
                    }
                    else
                    {
                        var newSoupAndLink = new Soup() { Soup1 = textboxFood.Text, Link = textboxLink.Text }; // VS2022 intelisense javaslata az object initializers használata e helyett://
                                                                                                               // Soup newSoupAndLink = new Soup(); newSoupAndLink.Soup1 = textBox.Text; newSoupAndLink.Link=textboxLink.Text;
                        _dc.Soups.InsertOnSubmit(newSoupAndLink);
                        _dc.SubmitChanges();

                        Load(dataGrid, soupItems, mainDishItems, textboxFood, textboxLink);

                        textboxFood.Text = textboxLink.Text = string.Empty;
                        textboxFood.Focus();
                    }
                }
                else if (mainDishItems.IsSelected)
                {
                    list = MainDishes();

                    if (list.Contains(textboxFood.Text))
                    {
                        MessageBox.Show("Az adatbázis már tartalmazza ezt a másodikat");
                        textboxFood.Focus();
                    }
                    else
                    {
                        var newMaindishAndLink = new MainDishe() { MainDish = textboxFood.Text, Link = textboxLink.Text };
                        _dc.MainDishes.InsertOnSubmit(newMaindishAndLink);
                        _dc.SubmitChanges();

                        Load(dataGrid, soupItems, mainDishItems, textboxFood, textboxLink);

                        textboxFood.Text = textboxLink.Text = string.Empty;
                        textboxFood.Focus();
                    }
                }
                else
                {
                    list = Dinners();

                    if (list.Contains(textboxFood.Text))
                    {
                        MessageBox.Show("Az adatbázis már tartalmazza ezt a vacsorát");
                        textboxFood.Focus();
                    }
                    else
                    {
                        var newDinnerAndLink = new Dinner() { Dinner1 = textboxFood.Text, Link = textboxLink.Text };
                        _dc.Dinners.InsertOnSubmit(newDinnerAndLink);
                        _dc.SubmitChanges();

                        Load(dataGrid, soupItems, mainDishItems, textboxFood, textboxLink);

                        textboxFood.Text = textboxLink.Text = string.Empty;
                        textboxFood.Focus();
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Nem sikerült kapcsolódni az adatbázishoz");
            }
        }
        public void ModifyDatabase(TextBox textboxFood, TextBox textboxLink, ComboBoxItem soupItems, ComboBoxItem mainDishItems, DataGrid dataGrid, Nullable<int> id) // Új ételek és linkek módosítása
        {
            try
            {
                List<string> list;

                if (soupItems.IsSelected)
                {
                    list = Soups();

                    if (list.Contains(textboxFood.Text))
                    {
                        MessageBox.Show("Az adatbázis már tartalmazza ezt a levest");
                        textboxFood.Focus();
                    }
                    else
                    {
                        Soup modifySoupAndLink = _dc.Soups.FirstOrDefault(soup => soup.ID.Equals(id));
                        
                        modifySoupAndLink.Soup1 = textboxFood.Text;
                        modifySoupAndLink.Link = textboxLink.Text;
                        _dc.SubmitChanges();

                        Load(dataGrid, soupItems, mainDishItems, textboxFood, textboxLink);

                        textboxFood.Text = textboxLink.Text = string.Empty;
                        textboxFood.Focus();
                    }
                }
                else if (mainDishItems.IsSelected)
                {
                    list = MainDishes();

                    if (list.Contains(textboxFood.Text))
                    {
                        MessageBox.Show("Az adatbázis már tartalmazza ezt a másodikat");
                        textboxFood.Focus();
                    }
                    else
                    {
                        MainDishe modifyMainddishAndLink = _dc.MainDishes.FirstOrDefault(maindish => maindish.ID.Equals(id));

                        modifyMainddishAndLink.MainDish = textboxFood.Text;
                        modifyMainddishAndLink.Link = textboxLink.Text;
                        _dc.SubmitChanges();

                        Load(dataGrid, soupItems, mainDishItems, textboxFood, textboxLink);

                        textboxFood.Text = textboxLink.Text = string.Empty;
                        textboxFood.Focus();
                    }
                }
                else
                {
                    list = Dinners();

                    if (list.Contains(textboxFood.Text))
                    {
                        MessageBox.Show("Az adatbázis már tartalmazza ezt a vacsorát");
                        textboxFood.Focus();
                    }
                    else
                    {
                        Dinner modigyDinnerAndLink = _dc.Dinners.FirstOrDefault(dinners => dinners.ID.Equals(id));

                        modigyDinnerAndLink.Dinner1 = textboxFood.Text;
                        modigyDinnerAndLink.Link = textboxLink.Text;
                        _dc.SubmitChanges();

                        Load(dataGrid, soupItems, mainDishItems, textboxFood, textboxLink);

                        textboxFood.Text = textboxLink.Text = string.Empty;
                        textboxFood.Focus();
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Nem sikerült kapcsolódni az adatbázishoz");
            }
        }
        public void DeleteFromDatabase(TextBox textboxFood, TextBox textboxLink, ComboBoxItem soupItems, ComboBoxItem mainDishItems, DataGrid dataGrid, Nullable<int> id) // Ételek és linkjei törlése
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

                    Load(dataGrid, soupItems, mainDishItems, textboxFood, textboxLink);

                    textboxFood.Text = textboxLink.Text = string.Empty;
                    textboxFood.Focus();
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

                    Load(dataGrid, soupItems, mainDishItems, textboxFood, textboxLink);

                    textboxFood.Text = textboxLink.Text = string.Empty;
                    textboxFood.Focus();
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

                    Load(dataGrid, soupItems, mainDishItems, textboxFood, textboxLink);

                    textboxFood.Text = textboxLink.Text = string.Empty;
                    textboxFood.Focus();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Nem sikerült kapcsolódni az adatbázishoz");
            }
        }
        #endregion
    }
}
