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
    /// Логика взаимодействия для DancerAdd.xaml
    /// </summary>
    public partial class DancerAdd : Window
    {
        public SkatingEntities db = new SkatingEntities();
        public DancerAdd()
        {
            InitializeComponent();

            // Выводим данные из базы в список.
            GenderCB.ItemsSource = db.Gender.ToList();
            ClubCB.ItemsSource = db.Clubs.ToList();
            PairNumberCB.ItemsSource = db.Pairs.ToList();
            CategoryCB.ItemsSource = db.Categories.ToList();
        }

        public event EventHandler ButtonClicked;
        private void AddClick(object sender, RoutedEventArgs e)
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

                        // При нажатии на кнопку DataGrid обновится.
                        if (ButtonClicked != null)
                        {
                            ButtonClicked(this, EventArgs.Empty);
                        }
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

        private void Border_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }
    }
}
