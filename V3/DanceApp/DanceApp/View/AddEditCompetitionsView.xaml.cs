using System.Windows;
using System.Linq;
using DanceApp.Model.Data;
using System;
using System.Net;

#nullable disable
namespace DanceApp.View.Page1
{
    /// <summary>
    /// Логика взаимодействия для AddEditCompetition.xaml
    /// </summary>
    public partial class AddEditCompetitionsView : Window
    {
        DataBaseContext db = new DataBaseContext();
        public int Id;
        public AddEditCompetitionsView(int id)
        {
            InitializeComponent();
            Id = id;
            ClubCB.ItemsSource = db.Clubs.ToList();
            ClubCB.SelectedValuePath = "ClubId";

            if (Id != 0)
            {
                var data = db.Competitions.Find(Id);
                TitleTB.Text = data.Title.ToString();
                StartDateTB.Text = data.StartDate.ToString();
                ManagerTB.Text = data.Manager.ToString();
                AddressTB.Text = data.Address.ToString();

                var data2 = db.Clubs.Find(data.ClubId);
                if (data2 != null) { ClubCB.SelectedValue = data2.ClubId; }
            }
        }

        public class DGItems
        {
            public int CompetitionId { get; set; }
            public string Title { get; set; }
            public string StartDate { get; set; }
            public string Manager { get; set; }
            public string Address { get; set; }
            public string Club { get; set; }
        }

        private void AddEdit_Click(object sender, RoutedEventArgs e)
        {
            if (TitleTB.Text == "" || StartDateTB.Text == "" || ManagerTB.Text == "" || AddressTB.Text == "")
            {
                MessageBox.Show("Не все поля заполнены!");
            }
            else
            {
                string title = TitleTB.Text.ToString();
                string startdate = StartDateTB.Text.ToString();
                string manager = ManagerTB.Text.ToString();
                string address = AddressTB.Text.ToString();
                var club = ClubCB.SelectedItem as Club;

                bool checkIsExist = db.Competitions.Any(x => x.Title == title);
                if (checkIsExist == true && Id == 0)
                {
                    MessageBox.Show("Соревнование с таким названием уже есть!");
                }
                else
                {
                    if (Id == 0)
                    {
                        Competition c = new Competition();
                        c.Title = title;
                        c.StartDate = startdate;
                        c.Manager = manager;
                        c.Address = address;
                        if (ClubCB.SelectedItem != null) { c.ClubId = club.ClubId; }

                        db.Competitions.Add(c);
                        try
                        {
                            db.SaveChanges();
                            MessageBox.Show("Запись добавлена!");
                        }
                        catch (Exception ex) { MessageBox.Show(ex.Message.ToString()); }
                    }
                    else
                    {
                        var data = db.Competitions.Where(u => u.CompetitionId == Id).FirstOrDefault();
                        data.Title = title;
                        data.StartDate = startdate;
                        data.Manager = manager;
                        data.Address = address;
                        if (ClubCB.SelectedItem != null) { data.ClubId = club.ClubId; }
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
        }
    }
}