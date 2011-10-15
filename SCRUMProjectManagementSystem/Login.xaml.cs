using System;
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
        private MainWindow parent;

        public Login(MainWindow newParent)
        {
            InitializeComponent();
            parent = newParent;
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            if (passwordBox1.Password.Equals("password"))
            {
                parent.Visibility = Visibility.Visible;
                this.Close();
            }
        }
    }
}
