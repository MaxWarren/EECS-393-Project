using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;
using System.Drawing;

namespace SCRUMProjectManagementSystem
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    [ExcludeFromCodeCoverage]
    public partial class BurndownWindow : Window
    {
        private double[] goal;
        private int[] actual;
        private DateTime[] datesGoal;
        private DateTime[] datesActual;

        public BurndownWindow(ViewModel.SPMSViewModel vm)
        {
            try
            {
                var burndown = vm.GetCurrSprintBurndown();
                actual = burndown.Item2.Values.ToArray();
                goal = burndown.Item1.Values.ToArray();
                datesGoal = burndown.Item1.Keys.ToArray();
                datesActual = burndown.Item2.Keys.ToArray();
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
            for (int i = 0; i < goal.Length; i++)
            {
                mainChart.Series[0].Points.AddXY(datesGoal[i], goal[i]);
            }
            for (int i = 0; i < actual.Length; i++)
            {
                mainChart.Series[1].Points.AddXY(datesActual[i], actual[i]);
                if (actual[i] > goal[i])
                {
                    //set the color of the actual points to red if they are below the goal
                    mainChart.Series[1].Points[i].MarkerColor = Color.Red;
                }
            }
        }
    }
}
