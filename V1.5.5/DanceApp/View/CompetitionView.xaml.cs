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
        public CompetitionView()
        {
            InitializeComponent();
            var data = db.Competitions.Where(u => u.ID == 1).FirstOrDefault();

            if (data.Rank != "")
            {
                RankTB.Text = data.Rank;
                ManagerTB.Text = data.Manager;
                CityTB.Text = data.City;
                MainJudgeTB.Text = data.MainJudge;
                CountingCommissionTB.Text = data.CountingCommission;
                ModeCB.SelectedItem = data.Mode;
                SiteCapacityCB.SelectedItem = data.SiteCapacity;
                FractionCB.SelectedItem = data.Fraction;
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (RankTB.Text == "" || ManagerTB.Text == "" || CityTB.Text == "" || MainJudgeTB.Text == "" 
                || CountingCommissionTB.Text == "" || ModeCB.SelectedItem != null || SiteCapacityCB.SelectedItem != null || FractionCB.SelectedItem != null)
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

            ComboBoxItem typeItem1 = (ComboBoxItem)ModeCB.SelectedItem;
            data.Mode = typeItem1.Content.ToString();

            ComboBoxItem typeItem2 = (ComboBoxItem)ModeCB.SelectedItem;
            data.SiteCapacity = typeItem2.Content.ToString();

            ComboBoxItem typeItem3 = (ComboBoxItem)ModeCB.SelectedItem;
            data.Fraction = typeItem3.Content.ToString();

            try { db.SaveChanges(); }
            catch (Exception ex) { MessageBox.Show(ex.Message.ToString()); }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        public class CBItems
        {
            public string Number { get; set; }
        }

        public List<CBItems> CBItemsList = new List<CBItems>();

        private void SiteCapacityCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FractionCB.ItemsSource = null;
            CBItemsList.Clear();

            ComboBoxItem typeItem = (ComboBoxItem)SiteCapacityCB.SelectedItem;
            string siteCapacity = typeItem.Content.ToString();

            switch (siteCapacity)
            {
                case "5":
                    CBItemsList.Add(new CBItems { Number = "3/5" });
                    CBItemsList.Add(new CBItems { Number = "4/5" });
                    break;
                case "6":
                    CBItemsList.Add(new CBItems { Number = "3/6" });
                    CBItemsList.Add(new CBItems { Number = "4/6" });
                    CBItemsList.Add(new CBItems { Number = "5/6" });
                    break;
                case "7":
                    CBItemsList.Add(new CBItems { Number = "4/7" });
                    CBItemsList.Add(new CBItems { Number = "5/7" });
                    CBItemsList.Add(new CBItems { Number = "6/7" });
                    break;
                case "8":
                    CBItemsList.Add(new CBItems { Number = "4/8" });
                    CBItemsList.Add(new CBItems { Number = "5/8" });
                    CBItemsList.Add(new CBItems { Number = "6/8" });
                    CBItemsList.Add(new CBItems { Number = "7/8" });
                    break;
                case "9":
                    CBItemsList.Add(new CBItems { Number = "5/9" });
                    CBItemsList.Add(new CBItems { Number = "6/9" });
                    CBItemsList.Add(new CBItems { Number = "7/9" });
                    CBItemsList.Add(new CBItems { Number = "8/9" });
                    break;
                case "10":
                    CBItemsList.Add(new CBItems { Number = "5/10" });
                    CBItemsList.Add(new CBItems { Number = "6/10" });
                    CBItemsList.Add(new CBItems { Number = "7/10" });
                    CBItemsList.Add(new CBItems { Number = "8/10" });
                    CBItemsList.Add(new CBItems { Number = "9/10" });
                    break;
            }
            FractionCB.ItemsSource = CBItemsList;
        }
    }
}
