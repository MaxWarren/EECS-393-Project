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
    public partial class BurndownWindow : Window
    {
        private double[] goal;
        private int[] actual;

        public BurndownWindow(DateTime[] newGoal, int[] newActual)
        {
            actual = newActual;
            goal = new double[actual.Count()];
            for (int i = 0; i < goal.Count(); i++)
            {
                goal[i] = newGoal[i].Ticks - newGoal[0].Ticks;
            }

            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            double diameter = 8;
            for (int i = 0; i < goal.Length; i++)
            {
                ColumnDefinition cd = new ColumnDefinition();
                cd.Width = new GridLength(goal[i], GridUnitType.Star);
                grid1.ColumnDefinitions.Add(cd);
            }
            for (int i = 0; i < goal.Length; i++)
            {
                ColumnDefinition cd = new ColumnDefinition();
                cd.Width = new GridLength(goal[i], GridUnitType.Star);
                grid2.ColumnDefinitions.Add(cd);
            }

            for (int i = 0; i < goal.Count(); i++)
            {
                RowDefinition rd = new RowDefinition();
                if (i > 0)
                {
                    rd.Height = new GridLength(goal[i] - goal[i - 1], GridUnitType.Star);
                }
                else
                {
                    rd.Height = new GridLength(goal[i], GridUnitType.Star);
                }
                grid1.RowDefinitions.Add(rd);
            }
            for (int i = 0; i < actual.Count(); i++)
            {
                RowDefinition rd = new RowDefinition();
                if (i > 0)
                {
                    rd.Height = new GridLength(actual[i] - actual[i - 1], GridUnitType.Star);
                }
                else
                {
                    rd.Height = new GridLength(actual[i], GridUnitType.Star);
                }
                grid2.RowDefinitions.Add(rd);
            }
            for (int i = 0; i < goal.Count(); i++)
            {
                Ellipse el = new Ellipse();
                el.Height = diameter;
                el.Width = diameter;
                el.Fill = System.Windows.Media.Brushes.Blue;
                el.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                el.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                Grid.SetRow(el, i + 1);
                Grid.SetColumn(el, i + 1);
                Grid.SetRowSpan(el, 500);
                Grid.SetColumnSpan(el, 500);
                el.Margin = new Thickness(-1 * diameter / 2);
                grid1.Children.Add(el);
            }
            for (int i = 0; i < actual.Count(); i++)
            {
                Ellipse el = new Ellipse();
                el.Height = diameter;
                el.Width = diameter;
                el.Fill = System.Windows.Media.Brushes.Red;
                el.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                el.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                Grid.SetRow(el, i + 1);
                Grid.SetColumn(el, i + 1);
                Grid.SetRowSpan(el, 500);
                Grid.SetColumnSpan(el, 500);
                el.Margin = new Thickness(-1 * diameter / 2);
                grid2.Children.Add(el);
            }
        }
    }
}
