using DanceApp.Model.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Win32;
using System.Data;

#nullable disable
namespace DanceApp.View
{
    /// <summary>
    /// Логика взаимодействия для JudgeView.xaml
    /// </summary>
    public partial class JudgesView : Page
    {
        public JudgesView()
        {
            InitializeComponent();
            GetJudges();
        }

        public class DGItems
        {
            public int JudgeId { get; set; }
            public string Surname { get; set; }
            public string Name { get; set; }
            public string Patronymic { get; set; }
            public string Position { get; set; }
            public string Club { get; set; }
        }

        void GetJudges()
        {
            using (DataBaseContext db = new DataBaseContext())
            {
                var query =
                from j in db.Judges
                join p in db.Positions on j.PositionId equals p.ID
                select new DGItems
                {
                    JudgeId = j.ID,
                    Surname = j.Surname,
                    Name = j.Name,
                    Patronymic = j.Patronymic,
                    Position = p.Title,
                    Club = j.Club ?? string.Empty
                };

                DG.ItemsSource = query.ToList();
            }
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            AddEditJudgesView c = new AddEditJudgesView(0);
            c.ShowDialog(); GetJudges();
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            int Id = (int)((Button)sender).CommandParameter;
            AddEditJudgesView c = new AddEditJudgesView(Id);
            c.ShowDialog(); GetJudges();
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            DGItems path = DG.SelectedItem as DGItems;

            if (MessageBox.Show("Удалить запись?", "Уведомление", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                using (DataBaseContext db = new DataBaseContext())
                {
                    var delete = db.Judges.Where(u => u.ID.Equals(path.JudgeId)).FirstOrDefault();
                    db.Judges.Remove(delete);

                    try
                    {
                        db.SaveChanges(); GetJudges();
                    }
                    catch (Exception ex) { MessageBox.Show(ex.Message.ToString()); }
                } 
            }
        }

        private void Export_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Import_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openfile = new OpenFileDialog();
            openfile.DefaultExt = ".xlsx";
            openfile.Filter = "(.xlsx)|*.xlsx";
            //openfile.ShowDialog();

            var browsefile = openfile.ShowDialog();

            if (browsefile == true)
            {
                string ExcelFilePath = openfile.FileName;
            }
        }
    }
}
