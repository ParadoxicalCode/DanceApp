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
    /// Логика взаимодействия для Dance.xaml
    /// </summary>
    public partial class Dance : Window
    {
        public SkatingEntities db = new SkatingEntities();
        public Dance()
        {
            InitializeComponent();
            // Привязываем данные к dataGrid.
            TestDG.ItemsSource = db.Dances.ToList();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            main.Show(); this.Hide();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            // Проверка на заполнение.
            if (NameTB.Text == "" || ShortTB.Text == "")
            {
                MessageBox.Show("Не все поля заполнены");
            }
            else
            {
                Dances data = new Dances();

                // Данные из textBox'ов записываем в БД.
                data.Name = NameTB.Text;
                data.ShortName = ShortTB.Text;

                db.Dances.Add(data);
                // Успех.
                try
                {
                    db.SaveChanges();
                    MessageBox.Show("Запись добавлена");

                    // Обновляем таблицу.
                    TestDG.ItemsSource = db.Dances.ToList();
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
            ShortTB.Text = "";
        }

        // При нажатии на строку таблицы данные выводятся в поля слева.
        private void TestDG_MouseUp(object sender, MouseButtonEventArgs e)
        {
            NameTB.Text = "";
            ShortTB.Text = "";
            Dances path = TestDG.SelectedItem as Dances;
            NameTB.Text = path.Name;
            ShortTB.Text = path.ShortName;
        }

        private void Change_Click(object sender, RoutedEventArgs e)
        {
            Dances path = TestDG.SelectedItem as Dances;
            if (path != null)
            {
                // Проверка на заполнение.
                if (NameTB.Text == "" || ShortTB.Text == "")
                {
                    MessageBox.Show("Не все поля заполнены");
                }
                else
                {
                    // Получаем id выбранной записи.
                    int id = path.DanceId;
                    // Находим запись в БД.
                    var data = db.Dances.Where(u => u.DanceId == id).FirstOrDefault();
                    // Записываем в БД данные из TextBox'ов.
                    data.Name = NameTB.Text;
                    data.ShortName = ShortTB.Text;

                    // Успех.
                    try
                    {
                        db.SaveChanges();
                        MessageBox.Show("Запись изменена");
                        // Обновляем таблицу.
                        TestDG.ItemsSource = db.Dances.ToList();
                    }
                    // Ошибка.
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
            Dances path = TestDG.SelectedItem as Dances;

            if (MessageBox.Show("Удалить запись?", "Уведомление", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                int id = path.DanceId;

                // Удаляем запись.
                var delete = db.Dances.Where(u => u.DanceId.Equals(id)).FirstOrDefault();
                db.Dances.Remove(delete);
                db.SaveChanges();

                // Обновляем таблицу.
                TestDG.ItemsSource = db.Dances.ToList();
            }
        }
    }
}
