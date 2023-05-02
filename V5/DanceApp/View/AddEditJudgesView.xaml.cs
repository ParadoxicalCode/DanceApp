using System.Windows;
using System.Linq;
using DanceApp.Model.Data;
using System;
using System.Net;
using System.Windows.Automation;

#nullable disable
namespace DanceApp.View
{
    /// <summary>
    /// Логика взаимодействия для AddEditJudgesView.xaml
    /// </summary>
    public partial class AddEditJudgesView : Window
    {
        DataBaseContext db = new DataBaseContext();
        public int Id;
        public AddEditJudgesView(int id)
        {
            InitializeComponent();
            Id = id;
            PositionCB.ItemsSource = db.Positions.ToList();

            if (Id != 0)
            {
                var data = db.Judges.Find(Id);
                SurnameTB.Text = data.Surname.ToString();
                NameTB.Text = data.Name.ToString();
                PatronymicTB.Text = data.Patronymic.ToString();
                if (data.Club != null) { ClubTB.Text = data.Club.ToString(); }
                PositionCB.SelectedValue = data.PositionId;
            }
        }


        public class DGItems
        {
            public int JudgeId { get; set; }
            public string Surname { get; set; }
            public string Name { get; set; }
            public string Patronymic { get; set; }
            public string Position { get; set; }
            public string Club { get; set; }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (SurnameTB.Text == "" || NameTB.Text == "" || PositionCB.SelectedItem == null)
            {
                MessageBox.Show("Не все поля заполнены!");
            }
            else
            {
                string surname = SurnameTB.Text.ToString();
                string name = NameTB.Text.ToString();
                string patronymic = PatronymicTB.Text.ToString();
                var position = PositionCB.SelectedItem as Position;
                var club = ClubTB.Text.ToString();

                if (Id == 0)
                {
                    Judge c = new Judge();
                    c.Surname = surname;
                    c.Name = name;
                    c.Patronymic = patronymic;
                    c.PositionId = position.ID;
                    c.Club = club;

                    db.Judges.Add(c);
                    try
                    {
                        db.SaveChanges();
                        MessageBox.Show("Запись добавлена!");
                    }
                    catch (Exception ex) { MessageBox.Show(ex.Message.ToString()); }
                }
                else
                {
                    var data = db.Judges.Where(u => u.ID == Id).FirstOrDefault();
                    data.Surname = surname;
                    data.Name = name;
                    data.Patronymic = patronymic;
                    data.PositionId = position.ID;
                    data.Club = club;
                    try
                    {
                        db.SaveChanges();
                        MessageBox.Show("Запись изменена!");
                        this.Close();
                    }
                    catch (Exception ex) { MessageBox.Show(ex.Message.ToString()); }
                }
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
