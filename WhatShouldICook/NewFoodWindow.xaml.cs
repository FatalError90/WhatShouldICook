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

        private void Window_Closed(object sender, EventArgs e) // Visszalépés a főablakra. 
        {
            WhatShouldICookWindow whatShouldICookWindow = new WhatShouldICookWindow();
            whatShouldICookWindow.Show();
            this.Close();
        }

        private void dgList_Loaded(object sender, RoutedEventArgs e) // DataGrid betöltése az adatbázisból
        {
            DataBaseHandler dataBaseHandler = new DataBaseHandler();
            dataBaseHandler.Load(dgList, comboBoxItemSoups, comboBoxItemMainDishes);
        }

        private void btnUpdateDataBase_Click(object sender, RoutedEventArgs e) // Új ételek és linkek hozzáadása
        {   
            if (tbInputText.Text != "")
            {
                DataBaseHandler dataBaseHandler = new DataBaseHandler();
                dataBaseHandler.UpdateDatabase(tbInputText, comboBoxItemSoups, comboBoxItemMainDishes, dgList);
            }
            else MessageBox.Show("A mező nem lehet üres"); tbInputText.Focus();
        }

        private void btnModifyDataBase_Click(object sender, RoutedEventArgs e)
        {   
            if (tbInputText.Text != "")
            {
                DataBaseHandler dataBaseHandler= new DataBaseHandler();
                dataBaseHandler.ModifyDatabase(tbInputText, comboBoxItemSoups, comboBoxItemMainDishes, dgList, id);
            }
            else MessageBox.Show("A mező nem lehet üres"); tbInputText.Focus();
        }

        private void btnDeleteDataBaseItem_Click(object sender, RoutedEventArgs e) // Ételek és linkjei törlése
        {
            DataBaseHandler dataBaseHandler = new DataBaseHandler();
            dataBaseHandler.DeleteFromDatabase(tbInputText, comboBoxItemSoups, comboBoxItemMainDishes, dgList, id);
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

    }
}

