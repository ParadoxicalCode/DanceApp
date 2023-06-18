using System.Windows;
using System.Linq;
using DanceApp.Model.Data;
using System;
using System.Net;
using System.Windows.Automation;
using DanceApp.Model;
using System.Text.RegularExpressions;

#nullable disable
namespace DanceApp.View
{
    /// <summary>
    /// Логика взаимодействия для AddEditJudgesView.xaml
    /// </summary>
    public partial class AddEditJudgesView : Window
    {
        public DataBaseContext db = GlobalClass.db;
        public int ID;
        public AddEditJudgesView(int id)
        {
            InitializeComponent();
            ID = id;

            if (ID != 0)
            {
                var data = db.Judge.Find(ID);
                IDText.Text = data.Character.ToString();
                SurnameTB.Text = data.Surname.ToString();
                NameTB.Text = data.Name.ToString();
                PatronymicTB.Text = data.Patronymic.ToString();
                if (data.Club != null) { ClubTB.Text = data.Club.ToString(); }
                CityTB.Text = data.City.ToString();
                CountryTB.Text = data.Country.ToString();
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (SurnameTB.Text == "" || NameTB.Text == "" || CityTB.Text == "" || CountryTB.Text == "")
            {
                MessageBox.Show("Не все поля заполнены!");
                return;
            }

            if (ID == 0)
            {
                if (db.Judge.Count() >= 10)
                {
                    MessageBox.Show("Нельзя добавить больше 10 судей!");
                }
                else
                {
                    Judge c = new Judge();
                    c.Surname = SurnameTB.Text.ToString();
                    c.Name = NameTB.Text.ToString();
                    c.Patronymic = PatronymicTB.Text.ToString();
                    c.Club = ClubTB.Text.ToString();
                    c.City = CityTB.Text.ToString();
                    c.Country = CountryTB.Text.ToString();

                    // Добавление идентификатора
                    string character = new string("ABCDEFGHIJ");
                    for (int i = 0; i < 10; i++)
                    {
                        bool checkIsExist = false;
                        foreach (var j in db.Judge.ToList())
                        {
                            if (j.Character == character[i])
                            {
                                checkIsExist = true;
                                break;
                            }
                        }
                        if (checkIsExist == false)
                        {
                            c.Character = character[i];
                            break;
                        }
                    }

                    db.Judge.Add(c);
                    try
                    {
                        db.SaveChanges();
                        SurnameTB.Text = "";
                        NameTB.Text = "";
                        PatronymicTB.Text = "";
                        ClubTB.Text = "";
                        CityTB.Text = "";
                        CountryTB.Text = "";
                    }
                    catch (Exception ex) { MessageBox.Show(ex.InnerException.Message); }
                }
            }
            else
            {
                var data = db.Judge.Where(u => u.ID == ID).FirstOrDefault();
                data.Surname = SurnameTB.Text.ToString();
                data.Name = NameTB.Text.ToString();
                data.Patronymic = PatronymicTB.Text.ToString();
                data.Club = ClubTB.Text.ToString();
                data.City = CityTB.Text.ToString();
                data.Country = CountryTB.Text.ToString();

                try
                {
                    db.SaveChanges();
                    MessageBox.Show("Запись изменена!");
                    this.Close();
                }
                catch (Exception ex) { MessageBox.Show(ex.InnerException.Message); }
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
