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
    /// Логика взаимодействия для Pair.xaml
    /// </summary>
    public partial class Pair : Window
    {
        public SkatingEntities db = new SkatingEntities();
        public GlobalClass gb = new GlobalClass();
        public Pair()
        {
            InitializeComponent();
            // Привязываем данные к dataGrid.
            TestDG.ItemsSource = db.Pairs.ToList();
            // Выводим данные из базы в список.
            GroupCB.ItemsSource = db.Groups.ToList();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            main.Show(); this.Hide();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            // Проверка на заполнение.
            if (NumberTB.Text == "" || GroupCB.SelectedValue == null)
            {
                MessageBox.Show("Не все поля заполнены");
            }
            else
            {
                if (gb.StringIsDigits(NumberTB.Text.TrimEnd()) == false)
                {
                    MessageBox.Show("В поле 'Номер' введены некорректные данные");
                }
                else
                {
                    Pairs data = new Pairs();
                    // Данные из textBox записываем в БД.
                    data.Number = NumberTB.Text;

                    // Берём id выбранного значения в ComboBox и записываем данные в БД.
                    var group = GroupCB.SelectedItem as Groups;
                    data.GroupId = group.GroupId;

                    db.Pairs.Add(data);
                    // Успех.
                    try
                    {
                        db.SaveChanges();
                        MessageBox.Show("Запись добавлена");

                        // Обновляем таблицу.
                        TestDG.ItemsSource = db.Pairs.ToList();
                    }
                    // Ошибка.
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message.ToString());
                    }
                }
            }
        }

        // Очистка всех полей.
        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            NumberTB.Text = "";
            GroupCB.SelectedIndex = -1;
        }

        // При нажатии на строку таблицы данные выводятся в поля слева.
        private void TestDG_MouseUp(object sender, MouseButtonEventArgs e)
        {
            // Очищаем все поля и вставляем данные из выбранной строки.
            NumberTB.Text = "";
            GroupCB.SelectedIndex = -1;

            Pairs path = TestDG.SelectedItem as Pairs;
            NumberTB.Text = path.Number;
            GroupCB.SelectedValue = path.Groups;
        }

        private void Change_Click(object sender, RoutedEventArgs e)
        {
            Pairs path = TestDG.SelectedItem as Pairs;
            if (path != null)
            {
                // Проверка на заполнение.
                if (NumberTB.Text == "" || GroupCB.SelectedValue == null)
                {
                    MessageBox.Show("Не все поля заполнены");
                }
                else
                {
                    if (gb.StringIsDigits(NumberTB.Text.TrimEnd()) == false)
                    {
                        MessageBox.Show("В поле 'Номер' введены некорректные данные");
                    }
                    else
                    {
                        // Получаем id выбранной записи.
                        int id = path.PairId;
                        // Находим запись в БД.
                        var data = db.Pairs.Where(u => u.PairId == id).FirstOrDefault();
                        // Данные из textBox записываем в БД.
                        data.Number = NumberTB.Text;

                        // Берём id выбранного значения в ComboBox и записываем данные в БД.
                        var group = GroupCB.SelectedItem as Groups;
                        data.GroupId = group.GroupId;

                        // Успех.
                        try
                        {
                            db.SaveChanges();
                            MessageBox.Show("Запись изменена");
                            // Обновляем таблицу.
                            TestDG.ItemsSource = db.Pairs.ToList();
                        }
                        // Ошибка.
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message.ToString());
                        }
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
            Pairs path = TestDG.SelectedItem as Pairs;

            if (MessageBox.Show("Удалить запись?", "Уведомление", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                int id = path.PairId;

                // Удаляем запись.
                var delete = db.Pairs.Where(u => u.PairId.Equals(id)).FirstOrDefault();
                db.Pairs.Remove(delete);
                db.SaveChanges();

                // Обновляем таблицу.
                TestDG.ItemsSource = db.Pairs.ToList();
            }
        }
    }
}
