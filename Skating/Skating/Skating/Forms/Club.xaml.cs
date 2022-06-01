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
    /// Логика взаимодействия для Club.xaml
    /// </summary>
    public partial class Club : Window
    {
        public SkatingEntities db = new SkatingEntities();
        public Club()
        {
            InitializeComponent();
            // Привязываем данные к dataGrid.
            TestDG.ItemsSource = db.Clubs.ToList();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            main.Show(); this.Hide();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            // Проверка на заполнение.
            if (NameTB.Text == "" || CityTB.Text == "" || CountryTB.Text == "" ||
                TrainerFIOTB.Text == "")
            {
                MessageBox.Show("Не все поля заполнены");
            }
            else
            {
                Clubs data = new Clubs();
                // Данные из textBox'ов записываем в БД.
                data.Name = NameTB.Text;
                data.City = CityTB.Text;
                data.Country = CountryTB.Text;
                data.TrainerFIO = TrainerFIOTB.Text;

                db.Clubs.Add(data);
                // Успех.
                try
                {
                    db.SaveChanges();
                    MessageBox.Show("Запись добавлена");

                    // Обновляем таблицу.
                    TestDG.ItemsSource = db.Clubs.ToList();
                }
                // Ошибка.
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }

        // Очистка всех полей.
        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            NameTB.Text = "";
            CityTB.Text = "";
            CountryTB.Text = "";
            TrainerFIOTB.Text = "";
        }

        // При нажатии на строку таблицы данные выводятся в поля слева.
        private void TestDG_MouseUp(object sender, MouseButtonEventArgs e)
        {
            NameTB.Text = "";
            CityTB.Text = "";
            CountryTB.Text = "";
            TrainerFIOTB.Text = "";
            Clubs path = TestDG.SelectedItem as Clubs;
            NameTB.Text = path.Name;
            CityTB.Text = path.City;
            CountryTB.Text = path.Country;
            TrainerFIOTB.Text = path.TrainerFIO;
        }

        private void Change_Click(object sender, RoutedEventArgs e)
        {
            // Проверка на заполнение.
            if (NameTB.Text == "" || CityTB.Text == "" || CountryTB.Text == "" ||
                TrainerFIOTB.Text == "")
            {
                MessageBox.Show("Не все поля заполнены");
            }
            else
            {
                Clubs path = TestDG.SelectedItem as Clubs;
                if (path != null)
                {
                    // Получаем id выбранной записи.
                    int id = path.ClubId;
                    // Находим запись в БД.
                    var data = db.Clubs.Where(u => u.ClubId == id).FirstOrDefault();
                    // Записываем в БД данные из TextBox'ов.
                    data.Name = NameTB.Text;
                    data.City = CityTB.Text;
                    data.Country = CountryTB.Text;
                    data.TrainerFIO = TrainerFIOTB.Text;

                    // Успех.
                    try
                    {
                        db.SaveChanges();
                        MessageBox.Show("Запись изменена");
                        // Обновляем таблицу.
                        TestDG.ItemsSource = db.Clubs.ToList();
                    }
                    // Ошибка.
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message.ToString());
                    }
                }
                else
                {
                    MessageBox.Show("Выберите запись для редактирования");
                }
                
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            // Получаем id выбранной записи.
            Clubs path = TestDG.SelectedItem as Clubs;

            if (MessageBox.Show("Удалить запись?", "Уведомление", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                int id = path.ClubId;

                // Удаляем запись.
                var delete = db.Clubs.Where(u => u.ClubId.Equals(id)).FirstOrDefault();
                db.Clubs.Remove(delete);
                db.SaveChanges();

                // Обновляем таблицу.
                TestDG.ItemsSource = db.Clubs.ToList();
            }
        }
    }
}
