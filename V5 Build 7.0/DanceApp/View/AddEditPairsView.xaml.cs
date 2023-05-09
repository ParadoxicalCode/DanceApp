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
        public int Id;
        public AddEditPairsView(int id)
        {
            InitializeComponent();
            Id = id;

            if (Id != 0)
            {
                var data = db.Pairs.Find(Id);

                // Когда редактируем надо чтобы radiobutton соответствовал данным в БД
                if (data.MaleName != "" && data.FemaleName == "")
                    RB1.IsChecked = true;
                else if (data.MaleName != "" && data.FemaleName != "")
                    RB2.IsChecked = true;
                else if (data.FemaleName != "" && data.MaleName == "")
                    RB3.IsChecked = true;

                AgeCategoryText.Text = data.AgeCategory;
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

        public class DGItems
        {
            public string Number { get; set; }
            public string MaleSurname { get; set; }
            public string MaleName { get; set; }
            public string MalePatronymic { get; set; }
            public string MaleBirthday { get; set; }
            public string FemaleSurname { get; set; }
            public string FemaleName { get; set; }
            public string FemalePatronymic { get; set; }
            public string FemaleBirthday { get; set; }
            public string Club { get; set; }
            public string City { get; set; }
            public string Country { get; set; }
            public string Trainer1 { get; set; }
            public string Trainer2 { get; set; }
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

            bool checkIsExist = db.Pairs.Any(x => x.Number == NumberTB.Text);
            var data = db.Pairs.Where(u => u.ID == Id).FirstOrDefault();
            if (checkIsExist == true && data.ID != Id)
            {
                MessageBox.Show("Пара с таким номером уже есть!");
            }
            else
            {
                if (Id == 0)
                {
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
                    }

                    pair.Number = NumberTB.Text;
                    pair.Club = ClubTB.Text;
                    pair.City = CityTB.Text;
                    pair.Country = CountryTB.Text;
                    pair.Trainer1 = Trainer1TB.Text;
                    pair.Trainer2 = Trainer2TB.Text;
                    pair.AgeCategory = Group(pair.MaleBirthday, pair.FemaleBirthday);

                    db.Pairs.Add(pair);
                    try
                    {
                        db.SaveChanges();
                        MessageBox.Show("Запись добавлена!");
                    }
                    catch (Exception ex) { MessageBox.Show(ex.Message.ToString()); }

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
                        data.PerformanceType = " (Соло)";
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
                        data.PerformanceType = "";
                    }

                    data.Number = NumberTB.Text;
                    data.Club = ClubTB.Text;
                    data.City = CityTB.Text;
                    data.Country = CountryTB.Text;
                    data.Trainer1 = Trainer1TB.Text;
                    data.Trainer2 = Trainer2TB.Text;
                    data.AgeCategory = Group(data.MaleBirthday, data.FemaleBirthday);

                    try
                    {
                        db.SaveChanges();
                        MessageBox.Show("Запись изменена!");
                        this.Close();

                        var data1 = db.Pairs.Where(u => u.ID == 1).FirstOrDefault();
                        string c = data1.Trainer1;
                    }
                    catch (Exception ex) { MessageBox.Show(ex.Message.ToString()); }
                }
                UpdateGroups();
            }
        }

        public void UpdateGroups()
        {
            // Обновляем список групп
            var x = db.Competitions.Where(u => u.ID == 1).FirstOrDefault();
            x.UpdateGroups = true;
            try
            {
                db.SaveChanges();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message.ToString()); }
        }

        public string Group(string partner1, string partner2) // На входе даты партнёров, на выходе группа. В таблицу группы должна добавляться запись, если такой группы ещё нет
        {
            int older;
            string group = "";
            DateTime Male = new DateTime(9999,01,01);
            DateTime Female = new DateTime(9999, 01, 01);

            if (partner1 != "")
                Male = DateTime.Parse(partner1);

            if (partner2 != "")
                Female = DateTime.Parse(partner2);

            if (Male < Female)
                older = Age(Male);
            else
                older = Age(Female);

            if (older <= 6) { group = "Дети 0"; }
            else if (older >= 7 && older <= 9) { group = "Дети 1"; }
            else if (older == 10 || older == 11) { group = "Дети 2"; }
            else if (older == 12 || older == 13) { group = "Юниоры 1"; }
            else if (older == 14 || older == 15) { group = "Юниоры 2"; }
            else if (older >= 16 && older <= 18) { group = "Молодёжь"; }
            else if (older >= 19 && older <= 34) { group = "Взрослые"; }
            else { group = "Сеньоры"; }

            return group;
        }

        public int Age(DateTime partner)
        {
            var age = GlobalClass.competitionDate.Year - partner.Year;
            if (GlobalClass.competitionDate.DayOfYear > partner.DayOfYear)
                age--;

            return age;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
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
                NumberTB.Text = "";
                MSurnameTB.Text = "";
                MNameTB.Text = "";
                MPatronymicTB.Text = "";
                MBirthdayDP.SelectedDate = null;
                FSurnameTB.Text = "";
                FNameTB.Text = "";
                FPatronymicTB.Text = "";
                FBirthdayDP.SelectedDate = null;
                CountryTB.Text = "";
                CityTB.Text = "";
                ClubTB.Text = "";
                Trainer1TB.Text = "";
                Trainer2TB.Text = "";
            }
        }
    }
}
