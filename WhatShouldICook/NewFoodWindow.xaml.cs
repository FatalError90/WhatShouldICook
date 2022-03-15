using System;
using System.Windows;
using System.Windows.Controls;


namespace WhatShouldICook
{
    /// <summary>
    /// Interaction logic for NewFoodWindow.xaml
    /// </summary>
    public partial class NewFoodWindow : Window
    {
        Nullable<int> id; //ID kezelése érdekében null értékkel ellátható int változó 
        public NewFoodWindow()
        {
            InitializeComponent();
        }

        #region Gombok hívása
        private void BTN_UpdateDataBase_Click(object sender, RoutedEventArgs e) // Új ételek és linkek hozzáadása
        {   
            if (tbInputText.Text != string.Empty & tbLinkText.Text != string.Empty)
            {
                DataBaseHandler dataBaseHandler = new DataBaseHandler();
                dataBaseHandler.UpdateDatabase(tbInputText, tbLinkText, comboBoxItemSoups, comboBoxItemMainDishes, dgList);

                id = null; //ID beállítása, hogy nullázódjon. Különben megjegyzi és mást törölhet, mint amit szereznénk. 
            }
            else MessageBox.Show("Az étel és a link mező nem lehet üres"); tbInputText.Focus();
            
            
        }
        private void BTN_ModifyDataBase_Click(object sender, RoutedEventArgs e) // Meglévő ételek és linkek módosítása
        {   
            if (tbInputText.Text != string.Empty & tbLinkText.Text != string.Empty)
            {
                DataBaseHandler dataBaseHandler= new DataBaseHandler();
                dataBaseHandler.ModifyDatabase(tbInputText,tbLinkText, comboBoxItemSoups, comboBoxItemMainDishes, dgList, id);

                id = null; //ID beállítása, hogy nullázódjon. Különben megjegyzi és mást törölhet, mint amit szereznénk.
            }
            else MessageBox.Show("Az étel és a link mező nem lehet üres"); tbInputText.Focus();
        }
        private void BTN_DeleteDataBaseItem_Click(object sender, RoutedEventArgs e) // Ételek és linkjei törlése
        {
            if (tbInputText.Text != string.Empty & tbLinkText.Text!= string.Empty)
            {
                DataBaseHandler dataBaseHandler = new DataBaseHandler();
                dataBaseHandler.DeleteFromDatabase(tbInputText, tbLinkText, comboBoxItemSoups, comboBoxItemMainDishes, dgList, id);

                id = null; //ID beállítása, hogy nullázódjon. Különben megjegyzi és mást törölhet, mint amit szereznénk.
            }
            else MessageBox.Show("Az étel és a link mező nem lehet üres"); tbInputText.Focus();  
        }
        #endregion

        #region Datagrid kezelése
        private void DG_List_Loaded(object sender, RoutedEventArgs e) // DataGrid betöltése az adatbázisból
        {
            DataBaseHandler dataBaseHandler = new DataBaseHandler();
            dataBaseHandler.Load(dgList, comboBoxItemSoups, comboBoxItemMainDishes, tbInputText, tbLinkText);
        }
        private void DG_List_SelectionChanged(object sender, SelectionChangedEventArgs e) // DataGrid kijelölésének dinamikus vezérlése és megjelenítése
        {
            if (dgList.SelectedItem != null)
            {
                if (comboBoxItemSoups.IsSelected)
                {
                    id = ((dynamic)dgList.SelectedItem).ID;
                    tbInputText.Text = ((dynamic)dgList.SelectedItem).Soup1;
                    tbLinkText.Text = ((dynamic)dgList.SelectedItem).Link;
                }
                else if (comboBoxItemMainDishes.IsSelected)
                {
                    id = ((dynamic)dgList.SelectedItem).ID;
                    tbInputText.Text = ((dynamic)dgList.SelectedItem).MainDish;
                    tbLinkText.Text = ((dynamic)dgList.SelectedItem).Link;
                }
                else
                {
                    id = ((dynamic)dgList.SelectedItem).ID;
                    tbInputText.Text = ((dynamic)dgList.SelectedItem).Dinner1;
                    tbLinkText.Text = ((dynamic)dgList.SelectedItem).Link;
                }
            }
        }
        #endregion

        private void Window_Closed(object sender, EventArgs e) // Visszalépés a főablakra. 
        {
            WhatShouldICookWindow whatShouldICookWindow = new WhatShouldICookWindow();
            whatShouldICookWindow.Show();
            this.Close();
        }
    }
}

