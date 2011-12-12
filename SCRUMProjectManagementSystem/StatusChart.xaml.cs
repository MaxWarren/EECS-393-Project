using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Windows;
using ViewModel;

namespace SCRUMProjectManagementSystem
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    [ExcludeFromCodeCoverage]
    public partial class StatusChart : Window
    {
        private Dictionary<UserView, int[]> status;

        public StatusChart(ViewModel.SPMSViewModel vm)
        {
            try
            {
                status = vm.GetCurrSprintUserStatus();
            }
            catch (InvalidOperationException)
            {
                MessageBox.Show(
                "You have selected an invalid sprint. Please try a different sprint.",
                "Invalid Sprint",
                MessageBoxButton.OK,
                MessageBoxImage.Warning);
                this.Close();
            }

            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            foreach (UserView user in status.Keys)
            {
                mainChart.Series[0].Points.AddXY(user.Name, status[user][0]);
            }
            foreach (UserView user in status.Keys)
            {
                mainChart.Series[1].Points.AddXY(user.Name, status[user][1]);
            }
            foreach (UserView user in status.Keys)
            {
                mainChart.Series[2].Points.AddXY(user.Name, status[user][2]);
            }
        }
    }
}
