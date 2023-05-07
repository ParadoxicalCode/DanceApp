using DanceApp.Model;
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
using System.Windows.Media.Animation;
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
        public DataBaseContext db = GlobalClass.db;
        public PairsView()
        {
            InitializeComponent();
            GetPairs();
        }

        public class DGItems
        {
            public int ID { get; set; }
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
            pairsCountText.Text = db.Pairs.Count().ToString();
            DG.ItemsSource = db.Pairs.ToList();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            AddEditPairsView c = new AddEditPairsView(0);
            c.ShowDialog(); GetPairs();
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            int ID = (int)((Button)sender).CommandParameter;
            AddEditPairsView c = new AddEditPairsView(ID);
            c.ShowDialog(); GetPairs();
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Удалить запись?", "Уведомление", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                int ID = (int)((Button)sender).CommandParameter;
                var delete = db.Pairs.Where(u => u.ID.Equals(ID)).FirstOrDefault();
                db.Pairs.Remove(delete);
                try
                {
                    // Обновляем список групп
                    var x = db.Competitions.Where(u => u.ID == 1).FirstOrDefault();
                    x.UpdateGroups = true;

                    db.SaveChanges(); GetPairs();
                }
                catch (Exception ex) { MessageBox.Show(ex.Message.ToString()); }
            }
        }

        private void Import_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Export_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
