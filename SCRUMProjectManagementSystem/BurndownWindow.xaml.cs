using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;

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
            double diameter = 8;
            ColumnDefinition cdtemp = new ColumnDefinition();
            cdtemp.Width = new GridLength(60, GridUnitType.Pixel);
            grid1.ColumnDefinitions.Add(cdtemp);
            for (int i = 0; i < datesGoal.Length; i++)
            {
                ColumnDefinition cd = new ColumnDefinition();
                cd.Width = new GridLength(1, GridUnitType.Star);
                grid1.ColumnDefinitions.Add(cd);
            }
            cdtemp = new ColumnDefinition();
            cdtemp.Width = new GridLength(60, GridUnitType.Pixel);
            grid2.ColumnDefinitions.Add(cdtemp);
            for (int i = 0; i < datesActual.Length; i++)
            {
                ColumnDefinition cd = new ColumnDefinition();
                cd.Width = new GridLength(1, GridUnitType.Star);
                grid2.ColumnDefinitions.Add(cd);
            }

            RowDefinition rdtemp = new RowDefinition();
            rdtemp.Height = new GridLength(diameter, GridUnitType.Pixel);
            grid1.RowDefinitions.Add(rdtemp);
            for (int i = 0; i < goal.Length; i++)
            {
                RowDefinition rd = new RowDefinition();

                rd.Height = new GridLength(1, GridUnitType.Star);

                grid1.RowDefinitions.Add(rd);
            }
            rdtemp = new RowDefinition();
            rdtemp.Height = new GridLength(30, GridUnitType.Pixel);
            grid1.RowDefinitions.Add(rdtemp);
            rdtemp = new RowDefinition();
            rdtemp.Height = new GridLength(diameter, GridUnitType.Pixel);
            grid2.RowDefinitions.Add(rdtemp);
            for (int i = 0; i < actual.Length; i++)
            {
                RowDefinition rd = new RowDefinition();
                int height = 0;
                if (i < actual.Length - 1)
                {
                    height = Math.Abs(actual[i + 1] - actual[i]);
                }
                else
                {
                    height = actual[i];
                }
                rd.Height = new GridLength(height, GridUnitType.Star);
                grid2.RowDefinitions.Add(rd);
            }
            rdtemp = new RowDefinition();
            rdtemp.Height = new GridLength(30, GridUnitType.Pixel);
            grid2.RowDefinitions.Add(rdtemp);

            for (int i = 0; i < goal.Length; i++)
            {
                Ellipse el = new Ellipse();
                el.Height = diameter;
                el.Width = diameter;
                el.Fill = System.Windows.Media.Brushes.Black;
                el.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                el.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                Grid.SetRow(el, i + 1);
                Grid.SetColumn(el, i + 1);
                Grid.SetRowSpan(el, 500);
                Grid.SetColumnSpan(el, 500);
                el.Margin = new Thickness(-1 * diameter / 2);
                grid1.Children.Add(el);
            }
            for (int i = 0; i < actual.Length; i++)
            {
                Ellipse el = new Ellipse();
                el.Height = diameter;
                el.Width = diameter;
                el.Fill = System.Windows.Media.Brushes.Green;
                if (actual[i] > goal[i])
                {
                    el.Fill = System.Windows.Media.Brushes.Red;
                }
                el.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                el.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                Grid.SetRow(el, i + 1);
                Grid.SetColumn(el, i + 1);
                Grid.SetRowSpan(el, 500);
                Grid.SetColumnSpan(el, 500);
                el.Margin = new Thickness(-1 * diameter / 2);
                grid2.Children.Add(el);
            }
            for (int i = 0; i < goal.Length; i+=goal.Length / 5)
            {
                Label label = new Label();
                label.Content = datesGoal.ElementAt(i).Date.ToString("MM/dd");
                label.VerticalAlignment = System.Windows.VerticalAlignment.Center;
                label.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                Grid.SetRow(label, goal.Length + 1);
                Grid.SetColumn(label, i + 1);
                Grid.SetColumnSpan(label, 5);
                label.Margin = new Thickness(-1 * diameter, 0, 0, 0);
                grid1.Children.Add(label);

                label = new Label();
                label.Content = Math.Round(goal.ElementAt(i), 1);
                label.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                label.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
                Grid.SetRow(label, i + 1);
                Grid.SetColumn(label, 0);
                Grid.SetRowSpan(label, 5);
                grid1.Children.Add(label);
            }
        }
    }
}
