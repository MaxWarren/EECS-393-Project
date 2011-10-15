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
        private selection currentSelection;

        private enum selection
        {
            Home,
            Project,
            Sprint,
            Story,
            Task
        };

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            currentSelection = selection.Home;
            leftList.ItemsSource = new string[] { "project1", "project2", "project3" };
            rightList.ItemsSource = new string[] { "task1", "task2", "task3" };
        }

        void crumb_Click(object sender, RoutedEventArgs e)
        {

        }

        private void leftList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (leftList.SelectedIndex >= 0)
            {
                button_project.Visibility = Visibility.Hidden;
                button_sprint.Visibility = Visibility.Hidden;
                button_story.Visibility = Visibility.Hidden;
                button_task.Visibility = Visibility.Hidden;
                switch (currentSelection)
                {
                    case selection.Home:
                        currentSelection++;
                        button_project.Visibility = Visibility.Visible;
                        leftList.ItemsSource = new string[] { "spint1", "spint2", "spint3" };
                        break;
                    case selection.Project:
                        currentSelection++;
                        button_project.Visibility = Visibility.Visible;
                        button_sprint.Visibility = Visibility.Visible;
                        leftList.ItemsSource = new string[] { "story1", "story2", "story3" };
                        break;
                    case selection.Sprint:
                        currentSelection++;
                        button_project.Visibility = Visibility.Visible;
                        button_sprint.Visibility = Visibility.Visible;
                        button_story.Visibility = Visibility.Visible;
                        leftList.ItemsSource = new string[] { "task1", "task2", "task3" };
                        break;
                    case selection.Story:
                        currentSelection++;
                        button_project.Visibility = Visibility.Visible;
                        button_sprint.Visibility = Visibility.Visible;
                        button_story.Visibility = Visibility.Visible;
                        button_task.Visibility = Visibility.Visible;
                        leftList.ItemsSource = new string[] {};
                        break;
                    case selection.Task:
                        button_project.Visibility = Visibility.Visible;
                        button_sprint.Visibility = Visibility.Visible;
                        button_story.Visibility = Visibility.Visible;
                        button_task.Visibility = Visibility.Visible;
                        leftList.ItemsSource = new string[] {};
                        break;
                    default:
                        break;
                }
            }
        }

        private void button_home_Click(object sender, RoutedEventArgs e)
        {
            currentSelection = selection.Home;
            leftList.ItemsSource = new string[] { "project1", "project2", "project3" };
        }

        private void button_project_Click(object sender, RoutedEventArgs e)
        {
            currentSelection = selection.Project;
            leftList.ItemsSource = new string[] { "spint1", "spint2", "spint3" };
        }

        private void button_sprint_Click(object sender, RoutedEventArgs e)
        {
            currentSelection = selection.Sprint;
            leftList.ItemsSource = new string[] { "story1", "story2", "story3" };
        }

        private void button_story_Click(object sender, RoutedEventArgs e)
        {
            currentSelection = selection.Story;
            leftList.ItemsSource = new string[] { "task1", "task2", "task3" };
        }

        private void button_task_Click(object sender, RoutedEventArgs e)
        {
            currentSelection = selection.Task;
            leftList.ItemsSource = new string[] { };
        }
    }
}
