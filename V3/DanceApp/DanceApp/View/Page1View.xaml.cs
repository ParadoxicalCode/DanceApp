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

namespace DanceApp.View
{
    /// <summary>
    /// Логика взаимодействия для Page1View.xaml
    /// </summary>
    public partial class Page1View : Page
    {
        public Page1View()
        {
            InitializeComponent();
            FramePage1.Content = new Page1.CompetitionsView();
        }

        private void Info_Click(object sender, RoutedEventArgs e)
        {
            InfoView form = new InfoView();
            form.Show();
        }

        private void Compitition_Click(object sender, RoutedEventArgs e)
        {
            FramePage1.Content = new Page1.CompetitionsView();
        }

        private void Judge_Click(object sender, RoutedEventArgs e)
        {
            FramePage1.Content = new Page1.JudgesView();
        }

        private void City_Click(object sender, RoutedEventArgs e)
        {
            FramePage1.Content = new Page1.CitiesView();
        }

        private void Country_Click(object sender, RoutedEventArgs e)
        {
            FramePage1.Content = new Page1.CountriesView();
        }
    }
}
