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
        private selection _currentSelection;
        private SPMSViewModel _viewModel;
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
            _viewModel = vm;
            InitializeComponent();
            BackgroundColor = Brushes.LightBlue;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (!_viewModel.IsManager)
            {
                menu_main.Visibility = Visibility.Hidden;
                button_New.Visibility = Visibility.Hidden;
            }
            this.DataContext = _viewModel;
            _currentSelection = selection.Home;
            update();
        }

        #region Change Current View
        private void leftList_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            //Event handler for clicking on the left list.
            //used MouseLeftButtonUp to fix bug with dragging the mouse
            if (leftList.SelectedIndex >= 0)
            {
                //hide all bread crumbs except home
                button_project.Visibility = Visibility.Hidden;
                button_sprint.Visibility = Visibility.Hidden;
                button_story.Visibility = Visibility.Hidden;
                button_task.Visibility = Visibility.Hidden;

                try
                {
                    //set up the next pane to load
                    switch (_currentSelection)
                    {
                        case selection.Home:
                            _viewModel.CurrProject = (ProjectView)leftList.SelectedItem;
                            comboBox_project_owner.ItemsSource = _viewModel.AllManagers;
                            grid_projectInfo.DataContext = _viewModel.CurrProject;
                            break;
                        case selection.Project:
                            _viewModel.CurrSprint = (SprintView)leftList.SelectedItem;
                            grid_sprintInfo.DataContext = _viewModel.CurrSprint;
                            break;
                        case selection.Sprint:
                            _viewModel.CurrStory = (StoryView)leftList.SelectedItem;
                            grid_storyInfo.DataContext = _viewModel.CurrStory;
                            comboBox_story_sprint.DataContext = _viewModel;
                            label_story_project.DataContext = _viewModel.CurrProject;
                            break;
                        case selection.Story:
                            _viewModel.CurrTask = (TaskView)leftList.SelectedItem;
                            grid_taskInfo.DataContext = _viewModel.CurrTask;
                            label_task_project.DataContext = _viewModel.CurrProject;
                            comboBox_task_owner.DataContext = _viewModel.GetTeamMembers(_viewModel.CurrTeam);
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
                _currentSelection++;
                update();
            }
        }

        private void rightList_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            //jumping to a task
            if (rightList.SelectedIndex >= 0)
            {
                _viewModel.CurrTask = (TaskView)rightList.SelectedItem;
                _viewModel.JumpToTask(_viewModel.CurrTask);
                _currentSelection = selection.Task;
                grid_projectInfo.DataContext = _viewModel.CurrProject;
                grid_sprintInfo.DataContext = _viewModel.CurrSprint;
                grid_storyInfo.DataContext = _viewModel.CurrStory;
                grid_taskInfo.DataContext = _viewModel.CurrTask;
                comboBox_project_owner.ItemsSource = _viewModel.AllManagers;
                comboBox_story_sprint.DataContext = _viewModel;
                label_story_project.DataContext = _viewModel.CurrProject;
                label_task_project.DataContext = _viewModel.CurrProject;
                comboBox_task_owner.DataContext = _viewModel.GetTeamMembers(_viewModel.CurrTeam);
                comboBox_task_complexity.ItemsSource = ViewModel.EnumValues.sizeComplexity;
                comboBox_task_state.ItemsSource = ViewModel.EnumValues.taskState;
                comboBox_task_value.ItemsSource = ViewModel.EnumValues.businessValue;
                comboBox_task_type.ItemsSource = ViewModel.EnumValues.taskType;
                update();
            }
            rightList.SelectedIndex = -1;
        }

        private void button_home_Click(object sender, RoutedEventArgs e)
        {
            //switch to the home tab
            _currentSelection = selection.Home;
            update();
        }

        private void button_project_Click(object sender, RoutedEventArgs e)
        {
            //switch to the project tab
            _currentSelection = selection.Project;
            update();
        }

        private void button_sprint_Click(object sender, RoutedEventArgs e)
        {
            //switch to the sprint tab
            _currentSelection = selection.Sprint;
            update();
        }

        private void button_story_Click(object sender, RoutedEventArgs e)
        {
            //switch to the story tab
            _currentSelection = selection.Story;
            update();
        } 

        private void button_task_Click(object sender, RoutedEventArgs e)
        {
            //switch to the task tab
            _currentSelection = selection.Task;
            update();
        }
        #endregion

        #region Update Methods
        /// <summary>
        /// Sets all visible content
        /// </summary>
        private void update()
        {
            try
            {
                //resets any attributes and UI elemensts that need to be reset whenever the program state changes
                button_New.Content = "Add " + System.Enum.GetName(_currentSelection.GetType(), _currentSelection + 1); //changes text of the add button
                leftList.SelectedIndex = -1; //nothing selected in left list
                //hide all of the right panes.  one will be made visible later
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
                //reset the blackout dates on date pickers so the code doesn't crash before new dates are set
                datePicker_sprint_start.BlackoutDates.Clear();
                datePicker_sprint_end.BlackoutDates.Clear();
                datePicker_task_completionDate.BlackoutDates.Clear();

                if (_viewModel.IsManager && _viewModel.HistoricMode == false)
                {
                    button_New.Visibility = Visibility.Visible;
                }
                switch (_currentSelection)
                {
                    case selection.Home:
                        UpdateHome();
                        break;
                    case selection.Project:
                        UpdateProject();
                        break;
                    case selection.Sprint:
                        UpdateSprint();
                        break;
                    case selection.Story:
                        UpdateStory();
                        break;
                    case selection.Task:
                        UpdateTask();
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

        /// <summary>
        /// sets the display when viewing home
        /// </summary>
        private void UpdateHome()
        {
            button_home.FontWeight = FontWeights.Bold;
            leftList.ItemsSource = _viewModel.ProjectsForUser;//the source for the list changes depending on view
            rightList.Visibility = Visibility.Visible;
            rightList.ItemsSource = _viewModel.TasksForUser;
        }

        /// <summary>
        /// sets the display when viewing project
        /// </summary>
        private void UpdateProject()
        {
            button_project.FontWeight = FontWeights.Bold;
            button_project.Visibility = Visibility.Visible;
            leftList.ItemsSource = _viewModel.SprintsForProject;
            grid_projectInfo.Visibility = Visibility.Visible;

            //sets the content of the combobox to be all managers then sets the correct initial selected value
            ObservableCollection<UserView> managerList = _viewModel.AllManagers;
            comboBox_project_owner.ItemsSource = managerList;
            comboBox_project_owner.SelectedItem = managerList.Where(user => user.UserID == _viewModel.CurrProject.OwnerID).FirstOrDefault();
            button_saveProject.Content = "Save";
        }

        /// <summary>
        /// sets the display when viewing sprint
        /// </summary>
        private void UpdateSprint()
        {
            button_sprint.FontWeight = FontWeights.Bold;
            button_project.Visibility = Visibility.Visible;
            button_sprint.Visibility = Visibility.Visible;
            leftList.ItemsSource = _viewModel.StoriesForSprint;
            grid_sprintInfo.Visibility = Visibility.Visible;
            button_burndown.IsEnabled = _viewModel.CurrSprint.EndDate.HasValue && _viewModel.StoriesForSprint.Count > 0;
            button_saveSprint.Content = "Save";
            //sets blackout dates on the start and end date
            if (!_viewModel.HistoricMode)
            {
                if (_viewModel.CurrSprint.StartDate < _viewModel.CurrProject.StartDate || (_viewModel.CurrProject.EndDate.HasValue && _viewModel.CurrSprint.StartDate > _viewModel.CurrProject.EndDate.Value))
                {
                    datePicker_sprint_start.SelectedDate = null;
                    button_saveSprint.Content = "Save*";
                    if ((_viewModel.CurrSprint.EndDate.HasValue && _viewModel.CurrSprint.EndDate.Value < _viewModel.CurrProject.StartDate) || (_viewModel.CurrProject.EndDate.HasValue && _viewModel.CurrSprint.EndDate.HasValue && _viewModel.CurrSprint.EndDate.Value > _viewModel.CurrProject.EndDate.Value))
                    {
                        datePicker_sprint_end.SelectedDate = null;
                        showInvalidDateWarning();
                    }
                    else
                    {
                        showInvalidDateWarning();
                    }
                }
                else if ((_viewModel.CurrSprint.EndDate.HasValue && _viewModel.CurrSprint.EndDate.Value < _viewModel.CurrProject.StartDate) || (_viewModel.CurrProject.EndDate.HasValue && _viewModel.CurrSprint.EndDate.HasValue && _viewModel.CurrSprint.EndDate.Value > _viewModel.CurrProject.EndDate.Value))
                {
                    datePicker_sprint_end.SelectedDate = null;
                    button_saveSprint.Content = "Save*";
                    showInvalidDateWarning();
                }
                CalendarDateRange cdr = new CalendarDateRange();
                cdr.End = _viewModel.CurrProject.StartDate.AddDays(-1);
                datePicker_sprint_start.BlackoutDates.Add(cdr);
                if (_viewModel.CurrProject.EndDate.HasValue)
                {
                    cdr = new CalendarDateRange();
                    cdr.Start = (DateTime)_viewModel.CurrProject.EndDate;
                    cdr.Start = cdr.Start.AddDays(1);
                    datePicker_sprint_start.BlackoutDates.Add(cdr);
                }
                cdr = new CalendarDateRange();
                cdr.End = _viewModel.CurrProject.StartDate.AddDays(-1);
                datePicker_sprint_end.BlackoutDates.Add(cdr);
                if (_viewModel.CurrProject.EndDate.HasValue)
                {
                    cdr = new CalendarDateRange();
                    cdr.Start = (DateTime)_viewModel.CurrProject.EndDate;
                    cdr.Start = cdr.Start.AddDays(1);
                    datePicker_sprint_end.BlackoutDates.Add(cdr);
                }
            }
        }

        /// <summary>
        /// sets the display when viewing story
        /// </summary>
        private void UpdateStory()
        {
            button_story.FontWeight = FontWeights.Bold;
            button_project.Visibility = Visibility.Visible;
            button_sprint.Visibility = Visibility.Visible;
            button_story.Visibility = Visibility.Visible;
            leftList.ItemsSource = _viewModel.TasksForStory;
            grid_storyInfo.Visibility = Visibility.Visible;

            ObservableCollection<SprintView> sv = _viewModel.SprintsForProject;
            comboBox_story_sprint.ItemsSource = sv;
            comboBox_story_sprint.SelectedItem = sv.Where(sprint => sprint.SprintID == _viewModel.CurrSprint.SprintID).FirstOrDefault();
            button_saveStory.Content = "Save";
        }

        /// <summary>
        /// sets the display when viewing task
        /// </summary>
        private void UpdateTask()
        {
            button_task.FontWeight = FontWeights.Bold;
            button_project.Visibility = Visibility.Visible;
            button_sprint.Visibility = Visibility.Visible;
            button_story.Visibility = Visibility.Visible;
            button_task.Visibility = Visibility.Visible;
            button_New.Visibility = Visibility.Hidden;
            leftList.ItemsSource = new string[] { };
            grid_taskInfo.Visibility = Visibility.Visible;

            //need to manually set the contents and initial values of comboboxs
            ObservableCollection<UserView> userList = _viewModel.GetTeamMembers(_viewModel.CurrTeam).Item1;
            comboBox_task_owner.ItemsSource = userList;
            comboBox_task_owner.SelectedItem = userList.Where(user => user.UserID == _viewModel.CurrTask.OwnerID).FirstOrDefault();

            comboBox_task_type.ItemsSource = EnumValues.taskType;
            comboBox_task_type.SelectedItem = _viewModel.CurrTask.Type;

            comboBox_task_complexity.ItemsSource = EnumValues.sizeComplexity;
            comboBox_task_complexity.SelectedItem = _viewModel.CurrTask.SizeComplexity;

            comboBox_task_value.ItemsSource = EnumValues.businessValue;
            comboBox_task_value.SelectedItem = _viewModel.CurrTask.BusinessValue;

            comboBox_task_state.ItemsSource = EnumValues.taskState;
            comboBox_task_state.SelectedItem = _viewModel.CurrTask.State;

            datePicker_task_completionDate.SelectedDate = _viewModel.CurrTask.CompletionDate;
            TaskDateChanged(null, null);
            button_saveTask.Content = "Save";
            //set the blackout dates
            if (!_viewModel.HistoricMode)
            {
                if ((_viewModel.CurrTask.CompletionDate.HasValue && _viewModel.CurrTask.CompletionDate.Value < _viewModel.CurrSprint.StartDate) || (_viewModel.CurrTask.CompletionDate.HasValue && _viewModel.CurrSprint.EndDate.HasValue && _viewModel.CurrTask.CompletionDate.Value > _viewModel.CurrSprint.EndDate.Value))
                {
                    datePicker_task_completionDate.SelectedDate = null;
                    button_saveTask.Content = "Save*";
                    showInvalidDateWarning();
                }

                CalendarDateRange cdr = new CalendarDateRange();
                cdr.End = _viewModel.CurrSprint.StartDate.AddDays(-1);
                datePicker_task_completionDate.BlackoutDates.Add(cdr);
                if (_viewModel.CurrSprint.EndDate.HasValue)
                {
                    cdr = new CalendarDateRange();
                    cdr.Start = (DateTime)_viewModel.CurrSprint.EndDate;
                    cdr.Start = cdr.Start.AddDays(1);
                    datePicker_task_completionDate.BlackoutDates.Add(cdr);
                }
            }
        }
        #endregion

        #region Info Changed
        private void ProjectInfoChanged(object sender, EventArgs e)
        {
            button_saveProject.Content = "Save*";
            button_saveProject.IsEnabled = _viewModel.ValidateProject(textBox_project_name.Text, datePicker_project_start.SelectedDate, datePicker_project_end.SelectedDate, (UserView)comboBox_project_owner.SelectedItem, _viewModel.CurrTeam);
        }

        private void SprintInfoChanged(object sender, EventArgs e)
        {
            button_saveSprint.Content = "Save*";
            button_saveSprint.IsEnabled = _viewModel.ValidateSprint(textBox_sprint_name.Text, datePicker_sprint_start.SelectedDate, datePicker_sprint_end.SelectedDate);
        }

        private void StoryInfoChanged(object sender, EventArgs e)
        {
            button_saveStory.Content = "Save*";
            button_saveStory.IsEnabled = _viewModel.ValidateStory(textBox_story_priority.Text, textBox_story_text.Text);
        }

        private void TaskInfoChanged(object sender, EventArgs e)
        {
            button_saveTask.Content = "Save*";
            button_saveTask.IsEnabled = _viewModel.ValidateTask(textBox_task_text.Text, (UserView)comboBox_task_owner.SelectedItem, (TaskType?)comboBox_task_type.SelectedItem, (int?)comboBox_task_complexity.SelectedItem, (int?)comboBox_task_value.SelectedItem, datePicker_task_completionDate.SelectedDate, (TaskState?)comboBox_task_state.SelectedItem);
        }

        private void TaskOwnerChanged(object sender, SelectionChangedEventArgs e)
        {
            //used to maintain the logic on task state
            if (comboBox_task_owner.SelectedIndex == -1)
            {
                comboBox_task_state.SelectedItem = TaskState.Unassigned;
                comboBox_task_state.IsEnabled = false;
            }
            else
            {
                if (comboBox_task_state.SelectedItem != null && comboBox_task_state.SelectedItem.Equals(TaskState.Unassigned))
                {
                    comboBox_task_state.SelectedItem = TaskState.In_Progress;
                }
                if (datePicker_task_completionDate.SelectedDate.HasValue)
                {
                    comboBox_task_state.SelectedItem = TaskState.Completed;
                }
                comboBox_task_state.IsEnabled = true;
            }
            TaskInfoChanged(sender, e);
        }

        private void TaskStateChanged(object sender, SelectionChangedEventArgs e)
        {
            //used to maintain the logic on task state
            if (comboBox_task_state.SelectedItem != null && comboBox_task_state.SelectedItem.Equals(TaskState.Unassigned))
            {
                comboBox_task_owner.SelectedIndex = -1;
                comboBox_task_state.IsEnabled = false;
            }
            TaskInfoChanged(sender, e);
        }

        private void TaskDateChanged(object sender, SelectionChangedEventArgs e)
        {
            //used to maintain the logic on task state
            if (datePicker_task_completionDate.SelectedDate.HasValue && comboBox_task_owner.SelectedIndex > -1)
            {
                comboBox_task_state.SelectedItem = TaskState.Completed;
                comboBox_task_state.IsEnabled = false;
            }
            else if (datePicker_task_completionDate.SelectedDate.HasValue && comboBox_task_state.SelectedItem.Equals(TaskState.Completed))
            {
                comboBox_task_state.IsEnabled = false;
            }
            else if (!datePicker_task_completionDate.SelectedDate.HasValue && comboBox_task_owner.SelectedIndex > -1)
            {
                comboBox_task_state.SelectedItem = TaskState.In_Progress;
                comboBox_task_state.IsEnabled = true;
            }
            else if (!datePicker_task_completionDate.SelectedDate.HasValue)
            {
                comboBox_task_state.SelectedItem = TaskState.Unassigned;
                comboBox_task_state.IsEnabled = false;
            }
            TaskInfoChanged(sender, e);
        }
        #endregion

        #region Save
        private void save_project_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (_viewModel.ChangeCurrProject(textBox_project_name.Text, datePicker_project_start.SelectedDate, datePicker_project_end.SelectedDate, (UserView)comboBox_project_owner.SelectedItem, _viewModel.CurrTeam))
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

        private void save_sprint_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (_viewModel.ChangeCurrSprint(textBox_sprint_name.Text, datePicker_sprint_start.SelectedDate, datePicker_sprint_end.SelectedDate))
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

        private void save_story_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (_viewModel.ChangeCurrStory(textBox_story_priority.Text, textBox_story_text.Text, (SprintView)comboBox_story_sprint.SelectedItem))
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

        private void save_task_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (_viewModel.ChangeCurrTask(textBox_task_text.Text, (int)comboBox_task_complexity.SelectedItem, (int)comboBox_task_value.SelectedItem, (UserView)comboBox_task_owner.SelectedItem, (TaskType)comboBox_task_type.SelectedItem, (TaskState)comboBox_task_state.SelectedItem, datePicker_task_completionDate.SelectedDate))
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
        #endregion

        #region Open Another Window
        private void button_New_Click(object sender, RoutedEventArgs e)
        {
            //opens the window for adding a new project, sprint, story, task
            NewItemWindow niw = new NewItemWindow(_currentSelection + 1, _viewModel);
            niw.ShowDialog();
        }

        private void menu_addTeam_Click(object sender, RoutedEventArgs e)
        {
            //opens the window for creating a new team
            NewItemWindow niw = new NewItemWindow(selection.Team, _viewModel);
            niw.ShowDialog();
        }

        private void menu_main_SubmenuOpened(object sender, RoutedEventArgs e)
        {
            //populates the list of teams when moving team members
            MenuItem[] teams = new MenuItem[_viewModel.AllTeams.Count];
            for (int i = 0; i < teams.Length; i++)
            {
                teams[i] = new MenuItem();
                teams[i].Header = _viewModel.AllTeams[i];
                teams[i].Click += new RoutedEventHandler(menu_teamName_Click);
            }
            menu_addToTeam.ItemsSource = teams;
        }

        private void menu_teamName_Click(object sender, RoutedEventArgs e)
        {
            //opens the window to move team members to a team
            MenuItem mi = (MenuItem)sender;
            TeamWindow tw = new TeamWindow((TeamView)mi.Header, _viewModel);
            tw.ShowDialog();
        }

        private void Burndown_Click(object sender, RoutedEventArgs e)
        {
            //displays the burndown
            new BurndownWindow(_viewModel).Visibility = Visibility.Visible;
        }

        private void Status_Click(object sender, RoutedEventArgs e)
        {
            //displays the status chart
            new StatusChart(_viewModel).Visibility = Visibility.Visible;
        }
        #endregion

        private void textBox_story_priority_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            e.Handled = e.Text.IsNonNumeric();
        }

        private void MenuItem_Checked(object sender, RoutedEventArgs e)
        {
            //turns on historic mode
            if (_viewModel.HistoricMode == false)
            {
                BackgroundColor = Brushes.Lavender;
                _viewModel.ToggleHistoricMode();
                _currentSelection = selection.Home;
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
            //turns off historic mode
            if (_viewModel.HistoricMode == true)
            {
                BackgroundColor = Brushes.LightBlue;
                _viewModel.ToggleHistoricMode();
                _currentSelection = selection.Home;
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
                if (_viewModel.IsManager)
                {
                    menu_main.Visibility = Visibility.Visible;
                    button_New.Visibility = Visibility.Visible;
                }
                update();
            }
        }

        #region Exception messages
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
        #endregion
    }
}
