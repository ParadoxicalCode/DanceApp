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
using static System.Reflection.Metadata.BlobBuilder;

namespace SQLite_Probe
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public BloggingContext db = new BloggingContext();
        public MainWindow()
        {
            InitializeComponent();
            DG1.ItemsSource = db.Cities.ToList();
            DG2.ItemsSource = db.Clubs.ToList();
        }

        private void AddCityClick(object sender, RoutedEventArgs e)
        {
            City data = new City();
            data.Name = CityNameTB.Text;
            db.Cities.Add(data);
            db.SaveChanges();
            DG1.ItemsSource = db.Cities.ToList();
            MessageBox.Show("Запись добавлена");
        }

        private void AddClubClick(object sender, RoutedEventArgs e)
        {
            Club data = new Club();
            data.Name = ClubNameTB.Text;
            data.CityId = int.Parse(CityTB.Text);
            db.Clubs.Add(data);
            db.SaveChanges();
            DG2.ItemsSource = db.Clubs.ToList();
            MessageBox.Show("Запись добавлена");
        }
    }
}
