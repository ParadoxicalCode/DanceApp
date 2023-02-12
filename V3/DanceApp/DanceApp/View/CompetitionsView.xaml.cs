using DanceApp.Model;
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
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

#nullable disable
namespace DanceApp.View.Page1
{
    /// <summary>
    /// Логика взаимодействия для CompetitionsView.xaml
    /// </summary>
    public partial class CompetitionsView : Page
    {
        DataBaseContext db = new DataBaseContext();
        public CompetitionsView()
        {
            InitializeComponent();
            GetCompetitions();
        }

        public class DGItems
        {
            public int CompetitionId { get; set; }
            public string Title { get; set; }
            public string StartDate { get; set; }
            public string Manager { get; set; }
            public string Address { get; set; }
            public string Club { get; set; }
        }

        void GetCompetitions()
        {
            var query =
                from co in db.Competitions
                join cl in db.Clubs on co.ClubId equals cl.ClubId into gj
                from empty in gj.DefaultIfEmpty()
                select new DGItems { CompetitionId = co.CompetitionId, Title = co.Title, StartDate = co.StartDate, 
                    Manager = co.Manager, Address = co.Address, Club = empty.Title ?? string.Empty };

            DG.ItemsSource = query.ToList();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            AddEditCompetitionsView c = new AddEditCompetitionsView(0);
            c.ShowDialog(); GetCompetitions();
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            int Id = (int)((Button)sender).CommandParameter;
            AddEditCompetitionsView c = new AddEditCompetitionsView(Id);
            c.ShowDialog(); GetCompetitions();
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            DGItems path = DG.SelectedItem as DGItems;

            if (MessageBox.Show("Удалить запись?", "Уведомление", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                var delete = db.Competitions.Where(u => u.CompetitionId.Equals(path.CompetitionId)).FirstOrDefault();
                db.Competitions.Remove(delete);

                try
                {
                    db.SaveChanges(); GetCompetitions();
                }
                catch (Exception ex) { MessageBox.Show(ex.Message.ToString()); }
            }
        }

        private void Next_Click(object sender, RoutedEventArgs e)
        {
            DGItems SelectedRow = (DGItems)DG.SelectedItem;
            GlobalClass.CompetitionId = SelectedRow.CompetitionId;
        }
    }
}
