using DanceApp.Model.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Win32;
using System.Data;
using DanceApp.Model;
using System.Windows.Markup;

#nullable disable
namespace DanceApp.View
{
    /// <summary>
    /// Логика взаимодействия для JudgeView.xaml
    /// </summary>
    public partial class JudgesView : Page
    {
        public DataBaseContext db = GlobalClass.db;
        OpenRegistration x = new OpenRegistration();
        public JudgesView()
        {
            InitializeComponent();
            GetJudges();
        }

        void GetJudges()
        {
            DG.ItemsSource = db.Judges.ToList();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            if (x.Delete() == true)
            {
                AddEditJudgesView c = new AddEditJudgesView(0);
                c.ShowDialog(); GetJudges();
            }
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            if (x.Delete() == true)
            {
                int ID = (int)((Button)sender).CommandParameter;
                AddEditJudgesView c = new AddEditJudgesView(ID);
                c.ShowDialog(); GetJudges();
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (x.Delete() == true)
            {
                if (MessageBox.Show("Удалить запись?", "Уведомление", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    int ID = (int)((Button)sender).CommandParameter;
                    var delete = db.Judges.Where(u => u.ID.Equals(ID)).FirstOrDefault();
                    db.Judges.Remove(delete);
                    try
                    {
                        db.SaveChanges();
                        GetJudges();
                    }
                    catch (Exception ex) { MessageBox.Show(ex.InnerException.Message); }
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

            var browsefile = openfile.ShowDialog();

            if (browsefile == true)
            {
                string ExcelFilePath = openfile.FileName;
            }
        }
    }
}
