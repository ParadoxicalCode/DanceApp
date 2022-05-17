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
    /// Логика взаимодействия для Dancers.xaml
    /// </summary>
    public partial class Dancers : Window
    {
        public Dancers()
        {
            InitializeComponent();
            // Вывод данных в DataGrid
            TestDG.ItemsSource = SkatingEntities.GetContext().Uchastniki.ToList();
            SkatingEntities db = new SkatingEntities();
        }

        // Переход на предыдущую форму
        private void Back_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            main.Show();
            Hide();
        }

        // Получаем данные из таблицы и записываем в TextBox.
        private void TestDG_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Uchastniki path = TestDG.SelectedItem as Uchastniki;
            FamiliyaTB.Text = path.Familiya;
            ImyaTB.Text = path.Imya;
            OtchestvoTB.Text = path.Otchestvo;
            // Конвертация Date в String.
            DateTime time = path.DataRozhdeniya;
            String MyString = time.ToString("yyyy-MM-dd");
            DataTB.Text = MyString;
            GorodTB.Text = path.Gorod;
            

            string idKluba = path.IdKluba.ToString();
            KlubTB.Text = idKluba;
            string kategoriya = path.IdKategorii.ToString();
            KategoriyaTB.Text = kategoriya;
            string nomerPary = path.IdPary.ToString();
            NomerParyTB.Text = nomerPary;
        }
    }
}
