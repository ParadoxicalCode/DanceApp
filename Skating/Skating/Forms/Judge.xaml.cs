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
    /// Логика взаимодействия для Judge.xaml
    /// </summary>
    public partial class Judge : Window
    {
        public SkatingEntities db = new SkatingEntities();
        public Judge()
        {
            InitializeComponent();
            // Привязываем данные к dataGrid.
            TestDG.ItemsSource = db.Judges.ToList();
            // Выводим данные из базы в список.
            PostCB.ItemsSource = db.Post.ToList();
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
            int fill = 0;
            if (PostCB.SelectedValue == null || NameTB.Text == "" || SurnameTB.Text == "" || PatronymicTB.Text == "" ||
                CityTB.Text == "")
            {
                MessageBox.Show("Не все поля заполнены"); fill = 1;
            }
            // Если выбранная должность не счётная комиссия.
            else
            {
                if (PostCB.SelectedIndex != 3)
                {
                    if (CharacterTB.Text == "" || GroupCB.SelectedValue == null)
                    {
                        MessageBox.Show("Не все поля заполнены"); fill = 1;
                    }
                }
            }
            if (CharacterTB.Text.Length > 1)
            {
                MessageBox.Show("В поле буква введено более одного символа");
            }
            if (fill != 1)
            {
                Judges judge = new Judges();
                // Берём id выбранного значения в ComboBox и записываем данные в БД.
                var post = PostCB.SelectedItem as Post;
                judge.PostId = post.PostId;


                // Данные из textBox'ов записываем в БД.
                judge.Surname = SurnameTB.Text;
                judge.Name = NameTB.Text;
                judge.Patronymic = PatronymicTB.Text;
                judge.City = CityTB.Text;

                // Если выбранная должность не счётная комиссия, то добавляем данные в БД.
                if (PostCB.SelectedIndex != 3)
                {
                    var group = GroupCB.SelectedItem as Groups;
                    judge.GroupId = group.GroupId;
                    judge.Character = CharacterTB.Text;
                }

                // Если все поля заполнены, то проверяем на исключения.
                if (fill != 1)
                {
                    db.Judges.Add(judge);
                    // Успех.
                    try
                    {
                        db.SaveChanges();
                        MessageBox.Show("Запись добавлена");

                        // Обновляем таблицу.
                        TestDG.ItemsSource = db.Judges.ToList();
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
            PostCB.SelectedIndex = -1;
            SurnameTB.Text = "";
            NameTB.Text = "";
            PatronymicTB.Text = "";
            CityTB.Text = "";
            CharacterTB.Text = "";
            GroupCB.SelectedIndex = -1;
        }

        // При нажатии на строку таблицы данные выводятся в поля слева.
        private void TestDG_MouseUp(object sender, MouseButtonEventArgs e)
        {
            // Очщаем все поля и вставляем данные из выбранной строки.
            PostCB.SelectedIndex = -1;
            SurnameTB.Text = "";
            NameTB.Text = "";
            PatronymicTB.Text = "";
            CityTB.Text = "";
            CharacterTB.Text = "";
            GroupCB.SelectedIndex = -1;

            Judges path = TestDG.SelectedItem as Judges;
            PostCB.SelectedValue = path.Post;
            SurnameTB.Text = path.Surname;
            NameTB.Text = path.Name;
            PatronymicTB.Text = path.Patronymic;
            CityTB.Text = path.City;
            CharacterTB.Text = path.Character;
            GroupCB.SelectedValue = path.Groups;
        }

        private void Change_Click(object sender, RoutedEventArgs e)
        {
            // Проверка на заполнение.
            int fill = 0;
            if (PostCB.SelectedValue == null || NameTB.Text == "" || SurnameTB.Text == "" || PatronymicTB.Text == "" ||
                CityTB.Text == "")
            {
                MessageBox.Show("Не все поля заполнены"); fill = 1;
            }
            // Если выбранная должность не счётная комиссия.
            else
            {
                if (PostCB.SelectedIndex != 3)
                {
                    if (CharacterTB.Text == "" || GroupCB.SelectedValue == null)
                    {
                        MessageBox.Show("Не все поля заполнены"); fill = 1;
                    }
                }
            }
            if (CharacterTB.Text.Length > 1)
            {
                MessageBox.Show("В поле буква введено более одного символа");
            }
            if (fill != 1)
            {
                Judges dataGrid = TestDG.SelectedItem as Judges;
                // Получаем id выбранной записи.
                int id = dataGrid.JudgeId;
                // Находим запись в БД.
                var editRow = db.Judges.Where(u => u.JudgeId == id).FirstOrDefault();

                // Берём id выбранного значения в ComboBox и записываем данные в БД
                var post = PostCB.SelectedItem as Post;
                editRow.PostId = post.PostId;

                // Записываем в БД данные из TextBox'ов.
                editRow.Surname = SurnameTB.Text;
                editRow.Name = NameTB.Text;
                editRow.Patronymic = PatronymicTB.Text;
                editRow.City = CityTB.Text;

                // Если выбранная должность не счётная комиссия, то добавляем данные в БД.
                if (PostCB.SelectedIndex != 3)
                {
                    var group = GroupCB.SelectedItem as Groups;
                    editRow.GroupId = group.GroupId;
                    editRow.Character = CharacterTB.Text;
                }

                // Если все поля заполнены, то проверяем на исключения.
                if (fill != 1)
                {
                    // Успех.
                    try
                    {
                        db.SaveChanges();
                        MessageBox.Show("Запись изменена");
                        // Обновляем таблицу.
                        TestDG.ItemsSource = db.Judges.ToList();
                    }
                    // Ошибка.
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message.ToString());
                    }
                }
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            // Получаем id выбранной записи.
            Judges path = TestDG.SelectedItem as Judges;

            if (MessageBox.Show("Удалить запись?", "Уведомление", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                int id = path.JudgeId;

                // Удаляем запись.
                var delete = db.Judges.Where(u => u.JudgeId.Equals(id)).FirstOrDefault();
                db.Judges.Remove(delete);
                db.SaveChanges();

                // Обновляем таблицу.
                TestDG.ItemsSource = db.Judges.ToList();
            }
        }
    }
}
