using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using SQLite.Data;
#nullable disable

namespace SQLite
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public DGItems row = new DGItems();
        Data.AppContext db = new Data.AppContext();
        public MainWindow()
        {
            InitializeComponent();
            GetCities();
        }

        
        public class DGItems
        {
            public int Id { get; set; }
            public string Title { get; set; }
            public string Country { get; set; }
            public int CountryId { get; set; }
        }

        void GetCities()
        {
            var query =
                from ci in db.Cities
                join co in db.Countries on ci.CountryId equals co.CountryId
                select new DGItems{ Id = ci.CityId, Title = ci.Title, Country = co.Title, CountryId = co.CountryId };
            DG.ItemsSource = query.ToList();
        }
        
        private void Country_Click(object sender, RoutedEventArgs e)
        {
            CountryView c = new CountryView();
            c.Show(); this.Close();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            AddCity c = new AddCity(0);
            c.ShowDialog(); GetCities();
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            int Id = (int)((Button)sender).CommandParameter;
            AddCity c = new AddCity(Id);
            c.ShowDialog(); GetCities();
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            DGItems path = DG.SelectedItem as DGItems;

            if (MessageBox.Show("Удалить запись?", "Уведомление", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                var delete = db.Cities.Where(u => u.CityId.Equals(path.Id)).FirstOrDefault();
                db.Cities.Remove(delete);

                try
                {
                    db.SaveChanges(); GetCities();
                }
                catch (Exception ex) { MessageBox.Show(ex.Message.ToString()); }
            }
        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility== Visibility.Visible) { GetCities(); }
        }
    }
}
