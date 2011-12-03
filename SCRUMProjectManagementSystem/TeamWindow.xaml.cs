﻿using System;
using System.Diagnostics.CodeAnalysis;
using System.Windows;

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
                    string originalTeamName = _viewModel.GetTeamMembers(_team).Item2[listBox1.SelectedIndex].TeamName;
                    if (_viewModel.MoveUserToTeam(_viewModel.GetTeamMembers(_team).Item2[listBox1.SelectedIndex], _team))
                    {
                        MessageBox.Show(_viewModel.GetTeamMembers(_team).Item2[listBox1.SelectedIndex].Name + "was moved from " + originalTeamName + " to " + _team.Name + ".", "User Moved", MessageBoxButton.OK);
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
            listBox1.ItemsSource = _viewModel.GetTeamMembers(_team).Item2;
            listBox2.ItemsSource = _viewModel.GetTeamMembers(_team).Item1;
        }
    }
}
