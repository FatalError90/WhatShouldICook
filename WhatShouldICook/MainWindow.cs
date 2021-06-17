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
using System.Windows.Navigation;
using System.Windows.Shapes;

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

        private void Window_Closed(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void btnNewFoods_Click(object sender, RoutedEventArgs e)
        {
            NewFoodWindow newFoodWindow = new NewFoodWindow();
            newFoodWindow.Show();
            this.Hide();
        }
    }
}
