using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Skating
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Club_Click(object sender, RoutedEventArgs e)
        {
            Forms.Club club = new Forms.Club();
            club.Show(); this.Hide();
        }

        private void Group_Click(object sender, RoutedEventArgs e)
        {
            Forms.Group group = new Forms.Group();
            group.Show(); this.Hide();
        }

        private void Pair_Click(object sender, RoutedEventArgs e)
        {
            Forms.Pair pair = new Forms.Pair();
            pair.Show(); this.Hide();
        }

        private void Dancer_Click(object sender, RoutedEventArgs e)
        {
            Forms.Dancer dancer = new Forms.Dancer();
            dancer.Show(); this.Hide();
        }

        private void Judge_Click(object sender, RoutedEventArgs e)
        {
            Forms.Judge judge = new Forms.Judge();
            judge.Show(); this.Hide();
        }

        private void Dance_Click(object sender, RoutedEventArgs e)
        {
            Forms.Dance dance = new Forms.Dance();
            dance.Show(); this.Hide();
        }

        private void Calculate_Click(object sender, RoutedEventArgs e)
        {
            Forms.DanceResult danceResult = new Forms.DanceResult();
            danceResult.Show(); this.Hide();
        }
    }
}
