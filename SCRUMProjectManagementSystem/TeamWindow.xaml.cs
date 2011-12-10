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

        public TeamWindow(ViewModel.TeamView team, ViewModel.SPMSViewModel viewModel)
        {
            InitializeComponent();
            _team = team;
            _viewModel = viewModel;
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
                        label1.Content = ((UserView)listBox1.SelectedItem).Name + " was moved from " + originalTeamName + " to " + _team.Name + ".";
                    }
                    else
                    {
                        label1.Content = "";
                        MessageBox.Show(
                            "Your changes could not be saved. Please review the data you entered and try again. If this problem persists, please contact your administrator.",
                            "Save Failed",
                            MessageBoxButton.OK,
                            MessageBoxImage.Warning);
                    }
                }
                catch (InvalidOperationException)
                {
                    MessageBox.Show(
                        "A serious error has occured. The client must shut down.",
                        "Critical Error",
                        MessageBoxButton.OK,
                        MessageBoxImage.Error);

                    Environment.Exit(1);
                }
                catch (ArgumentNullException)
                {
                    label1.Content = "";
                    MessageBox.Show(
                        "The values you have entered are not valid. Please review the data you have entered and try again.",
                        "Data Error",
                        MessageBoxButton.OK,
                        MessageBoxImage.Warning);
                }
                listBox1.ItemsSource = _viewModel.GetTeamMembers(_team).Item2;
                listBox2.ItemsSource = _viewModel.GetTeamMembers(_team).Item1;
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            listBox1.ItemsSource = _viewModel.GetTeamMembers(_team).Item2;
            listBox2.ItemsSource = _viewModel.GetTeamMembers(_team).Item1;
        }
    }
}
