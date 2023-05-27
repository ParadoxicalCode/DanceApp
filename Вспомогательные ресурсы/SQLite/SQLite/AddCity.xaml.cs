using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Microsoft.EntityFrameworkCore;
using SQLite.Data;

#nullable disable
namespace SQLite
{
    /// <summary>
    /// Логика взаимодействия для AddCity.xaml
    /// </summary>
    public partial class AddCity : Window
    {
        public int Id;
        Data.AppContext db = new Data.AppContext();

        public AddCity(int id)
        {
            InitializeComponent();
            Id = id;
            CountryCB.ItemsSource = db.Countries.ToList();
            CountryCB.SelectedValuePath = "CountryId";

            if (Id != 0)
            {
                var data = db.Cities.Find(Id);
                CityTB.Text = data.Title.ToString();

                var data2 = db.Countries.Find(data.CountryId);
                if (data2 != null)
                {
                    CountryCB.SelectedValue = data2.CountryId;
                }
            }
        }

        public class DGItems
        {
            public int Id { get; set; }
            public string Title { get; set; }
            public string Country { get; set; }
            public int CountryId { get; set; }
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            if (CityTB.Text == "")
            {
                MessageBox.Show("Не все поля заполнены!");
            }
            else
            {
                string title = CityTB.Text.ToString();
                var country = CountryCB.SelectedItem as Country;
                bool checkIsExist = db.Cities.Any(x => x.Title == title);

                if (checkIsExist == true)
                {
                    MessageBox.Show("Такой город уже есть!");
                }
                else
                {
                    if (Id == 0)
                    {
                        City c = new City();
                        c.Title = title;
                        if (CountryCB.SelectedValue == null) { c.CountryId = null; }
                        else { c.CountryId = country.CountryId; }

                        db.Cities.Add(c);
                        try
                        {
                            db.SaveChanges();
                            MessageBox.Show("Запись добавлена!");
                        }
                        catch (Exception ex) { MessageBox.Show(ex.Message.ToString()); }
                    }
                    else
                    {
                        var data = db.Cities.Where(u => u.CityId == Id).FirstOrDefault();
                        if (CountryCB.SelectedValue == null) { data.CountryId = null; }
                        else { data.CountryId = country.CountryId; }
                        try
                        {
                            db.SaveChanges();
                            MessageBox.Show("Запись изменена!");
                        }
                        catch (Exception ex) { MessageBox.Show(ex.Message.ToString()); }
                    }
                }
            }
        }
    }
}
