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

namespace SkatingSystem
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

        private void Dancers_Click(object sender, RoutedEventArgs e)
        {
            Forms.Dancers dancers = new Forms.Dancers();
            dancers.Show();
            Hide();
        }

        private void Judges_Click(object sender, RoutedEventArgs e)
        {
            Forms.Judges judges = new Forms.Judges();
            judges.Show();
            Hide();
        }

        private void Clubs_Click(object sender, RoutedEventArgs e)
        {
            Forms.Clubs clubs = new Forms.Clubs();
            clubs.Show();
            Hide();
        }

        private void Groups_Click(object sender, RoutedEventArgs e)
        {
            Forms.Groups groups = new Forms.Groups();
            groups.Show();
            Hide();
        }
    }
}
