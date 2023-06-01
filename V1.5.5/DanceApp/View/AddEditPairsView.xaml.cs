using System.Windows;
using System.Linq;
using DanceApp.Model.Data;
using System;
using System.Net;
using DanceApp.Model;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Windows.Controls;
using System.Globalization;
using static System.Net.Mime.MediaTypeNames;
using System.Windows.Markup;

#nullable disable
namespace DanceApp.View
{
    /// <summary>
    /// Логика взаимодействия для AddEditDancersView.xaml
    /// </summary>
    public partial class AddEditPairsView : Window
    {
        public DataBaseContext db = GlobalClass.db;
        public int ID;
        public AddEditPairsView(int id)
        {
            InitializeComponent();
            ID = id;

            if (ID != 0)
            {
                var data = db.Pairs.Find(ID);

                // Когда редактируем надо чтобы radiobutton соответствовал данным в БД
                if (data.MaleName != "" && data.FemaleName == "")
                    RB1.IsChecked = true;
                else if (data.MaleName != "" && data.FemaleName != "")
                    RB2.IsChecked = true;
                else if (data.FemaleName != "" && data.MaleName == "")
                    RB3.IsChecked = true;

                var ageCategory = db.AgeCategories.Where(u => u.ID == data.AgeCategoryID).FirstOrDefault();
                AgeCategoryText.Text = ageCategory.Title;
                NumberTB.Text = data.Number;

                if (data.MaleName != "") { MNameTB.Text = data.MaleName; }
                if (data.MaleSurname != "") { MSurnameTB.Text = data.MaleSurname; }
                if (data.MalePatronymic != "") { MPatronymicTB.Text = data.MalePatronymic; }
                if (data.MaleBirthday != "") { MBirthdayDP.SelectedDate = DateTime.ParseExact(data.MaleBirthday, "yyyy.MM.dd", CultureInfo.InvariantCulture); }
                if (data.FemaleSurname != "") { FSurnameTB.Text = data.FemaleSurname; }
                if (data.FemaleName != "") { FNameTB.Text = data.FemaleName; }
                if (data.FemalePatronymic != "") { FPatronymicTB.Text = data.FemalePatronymic; }
                if (data.FemaleBirthday != "") { FBirthdayDP.SelectedDate = DateTime.ParseExact(data.FemaleBirthday, "yyyy.MM.dd", CultureInfo.InvariantCulture); }

                ClubTB.Text = data.Club;
                CityTB.Text = data.City;
                CountryTB.Text = data.Country;
                Trainer1TB.Text = data.Trainer1;
                Trainer2TB.Text = data.Trainer2;
            }
            else
            {
                RB2.IsChecked = true;
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (RB1.IsChecked == true)
            {
                if (MSurnameTB.Text == "" || MNameTB.Text == "" || MBirthdayDP.SelectedDate == null)
                {
                    MessageBox.Show("Не все поля заполнены!");
                    return;
                }
            }

            else if (RB3.IsChecked == true)
            {
                if (FSurnameTB.Text == "" || FNameTB.Text == "" || FBirthdayDP.SelectedDate == null)
                {
                    MessageBox.Show("Не все поля заполнены!");
                    return;
                }
            }
            else
            {
                if (MSurnameTB.Text == "" || MNameTB.Text == "" || MBirthdayDP.SelectedDate == null || FSurnameTB.Text == "" ||
                FNameTB.Text == "" || FBirthdayDP.SelectedDate == null)
                {
                    MessageBox.Show("Не все поля заполнены!");
                    return;
                }
            }

            if (ClubTB.Text == "" || CityTB.Text == "" || CountryTB.Text == "" || Trainer1TB.Text == "")
            {
                MessageBox.Show("Не все поля заполнены!");
                return;
            }

            string number = NumberTB.Text;
            bool checkIsExist = db.Pairs.Any(x => x.Number == number);
            var data = db.Pairs.Where(u => u.ID == ID).FirstOrDefault();
            int identical = db.Pairs.Count(x => x.Number == number);

            // Валидация номера
            if (checkIsExist == true && number != "" && identical > 1)
            {
                MessageBox.Show("Пара с таким номером уже есть!");
                return;
            }

            if (TrimAndCheckNumber(ref number) == false && number != "")
            {
                MessageBox.Show("В поле номер обязательно должно быть число!");
                return;
            }

            if (number != "" && Int32.Parse(number) == 0) 
            {
                MessageBox.Show("Номер не может быть 0!");
                return;
            }

            if (ID == 0)
            {
                if (db.Pairs.Count() == 60)
                {
                    MessageBox.Show("Нельзя добавить более 60 пар!");
                    return;
                }
                var pair = new Pair();

                if (RB1.IsChecked == true)
                {
                    pair.MaleSurname = MSurnameTB.Text;
                    pair.MaleName = MNameTB.Text;
                    pair.MalePatronymic = MPatronymicTB.Text;
                    pair.MaleBirthday = MBirthdayDP.SelectedDate.Value.ToString("yyyy.MM.dd");
                    pair.FemaleSurname = "";
                    pair.FemaleName = "";
                    pair.FemalePatronymic = "";
                    pair.FemaleBirthday = "";
                    pair.PerformanceType = "Соло";
                }

                else if (RB3.IsChecked == true)
                {
                    pair.FemaleSurname = FSurnameTB.Text;
                    pair.FemaleName = FNameTB.Text;
                    pair.FemalePatronymic = FPatronymicTB.Text;
                    pair.FemaleBirthday = FBirthdayDP.SelectedDate.Value.ToString("yyyy.MM.dd");
                    pair.MaleSurname = "";
                    pair.MaleName = "";
                    pair.MalePatronymic = "";
                    pair.MaleBirthday = "";
                    pair.PerformanceType = "Соло";
                }
                else
                {
                    pair.MaleSurname = MSurnameTB.Text;
                    pair.MaleName = MNameTB.Text;
                    pair.MalePatronymic = MPatronymicTB.Text;
                    pair.MaleBirthday = MBirthdayDP.SelectedDate.Value.ToString("yyyy.MM.dd");
                    pair.FemaleSurname = FSurnameTB.Text;
                    pair.FemaleName = FNameTB.Text;
                    pair.FemalePatronymic = FPatronymicTB.Text;
                    pair.FemaleBirthday = FBirthdayDP.SelectedDate.Value.ToString("yyyy.MM.dd");
                    pair.PerformanceType = "Пара";
                }
                pair.Number = NumberTB.Text.Trim();
                pair.Club = ClubTB.Text;
                pair.City = CityTB.Text;
                pair.Country = CountryTB.Text;
                pair.Trainer1 = Trainer1TB.Text;
                pair.Trainer2 = Trainer2TB.Text;
                pair.AgeCategoryID = FindAgeCategory(pair.MaleBirthday, pair.FemaleBirthday);

                db.Pairs.Add(pair);
                try
                {
                    db.SaveChanges();
                    MessageBox.Show("Запись добавлена!");
                    this.DialogResult = true;
                }
                catch (Exception ex) { MessageBox.Show(ex.InnerException.Message); }

                Switch("Clear");
            }
            else
            {
                if (RB1.IsChecked == true)
                {
                    data.MaleSurname = MSurnameTB.Text;
                    data.MaleName = MNameTB.Text;
                    data.MalePatronymic = MPatronymicTB.Text;
                    data.MaleBirthday = MBirthdayDP.SelectedDate.Value.ToString("yyyy.MM.dd");
                    data.FemaleSurname = "";
                    data.FemaleName = "";
                    data.FemalePatronymic = "";
                    data.FemaleBirthday = "";
                    data.PerformanceType = "Соло";
                }

                else if (RB3.IsChecked == true)
                {
                    data.FemaleSurname = FSurnameTB.Text;
                    data.FemaleName = FNameTB.Text;
                    data.FemalePatronymic = FPatronymicTB.Text;
                    data.FemaleBirthday = FBirthdayDP.SelectedDate.Value.ToString("yyyy.MM.dd");
                    data.MaleSurname = "";
                    data.MaleName = "";
                    data.MalePatronymic = "";
                    data.MaleBirthday = "";
                    data.PerformanceType = "Соло";
                }
                else
                {
                    data.MaleSurname = MSurnameTB.Text;
                    data.MaleName = MNameTB.Text;
                    data.MalePatronymic = MPatronymicTB.Text;
                    data.MaleBirthday = MBirthdayDP.SelectedDate.Value.ToString("yyyy.MM.dd");
                    data.FemaleSurname = FSurnameTB.Text;
                    data.FemaleName = FNameTB.Text;
                    data.FemalePatronymic = FPatronymicTB.Text;
                    data.FemaleBirthday = FBirthdayDP.SelectedDate.Value.ToString("yyyy.MM.dd");
                    data.PerformanceType = "Пара";
                }

                if (checkIsExist == true && number != "" && data.Number != NumberTB.Text) // Как нам узнать номер повторяется у этой же пары или у другой пары в списке?
                {
                    MessageBox.Show("Пара с таким номером уже есть!");
                    return;
                }

                data.Number = NumberTB.Text.Trim();
                data.Club = ClubTB.Text;
                data.City = CityTB.Text;
                data.Country = CountryTB.Text;
                data.Trainer1 = Trainer1TB.Text;
                data.Trainer2 = Trainer2TB.Text;
                data.AgeCategoryID = FindAgeCategory(data.MaleBirthday, data.FemaleBirthday);

                try
                {
                    db.SaveChanges();
                    MessageBox.Show("Запись изменена!");
                    this.DialogResult = true;
                    this.Close();
                }
                catch (Exception ex) { MessageBox.Show(ex.InnerException.Message); }
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        public int FindAgeCategory(string partner1, string partner2)
        {
            int older;
            int group;
            DateTime Male = new DateTime(9999,01,01);
            DateTime Female = new DateTime(9999, 01, 01);

            if (partner1 != null && partner1 != "")
                Male = DateTime.Parse(partner1);

            if (partner2 != null && partner2 != "")
                Female = DateTime.Parse(partner2);

            if (Male < Female)
                older = Age(Male);
            else
                older = Age(Female);

            if (older <= 6) { group = 1; }
            else if (older >= 7 && older <= 9) { group = 2; }
            else if (older == 10 || older == 11) { group = 3; }
            else if (older == 12 || older == 13) { group = 4; }
            else if (older == 14 || older == 15) { group = 5; }
            else if (older >= 16 && older <= 18) { group = 6; }
            else if (older >= 19 && older <= 34) { group = 7; }
            else { group = 8; }

            return group;
        }

        public int Age(DateTime partner)
        {
            var age = GlobalClass.competitionDate.Year - partner.Year;
            if (GlobalClass.competitionDate.DayOfYear > partner.DayOfYear)
                age--;

            return age;
        }

        private void RB1_Checked(object sender, RoutedEventArgs e)
        {
            Switch("Male");
        }

        private void RB2_Checked(object sender, RoutedEventArgs e)
        {
            Switch("Pair");
        }

        private void RB3_Checked(object sender, RoutedEventArgs e)
        {
            Switch("Female");
        }

        public void Switch(string str)
        {
            if (str == "Male")
            {
                MSurnameTB.IsEnabled = true;
                MNameTB.IsEnabled = true;
                MPatronymicTB.IsEnabled = true;
                MBirthdayDP.IsEnabled = true;

                FSurnameTB.IsEnabled = false;
                FNameTB.IsEnabled = false;
                FPatronymicTB.IsEnabled = false;
                FBirthdayDP.IsEnabled = false;
            }

            else if (str == "Female")
            {
                MSurnameTB.IsEnabled = false;
                MNameTB.IsEnabled = false;
                MPatronymicTB.IsEnabled = false;
                MBirthdayDP.IsEnabled = false;

                FSurnameTB.IsEnabled = true;
                FNameTB.IsEnabled = true;
                FPatronymicTB.IsEnabled = true;
                FBirthdayDP.IsEnabled = true;
            }

            else if (str == "Pair")
            {
                MSurnameTB.IsEnabled = true;
                MNameTB.IsEnabled = true;
                MPatronymicTB.IsEnabled = true;
                MBirthdayDP.IsEnabled = true;

                FSurnameTB.IsEnabled = true;
                FNameTB.IsEnabled = true;
                FPatronymicTB.IsEnabled = true;
                FBirthdayDP.IsEnabled = true;
            }
            else
            {
                NumberTB.Text = null;
                MSurnameTB.Text = null;
                MNameTB.Text = null;
                MPatronymicTB.Text = null;
                MBirthdayDP.SelectedDate = null;
                FSurnameTB.Text = null;
                FNameTB.Text = null;
                FPatronymicTB.Text = null;
                FBirthdayDP.SelectedDate = null;
                CountryTB.Text = null;
                CityTB.Text = null;
                ClubTB.Text = null;
                Trainer1TB.Text = null;
                Trainer2TB.Text = null;
            }
        }

        public bool TrimAndCheckNumber(ref string str)
        {
            str = str.Trim();
            int number;
            if (int.TryParse(str, out number))
                return true;
            else
                return false;
        }
    }
}
