using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using ViewModel;
using System.Windows.Media;
using System.Windows.Data;

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
        private bool isUpdating;
        private bool taskReady;
        private TaskStateConverter tsConverter;
        private TaskTypeConverter ttConverter;
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
            tsConverter = new TaskStateConverter();
            ttConverter = new TaskTypeConverter();
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
            isUpdating = false;
            taskReady = false;
            update();
        }

        private void leftList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (leftList.SelectedIndex >= 0 && !isUpdating)
            {
                button_project.Visibility = Visibility.Hidden;
                button_sprint.Visibility = Visibility.Hidden;
                button_story.Visibility = Visibility.Hidden;
                button_task.Visibility = Visibility.Hidden;
                switch (currentSelection)
                {
                    case selection.Home:
                        try
                        {
                            viewModel.CurrProject = viewModel.ProjectsForUser[leftList.SelectedIndex];
                            comboBox_project_owner.ItemsSource = viewModel.AllManagers;
                            grid_projectInfo.DataContext = viewModel.CurrProject;
                        }
                        catch
                        {
                            this.Close();
                        }
                        break;
                    case selection.Project:
                        try
                        {
                            viewModel.CurrSprint = viewModel.SprintsForProject[leftList.SelectedIndex];
                            grid_sprintInfo.DataContext = viewModel.CurrSprint;
                        }
                        catch
                        {
                            this.Close();
                        }
                        break;
                    case selection.Sprint:
                        try
                        {
                            viewModel.CurrStory = viewModel.StoriesForSprint[leftList.SelectedIndex];
                            grid_storyInfo.DataContext = viewModel.CurrStory;
                            comboBox_story_sprint.DataContext = viewModel;
                            label_story_project.DataContext = viewModel.CurrProject;
                        }
                        catch
                        {
                            this.Close();
                        }
                        break;
                    case selection.Story:
                        try
                        {
                            viewModel.CurrTask = viewModel.TasksForStory[leftList.SelectedIndex];
                            grid_taskInfo.DataContext = viewModel.CurrTask;
                            label_task_project.DataContext = viewModel.CurrProject;
                            comboBox_task_owner.DataContext = viewModel.GetTeamMembers(viewModel.CurrTeam);
                            comboBox_task_complexity.ItemsSource = ViewModel.EnumValues.sizeComplexity;
                            comboBox_task_state.ItemsSource = ViewModel.EnumValues.taskState;
                            comboBox_task_value.ItemsSource = ViewModel.EnumValues.businessValue;
                            comboBox_task_type.ItemsSource = ViewModel.EnumValues.taskType;
                        }
                        catch
                        {
                            this.Close();
                        }
                        break;
                    case selection.Task:
                        break;
                    default:
                        break;
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
            if (!isUpdating)
            {
                try
                {
                    isUpdating = true;
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

                            UserView[] managerList = viewModel.AllManagers.ToArray();
                            comboBox_project_owner.ItemsSource = managerList;
                            comboBox_project_owner.SelectedItem = (from user in managerList where user.UserID == viewModel.CurrProject.OwnerID select user).Single();
                            break;
                        case selection.Sprint:
                            button_sprint.FontWeight = FontWeights.Bold;
                            button_project.Visibility = Visibility.Visible;
                            button_sprint.Visibility = Visibility.Visible;
                            leftList.ItemsSource = viewModel.StoriesForSprint;
                            grid_sprintInfo.Visibility = Visibility.Visible;
                            button_burndown.IsEnabled = viewModel.CurrSprint.EndDate.HasValue && viewModel.StoriesForSprint.Count > 0;
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
                            break;
                        case selection.Story:
                            button_story.FontWeight = FontWeights.Bold;
                            button_project.Visibility = Visibility.Visible;
                            button_sprint.Visibility = Visibility.Visible;
                            button_story.Visibility = Visibility.Visible;
                            leftList.ItemsSource = viewModel.TasksForStory;
                            grid_storyInfo.Visibility = Visibility.Visible;

                            SprintView[] sv = viewModel.SprintsForProject.ToArray();
                            comboBox_story_sprint.ItemsSource = sv;
                            comboBox_story_sprint.SelectedItem = (from sprint in sv where sprint.SprintID == viewModel.CurrSprint.SprintID select sprint).Single();
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

                            UserView[] userList = viewModel.GetTeamMembers(viewModel.CurrTeam).Item1.ToArray();
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
                            //TODO: disable completed combobox item

                            datePicker_task_completionDate.SelectedDate = viewModel.CurrTask.CompletionDate;

                            datePicker_task_completionDate.BlackoutDates.Clear();
                            cdr = new CalendarDateRange();
                            cdr.End = viewModel.CurrSprint.StartDate.AddDays(-1);
                            datePicker_task_completionDate.BlackoutDates.Add(cdr);
                            if (viewModel.CurrSprint.EndDate.HasValue)
                            {
                                cdr = new CalendarDateRange();
                                cdr.Start = (DateTime)viewModel.CurrSprint.EndDate;
                                cdr.Start = cdr.Start.AddDays(1);
                                datePicker_task_completionDate.BlackoutDates.Add(cdr);
                            }


                            taskReady = true;
                            break;
                        default:
                            break;

                    }
                }
                catch
                {
                    this.Close();
                }
            }
            isUpdating = false;
        }
            /*
            if (!isUpdating)
            {
                isUpdating = true;
                leftList.SelectedIndex = -1;
                column1.Width = new GridLength(1, GridUnitType.Star);
                column2.Width = new GridLength(1, GridUnitType.Star);
                button_New.Visibility = Visibility.Visible;
                switch (currentSelection)
                {
                    case selection.Home:
                        column1.Width = new GridLength(2, GridUnitType.Star);
                        column2.Width = new GridLength(0, GridUnitType.Star);
                        //left panel
                        //viewModel.UpdateProjectsForUser();
                        leftList.ItemsSource = viewModel.ProjectsForUser;
                        //right panel
                        stackPanel1.Children.RemoveRange(0, stackPanel1.Children.Count);
                        stackPanel2.Children.RemoveRange(0, stackPanel2.Children.Count);
                        ListBox lb = new ListBox();
                        lb.AlternationCount = 2;
                        lb.ItemsSource = viewModel.TasksForUser;
                        lb.SelectionChanged += new SelectionChangedEventHandler(lb_SelectionChanged);
                        stackPanel1.Children.Add(lb);
                        break;
                    case selection.Project:
                        //left panel
                        button_project.Visibility = Visibility.Visible;
                        //viewModel.UpdateSprintsForProject();
                        leftList.ItemsSource = viewModel.SprintsForProject;
                        //right panel
                        stackPanel1.Children.RemoveRange(0, stackPanel1.Children.Count);
                        stackPanel2.Children.RemoveRange(0, stackPanel2.Children.Count);
                        Label label = new Label();
                        label.Content = "Project Name:";
                        TextBox tb = new TextBox();
                        tb.Margin = new Thickness(0, 0, 0, 4);
                        tb.Text = viewModel.CurrProject.Name;
                        stackPanel1.Children.Add(label);
                        stackPanel2.Children.Add(tb);
                        label = new Label();
                        label.Content = "Team:";
                        stackPanel1.Children.Add(label);
                        label = new Label();
                        label.Content = viewModel.CurrTeam.Name;
                        stackPanel2.Children.Add(label);
                        label = new Label();
                        label.Content = "Start Date:";
                        stackPanel1.Children.Add(label);
                        DatePicker dp = new DatePicker();
                        dp.SelectedDate = viewModel.CurrProject.StartDate;
                        stackPanel2.Children.Add(dp);
                        label = new Label();
                        label.Content = "End Date:";
                        stackPanel1.Children.Add(label);
                        dp = new DatePicker();
                        dp.SelectedDate = viewModel.CurrProject.EndDate;
                        stackPanel2.Children.Add(dp);
                        label = new Label();
                        label.Content = "Owner:";
                        ComboBox cb = new ComboBox();
                        UserView[] managerList = viewModel.GetManagers().ToArray();
                        cb.ItemsSource = managerList;
                        UserView tempOwner = (from user in managerList where user.UserID == viewModel.CurrProject.OwnerID select user).Single();
                        cb.SelectedItem = tempOwner;
                        cb.Margin = new Thickness(0, 0, 0, 4);
                        stackPanel1.Children.Add(label);
                        stackPanel2.Children.Add(cb);
                        Button save = new Button();
                        save.Content = "Save";
                        save.Click += new RoutedEventHandler(save_project_Click);
                        stackPanel2.Children.Add(save);
                        break;
                    case selection.Sprint:
                        //left panel
                        button_project.Visibility = Visibility.Visible;
                        button_sprint.Visibility = Visibility.Visible;
                        //viewModel.UpdateStoriesForSprint();
                        leftList.ItemsSource = viewModel.StoriesForSprint;
                        //right panel
                        stackPanel1.Children.RemoveRange(0, stackPanel1.Children.Count);
                        stackPanel2.Children.RemoveRange(0, stackPanel2.Children.Count);
                        label = new Label();
                        label.Content = "Project Name:";
                        stackPanel1.Children.Add(label);
                        label = new Label();
                        label.Content = viewModel.CurrProject.Name;
                        stackPanel2.Children.Add(label);
                        label = new Label();
                        label.Content = "Sprint Name:";
                        tb = new TextBox();
                        tb.Margin = new Thickness(0, 0, 0, 4);
                        tb.Text = viewModel.CurrSprint.Name;
                        stackPanel1.Children.Add(label);
                        stackPanel2.Children.Add(tb);
                        label = new Label();
                        label.Content = "Start Date:";
                        stackPanel1.Children.Add(label);
                        dp = new DatePicker();
                        dp.SelectedDate = viewModel.CurrSprint.StartDate;
                        stackPanel2.Children.Add(dp);
                        label = new Label();
                        label.Content = "End Date:";
                        stackPanel1.Children.Add(label);
                        dp = new DatePicker();
                        dp.SelectedDate = viewModel.CurrSprint.EndDate;
                        stackPanel2.Children.Add(dp);
                        save = new Button();
                        save.Content = "Save";
                        save.Click += new RoutedEventHandler(save_sprint_Click);
                        stackPanel2.Children.Add(save);
                        break;
                    case selection.Story:
                        //left panel
                        button_project.Visibility = Visibility.Visible;
                        button_sprint.Visibility = Visibility.Visible;
                        button_story.Visibility = Visibility.Visible;
                        //viewModel.UpdateTasksForStory();
                        leftList.ItemsSource = viewModel.TasksForStory;
                        //right panel
                        stackPanel1.Children.RemoveRange(0, stackPanel1.Children.Count);
                        stackPanel2.Children.RemoveRange(0, stackPanel2.Children.Count);
                        label = new Label();
                        label.Content = "Project Name:";
                        stackPanel1.Children.Add(label);
                        label = new Label();
                        label.Content = viewModel.CurrProject.Name;
                        stackPanel2.Children.Add(label);
                        label = new Label();
                        label.Content = "Sprint:";
                        stackPanel1.Children.Add(label);
                        cb = new ComboBox();
                        SprintView[] sv = viewModel.SprintsForProject.ToArray();
                        cb.ItemsSource = sv;
                        SprintView tempSprint = (from sprint in sv where sprint.SprintID == viewModel.CurrSprint.SprintID select sprint).Single();
                        cb.SelectedValue = tempSprint;
                        cb.Margin = new Thickness(0, 0, 0, 4);
                        stackPanel2.Children.Add(cb);
                        label = new Label();
                        label.Content = "Priority:";
                        stackPanel1.Children.Add(label);
                        tb = new TextBox();
                        tb.Margin = new Thickness(0, 0, 0, 4);
                        tb.Text = viewModel.CurrStory.Priority.ToString();
                        tb.PreviewTextInput += new System.Windows.Input.TextCompositionEventHandler(tb_PreviewTextInput);
                        stackPanel2.Children.Add(tb);
                        label = new Label();
                        label.Content = "Text:";
                        tb = new TextBox();
                        tb.Margin = new Thickness(0, 0, 0, 4);
                        tb.TextWrapping = TextWrapping.Wrap;
                        tb.Text = viewModel.CurrStory.Text;
                        stackPanel1.Children.Add(label);
                        stackPanel2.Children.Add(tb);
                        save = new Button();
                        save.Content = "Save";
                        save.Click += new RoutedEventHandler(save_story_Click);
                        stackPanel2.Children.Add(save);
                        break;
                    case selection.Task:
                        //left panel
                        button_project.Visibility = Visibility.Visible;
                        button_sprint.Visibility = Visibility.Visible;
                        button_story.Visibility = Visibility.Visible;
                        button_task.Visibility = Visibility.Visible;
                        leftList.ItemsSource = new string[] { };
                        button_New.Visibility = Visibility.Hidden;
                        //right panel
                        stackPanel1.Children.RemoveRange(0, stackPanel1.Children.Count);
                        stackPanel2.Children.RemoveRange(0, stackPanel2.Children.Count);
                        label = new Label();
                        label.Content = "Project Name:";
                        stackPanel1.Children.Add(label);
                        label = new Label();
                        label.Content = viewModel.CurrProject.Name;
                        stackPanel2.Children.Add(label);
                        label = new Label();
                        label.Content = "Owner:";
                        cb = new ComboBox();
                        UserView[] userList = viewModel.GetTeamMembers(viewModel.CurrTeam).Item1.ToArray();
                        cb.ItemsSource = userList;
                        try
                        {
                            UserView tempUser = userList.Where(user => user.UserID == viewModel.CurrTask.OwnerID).FirstOrDefault();
                            cb.SelectedItem = tempUser;
                        }
                        catch (Exception)
                        {
                            //assume no owner.  will fix this later to be more specific
                        }
                        bool hasOwner = cb.SelectedIndex >= 0;
                        cb.Margin = new Thickness(0, 0, 0, 4);
                        stackPanel1.Children.Add(label);
                        stackPanel2.Children.Add(cb);
                        cb.SelectionChanged += new SelectionChangedEventHandler(taskOwnerChanged);
                        label = new Label();
                        label.Content = "Complexity:";
                        cb = new ComboBox();
                        cb.ItemsSource = ComplexityValues.sizeComplexity;
                        cb.SelectedItem = viewModel.CurrTask.SizeComplexity;
                        cb.Margin = new Thickness(0, 0, 0, 4);
                        stackPanel1.Children.Add(label);
                        stackPanel2.Children.Add(cb);
                        label = new Label();
                        label.Content = "Business Value:";
                        cb = new ComboBox();
                        cb.ItemsSource = ComplexityValues.businessValue;
                        cb.SelectedItem = viewModel.CurrTask.BusinessValue;
                        cb.Margin = new Thickness(0, 0, 0, 4);
                        stackPanel1.Children.Add(label);
                        stackPanel2.Children.Add(cb);
                        label = new Label();
                        label.Content = "Type:";
                        cb = new ComboBox();
                        cb.ItemsSource = Enum.GetValues(typeof(TaskType)).Cast<TaskType>().Select(type => ttConverter.Convert(type, typeof(string), null, null));
                        cb.SelectedItem = ttConverter.Convert(viewModel.CurrTask.Type, typeof(string), null, null);
                        cb.Margin = new Thickness(0, 0, 0, 4);
                        stackPanel1.Children.Add(label);
                        stackPanel2.Children.Add(cb);
                        label = new Label();
                        label.Content = "State:";
                        cb = new ComboBox();
                        // Max - this conversion and others like it can be done entirely in XAML
                        cb.ItemsSource = Enum.GetValues(typeof(TaskState)).Cast<TaskState>().Select(state => tsConverter.Convert(state, typeof(string), null, null));
                        cb.SelectedItem = tsConverter.Convert(viewModel.CurrTask.State, typeof(string), null, null);
                        cb.Margin = new Thickness(0, 0, 0, 4);
                        stackPanel1.Children.Add(label);
                        stackPanel2.Children.Add(cb);
                        cb.SelectionChanged += new SelectionChangedEventHandler(taskStateChanged);
                        cb.IsEnabled = hasOwner;
                        label = new Label();
                        label.Content = "Completion Date:";
                        dp = new DatePicker();
                        dp.SelectedDate = viewModel.CurrTask.CompletionDate;
                        stackPanel1.Children.Add(label);
                        stackPanel2.Children.Add(dp);
                        label = new Label();
                        label.Content = "Text:";
                        tb = new TextBox();
                        tb.Margin = new Thickness(0, 0, 0, 4);
                        tb.TextWrapping = TextWrapping.Wrap;
                        tb.Text = viewModel.CurrTask.Text;
                        stackPanel1.Children.Add(label);
                        stackPanel2.Children.Add(tb);
                        save = new Button();
                        save.Content = "Save";
                        save.Click += new RoutedEventHandler(save_task_Click);
                        stackPanel2.Children.Add(save);
                        break;
                    default:
                        break;
                }
            }
            isUpdating = false;
        }
        */
        

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
            if (datePicker_task_completionDate.SelectedDate.HasValue)
            {
                comboBox_task_state.SelectedIndex = 2;
                comboBox_task_state.IsEnabled = false;
            }
            else
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
                    comboBox_task_state.IsEnabled = true;
                }
            }
            TaskInfoChanged(sender, e);
        }

        void save_project_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (viewModel.ChangeCurrProject(textBox_project_name.Text, datePicker_project_start.SelectedDate, datePicker_project_end.SelectedDate, viewModel.AllManagers[comboBox_project_owner.SelectedIndex], viewModel.CurrTeam))
                {
                    MessageBox.Show("Your changes have been saved.", "Project Saved", MessageBoxButton.OK);
                }
                else
                {
                    MessageBox.Show("Your changes were not saved.", "Save Failed", MessageBoxButton.OK);
                }

            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show(ex.Message, "InvalidOperationException", MessageBoxButton.OK);
            }
            catch (ArgumentNullException ex)
            {
                MessageBox.Show(ex.Message, "ArgumentNullException", MessageBoxButton.OK);
            }
        }

        void save_sprint_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (viewModel.ChangeCurrSprint(textBox_sprint_name.Text, datePicker_sprint_start.SelectedDate, datePicker_sprint_end.SelectedDate))
                {
                    MessageBox.Show("Your changes have been saved.", "Sprint Saved", MessageBoxButton.OK);
                }
                else
                {
                    MessageBox.Show("Your changes were not saved.", "Save Failed", MessageBoxButton.OK);
                }

            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show(ex.Message, "InvalidOperationException", MessageBoxButton.OK);
            }
            catch (ArgumentNullException ex)
            {
                MessageBox.Show(ex.Message, "ArgumentNullException", MessageBoxButton.OK);
            }
        }

        void save_story_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (viewModel.ChangeCurrStory(textBox_story_priority.Text, textBox_story_text.Text, viewModel.SprintsForProject[comboBox_story_sprint.SelectedIndex]))
                {
                    MessageBox.Show("Your changes have been saved.", "Story Saved", MessageBoxButton.OK);
                }
                else
                {
                    MessageBox.Show("Your changes were not saved.", "Save Failed", MessageBoxButton.OK);
                }

            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show(ex.Message, "InvalidOperationException", MessageBoxButton.OK);
            }
            catch (ArgumentNullException ex)
            {
                MessageBox.Show(ex.Message, "ArgumentNullException", MessageBoxButton.OK);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                MessageBox.Show(ex.Message, "ArgumentOutOfRangeException", MessageBoxButton.OK);
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message, "ArgumentException", MessageBoxButton.OK);
            }
        }

        void save_task_Click(object sender, RoutedEventArgs e)
        {
            try
            {
            if (viewModel.ChangeCurrTask(textBox_task_text.Text, (int)comboBox_task_complexity.SelectedItem, (int)comboBox_task_value.SelectedItem, (UserView)comboBox_task_owner.SelectedItem, (TaskType)comboBox_task_type.SelectedItem, (TaskState)comboBox_task_state.SelectedItem, datePicker_task_completionDate.SelectedDate))
            {
                MessageBox.Show("Your changes have been saved.", "Task Saved", MessageBoxButton.OK);
            }
            else
            {
                MessageBox.Show("Your changes were not saved.", "Save Failed", MessageBoxButton.OK);
            }

            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show(ex.Message, "InvalidOperationException", MessageBoxButton.OK);
            }
            catch (ArgumentNullException ex)
            {
                MessageBox.Show(ex.Message, "ArgumentNullException", MessageBoxButton.OK);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                MessageBox.Show(ex.Message, "ArgumentOutOfRangeException", MessageBoxButton.OK);
            }
        }

        void textBox_story_priority_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            e.Handled = e.Text.IsNonNumeric();
        }
        /*
        void lb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListBox lb = (ListBox)sender;
            if (lb.SelectedIndex >= 0)
            {
                viewModel.CurrTask = viewModel.TasksForUser[lb.SelectedIndex];
                viewModel.JumpToTask(viewModel.CurrTask);
                currentSelection = selection.Task;
                update();
            }
        }
        */
        private void rightList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (rightList.SelectedIndex >= 0)
            {
                viewModel.CurrTask = viewModel.TasksForUser[rightList.SelectedIndex];
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
            NewItemWindow niw = new NewItemWindow(currentSelection + 1, viewModel, tsConverter, ttConverter);
            niw.Visibility = Visibility.Visible;
        }

        private void menu_addTeam_Click(object sender, RoutedEventArgs e)
        {
            NewItemWindow niw = new NewItemWindow(selection.Team, viewModel, tsConverter, ttConverter);
            niw.Visibility = Visibility.Visible;
        }

        private void menu_main_SubmenuOpened(object sender, RoutedEventArgs e)
        {
            //viewModel.UpdateAllTeams();
            MenuItem[] teams = new MenuItem[viewModel.AllTeams.Count];
            for (int i = 0; i < teams.Length; i++)
            {
                teams[i] = new MenuItem();
                teams[i].Header = viewModel.AllTeams[i].Name;
                teams[i].Click += new RoutedEventHandler(i_Click);
            }
            menu_addToTeam.ItemsSource = teams;
        }

        void i_Click(object sender, RoutedEventArgs e)
        {
            MenuItem mi = (MenuItem)sender;
            foreach (TeamView tv in viewModel.AllTeams)
            {
                if (tv.Name.Equals(mi.Header))
                {
                    TeamWindow tw = new TeamWindow(tv, viewModel);
                    tw.Visibility = Visibility.Visible;
                    break;
                }
            }
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
            button_saveProject.IsEnabled = viewModel.ValidateProject(textBox_project_name.Text, datePicker_project_start.SelectedDate, datePicker_project_end.SelectedDate, (UserView)comboBox_project_owner.SelectedItem, viewModel.CurrTeam);
        }

        private void SprintInfoChanged(object sender, EventArgs e)
        {
            button_saveSprint.IsEnabled = viewModel.ValidateSprint( textBox_sprint_name.Text, datePicker_sprint_start.SelectedDate, datePicker_sprint_end.SelectedDate);
        }

        private void StoryInfoChanged(object sender, EventArgs e)
        {
            button_saveStory.IsEnabled = viewModel.ValidateStory(textBox_story_priority.Text, textBox_story_text.Text);
        }

        private void TaskInfoChanged(object sender, EventArgs e)
        {
            if (taskReady)
            {
                try
                {
                    button_saveTask.IsEnabled = viewModel.ValidateTask(textBox_task_text.Text, (UserView)comboBox_task_owner.SelectedItem, (TaskType?)comboBox_task_type.SelectedItem, (int?)comboBox_task_complexity.SelectedItem, (int?)comboBox_task_value.SelectedItem, datePicker_task_completionDate.SelectedDate, (TaskState?)comboBox_task_state.SelectedItem);
                }
                catch
                {
                    button_saveTask.IsEnabled = false;
                }
            }
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
