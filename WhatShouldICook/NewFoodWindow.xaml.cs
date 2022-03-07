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
using System.Windows.Shapes;

namespace WhatShouldICook
{
    /// <summary>
    /// Interaction logic for NewFoodWindow.xaml
    /// </summary>
    public partial class NewFoodWindow : Window
    {
        int id;
        public NewFoodWindow()
        {
            InitializeComponent();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            WhatShouldICookWindow whatShouldICookWindow = new WhatShouldICookWindow();
            whatShouldICookWindow.Show();
            this.Close();
        }

        private void dgList_Loaded(object sender, RoutedEventArgs e) // DataGrid betöltése az adatbázisból
        {
            DataBaseHandler dataBaseHandler = new DataBaseHandler();

            dataBaseHandler.Load(dgList, comboBoxItemSoups, comboBoxItemMainDishes);
            dataBaseHandler.Load(dgList, comboBoxItemSoups, comboBoxItemMainDishes);
            dataBaseHandler.Load(dgList, comboBoxItemSoups, comboBoxItemMainDishes);  
        }

        private void btnUpdateDataBase_Click(object sender, RoutedEventArgs e)
        {
            DataClassesLINQDataContext dataContext = new DataClassesLINQDataContext();
            if (tbInputText.Text != "")
            {
                if (comboBoxItemSoups.IsSelected)
                {

                    Soup soup = new Soup();

                    soup.Soup1 = tbInputText.Text;

                    dataContext.Soups.InsertOnSubmit(soup);
                    dataContext.SubmitChanges();
                    tbInputText.Text = "";

                    Load();
                    
                }
                else if (comboBoxItemMainDishes.IsSelected)
                {
                    MainDishe mainDishes = new MainDishe();

                    mainDishes.MainDish = tbInputText.Text;

                    dataContext.MainDishes.InsertOnSubmit(mainDishes);
                    dataContext.SubmitChanges();
                    tbInputText.Text = "";

                    Load();
                }
                else
                {
                    Dinner dinner = new Dinner();
                    dinner.Dinner1 = tbInputText.Text;

                    dataContext.Dinners.InsertOnSubmit(dinner);
                    dataContext.SubmitChanges();
                    tbInputText.Text = "";

                    Load();
                }
            }
            else MessageBox.Show("A mező nem lehet üres"); tbInputText.Focus();

            }

        private void btnModifyDataBase_Click(object sender, RoutedEventArgs e)
        {

            DataClassesLINQDataContext dataContext = new DataClassesLINQDataContext();
            if (tbInputText.Text != "")
            {
                if (comboBoxItemSoups.IsSelected)
                {
                    Soup soups = dataContext.Soups.FirstOrDefault(soup => soup.ID.Equals(id));
                    soups.Soup1 = tbInputText.Text.ToString();
                    dataContext.SubmitChanges();
                    Load();
                    tbInputText.Text = "";
                }
                else if (comboBoxItemMainDishes.IsSelected)
                {
                    MainDishe dishes = dataContext.MainDishes.FirstOrDefault(maindish => maindish.ID.Equals(id));
                    dishes.MainDish = tbInputText.Text.ToString();
                    dataContext.SubmitChanges();
                    Load();
                    tbInputText.Text = "";
                }
                else
                {
                    Dinner dinner = dataContext.Dinners.FirstOrDefault(dinners => dinners.ID.Equals(id));
                    dinner.Dinner1 = tbInputText.Text.ToString();
                    dataContext.SubmitChanges();
                    Load();
                    tbInputText.Text = "";
                }
            }
            else MessageBox.Show("A mező nem lehet üres"); tbInputText.Focus();
        }

        private void btnDeleteDataBaseItem_Click(object sender, RoutedEventArgs e)
        {
            DataClassesLINQDataContext dataContext = new DataClassesLINQDataContext();
            try
            {
                if (comboBoxItemSoups.IsSelected)
                {
                    var deleteItem = from soup in dataContext.Soups
                                     where soup.ID == id
                                     select soup;

                    foreach (var item in deleteItem)
                    {
                        dataContext.Soups.DeleteOnSubmit(item);
                    }

                    dataContext.SubmitChanges();

                    Load();
                    tbInputText.Text = "";
                    tbInputText.Focus();
                }
                else if (comboBoxItemMainDishes.IsSelected)
                {
                    var deleteItem = from MainDishe in dataContext.MainDishes
                                     where MainDishe.ID == id
                                     select MainDishe;

                    foreach (var item in deleteItem)
                    {
                        dataContext.MainDishes.DeleteOnSubmit(item);
                    }

                    dataContext.SubmitChanges();

                    Load();
                    tbInputText.Text = "";
                    tbInputText.Focus();
                }
                else
                {
                    var deleteItem = from Dinner in dataContext.Dinners
                                     where Dinner.ID == id
                                     select Dinner;

                    foreach (var item in deleteItem)
                    {
                        dataContext.Dinners.DeleteOnSubmit(item);
                    }

                    dataContext.SubmitChanges();

                    Load();
                    tbInputText.Text = "";
                    tbInputText.Focus();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Hiba a törlés során!");
            }
            
        }

        private void dgList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgList.SelectedItem != null)
            {
                if (comboBoxItemSoups.IsSelected)
                {
                    id = ((dynamic)dgList.SelectedItem).ID;
                    tbInputText.Text = ((dynamic)dgList.SelectedItem).Soup1;
                }
                else if (comboBoxItemMainDishes.IsSelected)
                {
                    id = ((dynamic)dgList.SelectedItem).ID;
                    tbInputText.Text = ((dynamic)dgList.SelectedItem).MainDish;
                }
                else
                {
                    id = ((dynamic)dgList.SelectedItem).ID;
                    tbInputText.Text = ((dynamic)dgList.SelectedItem).Dinner1;
                }
            }
        }

        void Load()
        {
            try
            {
                DataClassesLINQDataContext dataContext = new DataClassesLINQDataContext();

                if (comboBoxItemSoups.IsSelected)
                {
                    var SeleceAll =
                        from all in dataContext.GetTable<Soup>()
                        select all;

                    dgList.ItemsSource = SeleceAll;
                }
                else if (comboBoxItemMainDishes.IsSelected)
                {
                    var SeleceAll =
                        from all in dataContext.GetTable<MainDishe>()
                        select all;

                    dgList.ItemsSource = SeleceAll;
                }
                else
                {
                    var SeleceAll =
                        from all in dataContext.GetTable<Dinner>()
                        select all;

                    dgList.ItemsSource = SeleceAll;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Nem sikerült kapcsolódni az adatbázishoz!");
            }
        }
    }
}

