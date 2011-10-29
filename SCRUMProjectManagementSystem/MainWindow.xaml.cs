using System.Windows;
using System.Windows.Controls;
using ViewModel;

namespace SCRUMProjectManagementSystem
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private selection currentSelection;
        private SPMSViewModel viewModel;

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
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            currentSelection = selection.Home;
            viewModel.UpdateAllTeams();
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
                        break;
                    case selection.Project:
                        viewModel.CurrSprint = viewModel.SprintsForProject[leftList.SelectedIndex];
                        break;
                    case selection.Sprint:
                        viewModel.CurrStory = viewModel.StoriesForSprint[leftList.SelectedIndex];
                        break;
                    case selection.Story:
                        viewModel.CurrTask = viewModel.TasksForStory[leftList.SelectedIndex];
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
                    viewModel.UpdateProjectsForUser();
                    leftList.ItemsSource = viewModel.ProjectsForUser;

                    //right panel
                    stackPanel1.Children.RemoveRange(0, stackPanel1.Children.Count);
                    stackPanel2.Children.RemoveRange(0, stackPanel2.Children.Count);
                    ListBox lb = new ListBox();
                    lb.ItemsSource = viewModel.TasksForUser;
                    lb.SelectionChanged += new SelectionChangedEventHandler(lb_SelectionChanged);
                    stackPanel1.Children.Add(lb);
                    break;
                case selection.Project:
                    //left panel
                    button_project.Visibility = Visibility.Visible;
                    viewModel.UpdateSprintsForProject();
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
                    cb.ItemsSource = new string[] { "doesn't work" };
                    cb.SelectedIndex = 0;
                    cb.Margin = new Thickness(0, 0, 0, 4);
                    stackPanel1.Children.Add(label);
                    stackPanel2.Children.Add(cb);
                    Button save = new Button();
                    save.Content = "Save";
                    stackPanel2.Children.Add(save);
                    break;
                case selection.Sprint:
                    //left panel
                    button_project.Visibility = Visibility.Visible;
                    button_sprint.Visibility = Visibility.Visible;
                    viewModel.UpdateStoriesForSprint();
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
                    stackPanel2.Children.Add(save);
                    break;
                case selection.Story:
                    //left panel
                    button_project.Visibility = Visibility.Visible;
                    button_sprint.Visibility = Visibility.Visible;
                    button_story.Visibility = Visibility.Visible;
                    viewModel.UpdateTasksForStory();
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
                    label.Content = "Priority:";
                    stackPanel1.Children.Add(label);
                    cb = new ComboBox();
                    cb.ItemsSource = new string[] { "doesn't work" };
                    cb.SelectedIndex = 0;
                    cb.Margin = new Thickness(0, 0, 0, 4);
                    stackPanel2.Children.Add(cb);
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
                    cb.ItemsSource = new string[] { "doesn't work" };
                    cb.SelectedIndex = 0;
                    cb.Margin = new Thickness(0, 0, 0, 4);
                    stackPanel1.Children.Add(label);
                    stackPanel2.Children.Add(cb);
                    label = new Label();
                    label.Content = "Complexity:";
                    cb = new ComboBox();
                    cb.ItemsSource = new string[] { "doesn't work" };
                    cb.SelectedIndex = 0;
                    cb.Margin = new Thickness(0, 0, 0, 4);
                    stackPanel1.Children.Add(label);
                    stackPanel2.Children.Add(cb);
                    label = new Label();
                    label.Content = "Business Value:";
                    cb = new ComboBox();
                    cb.ItemsSource = new string[] { "doesn't work" };
                    cb.SelectedIndex = 0;
                    cb.Margin = new Thickness(0, 0, 0, 4);
                    stackPanel1.Children.Add(label);
                    stackPanel2.Children.Add(cb);
                    label = new Label();
                    label.Content = "State:";
                    cb = new ComboBox();
                    cb.ItemsSource = new string[] { "doesn't work" };
                    cb.SelectedIndex = 0;
                    cb.Margin = new Thickness(0, 0, 0, 4);
                    cb.SelectedIndex = 2;
                    stackPanel1.Children.Add(label);
                    stackPanel2.Children.Add(cb);
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
                    stackPanel2.Children.Add(save);
                    break;
                default:
                    break;
            }
        }

        void lb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {/*
            ListBox lb = (ListBox)sender;
            if (lb.SelectedIndex >= 0)
            {
                viewModel.CurrTask = viewModel.TasksForUser[lb.SelectedIndex];
                currentSelection = selection.Task;
                update();
            }
          */
        }

        private void button_New_Click(object sender, RoutedEventArgs e)
        {
            NewItemWindow niw = new NewItemWindow(currentSelection + 1, viewModel);
            niw.Visibility = Visibility.Visible;
        }

        private void menu_addTeam_Click(object sender, RoutedEventArgs e)
        {
            NewItemWindow niw = new NewItemWindow(selection.Team, viewModel);
            niw.Visibility = Visibility.Visible;
        }

        private void menu_main_SubmenuOpened(object sender, RoutedEventArgs e)
        {
            string[] tempList = new string[] { "Maxxx & Friends", "HoN Team", "A Trail of Bagels", "If Everyone Attacked Me at Once, I Would Win" };
            menu_addToTeam.ItemsSource = new MenuItem[] { new MenuItem(), new MenuItem(), new MenuItem(), new MenuItem() };
            int temp = 0;
            foreach (MenuItem i in menu_addToTeam.Items)
            {
                i.Click += new RoutedEventHandler(i_Click);
                i.Header = tempList[temp];
                temp++;
            }
        }

        void i_Click(object sender, RoutedEventArgs e)
        {
            TeamWindow tw = new TeamWindow(viewModel.CurrTeam, viewModel);
            tw.Visibility = Visibility.Visible;
        }
    }
}
