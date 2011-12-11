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
            actual = vm.GetCurrSprintBurndown().Item2.Values.ToArray();
            goal = vm.GetCurrSprintBurndown().Item1.Values.ToArray();
            datesGoal = vm.GetCurrSprintBurndown().Item1.Keys.ToArray();
            datesActual = vm.GetCurrSprintBurndown().Item2.Keys.ToArray();

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
                    mainChart.Series[1].Points[i].MarkerColor = Color.Red;
                }
            }
        }
    }
}
