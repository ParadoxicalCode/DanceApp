using DanceApp.Model.Data;
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

#nullable disable
namespace DanceApp.View.Page1
{
    /// <summary>
    /// Логика взаимодействия для CountryView.xaml
    /// </summary>
    public partial class CountriesView : Page
    {
        public DataBaseContext db = new DataBaseContext();
        public CountriesView()
        {
            InitializeComponent();
            GetCountries();
        }

        public class DGItems
        {
            public int CountryId { get; set; }
            public string Title { get; set; }
        }

        void GetCountries()
        {
            DG.ItemsSource = db.Countries.ToList();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            AddEditCountriesView c = new AddEditCountriesView(0);
            c.ShowDialog(); GetCountries();
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            int Id = (int)((Button)sender).CommandParameter;
            AddEditCountriesView c = new AddEditCountriesView(Id);
            c.ShowDialog(); GetCountries();
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            DGItems path = DG.SelectedItem as DGItems;

            if (MessageBox.Show("Удалить запись?", "Уведомление", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                var delete = db.Countries.Where(u => u.CountryId.Equals(path.CountryId)).FirstOrDefault();
                db.Countries.Remove(delete);

                try
                {
                    db.SaveChanges();
                    DG.ItemsSource = db.Countries.ToList();
                }
                catch (Exception ex) { MessageBox.Show(ex.Message.ToString()); }
            }
        }
    }
}