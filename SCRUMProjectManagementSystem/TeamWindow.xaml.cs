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
