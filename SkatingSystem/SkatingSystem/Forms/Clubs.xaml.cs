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

namespace SkatingSystem.Forms
{
    /// <summary>
    /// Логика взаимодействия для Clubs.xaml
    /// </summary>
    public partial class Clubs : Window
    {
        public Clubs()
        {
            InitializeComponent();
            TestDG.ItemsSource = SkatingEntities.GetContext().Kluby.ToList();
            SkatingEntities db = new SkatingEntities();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            main.Show();
            Hide();
        }

        // Получаем данные из таблицы и записываем в TextBox.
        private void TestDG_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Kluby path = TestDG.SelectedItem as Kluby;
            NazvanieTB.Text = path.Nazvanie;
            GorodTB.Text = path.Gorod;
            StranaTB.Text = path.Strana;
            FIOTreneraTB.Text = path.FIOTrenera;
        }
    }
}
