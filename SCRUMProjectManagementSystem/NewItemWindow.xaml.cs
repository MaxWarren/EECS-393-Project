using System;
using System.Diagnostics.CodeAnalysis;
using System.Windows;
using System.Windows.Controls;
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

        public NewItemWindow(MainWindow.selection type, ViewModel.SPMSViewModel vm)
        {
            InitializeComponent();
            _type = type;
            _viewModel = vm;
        }

        private void showCriticalError()
        {
            MessageBox.Show(
                    "A serious error has occured. The client must shut down.",
                    "Critical Error",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);

            Environment.Exit(1);
        }

        private void showArgumentError()
        {
            MessageBox.Show(
                "The values you have entered are not valid. Please review the data you have entered and try again.",
                "Data Error",
                MessageBoxButton.OK,
                MessageBoxImage.Warning);
        }

        private void saveSuccess()
        {
            MessageBox.Show(
                "Your changes have been saved.",
                "Save Successful",
                MessageBoxButton.OK,
                MessageBoxImage.Information);
        }

        private void saveFailure()
        {
            MessageBox.Show(
                "Your changes could not be saved. Please review the data you entered and try again. If this problem persists, please contact your administrator.",
                "Save Failed",
                MessageBoxButton.OK,
                MessageBoxImage.Warning);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.Title = "New " + System.Enum.GetName(_type.GetType(), _type);
            // Maybe use a switch statement???
            if (_type == MainWindow.selection.Project)
            {
                label1.Content = "Project Name";
                label2.Content = "Start Date";
                label3.Content = "End Date";
                label4.Content = "Owner";
                label5.Content = "Team";
                try
                {
                    comboBox_project1.ItemsSource = _viewModel.AllManagers;
                    comboBox_project2.ItemsSource = _viewModel.AllTeams;
                }
                catch (InvalidOperationException)
                {
                    showCriticalError();
                }
                stackPanel_project.Visibility = Visibility.Visible;
            }
            if (_type == MainWindow.selection.Sprint)
            {
                label1.Content = "Sprint Name";
                label2.Content = "Start Date";
                label3.Content = "End Date";
                datePicker_sprint1.BlackoutDates.Clear();
                CalendarDateRange cdr = new CalendarDateRange();
                cdr.End = _viewModel.CurrProject.StartDate.AddDays(-1);
                datePicker_sprint1.BlackoutDates.Add(cdr);
                if (_viewModel.CurrProject.EndDate.HasValue)
                {
                    cdr = new CalendarDateRange();
                    cdr.Start = (DateTime)_viewModel.CurrProject.EndDate;
                    cdr.Start = cdr.Start.AddDays(1);
                    datePicker_sprint1.BlackoutDates.Add(cdr);
                }
                datePicker_sprint2.BlackoutDates.Clear();
                cdr = new CalendarDateRange();
                cdr.End = _viewModel.CurrProject.StartDate.AddDays(-1);
                datePicker_sprint2.BlackoutDates.Add(cdr);
                if (_viewModel.CurrProject.EndDate.HasValue)
                {
                    cdr = new CalendarDateRange();
                    cdr.Start = (DateTime)_viewModel.CurrProject.EndDate;
                    cdr.Start = cdr.Start.AddDays(1);
                    datePicker_sprint2.BlackoutDates.Add(cdr);
                }
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
                comboBox_task1.ItemsSource = EnumValues.sizeComplexity;
                comboBox_task2.ItemsSource = EnumValues.businessValue;
                try
                {
                    comboBox_task3.ItemsSource = _viewModel.CurrTeamMembers;
                }
                catch (InvalidOperationException)
                {
                    showCriticalError();
                }
                comboBox_task4.ItemsSource = EnumValues.taskType;
                comboBox_task5.ItemsSource = EnumValues.taskState;
                comboBox_task5.SelectedIndex = 0;
                comboBox_task5.IsEnabled = false;
                datePicker_task1.BlackoutDates.Clear();
                CalendarDateRange cdr = new CalendarDateRange();
                cdr.End = _viewModel.CurrSprint.StartDate.AddDays(-1);
                datePicker_task1.BlackoutDates.Add(cdr);
                if (_viewModel.CurrSprint.EndDate.HasValue)
                {
                    cdr = new CalendarDateRange();
                    cdr.Start = (DateTime)_viewModel.CurrSprint.EndDate;
                    cdr.Start = cdr.Start.AddDays(1);
                    datePicker_task1.BlackoutDates.Add(cdr);
                }

                stackPanel_task.Visibility = Visibility.Visible;
            }
            if (_type == MainWindow.selection.Team)
            {
                label1.Content = "Team Name";
                label2.Content = "Manager";
                label3.Content = "Team Lead";
                try
                {
                    comboBox_team1.ItemsSource = _viewModel.AllManagers;
                    comboBox_team2.ItemsSource = _viewModel.AllManagers;
                }
                catch (InvalidOperationException)
                {
                    showCriticalError();
                }
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
                        if (_viewModel.CreateProject(textBox_project1.Text, datePicker_project1.SelectedDate, datePicker_project2.SelectedDate, (UserView)comboBox_project1.SelectedItem, (TeamView)comboBox_project2.SelectedItem))
                        {
                            saveSuccess();
                            this.Close();
                        }
                        else
                        {
                            saveFailure();
                        }
                        break;
                    case MainWindow.selection.Sprint:
                        if (_viewModel.CreateSprint(textBox_sprint1.Text, datePicker_sprint1.SelectedDate, datePicker_sprint2.SelectedDate))
                        {
                            this.Close();
                        }
                        else
                        {
                            saveFailure();
                        }
                        break;
                    case MainWindow.selection.Story:
                        if (_viewModel.CreateStory(textBox_story1.Text, textBox_story2.Text))
                        {
                            this.Close();
                        }
                        else
                        {
                            saveFailure();
                        }
                        break;
                    case MainWindow.selection.Task:
                        if (_viewModel.CreateTask(textBox_task1.Text, (int)comboBox_task1.SelectedItem, (int)comboBox_task2.SelectedItem, (UserView)comboBox_task3.SelectedItem, (TaskType)comboBox_task4.SelectedItem, (TaskState)comboBox_task5.SelectedItem, datePicker_task1.SelectedDate))
                        {
                            this.Close();
                        }
                        else
                        {
                            saveFailure();
                        }
                        break;
                    case MainWindow.selection.Team:
                        if (_viewModel.CreateTeam(textBox_team1.Text, (UserView)comboBox_team1.SelectedItem, (UserView)comboBox_team2.SelectedItem))
                        {
                            saveSuccess();
                            this.Close();
                        }
                        else
                        {
                            saveFailure();
                        }
                        break;
                    default:
                        break;
                };
            }
            catch (InvalidOperationException)
            {
                showCriticalError();
            }
            catch (ArgumentNullException)
            {
                showArgumentError();
            }
            catch (ArgumentOutOfRangeException)
            {
                showArgumentError();
            }
            catch (ArgumentException)
            {
                showArgumentError();
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

            button1.IsEnabled = _viewModel.ValidateTask(textBox_task1.Text, (UserView)comboBox_task3.SelectedItem, (TaskType?)comboBox_task4.SelectedItem, (int?)comboBox_task1.SelectedItem, (int?)comboBox_task2.SelectedItem, datePicker_task1.SelectedDate, (TaskState?)comboBox_task5.SelectedItem);
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
            button1.IsEnabled = _viewModel.ValidateTask(textBox_task1.Text, (UserView)comboBox_task3.SelectedItem, (TaskType?)comboBox_task4.SelectedItem, (int?)comboBox_task1.SelectedItem, (int?)comboBox_task2.SelectedItem, (DateTime?)datePicker_task1.SelectedDate, (TaskState?)comboBox_task5.SelectedItem);
        }

        private void teamChanged(object sender, EventArgs e)
        {
            button1.IsEnabled = _viewModel.ValidateTeam(textBox_team1.Text, (UserView)comboBox_team1.SelectedItem, (UserView)comboBox_team2.SelectedItem);
        }
    }
}
