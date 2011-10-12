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
        private static List<string> projs = new string[] { "Proj1", "Proj2" }.ToList();
        private List<string> sprints;
        private List<string> stories;
        private List<string> tasks;
        private Dictionary<string, System.Collections.IEnumerable> crumbMemory = new Dictionary<string,System.Collections.IEnumerable>();
        private Button currentCrumb = new Button();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            projs = new string[] { "Proj1", "Proj2" }.ToList();
            tasks = new string[] { "Task1", "Task2" }.ToList();

            projectList.ItemsSource = projs;
            taskList.ItemsSource = tasks;
        }

        private void ProjectList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (projectList.SelectedIndex >= 0)
            {
                currentCrumb.Background = new SolidColorBrush(Colors.Green);
                Button crumb = new Button();
                crumb.Background = new SolidColorBrush(Colors.Green);
                crumb.Content = projectList.SelectedValue;
                crumb.Click += new RoutedEventHandler(crumb_Click);
                if (!crumbMemory.ContainsKey(crumb.Content.ToString()))
                {
                    if (crumbMemory.Count > 0)
                    {
                        stackPanel1.Children.RemoveRange(stackPanel1.Children.IndexOf(currentCrumb) + 1, stackPanel1.Children.Count - stackPanel1.Children.IndexOf(currentCrumb));
                    }
                    crumbMemory.Add(crumb.Content.ToString(), projectList.ItemsSource);
                    stackPanel1.Children.Add(crumb);
                    currentCrumb = crumb;
                }
                else
                {
                    foreach (Button b in stackPanel1.Children)
                    {
                        if (b.Content.Equals(crumb.Content))
                        {
                            currentCrumb = b;
                        }
                    }
                }
                currentCrumb.Background = new SolidColorBrush(Colors.Red);
                projectList.SelectedIndex = -1;
            }
        }
        void crumb_Click(object sender, RoutedEventArgs e)
        {
            projectList.ItemsSource = crumbMemory[((Button)sender).Content.ToString()];
            currentCrumb.Background = new SolidColorBrush(Colors.Green);
            currentCrumb = (Button)sender;
            currentCrumb.Background = new SolidColorBrush(Colors.Red);
        }

        private void taskList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            projectList.ItemsSource = new string[] { "aaa", "bbb", "ccc" }.ToList();
        }
    }
}
