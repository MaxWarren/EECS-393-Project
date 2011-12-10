using System;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using ViewModel;

namespace SCRUMProjectManagementSystem
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    [ExcludeFromCodeCoverage]
    public partial class MainWindow : Window
    {
        private selection currentSelection;
        private SPMSViewModel viewModel;
        private bool taskReady;
        private Brush _color;

        public enum selection
        {
            Home,
            Project,
            Sprint,
            Story,
            Task,
            Team
        };

        public Brush BackgroundColor
        {
            get { return _color; }
            set { _color = value; this.Background = value; }
        }

        public MainWindow(SPMSViewModel vm)
        {
            viewModel = vm;
            InitializeComponent();
            BackgroundColor = Brushes.LightBlue;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (!viewModel.IsManager)
            {
                menu_main.Visibility = Visibility.Hidden;
                button_New.Visibility = Visibility.Hidden;
            }
            this.DataContext = viewModel;
            currentSelection = selection.Home;
            taskReady = false;
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

                try
                {
                    switch (currentSelection)
                    {
                        case selection.Home:
                            viewModel.CurrProject = (ProjectView)leftList.SelectedItem;
                            comboBox_project_owner.ItemsSource = viewModel.AllManagers;
                            grid_projectInfo.DataContext = viewModel.CurrProject;
                            break;
                        case selection.Project:
                            viewModel.CurrSprint = (SprintView)leftList.SelectedItem;
                            grid_sprintInfo.DataContext = viewModel.CurrSprint;
                            break;
                        case selection.Sprint:
                            viewModel.CurrStory = (StoryView)leftList.SelectedItem;
                            grid_storyInfo.DataContext = viewModel.CurrStory;
                            comboBox_story_sprint.DataContext = viewModel;
                            label_story_project.DataContext = viewModel.CurrProject;
                            break;
                        case selection.Story:
                            viewModel.CurrTask = (TaskView)leftList.SelectedItem;
                            grid_taskInfo.DataContext = viewModel.CurrTask;
                            label_task_project.DataContext = viewModel.CurrProject;
                            comboBox_task_owner.DataContext = viewModel.GetTeamMembers(viewModel.CurrTeam);
                            comboBox_task_complexity.ItemsSource = ViewModel.EnumValues.sizeComplexity;
                            comboBox_task_state.ItemsSource = ViewModel.EnumValues.taskState;
                            comboBox_task_value.ItemsSource = ViewModel.EnumValues.businessValue;
                            comboBox_task_type.ItemsSource = ViewModel.EnumValues.taskType;
                            break;
                        default:
                            break;
                    }
                }
                catch (InvalidOperationException)
                {
                    showCriticalError();
                }
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
            try
            {
                taskReady = false;
                button_New.Content = "Add " + System.Enum.GetName(currentSelection.GetType(), currentSelection + 1);
                leftList.SelectedIndex = -1;
                grid_projectInfo.Visibility = Visibility.Hidden;
                grid_sprintInfo.Visibility = Visibility.Hidden;
                grid_storyInfo.Visibility = Visibility.Hidden;
                grid_taskInfo.Visibility = Visibility.Hidden;
                rightList.Visibility = Visibility.Hidden;
                button_home.FontWeight = FontWeights.Normal;
                button_project.FontWeight = FontWeights.Normal;
                button_sprint.FontWeight = FontWeights.Normal;
                button_story.FontWeight = FontWeights.Normal;
                button_task.FontWeight = FontWeights.Normal;

                if (viewModel.IsManager && viewModel.HistoricMode == false)
                {
                    button_New.Visibility = Visibility.Visible;
                }
                switch (currentSelection)
                {
                    case selection.Home:
                        button_home.FontWeight = FontWeights.Bold;
                        leftList.ItemsSource = viewModel.ProjectsForUser;
                        rightList.Visibility = Visibility.Visible;
                        rightList.ItemsSource = viewModel.TasksForUser;
                        break;
                    case selection.Project:
                        button_project.FontWeight = FontWeights.Bold;
                        button_project.Visibility = Visibility.Visible;
                        leftList.ItemsSource = viewModel.SprintsForProject;
                        grid_projectInfo.Visibility = Visibility.Visible;

                        ObservableCollection<UserView> managerList = viewModel.AllManagers;
                        comboBox_project_owner.ItemsSource = managerList;
                        comboBox_project_owner.SelectedItem = managerList.Where(user => user.UserID == viewModel.CurrProject.OwnerID).FirstOrDefault();
                        button_saveProject.Content = "Save";
                        break;
                    case selection.Sprint:
                        button_sprint.FontWeight = FontWeights.Bold;
                        button_project.Visibility = Visibility.Visible;
                        button_sprint.Visibility = Visibility.Visible;
                        leftList.ItemsSource = viewModel.StoriesForSprint;
                        grid_sprintInfo.Visibility = Visibility.Visible;
                        button_burndown.IsEnabled = viewModel.CurrSprint.EndDate.HasValue && viewModel.StoriesForSprint.Count > 0;
                        button_saveSprint.Content = "Save";
                        if (viewModel.HistoricMode == false)
                        {
                            if (DateTime.Compare(viewModel.CurrSprint.StartDate, viewModel.CurrProject.StartDate) < 0 || (viewModel.CurrProject.EndDate.HasValue && DateTime.Compare(viewModel.CurrSprint.StartDate, (DateTime)viewModel.CurrProject.EndDate) > 0))
                            {
                                datePicker_sprint_start.SelectedDate = null;
                                button_saveSprint.Content = "Save*";
                                if ((viewModel.CurrSprint.EndDate.HasValue && DateTime.Compare((DateTime)viewModel.CurrSprint.EndDate, viewModel.CurrProject.StartDate) < 0) || (viewModel.CurrProject.EndDate.HasValue && viewModel.CurrSprint.EndDate.HasValue && DateTime.Compare((DateTime)viewModel.CurrSprint.EndDate, (DateTime)viewModel.CurrProject.EndDate) > 0))
                                {
                                    datePicker_sprint_end.SelectedDate = null;
                                    showInvalidDateWarning();
                                }
                                else
                                {
                                    showInvalidDateWarning();
                                }
                            }
                            else if ((viewModel.CurrSprint.EndDate.HasValue && DateTime.Compare((DateTime)viewModel.CurrSprint.EndDate, viewModel.CurrProject.StartDate) < 0) || (viewModel.CurrProject.EndDate.HasValue && viewModel.CurrSprint.EndDate.HasValue && DateTime.Compare((DateTime)viewModel.CurrSprint.EndDate, (DateTime)viewModel.CurrProject.EndDate) > 0))
                            {
                                datePicker_sprint_end.SelectedDate = null;
                                button_saveSprint.Content = "Save*";
                                showInvalidDateWarning();
                            }
                            datePicker_sprint_start.BlackoutDates.Clear();
                            CalendarDateRange cdr = new CalendarDateRange();
                            cdr.End = viewModel.CurrProject.StartDate.AddDays(-1);
                            datePicker_sprint_start.BlackoutDates.Add(cdr);
                            if (viewModel.CurrProject.EndDate.HasValue)
                            {
                                cdr = new CalendarDateRange();
                                cdr.Start = (DateTime)viewModel.CurrProject.EndDate;
                                cdr.Start = cdr.Start.AddDays(1);
                                datePicker_sprint_start.BlackoutDates.Add(cdr);
                            }
                            datePicker_sprint_end.BlackoutDates.Clear();
                            cdr = new CalendarDateRange();
                            cdr.End = viewModel.CurrProject.StartDate.AddDays(-1);
                            datePicker_sprint_end.BlackoutDates.Add(cdr);
                            if (viewModel.CurrProject.EndDate.HasValue)
                            {
                                cdr = new CalendarDateRange();
                                cdr.Start = (DateTime)viewModel.CurrProject.EndDate;
                                cdr.Start = cdr.Start.AddDays(1);
                                datePicker_sprint_end.BlackoutDates.Add(cdr);
                            }
                        }
                        break;
                    case selection.Story:
                        button_story.FontWeight = FontWeights.Bold;
                        button_project.Visibility = Visibility.Visible;
                        button_sprint.Visibility = Visibility.Visible;
                        button_story.Visibility = Visibility.Visible;
                        leftList.ItemsSource = viewModel.TasksForStory;
                        grid_storyInfo.Visibility = Visibility.Visible;

                        ObservableCollection<SprintView> sv = viewModel.SprintsForProject;
                        comboBox_story_sprint.ItemsSource = sv;
                        comboBox_story_sprint.SelectedItem = sv.Where(sprint => sprint.SprintID == viewModel.CurrSprint.SprintID).FirstOrDefault();
                        button_saveStory.Content = "Save";
                        break;
                    case selection.Task:
                        button_task.FontWeight = FontWeights.Bold;
                        button_project.Visibility = Visibility.Visible;
                        button_sprint.Visibility = Visibility.Visible;
                        button_story.Visibility = Visibility.Visible;
                        button_task.Visibility = Visibility.Visible;
                        button_New.Visibility = Visibility.Hidden;
                        leftList.ItemsSource = new string[] { };
                        grid_taskInfo.Visibility = Visibility.Visible;

                        ObservableCollection<UserView> userList = viewModel.GetTeamMembers(viewModel.CurrTeam).Item1;
                        comboBox_task_owner.ItemsSource = userList;
                        comboBox_task_owner.SelectedItem = userList.Where(user => user.UserID == viewModel.CurrTask.OwnerID).FirstOrDefault();

                        comboBox_task_type.ItemsSource = EnumValues.taskType;
                        comboBox_task_type.SelectedItem = viewModel.CurrTask.Type;

                        comboBox_task_complexity.ItemsSource = EnumValues.sizeComplexity;
                        comboBox_task_complexity.SelectedItem = viewModel.CurrTask.SizeComplexity;

                        comboBox_task_value.ItemsSource = EnumValues.businessValue;
                        comboBox_task_value.SelectedItem = viewModel.CurrTask.BusinessValue;

                        comboBox_task_state.ItemsSource = EnumValues.taskState;
                        comboBox_task_state.SelectedItem = viewModel.CurrTask.State;

                        datePicker_task_completionDate.SelectedDate = viewModel.CurrTask.CompletionDate;
                        TaskDateChanged(null, null);
                        button_saveTask.Content = "Save";
                        if (viewModel.HistoricMode == false)
                        {
                            if ((viewModel.CurrTask.CompletionDate.HasValue && DateTime.Compare((DateTime)viewModel.CurrTask.CompletionDate, viewModel.CurrSprint.StartDate) < 0) || (viewModel.CurrTask.CompletionDate.HasValue && viewModel.CurrSprint.EndDate.HasValue && DateTime.Compare((DateTime)viewModel.CurrTask.CompletionDate, (DateTime)viewModel.CurrSprint.EndDate) > 0))
                            {
                                datePicker_task_completionDate.SelectedDate = null;
                                button_saveTask.Content = "Save*";
                                showInvalidDateWarning();
                            }

                            datePicker_task_completionDate.BlackoutDates.Clear();
                            CalendarDateRange cdr = new CalendarDateRange();
                            cdr.End = viewModel.CurrSprint.StartDate.AddDays(-1);
                            datePicker_task_completionDate.BlackoutDates.Add(cdr);
                            if (viewModel.CurrSprint.EndDate.HasValue)
                            {
                                cdr = new CalendarDateRange();
                                cdr.Start = (DateTime)viewModel.CurrSprint.EndDate;
                                cdr.Start = cdr.Start.AddDays(1);
                                datePicker_task_completionDate.BlackoutDates.Add(cdr);
                            }
                        }

                        taskReady = true;
                        break;
                    default:
                        break;
                }
            }
            catch (InvalidOperationException)
            {
                showCriticalError();
            }
        }

        void TaskOwnerChanged(object sender, SelectionChangedEventArgs e)
        {
            if (comboBox_task_owner.SelectedIndex == -1)
            {
                comboBox_task_state.SelectedIndex = 0;
                comboBox_task_state.IsEnabled = false;
            }
            else
            {
                if (comboBox_task_state.SelectedIndex == 0)
                {
                    comboBox_task_state.SelectedIndex = 1;
                }
                if (datePicker_task_completionDate.SelectedDate.HasValue)
                {
                    comboBox_task_state.SelectedIndex = 2;
                }
                comboBox_task_state.IsEnabled = true;
            }
            TaskInfoChanged(sender, e);
        }

        void TaskStateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (comboBox_task_state.SelectedIndex == 0)
            {
                comboBox_task_owner.SelectedIndex = -1;
                comboBox_task_state.IsEnabled = false;
            }
            TaskInfoChanged(sender, e);
        }

        private void TaskDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (datePicker_task_completionDate.SelectedDate.HasValue && comboBox_task_owner.SelectedIndex > -1)
            {
                comboBox_task_state.SelectedIndex = 2;
                comboBox_task_state.IsEnabled = false;
            }
            else if (datePicker_task_completionDate.SelectedDate.HasValue && comboBox_task_state.SelectedIndex == 2)
            {
                comboBox_task_state.IsEnabled = false;
            }
            else if (!datePicker_task_completionDate.SelectedDate.HasValue && comboBox_task_owner.SelectedIndex > -1)
            {
                comboBox_task_state.SelectedIndex = 1;
                comboBox_task_state.IsEnabled = true;
            }
            else if (!datePicker_task_completionDate.SelectedDate.HasValue)
            {
                comboBox_task_state.SelectedIndex = 0;
                comboBox_task_state.IsEnabled = false;
            }
            TaskInfoChanged(sender, e);
        }

        void save_project_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (viewModel.ChangeCurrProject(textBox_project_name.Text, datePicker_project_start.SelectedDate, datePicker_project_end.SelectedDate, (UserView)comboBox_project_owner.SelectedItem, viewModel.CurrTeam))
                {
                    button_saveProject.Content = "Save";
                }
                else
                {
                    saveFailure();
                }

            }
            catch (InvalidOperationException)
            {
                showCriticalError();
            }
            catch (ArgumentNullException)
            {
                showArgumentError();
            }
            update();
        }

        void save_sprint_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (viewModel.ChangeCurrSprint(textBox_sprint_name.Text, datePicker_sprint_start.SelectedDate, datePicker_sprint_end.SelectedDate))
                {
                    button_saveSprint.Content = "Save";
                }
                else
                {
                    saveFailure();
                }
            }
            catch (InvalidOperationException)
            {
                showCriticalError();
            }
            catch (ArgumentNullException)
            {
                showArgumentError();
            }
            update();
        }

        void save_story_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (viewModel.ChangeCurrStory(textBox_story_priority.Text, textBox_story_text.Text, (SprintView)comboBox_story_sprint.SelectedItem))
                {
                    button_saveStory.Content = "Save";
                }
                else
                {
                    saveFailure();
                }
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
            update();
        }

        void save_task_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (viewModel.ChangeCurrTask(textBox_task_text.Text, (int)comboBox_task_complexity.SelectedItem, (int)comboBox_task_value.SelectedItem, (UserView)comboBox_task_owner.SelectedItem, (TaskType)comboBox_task_type.SelectedItem, (TaskState)comboBox_task_state.SelectedItem, datePicker_task_completionDate.SelectedDate))
                {
                    button_saveTask.Content = "Save";
                }
                else
                {
                    saveFailure();
                }
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
            update();
        }

        void textBox_story_priority_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            e.Handled = e.Text.IsNonNumeric();
        }

        private void rightList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (rightList.SelectedIndex >= 0)
            {
                viewModel.CurrTask = (TaskView)rightList.SelectedItem;
                viewModel.JumpToTask(viewModel.CurrTask);
                currentSelection = selection.Task;
                grid_projectInfo.DataContext = viewModel.CurrProject;
                grid_sprintInfo.DataContext = viewModel.CurrSprint;
                grid_storyInfo.DataContext = viewModel.CurrStory;
                grid_taskInfo.DataContext = viewModel.CurrTask;
                comboBox_project_owner.ItemsSource = viewModel.AllManagers;
                comboBox_story_sprint.DataContext = viewModel;
                label_story_project.DataContext = viewModel.CurrProject;
                label_task_project.DataContext = viewModel.CurrProject;
                comboBox_task_owner.DataContext = viewModel.GetTeamMembers(viewModel.CurrTeam);
                comboBox_task_complexity.ItemsSource = ViewModel.EnumValues.sizeComplexity;
                comboBox_task_state.ItemsSource = ViewModel.EnumValues.taskState;
                comboBox_task_value.ItemsSource = ViewModel.EnumValues.businessValue;
                comboBox_task_type.ItemsSource = ViewModel.EnumValues.taskType;
                update();
            }
            rightList.SelectedIndex = -1;
        }

        private void button_New_Click(object sender, RoutedEventArgs e)
        {
            NewItemWindow niw = new NewItemWindow(currentSelection + 1, viewModel);
            niw.ShowDialog();
        }

        private void menu_addTeam_Click(object sender, RoutedEventArgs e)
        {
            NewItemWindow niw = new NewItemWindow(selection.Team, viewModel);
            niw.ShowDialog();
        }

        private void menu_main_SubmenuOpened(object sender, RoutedEventArgs e)
        {
            MenuItem[] teams = new MenuItem[viewModel.AllTeams.Count];
            for (int i = 0; i < teams.Length; i++)
            {
                teams[i] = new MenuItem();
                teams[i].Header = viewModel.AllTeams[i];
                teams[i].Click += new RoutedEventHandler(i_Click);
            }
            menu_addToTeam.ItemsSource = teams;
        }

        void i_Click(object sender, RoutedEventArgs e)
        {
            MenuItem mi = (MenuItem)sender;
            TeamWindow tw = new TeamWindow((TeamView)mi.Header, viewModel);
            tw.ShowDialog();
        }

        private void MenuItem_Checked(object sender, RoutedEventArgs e)
        {
            if (viewModel.HistoricMode == false)
            {
                BackgroundColor = Brushes.Lavender;
                viewModel.ToggleHistoricMode();
                currentSelection = selection.Home;
                grid_projectInfo.IsEnabled = false;
                foreach (UIElement child in grid_sprintInfo.Children)
                {
                    child.IsEnabled = false;
                }
                button_burndown.IsEnabled = true;
                grid_storyInfo.IsEnabled = false;
                grid_taskInfo.IsEnabled = false;
                button_project.Visibility = Visibility.Hidden;
                button_sprint.Visibility = Visibility.Hidden;
                button_story.Visibility = Visibility.Hidden;
                button_task.Visibility = Visibility.Hidden;
                menu_main.Visibility = Visibility.Hidden;
                button_New.Visibility = Visibility.Hidden;
                update();
            }
        }

        private void MenuItem_Unchecked(object sender, RoutedEventArgs e)
        {
            if (viewModel.HistoricMode == true)
            {
                BackgroundColor = Brushes.LightBlue;
                viewModel.ToggleHistoricMode();
                currentSelection = selection.Home;
                grid_projectInfo.IsEnabled = true;
                foreach (UIElement child in grid_sprintInfo.Children)
                {
                    child.IsEnabled = true;
                }
                grid_storyInfo.IsEnabled = true;
                grid_taskInfo.IsEnabled = true;
                button_project.Visibility = Visibility.Hidden;
                button_sprint.Visibility = Visibility.Hidden;
                button_story.Visibility = Visibility.Hidden;
                button_task.Visibility = Visibility.Hidden;
                if (viewModel.IsManager)
                {
                    menu_main.Visibility = Visibility.Visible;
                    button_New.Visibility = Visibility.Visible;
                }
                update();
            }
        }

        private void Burndown_Click(object sender, RoutedEventArgs e)
        {
            new BurndownWindow(viewModel).Visibility = Visibility.Visible;
        }

        private void ProjectInfoChanged(object sender, EventArgs e)
        {
            button_saveProject.Content = "Save*";
            button_saveProject.IsEnabled = viewModel.ValidateProject(textBox_project_name.Text, datePicker_project_start.SelectedDate, datePicker_project_end.SelectedDate, (UserView)comboBox_project_owner.SelectedItem, viewModel.CurrTeam);
        }

        private void SprintInfoChanged(object sender, EventArgs e)
        {
            button_saveSprint.Content = "Save*";
            button_saveSprint.IsEnabled = viewModel.ValidateSprint(textBox_sprint_name.Text, datePicker_sprint_start.SelectedDate, datePicker_sprint_end.SelectedDate);
        }

        private void StoryInfoChanged(object sender, EventArgs e)
        {
            button_saveStory.Content = "Save*";
            button_saveStory.IsEnabled = viewModel.ValidateStory(textBox_story_priority.Text, textBox_story_text.Text);
        }

        private void TaskInfoChanged(object sender, EventArgs e)
        {
            if (taskReady)
            {
                button_saveTask.Content = "Save*";
                button_saveTask.IsEnabled = viewModel.ValidateTask(textBox_task_text.Text, (UserView)comboBox_task_owner.SelectedItem, (TaskType?)comboBox_task_type.SelectedItem, (int?)comboBox_task_complexity.SelectedItem, (int?)comboBox_task_value.SelectedItem, datePicker_task_completionDate.SelectedDate, (TaskState?)comboBox_task_state.SelectedItem);
            }
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

        private void saveFailure()
        {
            MessageBox.Show(
                "Your changes could not be saved. Please review the data you entered and try again. If this problem persists, please contact your administrator.",
                "Save Failed",
                MessageBoxButton.OK,
                MessageBoxImage.Warning);
        }

        private void showInvalidDateWarning()
        {
            MessageBox.Show(
                "The dates on this item are no longer valid. They have been moved into the valid range of dates. Please review and correct this information.",
                "Invalid Date",
                MessageBoxButton.OK,
                MessageBoxImage.Warning);
        }
    }
}
