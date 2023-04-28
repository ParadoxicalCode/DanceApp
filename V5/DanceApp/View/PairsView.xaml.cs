using DanceApp.Model.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
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
namespace DanceApp.View
{
    /// <summary>
    /// Логика взаимодействия для PairsView.xaml
    /// </summary>
    public partial class PairsView : Page
    {
        DataBaseContext db = new DataBaseContext();
        public PairsView()
        {
            InitializeComponent();
            GetPairs();
        }

        public class DGItems
        {
            public int PairId { get; set; }
            public string Number { get; set; }

            public string MaleSurname { get; set; }
            public string MaleName { get; set; }
            public string MalePatronymic { get; set; }
            public string MaleBirthday { get; set; }

            public string FemaleSurname { get; set; }
            public string FemaleName { get; set; }
            public string FemalePatronymic { get; set; }
            public string FemaleBirthday { get; set; }

            public string Club { get; set; }
            public string City { get; set; }
            public string Country { get; set; }

            public string Trainer1 { get; set; }
            public string Trainer2 { get; set; }
            public int GroupId { get; set; }
        }

        void GetPairs()
        {
            var query =
                from p in db.Pairs
                select new DGItems
                {
                    PairId = p.ID,
                    Number = p.Number,
                    MaleSurname = p.MaleSurname,
                    MaleName = p.MaleName,
                    MalePatronymic = p.MalePatronymic,
                    MaleBirthday = p.MaleBirthday,
                    FemaleSurname = p.FemaleSurname,
                    FemaleName = p.FemaleName,
                    FemalePatronymic = p.FemalePatronymic,
                    FemaleBirthday = p.FemaleBirthday,
                    Club = p.Club,
                    City = p.City,
                    Country = p.Country,
                    Trainer1 = p.Trainer1,
                    Trainer2 = p.Trainer2,
                };

            DG.ItemsSource = query.ToList();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            AddEditPairsView c = new AddEditPairsView(0);
            c.ShowDialog(); GetPairs();
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            int Id = (int)((Button)sender).CommandParameter;
            AddEditPairsView c = new AddEditPairsView(Id);
            c.ShowDialog(); GetPairs();
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            /*
            DGItems path = DG.SelectedItem as DGItems;

            if (MessageBox.Show("Удалить запись?", "Уведомление", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                var delete = db.Dancers.Where(u => u.DancerId.Equals(path.DancerId)).FirstOrDefault();
                db.Dancers.Remove(delete);

                try
                {
                    db.SaveChanges(); GetDancers();
                }
                catch (Exception ex) { MessageBox.Show(ex.Message.ToString()); }
            }
            */
        }

        private void Import_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Export_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
