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
using Utilities;

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
            this.Title = "New " + System.Enum.GetName(_type.GetType(), _type);
            if (_type == MainWindow.selection.Project)
            {
                label1.Content = "Project Name";
                label2.Content = "Start Date";
                label3.Content = "End Date";
                label4.Content = "Owner";
                label5.Content = "Team";
                stackPanel_project.Visibility = Visibility.Visible;
            }
            if (_type == MainWindow.selection.Sprint)
            {
                label1.Content = "Sprint Name";
                label2.Content = "Start Date";
                label3.Content = "End Date";
                stackPanel_sprint.Visibility = Visibility.Visible;
            }
            if (_type == MainWindow.selection.Story)
            {
                label1.Content = "Priority Number";
                label2.Content = "Text";
                stackPanel_story.Visibility = Visibility.Visible;
            }
            if (_type == MainWindow.selection.Task)
            {
                label1.Content = "Text";
                label2.Content = "Size Complexity";
                label3.Content = "Business Value";
                label4.Content = "Owner";
                label5.Content = "Type";
                label6.Content = "State";
                stackPanel_task.Visibility = Visibility.Visible;
            }
            if (_type == MainWindow.selection.Team)
            {
                label1.Content = "Team Name";
                label2.Content = "Manager";
                label3.Content = "Team Lead";
                stackPanel_team.Visibility = Visibility.Visible;
            }
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void textBox_story1_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = Utility.IsTextNumeric(e.Text);
        }
    }
}
