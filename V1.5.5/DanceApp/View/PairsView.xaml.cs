using DanceApp.Model;
using DanceApp.Model.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
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
        private DataBaseContext db = GlobalClass.db;
        OpenRegistration x = new OpenRegistration();
        public List<Pair2> pairs = new List<Pair2>();
        public PairsView()
        {
            InitializeComponent();
            PagesCount(false);
            PairsDG.ItemsSource = pairs.Take(10);
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            if (x.Delete() == true)
            {
                AddEditPairsView c = new AddEditPairsView(0);
                if (c.ShowDialog() == true)
                    PagesCount(true);  
            }
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            if (x.Delete() == true)
            {
                int ID = (int)((Button)sender).CommandParameter;
                AddEditPairsView c = new AddEditPairsView(ID);
                if (c.ShowDialog() == true)
                    Update();
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (x.Delete() == true)
            {
                if (MessageBox.Show("Удалить запись?", "Уведомление", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    int ID = (int)((Button)sender).CommandParameter;
                    var delete = db.Pairs.Where(u => u.ID.Equals(ID)).FirstOrDefault();
                    db.Pairs.Remove(delete);
                    try
                    {
                        db.SaveChanges();
                        PagesCount(false);
                    }
                    catch (Exception ex) { MessageBox.Show(ex.InnerException.Message); }
                }
            }
        }

        // Пагинация
        private int PageIndex = 1;
        private int pageCount;
        private void PaginationButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            switch (button.Name)
            {
                case "BeginningBtn":
                    PageIndex = 1;
                    break;
                case "BackBtn":
                    PageIndex--;
                    break;
                case "NextBtn":
                    PageIndex++;
                    break;
                case "EndBtn":
                    PageIndex = pageCount;
                    break;
            }
            Update();
        }

        public class Pair2
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
            public string PerformanceType { get; set; }
            public string AgeCategory { get; set; }
        }

        private void PagesCount(bool add)
        {
            var pair2 = db.Pairs;
            pairs.Clear();
            foreach (var p in pair2)
            {
                var ageCategory = db.AgeCategories.Where(u => u.ID == p.AgeCategoryID).FirstOrDefault();
                pairs.Add(new Pair2 
                {
                    ID = p.ID,
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
                    PerformanceType = p.PerformanceType,
                    AgeCategory = ageCategory.Title
                });
            }

            // Узнаём количество страниц
            if (pairs.Count > 10)
            {
                if (pairs.Count % 10 == 0)
                    pageCount = pairs.Count / 10;
                else
                    pageCount = (pairs.Count / 10) + 1;
            }
            else
                pageCount = 1;

            if (add == true || PageIndex > pageCount)
                PageIndex = pageCount;

            Update();
        }

        private void Update()
        {
            PairsDG.ItemsSource = null;
            PairsDG.Items.Clear();

            if (PageIndex == 1)
            {
                BackBtn.Visibility = Visibility.Hidden;
                BeginningBtn.Visibility = Visibility.Hidden;
                PairsDG.ItemsSource = pairs.Take(10);

                if (pageCount == 1)
                {
                    NextBtn.Visibility = Visibility.Hidden;
                    EndBtn.Visibility = Visibility.Hidden;
                    NumberOfRecords.Content = pairs.Count + " из " + pairs.Count;
                }
                else
                {
                    NextBtn.Visibility = Visibility.Visible;
                    EndBtn.Visibility = Visibility.Visible;
                    NumberOfRecords.Content = 10 + " из " + pairs.Count;
                }
            }
            else
            {
                BackBtn.Visibility = Visibility.Visible;
                BeginningBtn.Visibility = Visibility.Visible;
                NextBtn.Visibility = Visibility.Hidden;
                EndBtn.Visibility = Visibility.Hidden;

                PairsDG.ItemsSource = pairs.Skip((PageIndex - 1) * 10).Take(10);

                // Если открыта последняя страница
                if (PageIndex >= pageCount && pageCount >= 2)
                {
                    PageIndex = pageCount;
                    NumberOfRecords.Content = pairs.Count + " из " + pairs.Count;
                }
                else // Если открыта страница между первой и последней
                    NumberOfRecords.Content = PageIndex * 10 + " из " + pairs.Count;
            }
        }
    }
}
