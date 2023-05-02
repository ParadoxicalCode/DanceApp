using System.Windows;
using System.Linq;
using DanceApp.Model.Data;
using System;
using System.Net;
using DanceApp.Model;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Windows.Controls;

#nullable disable
namespace DanceApp.View
{
    /// <summary>
    /// Логика взаимодействия для AddEditDancersView.xaml
    /// </summary>
    public partial class AddEditPairsView : Window
    {
        DataBaseContext db = new DataBaseContext();
        public int Id;
        public AddEditPairsView(int id)
        {
            InitializeComponent();
            RB2.IsChecked = true;
            Id = id;

            if (Id != 0)
            {
                var data = db.Pairs.Find(Id);
                NumberTB.Text = data.Number;
                MNameTB.Text = data.MaleName;
                MSurnameTB.Text = data.MaleSurname;
                MPatronymicTB.Text = data.MalePatronymic;
                MBirthdayTB.Text = data.MaleBirthday;
                FSurnameTB.Text = data.FemaleSurname;
                FNameTB.Text = data.FemaleName;
                FPatronymicTB.Text = data.FemalePatronymic;
                FBirthdayTB.Text = data.FemaleBirthday;
                ClubTB.Text = data.Club;
                CityTB.Text = data.City;
                CountryTB.Text = data.Country;
                Trainer1TB.Text = data.Trainer1;
                Trainer2TB.Text = data.Trainer2;
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
                if (MSurnameTB.Text == "" || MNameTB.Text == "" || MBirthdayTB.Text == "")
                {
                    MessageBox.Show("Не все поля заполнены!");
                    return;
                }
            }

            else if (RB3.IsChecked == true)
            {
                if (FSurnameTB.Text == "" || FNameTB.Text == "" || FBirthdayTB.Text == "")
                {
                    MessageBox.Show("Не все поля заполнены!");
                    return;
                }
            }
            else
            {
                if (MSurnameTB.Text == "" || MNameTB.Text == "" || MBirthdayTB.Text == "" || FSurnameTB.Text == "" ||
                FNameTB.Text == "" || FBirthdayTB.Text == "")
                {
                    MessageBox.Show("Не все поля заполнены!");
                    return;
                }
            }

            if (NumberTB.Text == "" || ClubTB.Text == "" || CityTB.Text == "" || CountryTB.Text == "" || Trainer1TB.Text == "")
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
                        pair.MaleBirthday = MBirthdayTB.Text;
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
                        pair.FemaleBirthday = FBirthdayTB.Text;
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
                        pair.MaleBirthday = MBirthdayTB.Text;
                        pair.FemaleSurname = FSurnameTB.Text;
                        pair.FemaleName = FNameTB.Text;
                        pair.FemalePatronymic = FPatronymicTB.Text;
                        pair.FemaleBirthday = FBirthdayTB.Text;
                    }

                    pair.Number = NumberTB.Text;
                    pair.Club = ClubTB.Text;
                    pair.City = CityTB.Text;
                    pair.Country = CountryTB.Text;
                    pair.Trainer1 = Trainer1TB.Text;
                    pair.Trainer2 = Trainer2TB.Text;

                    db.Pairs.Add(pair);
                    try
                    {
                        db.SaveChanges();
                        MessageBox.Show("Запись добавлена!");
                    }
                    catch (Exception ex) { MessageBox.Show(ex.Message.ToString()); }
                }
                else
                {
                    if (RB1.IsChecked == true)
                    {
                        data.MaleSurname = MSurnameTB.Text;
                        data.MaleName = MNameTB.Text;
                        data.MalePatronymic = MPatronymicTB.Text;
                        data.MaleBirthday = MBirthdayTB.Text;
                        data.FemaleSurname = "";
                        data.FemaleName = "";
                        data.FemalePatronymic = "";
                        data.FemaleBirthday = "";
                    }

                    else if (RB3.IsChecked == true)
                    {
                        data.FemaleSurname = FSurnameTB.Text;
                        data.FemaleName = FNameTB.Text;
                        data.FemalePatronymic = FPatronymicTB.Text;
                        data.FemaleBirthday = FBirthdayTB.Text;
                        data.MaleSurname = "";
                        data.MaleName = "";
                        data.MalePatronymic = "";
                        data.MaleBirthday = "";
                    }
                    else
                    {
                        data.MaleSurname = MSurnameTB.Text;
                        data.MaleName = MNameTB.Text;
                        data.MalePatronymic = MPatronymicTB.Text;
                        data.MaleBirthday = MBirthdayTB.Text;
                        data.FemaleSurname = FSurnameTB.Text;
                        data.FemaleName = FNameTB.Text;
                        data.FemalePatronymic = FPatronymicTB.Text;
                        data.FemaleBirthday = FBirthdayTB.Text;
                    }

                    data.Number = NumberTB.Text;
                    data.Club = ClubTB.Text;
                    data.City = CityTB.Text;
                    data.Country = CountryTB.Text;
                    data.Trainer1 = Trainer1TB.Text;
                    data.Trainer2 = Trainer2TB.Text;
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
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void RB1_Checked(object sender, RoutedEventArgs e)
        {
            PerformanceType.Text = "соло";

            MSurnameTB.IsEnabled = true;
            MNameTB.IsEnabled = true;
            MPatronymicTB.IsEnabled = true;
            MBirthdayTB.IsEnabled = true;

            FSurnameTB.IsEnabled = false;
            FNameTB.IsEnabled = false;
            FPatronymicTB.IsEnabled = false;
            FBirthdayTB.IsEnabled = false;
        }

        private void RB2_Checked(object sender, RoutedEventArgs e)
        {
            PerformanceType.Text = "пара";

            MSurnameTB.IsEnabled = true;
            MNameTB.IsEnabled = true;
            MPatronymicTB.IsEnabled = true;
            MBirthdayTB.IsEnabled = true;

            FSurnameTB.IsEnabled = true;
            FNameTB.IsEnabled = true;
            FPatronymicTB.IsEnabled = true;
            FBirthdayTB.IsEnabled = true;
        }

        private void RB3_Checked(object sender, RoutedEventArgs e)
        {
            PerformanceType.Text = "соло";

            MSurnameTB.IsEnabled = false;
            MNameTB.IsEnabled = false;
            MPatronymicTB.IsEnabled = false;
            MBirthdayTB.IsEnabled = false;

            FSurnameTB.IsEnabled = true;
            FNameTB.IsEnabled = true;
            FPatronymicTB.IsEnabled = true;
            FBirthdayTB.IsEnabled = true;
        }    
    }
}
