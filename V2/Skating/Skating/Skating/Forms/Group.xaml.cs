using Microsoft.SqlServer.Server;
using Skating.Forms;
using Skating;
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
using System.Windows.Shapes;

namespace Skating.Forms
{
    /// <summary>
    /// Логика взаимодействия для Group.xaml
    /// </summary>
    public partial class Group : Window
    {
        public SkatingEntities db = new SkatingEntities();
        public Group()
        {
            InitializeComponent();
            DG.ItemsSource = db.Groups.ToList();
            TableCB.SelectedIndex = 2;
            Update();
        }

        private int rowCount = 0;
        public void Update()
        {
            rowCount = db.Groups.Count();
            numberOfEntries.Text = rowCount.ToString();
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void Image_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Hide_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            // Получаем id выбранной записи.
            Dancers path = DG.SelectedItem as Dancers;

            if (MessageBox.Show("Удалить запись?", "Уведомление", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                int id = path.DancerId;

                // Удаляем запись.
                var delete = db.Dancers.Where(u => u.DancerId.Equals(id)).FirstOrDefault();
                db.Dancers.Remove(delete);
                db.SaveChanges();

                // Обновляем таблицу.
                DG.ItemsSource = db.Dancers.ToList(); Update();
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            db.SaveChanges();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            GroupAdd ga = new GroupAdd(); ga.Show();
        }

        private void DanceResultClick(object sender, RoutedEventArgs e)
        {
            DanceResult dr = new DanceResult();
            dr.Show(); this.Hide();
        }

        private void TableCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (TableCB.SelectedIndex)
            {
                case 0:
                    MainWindow mw = new MainWindow();
                    mw.Show(); this.Hide();
                    break;
                case 1:
                    Club club = new Club();
                    club.Show(); this.Hide();
                    break;
                case 3:
                    Judge judge = new Judge();
                    judge.Show(); this.Hide();
                    break;
                case 4:
                    Dance dance = new Dance();
                    dance.Show(); this.Hide();
                    break;
            }
        }
    }
}
