﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SCRUMProjectManagementSystem
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            login();
        }

        private void login_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key.Equals(Key.Enter))
            {
                login();
            }
        }

        private void login()
        {
            ViewModel.SPMSViewModel test = new ViewModel.SPMSViewModel();
<<<<<<< HEAD
            test.AuthenticateUser(1, "bagel");
=======
            test.AuthenticateUser(2, "bagel");
>>>>>>> max/master
            if (passwordBox1.Password.Equals("password"))
            {
                new MainWindow().Visibility = Visibility.Visible;
                this.Close();
            }
        }
    }
}
