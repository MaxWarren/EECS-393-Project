using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
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
        private bool isUpdating;
        private TaskStateConverter tsConverter;
        private TaskTypeConverter ttConverter;

        public enum selection
        {
            Home,
            Project,
            Sprint,
            Story,
            Task,
            Team
        };

        public MainWindow(SPMSViewModel vm)
        {
            viewModel = vm;
            InitializeComponent();
            tsConverter = new TaskStateConverter();
            ttConverter = new TaskTypeConverter();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (!viewModel.IsManager)
            {
                menu1.Visibility = Visibility.Hidden;
                grid3.Margin = new Thickness(0, 0, 0, 0);
                grid2.Margin = new Thickness(0, 35, 0, 0);
                button_New.Visibility = Visibility.Hidden;
            }
            this.DataContext = viewModel;
            currentSelection = selection.Home;
            isUpdating = false;
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
                switch (currentSelection)
                {
                    case selection.Home:
                        viewModel.CurrProject = viewModel.ProjectsForUser[leftList.SelectedIndex];
                        comboBox_project_owner.ItemsSource = viewModel.GetManagers();
                        grid_projectInfo.DataContext = viewModel.CurrProject;
                        break;
                    case selection.Project:
                        viewModel.CurrSprint = viewModel.SprintsForProject[leftList.SelectedIndex];
                        grid_sprintInfo.DataContext = viewModel.CurrSprint;
                        break;
                    case selection.Sprint:
                        viewModel.CurrStory = viewModel.StoriesForSprint[leftList.SelectedIndex];
                        grid_storyInfo.DataContext = viewModel.CurrStory;
                        comboBox_story_sprint.DataContext = viewModel;
                        label_story_project.DataContext = viewModel.CurrProject;
                        break;
                    case selection.Story:
                        viewModel.CurrTask = viewModel.TasksForStory[leftList.SelectedIndex];
                        grid_taskInfo.DataContext = viewModel.CurrTask;
                        label_task_project.DataContext = viewModel.CurrProject;
                        comboBox_task_owner.DataContext = viewModel.GetTeamMembers(viewModel.CurrTeam);
                        comboBox_task_complexity.ItemsSource = ViewModel.EnumValues.sizeComplexity;
                        comboBox_task_state.ItemsSource = ViewModel.EnumValues.taskState;
                        comboBox_task_value.ItemsSource = ViewModel.EnumValues.businessValue;
                        comboBox_task_type.ItemsSource = ViewModel.EnumValues.taskType;
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
                grid_projectInfo.Visibility = Visibility.Hidden;
                grid_sprintInfo.Visibility = Visibility.Hidden;
                grid_storyInfo.Visibility = Visibility.Hidden;
                grid_taskInfo.Visibility = Visibility.Hidden;
                switch (currentSelection)
                {
                    case selection.Home:
                        leftList.ItemsSource = viewModel.ProjectsForUser;
                        break;
                    case selection.Project:
                        leftList.ItemsSource = viewModel.SprintsForProject;
                        grid_projectInfo.Visibility = Visibility.Visible;
                        break;
                    case selection.Sprint:
                        leftList.ItemsSource = viewModel.StoriesForSprint;
                        grid_sprintInfo.Visibility = Visibility.Visible;
                        break;
                    case selection.Story:
                        leftList.ItemsSource = viewModel.TasksForStory;
                        grid_storyInfo.Visibility = Visibility.Visible;
                        break;
                    case selection.Task:
                        leftList.ItemsSource = new string[] { };
                        grid_taskInfo.Visibility = Visibility.Visible;
                        break;
                    default:
                        break;
                }
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
          */
        }

        void taskOwnerChanged(object sender, SelectionChangedEventArgs e)
        {/*
            ComboBox owner = (ComboBox)stackPanel2.Children[1];
            ComboBox state = (ComboBox)stackPanel2.Children[5];
            if (owner.SelectedIndex == -1)
            {
                state.SelectedIndex = 0;
                state.IsEnabled = false;
            }
            else
            {
                if (state.SelectedIndex == 0)
                {
                    state.SelectedIndex = 1;
                }
                state.IsEnabled = true;
            }
          */
        }

        void taskStateChanged(object sender, SelectionChangedEventArgs e)
        {/*
            ComboBox owner = (ComboBox)stackPanel2.Children[1];
            ComboBox state = (ComboBox)stackPanel2.Children[5];
            if (state.SelectedIndex == 0)
            {
                owner.SelectedIndex = -1;
                state.IsEnabled = false;
            }
          */
        }

        void save_project_Click(object sender, RoutedEventArgs e)
        {/*
            TextBox name = (TextBox)stackPanel2.Children[0];
            Label team = (Label)stackPanel2.Children[1];
            DatePicker start = (DatePicker)stackPanel2.Children[2];
            DatePicker end = (DatePicker)stackPanel2.Children[3];
            ComboBox owner = (ComboBox)stackPanel2.Children[4];
            viewModel.ChangeCurrProject(name.Text, start.SelectedDate, end.SelectedDate, viewModel.GetManagers()[owner.SelectedIndex], viewModel.CurrTeam);
          */
        }

        void save_sprint_Click(object sender, RoutedEventArgs e)
        {/*
            TextBox name = (TextBox)stackPanel2.Children[1];
            DatePicker start = (DatePicker)stackPanel2.Children[2];
            DatePicker end = (DatePicker)stackPanel2.Children[3];
            viewModel.ChangeCurrSprint(name.Text, start.SelectedDate, end.SelectedDate);
          */
        }

        void save_story_Click(object sender, RoutedEventArgs e)
        {/*
            ComboBox sprint = (ComboBox)stackPanel2.Children[1];
            TextBox priority = (TextBox)stackPanel2.Children[2];
            TextBox text = (TextBox)stackPanel2.Children[3];
            viewModel.ChangeCurrStory(priority.Text, text.Text, viewModel.SprintsForProject[sprint.SelectedIndex]);
          */
        }

        void save_task_Click(object sender, RoutedEventArgs e)
        {/*
            ComboBox owner = (ComboBox)stackPanel2.Children[1];
            ComboBox complexity = (ComboBox)stackPanel2.Children[2];
            ComboBox value = (ComboBox)stackPanel2.Children[3];
            ComboBox type = (ComboBox)stackPanel2.Children[4];
            ComboBox state = (ComboBox)stackPanel2.Children[5];
            DatePicker completion = (DatePicker)stackPanel2.Children[6];
            TextBox text = (TextBox)stackPanel2.Children[7];
            TaskStateConverter converter = new TaskStateConverter();

            viewModel.ChangeCurrTask(text.Text, (int)complexity.SelectedItem, (int)value.SelectedItem, (UserView)owner.SelectedItem, (TaskType)ttConverter.ConvertBack(type.SelectedItem, typeof(TaskType), null, null), (TaskState)tsConverter.ConvertBack(state.SelectedItem, typeof(TaskState), null, null), completion.SelectedDate);
          */
        }

        void tb_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            e.Handled = e.Text.IsNonNumeric();
        }

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

    }
}
