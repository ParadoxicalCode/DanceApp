using System.Windows;
using System.Linq;
using DanceApp.Model.Data;
using System;
using System.Net;
using DanceApp.Model;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

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
            Id = id;

            if (Id != 0)
            {
                var data = db.Pairs.Find(Id);
                NumberTB.Text = data.Number;
                MaleSurnameTB.Text = data.MaleSurname;
                MaleNameTB.Text = data.MaleName;
                MalePatronymicTB.Text = data.MalePatronymic;
                MaleBirthdayTB.Text = data.MaleBirthday;
                FemaleSurnameTB.Text = data.FemaleSurname;
                FemaleNameTB.Text = data.FemaleName;
                FemalePatronymicTB.Text = data.FemalePatronymic;
                FemaleBirthdayTB.Text = data.FemaleBirthday;
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

        private void AddEdit_Click(object sender, RoutedEventArgs e)
        {
            if (MaleNameTB.Text == "" || MaleNameTB.Text == "" || MaleBirthdayTB.Text == "" || FemaleSurnameTB.Text == "" || 
                FemaleNameTB.Text == "" || FemaleBirthdayTB.Text == "" || ClubTB.Text == "" || CityTB.Text == "" || 
                CountryTB.Text == "" || Trainer1TB.Text == "")
            {
                MessageBox.Show("Не все поля заполнены!");
            }
            else
            {
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
                        Pair c = new Pair();
                        if (NumberTB.Text != "") { c.Number = NumberTB.Text; }
                        c.MaleSurname = MaleSurnameTB.Text;
                        c.MaleName = MaleNameTB.Text;
                        if (MalePatronymicTB.Text != "") { c.MalePatronymic = MalePatronymicTB.Text; }
                        c.MaleBirthday = MaleBirthdayTB.Text;
                        c.FemaleSurname = FemaleSurnameTB.Text;
                        c.FemaleName = FemaleNameTB.Text;
                        if (FemalePatronymicTB.Text != "") { c.FemalePatronymic = FemalePatronymicTB.Text; }
                        c.FemaleBirthday = FemaleBirthdayTB.Text;
                        c.Club = ClubTB.Text;
                        c.City = CityTB.Text;
                        c.Country = CountryTB.Text;
                        c.Trainer1 = Trainer1TB.Text;
                        if (Trainer2TB.Text != "") { c.Trainer2 = Trainer2TB.Text; }

                        db.Pairs.Add(c);
                        try
                        {
                            db.SaveChanges();
                            MessageBox.Show("Запись добавлена!");
                        }
                        catch (Exception ex) { MessageBox.Show(ex.Message.ToString()); }
                    }
                    else
                    {
                        if (NumberTB.Text != "") { data.Number = NumberTB.Text; }
                        data.MaleSurname = MaleSurnameTB.Text;
                        data.MaleName = MaleNameTB.Text;
                        if (MalePatronymicTB.Text != "") { data.MalePatronymic = MalePatronymicTB.Text; }
                        data.MaleBirthday = MaleBirthdayTB.Text;
                        data.FemaleSurname = FemaleSurnameTB.Text;
                        data.FemaleName = FemaleNameTB.Text;
                        if (FemalePatronymicTB.Text != "") { data.FemalePatronymic = FemalePatronymicTB.Text; }
                        data.FemaleBirthday = FemaleBirthdayTB.Text;
                        data.Club = ClubTB.Text;
                        data.City = CityTB.Text;
                        data.Country = CountryTB.Text;
                        data.Trainer1 = Trainer1TB.Text;
                        if (Trainer2TB.Text != "") { data.Trainer2 = Trainer2TB.Text; }
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
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
