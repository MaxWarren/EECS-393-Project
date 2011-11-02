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
using ViewModel;

namespace SCRUMProjectManagementSystem
{
    /// <summary>
    /// Interaction logic for NewItemWindow.xaml
    /// </summary>
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
                comboBox_project1.ItemsSource = _viewModel.GetManagers();
                comboBox_project2.ItemsSource = _viewModel.AllTeams;
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
                comboBox_task1.ItemsSource = ComplexityValues.sizeComplexity;
                comboBox_task2.ItemsSource = ComplexityValues.businessValue;
                comboBox_task3.ItemsSource = _viewModel.GetTeamMembers(_viewModel.CurrTeam).Item1;
                comboBox_task4.ItemsSource = TaskTypeConverter.nameMap.Keys;
                comboBox_task5.ItemsSource = TaskStateConverter.nameMap.Keys;
                comboBox_task5.SelectedIndex = 0;
                comboBox_task5.IsEnabled = false;
                stackPanel_task.Visibility = Visibility.Visible;
            }
            if (_type == MainWindow.selection.Team)
            {
                label1.Content = "Team Name";
                label2.Content = "Manager";
                label3.Content = "Team Lead";
                comboBox_team1.ItemsSource = _viewModel.GetManagers();
                comboBox_team2.ItemsSource = _viewModel.GetManagers();
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
                        DateTime startDate = datePicker_project1.SelectedDate.Value;
                        _viewModel.CreateProject(textBox_project1.Text, startDate, datePicker_project2.SelectedDate, _viewModel.GetManagers()[comboBox_project1.SelectedIndex], _viewModel.AllTeams[comboBox_project2.SelectedIndex]);
                        break;
                    case MainWindow.selection.Sprint:
                        startDate = datePicker_sprint1.SelectedDate.Value;
                        _viewModel.CreateSprint(textBox_sprint1.Text, startDate, datePicker_sprint2.SelectedDate);
                        break;
                    case MainWindow.selection.Story:
                        _viewModel.CreateStory(Int32.Parse(textBox_story1.Text), textBox_story2.Text);
                        break;
                    case MainWindow.selection.Task:
                        if (comboBox_task3.SelectedIndex <= 0 || comboBox_task5.SelectedIndex == 0)
                            _viewModel.CreateTask(textBox_task1.Text, Int32.Parse(comboBox_task1.SelectedValue.ToString()), Int32.Parse(comboBox_task2.SelectedValue.ToString()), null, TaskTypeConverter.nameMap[comboBox_task4.SelectedItem.ToString()], TaskStateConverter.nameMap[comboBox_task5.SelectedItem.ToString()]);
                        else
                        _viewModel.CreateTask(textBox_task1.Text, Int32.Parse(comboBox_task1.SelectedValue.ToString()), Int32.Parse(comboBox_task2.SelectedValue.ToString()), _viewModel.GetTeamMembers(_viewModel.CurrTeam).Item1[comboBox_task3.SelectedIndex], TaskTypeConverter.nameMap[comboBox_task4.SelectedItem.ToString()], TaskStateConverter.nameMap[comboBox_task5.SelectedItem.ToString()]);
                        break;
                    case MainWindow.selection.Team:
                        _viewModel.CreateTeam(textBox_team1.Text, _viewModel.GetManagers()[comboBox_team1.SelectedIndex], _viewModel.GetManagers()[comboBox_team2.SelectedIndex]);
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
            e.Handled = Utility.IsTextNumeric(e.Text);
        }

        private void projectChanged(object sender, EventArgs e)
        {
            button1.IsEnabled = textBox_project1.Text.Length > 0 && datePicker_project1.SelectedDate.HasValue && comboBox_project1.SelectedIndex > -1 && comboBox_project2.SelectedIndex > -1;
        }

        private void sprintChanged(object sender, EventArgs e)
        {
            button1.IsEnabled = textBox_sprint1.Text.Length > 0 && datePicker_sprint1.SelectedDate.HasValue;
        }

        private void storyChanged(object sender, EventArgs e)
        {
            button1.IsEnabled = textBox_story1.Text.Length > 0 && textBox_story2.Text.Length > 0;
        }

        private void taskChanged(object sender, EventArgs e)
        {
            if (comboBox_task5.SelectedIndex == 0)
            {
                comboBox_task3.SelectedIndex = -1;
            }
            button1.IsEnabled = textBox_task1.Text.Length > 0 && comboBox_task1.SelectedIndex > -1 && comboBox_task2.SelectedIndex > -1 && comboBox_task4.SelectedIndex > -1 && comboBox_task5.SelectedIndex > -1;
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
            button1.IsEnabled = textBox_task1.Text.Length > 0 && comboBox_task1.SelectedIndex > -1 && comboBox_task2.SelectedIndex > -1 && comboBox_task4.SelectedIndex > -1 && comboBox_task5.SelectedIndex > -1;
        }

        private void teamChanged(object sender, EventArgs e)
        {
            button1.IsEnabled = textBox_team1.Text.Length > 0 && comboBox_team1.SelectedIndex > -1 && comboBox_team2.SelectedIndex > -1;
        }
    }
}
