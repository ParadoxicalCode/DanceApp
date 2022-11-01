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
        public GlobalClass gb = new GlobalClass();
        public Group()
        {
            InitializeComponent();
            // Привязываем данные к dataGrid.
            TestDG.ItemsSource = db.Groups.ToList();
            // Выводим данные из базы в список.
            TypeOfPerformanceCB.ItemsSource = db.TypesOfPerformance.ToList();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            main.Show(); this.Hide();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            // Проверка на заполнение.
            if (NumberTB.Text == "" || NameTB.Text == "" || TypeOfPerformanceCB.SelectedValue == null || NumberOfDancesTB.Text == "")
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
                    if (gb.StringIsDigits(NumberOfDancesTB.Text.TrimEnd()) == false)
                    {
                        MessageBox.Show("В поле 'Количество танцев' введены некорректные данные");
                    }
                    else
                    {
                        Groups data = new Groups();
                        // Берём id выбранного значения в ComboBox и записываем данные в БД.
                        var performance = TypeOfPerformanceCB.SelectedItem as TypesOfPerformance;
                        data.TypeOfPerformanceId = performance.TypeOfPerformanceId;

                        // Данные из textBox'ов записываем в БД.
                        data.Number = int.Parse(NumberTB.Text);
                        data.Name = NameTB.Text;
                        data.NumberOfDances = int.Parse(NumberOfDancesTB.Text);

                        db.Groups.Add(data);
                        // Успех.
                        try
                        {
                            db.SaveChanges();
                            MessageBox.Show("Запись добавлена");

                            // Обновляем таблицу.
                            TestDG.ItemsSource = db.Groups.ToList();
                        }
                        // Ошибка.
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message.ToString());
                        }
                    }
                } 
            }
        }

        // Очистка всех полей.
        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            NumberTB.Text = "";
            NameTB.Text = "";
            TypeOfPerformanceCB.SelectedIndex = -1;
            NumberOfDancesTB.Text = "";
        }

        // При нажатии на строку таблицы данные выводятся в поля слева.
        private void TestDG_MouseUp(object sender, MouseButtonEventArgs e)
        {
            // Очищаем все поля и вставляем данные из выбранной строки.
            NumberTB.Text = "";
            NameTB.Text = "";
            TypeOfPerformanceCB.SelectedIndex = -1;
            NumberOfDancesTB.Text = "";

            Groups path = TestDG.SelectedItem as Groups;
            NumberTB.Text = path.Number.ToString();
            NameTB.Text = path.Name;
            TypeOfPerformanceCB.SelectedValue = path.TypesOfPerformance;
            NumberOfDancesTB.Text = path.NumberOfDances.ToString();
        }

        private void Change_Click(object sender, RoutedEventArgs e)
        {
            Groups path = TestDG.SelectedItem as Groups;
            if (path != null)
            {
                // Проверка на заполнение.
                if (NumberTB.Text == "" || NameTB.Text == "" || TypeOfPerformanceCB.SelectedValue == null || NumberOfDancesTB.Text == "")
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
                        if (gb.StringIsDigits(NumberOfDancesTB.Text.TrimEnd()) == false)
                        {
                            MessageBox.Show("В поле 'Количество танцев' введены некорректные данные");
                        }
                        else
                        {
                            // Получаем id выбранной записи.
                            int id = path.GroupId;
                            // Находим запись в БД.
                            var data = db.Groups.Where(u => u.GroupId == id).FirstOrDefault();

                            // Берём id выбранного значения в ComboBox и записываем данные в БД.
                            var performance = TypeOfPerformanceCB.SelectedItem as TypesOfPerformance;
                            data.TypeOfPerformanceId = performance.TypeOfPerformanceId;

                            // Данные из textBox'ов записываем в БД.
                            data.Number = int.Parse(NumberTB.Text);
                            data.Name = NameTB.Text;
                            data.NumberOfDances = int.Parse(NumberOfDancesTB.Text);

                            // Успех.
                            try
                            {
                                db.SaveChanges();
                                MessageBox.Show("Запись изменена");
                                // Обновляем таблицу.
                                TestDG.ItemsSource = db.Groups.ToList();
                            }
                            // Ошибка.
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message.ToString());
                            }
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
            Groups path = TestDG.SelectedItem as Groups;

            if (MessageBox.Show("Удалить запись?", "Уведомление", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                int id = path.GroupId;

                // Удаляем запись.
                var delete = db.Groups.Where(u => u.GroupId.Equals(id)).FirstOrDefault();
                db.Groups.Remove(delete);
                db.SaveChanges();

                // Обновляем таблицу.
                TestDG.ItemsSource = db.Groups.ToList();
            }
        }
    }
}
