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
    /// Логика взаимодействия для Groups.xaml
    /// </summary>
    public partial class Groups : Window
    {
        public Groups()
        {
            InitializeComponent();
            TestDG.ItemsSource = SkatingEntities.GetContext().Gruppy.ToList();
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
            Gruppy path = TestDG.SelectedItem as Gruppy;

            string nomerGruppy = path.Nomer.ToString();
            NomerGruppyTB.Text = nomerGruppy;
            NazvanieTB.Text = path.Nazvanie;
            string tip = path.IdTipaVystupleniya.ToString();
            TipTB.Text = tip;
            string kolichestvo = path.KolichestvoTancev.ToString();
            KolichestvoTancevTB.Text = kolichestvo;
        }
    }
}
