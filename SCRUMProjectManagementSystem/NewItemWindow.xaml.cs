using System;
using System.Diagnostics.CodeAnalysis;
using System.Windows;
using System.Windows.Input;
using ViewModel;

namespace SCRUMProjectManagementSystem
{
    /// <summary>
    /// Interaction logic for NewItemWindow.xaml
    /// </summary>
    [ExcludeFromCodeCoverage]
    public partial class NewItemWindow : Window
    {
        private MainWindow.selection _type;
        private ViewModel.SPMSViewModel _viewModel;
        private TaskStateConverter tsConverter;
        private TaskTypeConverter ttConverter;

        public NewItemWindow(MainWindow.selection type, ViewModel.SPMSViewModel vm, TaskStateConverter ts, TaskTypeConverter tt)
        {
            InitializeComponent();
            _type = type;
            _viewModel = vm;
            tsConverter = ts;
            ttConverter = tt;
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
                comboBox_project1.ItemsSource = _viewModel.AllManagers;
                stackPanel_team.Visibility = Visibility.Visible;
            }
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                switch (_type)
                {
                    case MainWindow.selection.Project:
                        _viewModel.CreateProject(textBox_project1.Text, datePicker_project1.SelectedDate, datePicker_project2.SelectedDate, _viewModel.AllManagers[comboBox_project1.SelectedIndex], _viewModel.AllTeams[comboBox_project2.SelectedIndex]);
                        break;
                    case MainWindow.selection.Sprint:
                        _viewModel.CreateSprint(textBox_sprint1.Text, datePicker_sprint1.SelectedDate, datePicker_sprint2.SelectedDate);
                        break;
                    case MainWindow.selection.Story:
                        _viewModel.CreateStory(textBox_story1.Text, textBox_story2.Text);
                        break;
                    case MainWindow.selection.Task:
                        _viewModel.CreateTask(textBox_task1.Text, (int)comboBox_task1.SelectedItem, (int)comboBox_task2.SelectedItem, (UserView)comboBox_task3.SelectedItem, (TaskType)comboBox_task4.SelectedItem, (TaskState)comboBox_task5.SelectedItem);
                        break;
                    case MainWindow.selection.Team:
                        _viewModel.CreateTeam(textBox_team1.Text, _viewModel.AllManagers[comboBox_team1.SelectedIndex], _viewModel.AllManagers[comboBox_team2.SelectedIndex]);
                        break;
                    default:
                        break;
                };
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void textBox_story1_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = e.Text.IsNonNumeric();
        }

        private void projectChanged(object sender, EventArgs e)
        {
            button1.IsEnabled = _viewModel.ValidateProject(textBox_project1.Text, datePicker_project1.SelectedDate, datePicker_project2.SelectedDate, (UserView)comboBox_project1.SelectedItem, (TeamView)comboBox_project2.SelectedItem);
        }

        private void sprintChanged(object sender, EventArgs e)
        {
            button1.IsEnabled = _viewModel.ValidateSprint(textBox_sprint1.Text, datePicker_sprint1.SelectedDate, datePicker_sprint2.SelectedDate);
        }

        private void storyChanged(object sender, EventArgs e)
        {
            button1.IsEnabled = _viewModel.ValidateStory(textBox_story1.Text, textBox_story2.Text);
        }

        private void taskChanged(object sender, EventArgs e)
        {
            if (comboBox_task5.SelectedIndex == 0)
            {
                comboBox_task3.SelectedIndex = -1;
            }

            button1.IsEnabled = _viewModel.ValidateTask(textBox_task1.Text, (UserView)comboBox_task3.SelectedItem, (TaskType?)comboBox_task4.SelectedItem, (int?)comboBox_task1.SelectedItem, (int?)comboBox_task2.SelectedItem, null, (TaskState?)comboBox_task5.SelectedItem);
        }

        private void taskChanged2(object sender, EventArgs e)
        {
            if (comboBox_task3.SelectedIndex == -1)
            {
                comboBox_task5.SelectedIndex = 0;
                comboBox_task5.IsEnabled = false;
            }
            else
            {
                if (comboBox_task5.SelectedIndex == 0)
                {
                    comboBox_task5.SelectedIndex = 1;
                }
                comboBox_task5.IsEnabled = true;
            }
            button1.IsEnabled = _viewModel.ValidateTask(textBox_task1.Text, (UserView)comboBox_task3.SelectedItem, (TaskType?)comboBox_task4.SelectedItem, (int?)comboBox_task1.SelectedItem, (int?)comboBox_task2.SelectedItem, null, (TaskState?)comboBox_task5.SelectedItem);
        }

        private void teamChanged(object sender, EventArgs e)
        {
            button1.IsEnabled = _viewModel.ValidateTeam(textBox_team1.Text, (UserView)comboBox_team1.SelectedItem, (UserView)comboBox_team2.SelectedItem);
        }
    }
}
