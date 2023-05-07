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
using System.Windows.Shapes;
using SQLite.Data;

namespace SQLite
{
    /// <summary>
    /// Логика взаимодействия для CountryView.xaml
    /// </summary>
    public partial class CountryView : Window
    {
        Data.AppContext db = new Data.AppContext();
        public CountryView()
        {
            InitializeComponent();
            GetCountries();
        }

        void GetCountries()
        {
            DG.ItemsSource = db.Countries.ToList();
        }

        private void City_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mw = new MainWindow();
            mw.Show(); this.Close();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            AddCountry c = new AddCountry();
            c.ShowDialog(); GetCountries();
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
