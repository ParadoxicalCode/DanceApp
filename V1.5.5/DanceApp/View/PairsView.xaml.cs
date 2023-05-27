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
        List<Pair> pairs;
        private int PageIndex = 1;
        int pageCount;
        public PairsView()
        {
            InitializeComponent();
            Update();
        }

        private void Update()
        {
            pairs = db.Pairs.ToList();
            PairsDG.ItemsSource = pairs.Take(10);

            if (pairs.Count >= 10)
                NumberOfRecords.Content = 10 + " из " + pairs.Count;
            else
                NumberOfRecords.Content = pairs.Count + " из " + pairs.Count;

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

            if (pageCount > 1)
                ButtonSwitch("Вперёд", false);
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            if (x.Delete() == true)
            {
                AddEditPairsView c = new AddEditPairsView(0);
                c.ShowDialog(); Update();
            }
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            if (x.Delete() == true)
            {
                int ID = (int)((Button)sender).CommandParameter;
                AddEditPairsView c = new AddEditPairsView(ID);
                c.ShowDialog(); Update();
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
                        Update();
                    }
                    catch (Exception ex) { MessageBox.Show(ex.Message.ToString()); }
                }
            }
        }

        private void Import_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Export_Click(object sender, RoutedEventArgs e)
        {

        }

        private void TourCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        // Пагинация
        private void BeginningBtn_Click(object sender, RoutedEventArgs e)
        {
            ButtonSwitch("Назад", true);
            ButtonSwitch("Вперёд", false);

            PairsDG.ItemsSource = null;
            PairsDG.ItemsSource = pairs.Take(10);
            if (pairs.Count >= 10)
                NumberOfRecords.Content = 10 + " из " + pairs.Count;
            else
                NumberOfRecords.Content = pairs.Count + " из " + pairs.Count;

            PageIndex = 1;
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            ButtonSwitch("Вперёд", false);
            PairsDG.ItemsSource = null;
            PageIndex--;

            if (PageIndex == 1) // Если предыдущая страница первая
            {
                PairsDG.ItemsSource = pairs.Take(10);
                NumberOfRecords.Content = 10 + " из " + pairs.Count;

                ButtonSwitch("Назад", true);
            }
            else
            {
                PairsDG.ItemsSource = pairs.Skip((PageIndex - 1) * 10).Take(10);
                NumberOfRecords.Content = (PageIndex * 10) + " из " + pairs.Count;
            }
        }

        private void NextBtn_Click(object sender, RoutedEventArgs e)
        {
            ButtonSwitch("Назад", false);

            PairsDG.ItemsSource = null;
            PairsDG.ItemsSource = pairs.Skip(PageIndex * 10).Take(10);
            PageIndex++;

            if (PageIndex == pageCount) // Если следующая страница последняя
            {
                NumberOfRecords.Content = pairs.Count + " из " + pairs.Count;
                ButtonSwitch("Вперёд", true);
            }
            else
                NumberOfRecords.Content = (PageIndex * 10) + " из " + pairs.Count;
        }

        private void EndBtn_Click(object sender, RoutedEventArgs e)
        {
            ButtonSwitch("Назад", false);
            ButtonSwitch("Вперёд", true);

            PairsDG.ItemsSource = null;
            PairsDG.ItemsSource = pairs.Skip((pageCount - 1) * 10).Take(10);
            NumberOfRecords.Content = pairs.Count + " из " + pairs.Count;

            PageIndex = pageCount;
        }

        private void ButtonSwitch(string buttonType, bool hide)
        {
            if (hide == true)
            {
                if (buttonType == "Назад")
                {
                    BackBtn.Visibility = Visibility.Hidden;
                    BeginningBtn.Visibility = Visibility.Hidden;
                }
                else
                {
                    NextBtn.Visibility = Visibility.Hidden;
                    EndBtn.Visibility = Visibility.Hidden;
                }
            }
            else
            {
                if (buttonType == "Назад")
                {
                    BackBtn.Visibility = Visibility.Visible;
                    BeginningBtn.Visibility = Visibility.Visible;
                }
                else
                {
                    NextBtn.Visibility = Visibility.Visible;
                    EndBtn.Visibility = Visibility.Visible;
                }
            }
        }
    }
}
