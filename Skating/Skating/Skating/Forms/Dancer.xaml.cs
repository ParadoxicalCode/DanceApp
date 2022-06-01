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
    /// Логика взаимодействия для Dancer.xaml
    /// </summary>
    public partial class Dancer : Window
    {
        public SkatingEntities db = new SkatingEntities();
        public Dancer()
        {
            InitializeComponent();
            // Привязываем данные к dataGrid.
            TestDG.ItemsSource = db.Dancers.ToList();
            // Выводим данные из базы в список.
            ClubCB.ItemsSource = db.Clubs.ToList();
            PairNumberCB.ItemsSource = db.Pairs.ToList();
            CategoryCB.ItemsSource = db.Categories.ToList();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            main.Show(); this.Hide();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            // Проверка на заполнение.
            if (SurnameTB.Text == "" || NameTB.Text == "" || PatronymicTB.Text == "" || DateOfBirthTB.Text == "" || 
                CityTB.Text == "" || ClubCB.SelectedValue == null || PairNumberCB.SelectedValue == null || CategoryCB.SelectedValue == null)
            {
                MessageBox.Show("Не все поля заполнены");
            }
            else
            {
                Dancers data = new Dancers();
                // Берём id выбранного значения в ComboBox и записываем данные в БД.
                var club = ClubCB.SelectedItem as Clubs;
                data.ClubId = club.ClubId;
                var pair = PairNumberCB.SelectedItem as Pairs;
                data.PairId = pair.PairId;
                var category = CategoryCB.SelectedItem as Categories;
                data.CategoryId = category.CategoryId;

                // Данные из textBox'ов записываем в БД.
                data.Surname = SurnameTB.Text;
                data.Name = NameTB.Text;
                data.Patronymic = PatronymicTB.Text;
                data.City = CityTB.Text;

                // Конвертация String в Date.
                DateTime date;
                string dateString = DateOfBirthTB.Text;
                try
                {
                    date = DateTime.Parse(dateString);
                    data.DateOfBirth = date;

                    db.Dancers.Add(data);
                    // Успех.
                    try
                    {
                        db.SaveChanges();
                        MessageBox.Show("Запись добавлена");

                        // Обновляем таблицу.
                        TestDG.ItemsSource = db.Dancers.ToList();
                    }
                    // Ошибка.
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message.ToString());
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }

        // Очистка всех полей.
        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            SurnameTB.Text = "";
            NameTB.Text = "";
            PatronymicTB.Text = "";
            DateOfBirthTB.Text = "";
            CityTB.Text = "";
            ClubCB.SelectedIndex = -1;
            PairNumberCB.SelectedIndex = -1;
            CategoryCB.SelectedIndex = -1;
        }

        // При нажатии на строку таблицы данные выводятся в поля слева.
        private void TestDG_MouseUp(object sender, MouseButtonEventArgs e)
        {
            // Очищаем все поля и вставляем данные из выбранной строки.
            SurnameTB.Text = "";
            NameTB.Text = "";
            PatronymicTB.Text = "";
            DateOfBirthTB.Text = "";
            CityTB.Text = "";
            ClubCB.SelectedIndex = -1;
            PairNumberCB.SelectedIndex = -1;
            CategoryCB.SelectedIndex = -1;

            Dancers path = TestDG.SelectedItem as Dancers;
            SurnameTB.Text = path.Surname;
            NameTB.Text = path.Name;
            PatronymicTB.Text = path.Patronymic;

            // Конвертация Date в string.
            DateTime time = path.DateOfBirth;
            String stringTime = time.ToString("yyyy-MM-dd");
            DateOfBirthTB.Text = stringTime;

            CityTB.Text = path.City;
            ClubCB.SelectedValue = path.Clubs;
            PairNumberCB.SelectedValue = path.Pairs;
            CategoryCB.SelectedValue = path.Categories;
        }

        private void Change_Click(object sender, RoutedEventArgs e)
        {
            Dancers path = TestDG.SelectedItem as Dancers;
            if (path != null)
            {
                // Проверка на заполнение.
                if (SurnameTB.Text == "" || NameTB.Text == "" || PatronymicTB.Text == "" || DateOfBirthTB.Text == "" ||
                CityTB.Text == "" || ClubCB.SelectedValue == null || PairNumberCB.SelectedValue == null || CategoryCB.SelectedValue == null)
                {
                    MessageBox.Show("Не все поля заполнены");
                }
                else
                {
                    // Получаем id выбранной записи.
                    int id = path.DancerId;
                    // Находим запись в БД.
                    var data = db.Dancers.Where(u => u.DancerId == id).FirstOrDefault();

                    var club = ClubCB.SelectedItem as Clubs;
                    data.ClubId = club.ClubId;
                    var pair = PairNumberCB.SelectedItem as Pairs;
                    data.PairId = pair.PairId;
                    var category = CategoryCB.SelectedItem as Categories;
                    data.CategoryId = category.CategoryId;

                    // Данные из textBox'ов записываем в БД.
                    data.Surname = SurnameTB.Text;
                    data.Name = NameTB.Text;
                    data.Patronymic = PatronymicTB.Text;
                    data.City = CityTB.Text;

                    // Конвертация String в Date.
                    DateTime date;
                    string dateString = DateOfBirthTB.Text;
                    try
                    {
                        date = DateTime.Parse(dateString);
                        data.DateOfBirth = date;

                        // Успех.
                        try
                        {
                            db.SaveChanges();
                            MessageBox.Show("Запись изменена");
                            // Обновляем таблицу.
                            TestDG.ItemsSource = db.Dancers.ToList();
                        }
                        // Ошибка.
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message.ToString());
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message.ToString());
                    }
                }
            }
            else
            {
                MessageBox.Show("Выберите запись для редактирования");
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            // Получаем id выбранной записи.
            Dancers path = TestDG.SelectedItem as Dancers;

            if (MessageBox.Show("Удалить запись?", "Уведомление", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                int id = path.DancerId;

                // Удаляем запись.
                var delete = db.Dancers.Where(u => u.DancerId.Equals(id)).FirstOrDefault();
                db.Dancers.Remove(delete);
                db.SaveChanges();

                // Обновляем таблицу.
                TestDG.ItemsSource = db.Dancers.ToList();
            }
        }
    }
}
