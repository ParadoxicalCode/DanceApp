using DanceApp.Model;
using DanceApp.Model.Data;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace DanceApp.View
{
    /// <summary>
    /// Логика взаимодействия для SettingsView.xaml
    /// </summary>
    #nullable disable
    public partial class CompetitionView : Window
    {
        public DataBaseContext db = GlobalClass.db;
        public List<CBItems> SiteCapacityCBItemsList = new List<CBItems>();
        public List<CBItems> FractionCBItemsList = new List<CBItems>();
        private bool CBSwitch;
        public CompetitionView()
        {
            InitializeComponent();

            SiteCapacityCBItemsList.Add(new CBItems { Element = "5" });
            SiteCapacityCBItemsList.Add(new CBItems { Element = "6" });
            SiteCapacityCBItemsList.Add(new CBItems { Element = "7" });
            SiteCapacityCBItemsList.Add(new CBItems { Element = "8" });
            SiteCapacityCBItemsList.Add(new CBItems { Element = "9" });
            SiteCapacityCBItemsList.Add(new CBItems { Element = "10" });
            CBSwitch = false;
            SiteCapacityCB.ItemsSource = SiteCapacityCBItemsList;
            CBSwitch = true;

            var data = db.Competitions.Where(u => u.ID == 1).FirstOrDefault();

            if (data.Rank != "")
            {
                RankTB.Text = data.Rank;
                ManagerTB.Text = data.Manager;
                CityTB.Text = data.City;
                MainJudgeTB.Text = data.MainJudge;
                CountingCommissionTB.Text = data.CountingCommission;

                SiteCapacityCB.SelectedValue = data.SiteCapacity;
                AddFractionCBItems();
                FractionCB.SelectedValue = data.Fraction;
            }
        }

        public class CBItems
        {
            public string Element { get; set; }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            OpenRegistration x = new OpenRegistration();
            if (x.Delete() == true)
            {
                if (RankTB.Text == "" || ManagerTB.Text == "" || CityTB.Text == "" || MainJudgeTB.Text == "" || 
                    CountingCommissionTB.Text == "" || SiteCapacityCB.SelectedItem == null || FractionCB.SelectedItem == null)
                {
                    MessageBox.Show("Не все поля заполнены!");
                    return;
                }

                var data = db.Competitions.Where(u => u.ID == 1).FirstOrDefault();

                data.Rank = RankTB.Text;
                data.Manager = ManagerTB.Text;
                data.City = CityTB.Text;
                data.MainJudge = MainJudgeTB.Text;
                data.CountingCommission = CountingCommissionTB.Text;

                var siteCapacity = SiteCapacityCB.SelectedItem as CBItems;
                data.SiteCapacity = siteCapacity.Element;

                var fraction = FractionCB.SelectedItem as CBItems;
                data.Fraction = fraction.Element;

                try
                {
                    db.SaveChanges();
                    MessageBox.Show("Данные успешно сохранены!");
                    this.Close();
                }
                catch (Exception ex) { MessageBox.Show(ex.InnerException.Message); }
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void SiteCapacityCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CBSwitch == true)
                AddFractionCBItems();
        }

        private void AddFractionCBItems()
        {
            FractionCB.ItemsSource = null;
            FractionCBItemsList.Clear();

            var siteCapacity = SiteCapacityCB.SelectedItem as CBItems;
            string number = siteCapacity.Element;

            switch (number)
            {
                case "5":
                    FractionCBItemsList.Add(new CBItems { Element = "3/5" });
                    FractionCBItemsList.Add(new CBItems { Element = "4/5" });
                    break;
                case "6":
                    FractionCBItemsList.Add(new CBItems { Element = "3/6" });
                    FractionCBItemsList.Add(new CBItems { Element = "4/6" });
                    FractionCBItemsList.Add(new CBItems { Element = "5/6" });
                    break;
                case "7":
                    FractionCBItemsList.Add(new CBItems { Element = "4/7" });
                    FractionCBItemsList.Add(new CBItems { Element = "5/7" });
                    FractionCBItemsList.Add(new CBItems { Element = "6/7" });
                    break;
                case "8":
                    FractionCBItemsList.Add(new CBItems { Element = "4/8" });
                    FractionCBItemsList.Add(new CBItems { Element = "5/8" });
                    FractionCBItemsList.Add(new CBItems { Element = "6/8" });
                    FractionCBItemsList.Add(new CBItems { Element = "7/8" });
                    break;
                case "9":
                    FractionCBItemsList.Add(new CBItems { Element = "5/9" });
                    FractionCBItemsList.Add(new CBItems { Element = "6/9" });
                    FractionCBItemsList.Add(new CBItems { Element = "7/9" });
                    FractionCBItemsList.Add(new CBItems { Element = "8/9" });
                    break;
                case "10":
                    FractionCBItemsList.Add(new CBItems { Element = "5/10" });
                    FractionCBItemsList.Add(new CBItems { Element = "6/10" });
                    FractionCBItemsList.Add(new CBItems { Element = "7/10" });
                    FractionCBItemsList.Add(new CBItems { Element = "8/10" });
                    FractionCBItemsList.Add(new CBItems { Element = "9/10" });
                    break;
            }
            FractionCB.ItemsSource = FractionCBItemsList;
        }
    }
}
