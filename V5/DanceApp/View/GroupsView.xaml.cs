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
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

#nullable disable
namespace DanceApp.View
{
    /// <summary>
    /// Логика взаимодействия для GroupsView.xaml
    /// </summary>
    public partial class GroupsView : Page
    {
        DataBaseContext db = new DataBaseContext();
        public GroupsView()
        {
            InitializeComponent();
            GetGroups();
        }

        public class DGItems
        {
            public int GroupId { get; set; }
            public int CompetitionId { get; set; }
            public string Number { get; set; }
            public string Title { get; set; }
            public string Program { get; set; }
            public string NumberOfOutputs { get; set; }
            public string DancersCount { get; set; }
            public string TypeOfPerformance { get; set; }
        }

        void GetGroups()
        {
            var query =
                from g in db.Groups
                join t in db.TypesOfPerformance on g.TypeOfPerformanceId equals t.ID
                select new DGItems
                {
                    GroupId = g.ID,
                    Number = g.Number,
                    Title = g.Title,
                    Program = g.Program,
                    NumberOfOutputs = g.NumberOfOutputs,
                    DancersCount = g.DancersCount,
                    TypeOfPerformance = t.Title
                };

            DG.ItemsSource = query.ToList();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            AddEditGroupsView c = new AddEditGroupsView(0);
            c.ShowDialog(); GetGroups();
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            int Id = (int)((Button)sender).CommandParameter;
            AddEditGroupsView c = new AddEditGroupsView(Id);
            c.ShowDialog(); GetGroups();
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            DGItems path = DG.SelectedItem as DGItems;

            if (MessageBox.Show("Удалить запись?", "Уведомление", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                var delete = db.Groups.Where(u => u.ID.Equals(path.GroupId)).FirstOrDefault();
                db.Groups.Remove(delete);

                try
                {
                    db.SaveChanges(); GetGroups();
                }
                catch (Exception ex) { MessageBox.Show(ex.Message.ToString()); }
            }
        }
    }
}
