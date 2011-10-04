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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SCRUMProjectManagementSystem
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<String> projs;
        private List<String> tasks;
        private List<String> sprints;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            projs = new String[] { "Proj1", "Proj2" }.ToList();
            tasks = new String[] { "Task1", "Task2" }.ToList();
            sprints = new String[] { "Sprint1", "Sprint2" }.ToList();

            projectList.ItemsSource = projs;
            taskList.ItemsSource = tasks;
            sprintList.ItemsSource = sprints;
        }

        private void ProjectList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (taskCol.ActualWidth > 0)
            {
                taskCol.Width = new GridLength(0);
                projCol.Width = new GridLength(projCol.ActualWidth / 2);
                sprintCol.Width = new GridLength(projCol.ActualWidth * 1.5);
            }
        }
    }
}
