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

                    //Binding binding = new Binding();
                    //binding.Path = new PropertyPath("
                    //right panel
                    stackPanel1.Children.RemoveRange(0, stackPanel1.Children.Count);
                    stackPanel2.Children.RemoveRange(0, stackPanel2.Children.Count);
                    ListBox lb = new ListBox();
                    lb.ItemsSource = viewModel.TasksForUser;
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
                    tb.Text = "project1";
                    stackPanel1.Children.Add(label);
                    stackPanel2.Children.Add(tb);
                    label = new Label();
                    label.Content = "Team:";
                    tb = new TextBox();
                    tb.Margin = new Thickness(0, 0, 0, 4);
                    tb.Text = "HON TEAM!!!!";
                    stackPanel1.Children.Add(label);
                    stackPanel2.Children.Add(tb);
                    label = new Label();
                    label.Content = "Start Date:";
                    stackPanel1.Children.Add(label);
                    label = new Label();
                    label.Content = "10/13/1989";
                    stackPanel2.Children.Add(label);
                    label = new Label();
                    label.Content = "End Date:";
                    stackPanel1.Children.Add(label);
                    label = new Label();
                    label.Content = "12/12/2012";
                    stackPanel2.Children.Add(label);
                    label = new Label();
                    label.Content = "Owner:";
                    tb = new TextBox();
                    tb.Margin = new Thickness(0, 0, 0, 4);
                    tb.Text = "Andy";
                    stackPanel1.Children.Add(label);
                    stackPanel2.Children.Add(tb);
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
                    label.Content = "project1";
                    stackPanel2.Children.Add(label);
                    label = new Label();
                    label.Content = "Sprint Name:";
                    tb = new TextBox();
                    tb.Margin = new Thickness(0, 0, 0, 4);
                    tb.Text = "sprint1";
                    stackPanel1.Children.Add(label);
                    stackPanel2.Children.Add(tb);
                    label = new Label();
                    label.Content = "Start Date:";
                    stackPanel1.Children.Add(label);
                    label = new Label();
                    label.Content = "10/13/1989";
                    stackPanel2.Children.Add(label);
                    label = new Label();
                    label.Content = "End Date:";
                    stackPanel1.Children.Add(label);
                    label = new Label();
                    label.Content = "12/12/2012";
                    stackPanel2.Children.Add(label);
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
                    label.Content = "project1";
                    stackPanel2.Children.Add(label);
                    label = new Label();
                    label.Content = "Story Name:";
                    tb = new TextBox();
                    tb.Margin = new Thickness(0, 0, 0, 4);
                    tb.Text = "story1";
                    stackPanel1.Children.Add(label);
                    stackPanel2.Children.Add(tb);
                    label = new Label();
                    label.Content = "Priority:";
                    stackPanel1.Children.Add(label);
                    label = new Label();
                    label.Content = "4";
                    stackPanel2.Children.Add(label);
                    label = new Label();
                    label.Content = "Text:";
                    tb = new TextBox();
                    tb.Margin = new Thickness(0, 0, 0, 4);
                    tb.TextWrapping = TextWrapping.Wrap;                    
                    tb.Text = "So I called up all the head Rabbis in Jerusalem and was like \"yo, I got this pizza\" and they were all like \"yea\"";
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
                    label.Content = "project1";
                    stackPanel2.Children.Add(label);
                    label = new Label();
                    label.Content = "Task Name:";
                    tb = new TextBox();
                    tb.Margin = new Thickness(0, 0, 0, 4);
                    tb.Text = "task1";
                    stackPanel1.Children.Add(label);
                    stackPanel2.Children.Add(tb);
                    label = new Label();
                    label.Content = "Owner:";
                    tb = new TextBox();
                    tb.Margin = new Thickness(0, 0, 0, 4);
                    tb.Text = "Andy";
                    stackPanel1.Children.Add(label);
                    stackPanel2.Children.Add(tb);
                    label = new Label();
                    label.Content = "Complexity:";
                    tb = new TextBox();
                    tb.Margin = new Thickness(0, 0, 0, 4);
                    tb.Text = "4";
                    stackPanel1.Children.Add(label);
                    stackPanel2.Children.Add(tb);
                    label = new Label();
                    label.Content = "Business Value:";
                    tb = new TextBox();
                    tb.Margin = new Thickness(0, 0, 0, 4);
                    tb.Text = "$4.00";
                    stackPanel1.Children.Add(label);
                    stackPanel2.Children.Add(tb);
                    label = new Label();
                    label.Content = "State:";
                    ComboBox cb = new ComboBox();
                    cb.ItemsSource = new string[] { "Not started", "In Development", "Almost ready, I swear", "Complete" };
                    cb.Margin = new Thickness(0, 0, 0, 4);
                    cb.SelectedIndex = 2;
                    stackPanel1.Children.Add(label);
                    stackPanel2.Children.Add(cb);
                    label = new Label();
                    label.Content = "Completion Date:";
                    tb = new TextBox();
                    tb.Margin = new Thickness(0, 0, 0, 4);
                    tb.Text = "10/16/2011";
                    stackPanel1.Children.Add(label);
                    stackPanel2.Children.Add(tb);
                    label = new Label();
                    label.Content = "Text:";
                    tb = new TextBox();
                    tb.Margin = new Thickness(0, 0, 0, 4);
                    tb.TextWrapping = TextWrapping.Wrap;                    
                    tb.Text = "Call up all the head Rabbis in Jerusalem and be like \"yo, I got this pizza\" and they'll all be like \"yea\"";
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
