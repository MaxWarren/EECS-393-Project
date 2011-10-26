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
                _viewModel.ProjectsForUser.Add(new Project());
            }
            if (_type == MainWindow.selection.Sprint)
            {
                _viewModel.SprintsForProject.Add(new Sprint());
            }
            if (_type == MainWindow.selection.Story)
            {
                _viewModel.StoriesForSprint.Add(new Story());
            }
            if (_type == MainWindow.selection.Task)
            {
                _viewModel.TasksForStory.Add(new Task());
            }
        }
    }
}
