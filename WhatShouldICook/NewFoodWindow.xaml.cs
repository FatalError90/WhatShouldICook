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

        private void dgList_Loaded(object sender, RoutedEventArgs e)
        {
            Load();
           
                
        }

        void Load()
        {
            //if (comboBoxItem.IsSelected)
            //{
            //    var SeleceAll =
            //        from all in dataContext.GetTable<>()
            //        select all;

            //    dgList.ItemsSource = SeleceAll;
            //}
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

                    //var SeleceAll =
                    //    from all in dataContext.GetTable<Soup>()
                    //    select all;

                    //dgList.ItemsSource = SeleceAll;
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

                    Soup soup = new Soup();

                    soup.Soup1 = tbInputText.Text;

                    dataContext.Soups.InsertOnSubmit(soup);
                    dataContext.SubmitChanges();
                    tbInputText.Text = "";

                    Load();

                    //var SeleceAll =
                    //    from all in dataContext.GetTable<Soup>()
                    //    select all;

                    //dgList.ItemsSource = SeleceAll;
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

        private void btnDeleteDataBaseItem_Click(object sender, RoutedEventArgs e)
        {

        }

        private void dgList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int id;
            if (comboBoxItemSoups.IsSelected)
            {
                id = ((dynamic)dgList.SelectedIndex) + 1;
                tbInputText.Text = id.ToString();
            }
        }
    }
}

