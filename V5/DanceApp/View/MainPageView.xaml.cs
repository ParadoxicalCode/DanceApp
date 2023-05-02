using DanceApp.Model;
using DanceApp.Model.Data;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.EntityFrameworkCore;

#nullable disable
namespace DanceApp.View
{
    /// <summary>
    /// Логика взаимодействия для Page2View.xaml
    /// </summary>
    public partial class MainPageView : Page
    {
        public MainPageView()
        {
            InitializeComponent();
        }

        private void Groups_Click(object sender, RoutedEventArgs e)
        {
            if (Next("PairsToGroups") == true)
            {
                if (!(Frame.Content is GroupsView))
                {
                    Frame.Content = new GroupsView();
                }
            }
        }

        private void Places_Click(object sender, RoutedEventArgs e)
        {
            if (!(Frame.Content is PlacesView))
            {
                Frame.Content = new PlacesView();
            }  
        }

        private void Pairs_Click(object sender, RoutedEventArgs e)
        {
            if (!(Frame.Content is PairsView))
            {
                Frame.Content = new PairsView();
            }
        }

        private void Settings_Click(object sender, RoutedEventArgs e)
        {
            if (!(Frame.Content is SettingsView))
            {
                Frame.Content = new SettingsView();
            }
        }

        private void Judge_Click(object sender, RoutedEventArgs e)
        {
            if (!(Frame.Content is JudgesView))
            {
                Frame.Content = new JudgesView();
            }
        }

        private bool Next(string stage)
        {
            bool result = false;

            using (DataBaseContext db = new DataBaseContext())
            {
                var data = db.Competitions.Where(u => u.ID == 1).FirstOrDefault();
                switch (stage)
                {
                    case "SettingsToRegistration":
                        if (data.SettingsToJudges == 1) { result = true; }
                        else
                        {
                            MessageBox.Show("Завершите регистрацию соревнования прежде чем переходить к следующему этапу!");
                        }
                        break;

                    case "PairsToGroups":
                        if (data.PairsToGroups == 1) { result = true; }
                        else
                        {
                            MessageBox.Show("Завершите регистрацию пар прежде чем переходить к следующему этапу!");
                            return false;
                        }
                        break;
                }
            }
            
            return result;
        }

        private void DataBase_Click(object sender, RoutedEventArgs e)
        {
            var db = new DataBaseContext();
            db.Database.CloseConnection(); // Отключение от базы данных
            db.Dispose();

            MainWindow window = new MainWindow();
            window.Show();
            var myWindow = Window.GetWindow(this);
            myWindow.Close();
        }
    }
}
