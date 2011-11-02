using System.Windows;

namespace SCRUMProjectManagementSystem
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class TeamWindow : Window
    {
        private ViewModel.TeamView _team;
        private ViewModel.SPMSViewModel _viewModel;


        public TeamWindow(ViewModel.TeamView team, ViewModel.SPMSViewModel viewModel)
        {
            InitializeComponent();
            _team = team;
            _viewModel = viewModel;
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.MoveUserToTeam(_viewModel.GetTeamMembers(_team).Item2[listBox1.SelectedIndex], _team);
            listBox1.ItemsSource = _viewModel.GetTeamMembers(_team).Item2;
            listBox2.ItemsSource = _viewModel.GetTeamMembers(_team).Item1;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            listBox1.ItemsSource = _viewModel.GetTeamMembers(_team).Item2;
            listBox2.ItemsSource = _viewModel.GetTeamMembers(_team).Item1;
        }
    }
}
