using DanceApp.Model;
using DanceApp.Model.Data;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public partial class Competition : Page
    {
        public DataBaseContext db = GlobalClass.db;
        public Competition()
        {
            InitializeComponent();

            /*
            dataBaseName.Text = GlobalClass.dataBaseName;

            var data = db.Competitions.Where(u => u.ID == 1).FirstOrDefault();
            TitleTB.Text = data.Title;
            DateTB.Text = data.StartDate;
            TimeTB.Text = data.StartTime;
            ManagerTB.Text = data.Manager;
            AddressTB.Text = data.Address;
            ClubTB.Text = data.Club;
            }   
            */
        }

        public class DGItems
        {
            public string Title { get; set; }
            public string StartDate { get; set; }
            public string StartTime { get; set; }
            public string Manager { get; set; }
            public string Address { get; set; }
            public string Club { get; set; }
            public int SettingsToJudges { get; set; }
            public int PairsToGroups { get; set; }
        }

        /*
        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            var data = db.Competitions.Where(u => u.ID == 1).FirstOrDefault();
            if (TitleTB.Text != "") { data.Title = TitleTB.Text; }
            if (DateTB.Text != "") { data.StartDate = DateTB.Text; }
            if (TimeTB.Text != "") { data.StartTime = TimeTB.Text; }
            if (ManagerTB.Text != "") { data.Manager = ManagerTB.Text; }
            if (AddressTB.Text != "") { data.Address = AddressTB.Text; }
            if (ClubTB.Text != "") { data.Club = ClubTB.Text; }

            try
            {
                db.SaveChanges();
                MessageBox.Show("Настройки сохранены!");
            }
            catch (Exception ex) { MessageBox.Show(ex.Message.ToString()); }
        }
        */

        private void Select_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
