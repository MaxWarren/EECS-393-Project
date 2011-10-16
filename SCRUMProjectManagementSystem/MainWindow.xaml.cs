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
            update();
        }

        private void leftList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (leftList.SelectedIndex >= 0)
            {
                button_project.Visibility = Visibility.Hidden;
                button_sprint.Visibility = Visibility.Hidden;
                button_story.Visibility = Visibility.Hidden;
                button_task.Visibility = Visibility.Hidden;
                currentSelection++;
                update();
            }
        }

        private void button_home_Click(object sender, RoutedEventArgs e)
        {
            currentSelection = selection.Home;
            update();
        }

        private void button_project_Click(object sender, RoutedEventArgs e)
        {
            currentSelection = selection.Project;
            update();
        }

        private void button_sprint_Click(object sender, RoutedEventArgs e)
        {
            currentSelection = selection.Sprint;
            update();
        }

        private void button_story_Click(object sender, RoutedEventArgs e)
        {
            currentSelection = selection.Story;
            update();
        }

        private void button_task_Click(object sender, RoutedEventArgs e)
        {
            currentSelection = selection.Task;
            update();
        }

        private void update()
        {
            leftList.SelectedIndex = -1;
            column1.Width = new GridLength(1, GridUnitType.Star);
            column2.Width = new GridLength(1, GridUnitType.Star);
            switch (currentSelection)
            {
                case selection.Home:
                    column1.Width = new GridLength(2, GridUnitType.Star);
                    column2.Width = new GridLength(0, GridUnitType.Star);
                    //left panel
                    leftList.ItemsSource = new string[] { "project1", "project2", "project3" };
                    //right panel
                    stackPanel1.Children.RemoveRange(0, stackPanel1.Children.Count);
                    stackPanel2.Children.RemoveRange(0, stackPanel2.Children.Count);
                    ListBox lb = new ListBox();
                    lb.ItemsSource = new string[] { "task1", "task2", "task3" };
                    stackPanel1.Children.Add(lb);
                    break;
                case selection.Project:
                    //left panel
                    button_project.Visibility = Visibility.Visible;
                    leftList.ItemsSource = new string[] { "spint1", "spint2", "spint3" };
                    //right panel
                    stackPanel1.Children.RemoveRange(0, stackPanel1.Children.Count);
                    stackPanel2.Children.RemoveRange(0, stackPanel2.Children.Count);
                    Label label = new Label();
                    label.Content = "Project Name:";
                    TextBox tb = new TextBox();
                    tb.Margin = new Thickness(0, 0, 0, 4);
                    tb.Text = "project1";
                    stackPanel1.Children.Add(label);
                    stackPanel2.Children.Add(tb);
                    label = new Label();
                    label.Content = "Team:";
                    tb = new TextBox();
                    tb.Margin = new Thickness(0, 0, 0, 4);
                    tb.Text = "HON TEAM!!!!";
                    stackPanel1.Children.Add(label);
                    stackPanel2.Children.Add(tb);
                    label = new Label();
                    label.Content = "Start Date:";
                    stackPanel1.Children.Add(label);
                    label = new Label();
                    label.Content = "10/13/1989";
                    stackPanel2.Children.Add(label);
                    label = new Label();
                    label.Content = "End Date:";
                    stackPanel1.Children.Add(label);
                    label = new Label();
                    label.Content = "12/12/2012";
                    stackPanel2.Children.Add(label);
                    label = new Label();
                    label.Content = "Owner:";
                    tb = new TextBox();
                    tb.Margin = new Thickness(0, 0, 0, 4);
                    tb.Text = "Andy";
                    stackPanel1.Children.Add(label);
                    stackPanel2.Children.Add(tb);
                    Button save = new Button();
                    save.Content = "Save";
                    stackPanel2.Children.Add(save);
                    break;
                case selection.Sprint:
                    //left panel
                    button_project.Visibility = Visibility.Visible;
                    button_sprint.Visibility = Visibility.Visible;
                    leftList.ItemsSource = new string[] { "story1", "story2", "story3" };
                    //right panel
                    stackPanel1.Children.RemoveRange(0, stackPanel1.Children.Count);
                    stackPanel2.Children.RemoveRange(0, stackPanel2.Children.Count);
                    label = new Label();
                    label.Content = "Project Name:";
                    stackPanel1.Children.Add(label);
                    label = new Label();
                    label.Content = "project1";
                    stackPanel2.Children.Add(label);
                    label = new Label();
                    label.Content = "Sprint Name:";
                    tb = new TextBox();
                    tb.Margin = new Thickness(0, 0, 0, 4);
                    tb.Text = "sprint1";
                    stackPanel1.Children.Add(label);
                    stackPanel2.Children.Add(tb);
                    label = new Label();
                    label.Content = "Start Date:";
                    stackPanel1.Children.Add(label);
                    label = new Label();
                    label.Content = "10/13/1989";
                    stackPanel2.Children.Add(label);
                    label = new Label();
                    label.Content = "End Date:";
                    stackPanel1.Children.Add(label);
                    label = new Label();
                    label.Content = "12/12/2012";
                    stackPanel2.Children.Add(label);
                    save = new Button();
                    save.Content = "Save";
                    stackPanel2.Children.Add(save);
                    break;
                case selection.Story:
                    //left panel
                    button_project.Visibility = Visibility.Visible;
                    button_sprint.Visibility = Visibility.Visible;
                    button_story.Visibility = Visibility.Visible;
                    leftList.ItemsSource = new string[] { "task1", "task2", "task3" };
                    //right panel
                    stackPanel1.Children.RemoveRange(0, stackPanel1.Children.Count);
                    stackPanel2.Children.RemoveRange(0, stackPanel2.Children.Count);
                    label = new Label();
                    label.Content = "Project Name:";
                    stackPanel1.Children.Add(label);
                    label = new Label();
                    label.Content = "project1";
                    stackPanel2.Children.Add(label);
                    label = new Label();
                    label.Content = "Story Name:";
                    tb = new TextBox();
                    tb.Margin = new Thickness(0, 0, 0, 4);
                    tb.Text = "story1";
                    stackPanel1.Children.Add(label);
                    stackPanel2.Children.Add(tb);
                    label = new Label();
                    label.Content = "Priority:";
                    stackPanel1.Children.Add(label);
                    label = new Label();
                    label.Content = "4";
                    stackPanel2.Children.Add(label);
                    label = new Label();
                    label.Content = "Text:";
                    tb = new TextBox();
                    tb.Margin = new Thickness(0, 0, 0, 4);
                    tb.TextWrapping = TextWrapping.Wrap;                    
                    tb.Text = "So I called up all the head Rabbis in Jerusalem and was like \"yo, I got this pizza\" and they were all like \"yea\"";
                    stackPanel1.Children.Add(label);
                    stackPanel2.Children.Add(tb);
                    save = new Button();
                    save.Content = "Save";
                    stackPanel2.Children.Add(save);
                    break;
                case selection.Task:
                    //left panel
                    button_project.Visibility = Visibility.Visible;
                    button_sprint.Visibility = Visibility.Visible;
                    button_story.Visibility = Visibility.Visible;
                    button_task.Visibility = Visibility.Visible;
                    leftList.ItemsSource = new string[] { };
                    //right panel
                    stackPanel1.Children.RemoveRange(0, stackPanel1.Children.Count);
                    stackPanel2.Children.RemoveRange(0, stackPanel2.Children.Count);
                    label = new Label();
                    label.Content = "Project Name:";
                    stackPanel1.Children.Add(label);
                    label = new Label();
                    label.Content = "project1";
                    stackPanel2.Children.Add(label);
                    label = new Label();
                    label.Content = "Task Name:";
                    tb = new TextBox();
                    tb.Margin = new Thickness(0, 0, 0, 4);
                    tb.Text = "task1";
                    stackPanel1.Children.Add(label);
                    stackPanel2.Children.Add(tb);
                    label = new Label();
                    label.Content = "Owner:";
                    tb = new TextBox();
                    tb.Margin = new Thickness(0, 0, 0, 4);
                    tb.Text = "Andy";
                    stackPanel1.Children.Add(label);
                    stackPanel2.Children.Add(tb);
                    label = new Label();
                    label.Content = "Complexity:";
                    tb = new TextBox();
                    tb.Margin = new Thickness(0, 0, 0, 4);
                    tb.Text = "4";
                    stackPanel1.Children.Add(label);
                    stackPanel2.Children.Add(tb);
                    label = new Label();
                    label.Content = "Business Value:";
                    tb = new TextBox();
                    tb.Margin = new Thickness(0, 0, 0, 4);
                    tb.Text = "$4.00";
                    stackPanel1.Children.Add(label);
                    stackPanel2.Children.Add(tb);
                    label = new Label();
                    label.Content = "State:";
                    ComboBox cb = new ComboBox();
                    cb.ItemsSource = new string[] { "Not started", "In Development", "Almost ready, I swear", "Complete" };
                    cb.Margin = new Thickness(0, 0, 0, 4);
                    cb.SelectedIndex = 2;
                    stackPanel1.Children.Add(label);
                    stackPanel2.Children.Add(cb);
                    label = new Label();
                    label.Content = "Completion Date:";
                    tb = new TextBox();
                    tb.Margin = new Thickness(0, 0, 0, 4);
                    tb.Text = "10/16/2011";
                    stackPanel1.Children.Add(label);
                    stackPanel2.Children.Add(tb);
                    label = new Label();
                    label.Content = "Text:";
                    tb = new TextBox();
                    tb.Margin = new Thickness(0, 0, 0, 4);
                    tb.TextWrapping = TextWrapping.Wrap;                    
                    tb.Text = "Call up all the head Rabbis in Jerusalem and be like \"yo, I got this pizza\" and they'll all be like \"yea\"";
                    stackPanel1.Children.Add(label);
                    stackPanel2.Children.Add(tb);
                    save = new Button();
                    save.Content = "Save";
                    stackPanel2.Children.Add(save);
                    break;
                default:
                    break;
            }
        }
    }
}
