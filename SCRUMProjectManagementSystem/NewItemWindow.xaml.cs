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
    /// Interaction logic for NewItemWindow.xaml
    /// </summary>
    public partial class NewItemWindow : Window
    {
        private MainWindow.selection _type;
        private ViewModel.SPMSViewModel _viewModel;


        public NewItemWindow(MainWindow.selection type, ViewModel.SPMSViewModel viewModel)
        {
            InitializeComponent();
            _type = type;
            _viewModel = viewModel;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (_type == MainWindow.selection.Project)
            {
                label1.Content = "Project Name";
                label2.Content = "Start Date";
                label3.Content = "Owner";
                label4.Content = "Team";
                textBox5.Visibility = Visibility.Hidden;
                textBox6.Visibility = Visibility.Hidden;
                textBox7.Visibility = Visibility.Hidden;
                textBox8.Visibility = Visibility.Hidden;
                textBox9.Visibility = Visibility.Hidden;
            }
            if (_type == MainWindow.selection.Sprint)
            {
                label1.Content = "Sprint Name";
                label2.Content = "Start Date";
                label3.Content = "End Date";
                textBox4.Visibility = Visibility.Hidden;
                textBox5.Visibility = Visibility.Hidden;
                textBox6.Visibility = Visibility.Hidden;
                textBox7.Visibility = Visibility.Hidden;
                textBox8.Visibility = Visibility.Hidden;
                textBox9.Visibility = Visibility.Hidden;
            }
            if (_type == MainWindow.selection.Story)
            {
                label1.Content = "Priority Number";
                label2.Content = "Text";
                textBox3.Visibility = Visibility.Hidden;
                textBox4.Visibility = Visibility.Hidden;
                textBox5.Visibility = Visibility.Hidden;
                textBox6.Visibility = Visibility.Hidden;
                textBox7.Visibility = Visibility.Hidden;
                textBox8.Visibility = Visibility.Hidden;
                textBox9.Visibility = Visibility.Hidden;
            }
            if (_type == MainWindow.selection.Task)
            {
                label1.Content = "Text";
                label2.Content = "Size Complexity";
                label3.Content = "Business Value";
                textBox4.Visibility = Visibility.Hidden;
                textBox5.Visibility = Visibility.Hidden;
                textBox6.Visibility = Visibility.Hidden;
                textBox7.Visibility = Visibility.Hidden;
                textBox8.Visibility = Visibility.Hidden;
                textBox9.Visibility = Visibility.Hidden;
            }
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
