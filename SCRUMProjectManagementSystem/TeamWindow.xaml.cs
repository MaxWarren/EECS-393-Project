using System;
using System.Diagnostics.CodeAnalysis;
using System.Windows;
using ViewModel;

namespace SCRUMProjectManagementSystem
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    [ExcludeFromCodeCoverage]
    public partial class TeamWindow : Window
    {
        private ViewModel.TeamView _team;
        private ViewModel.SPMSViewModel _viewModel;
        private Window _parent;

        public TeamWindow(Window parent, ViewModel.TeamView team, ViewModel.SPMSViewModel viewModel)
        {
            InitializeComponent();
            _team = team;
            _viewModel = viewModel;
            _parent = parent;
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            if (listBox1.SelectedIndex >= 0)
            {
                try
                {
                    string originalTeamName = ((UserView)listBox1.SelectedItem).TeamName;
                    if (_viewModel.MoveUserToTeam((UserView)listBox1.SelectedItem, _team))
                    {
                        MessageBox.Show(((UserView)listBox1.SelectedItem).Name + " was moved from " + originalTeamName + " to " + _team.Name + ".", "User Moved", MessageBoxButton.OK);
                    }
                    else
                    {
                        MessageBox.Show("Your changes were not saved.", "Move Failed", MessageBoxButton.OK);
                    }
                }
                catch (ArgumentNullException ex)
                {
                    MessageBox.Show(ex.Message, "ArgumentNullException", MessageBoxButton.OK);
                }
                listBox1.ItemsSource = _viewModel.GetTeamMembers(_team).Item2;
                listBox2.ItemsSource = _viewModel.GetTeamMembers(_team).Item1;
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _parent.IsEnabled = false;
            listBox1.ItemsSource = _viewModel.GetTeamMembers(_team).Item2;
            listBox2.ItemsSource = _viewModel.GetTeamMembers(_team).Item1;
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            _parent.IsEnabled = true;
        }
    }
}
