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
        public DataBaseContext db = GlobalClass.db;
        public MainPageView()
        {
            InitializeComponent();
        }

        private void Groups_Click(object sender, RoutedEventArgs e)
        {
            if (Next() == true)
            {
                if (!(Frame.Content is GroupsView))
                {
                    Frame.Content = new GroupsView();
                }
            }
        }

        private void Places_Click(object sender, RoutedEventArgs e)
        {
            if (Next() == true)
            {
                if (!(Frame.Content is PlacesView))
                {
                    Frame.Content = new PlacesView();
                }
            }
        }

        private void Pairs_Click(object sender, RoutedEventArgs e)
        {
            if (!(Frame.Content is PairsView))
            {
                Frame.Content = new PairsView();
            }
        }

        private void Competition_Click(object sender, RoutedEventArgs e)
        {
            if (!(Frame.Content is Competition))
            {
                Frame.Content = new Competition();
            }
        }

        private void Judge_Click(object sender, RoutedEventArgs e)
        {
            if (!(Frame.Content is JudgesView))
            {
                Frame.Content = new JudgesView();
            }
        }

        private bool Next()
        {
            var data = db.Competitions.Where(u => u.ID == 1).FirstOrDefault();

            if (data.RegistrationSwitch == true) { return true; }
            else
            {
                MessageBox.Show("Завершите регистрацию прежде чем переходить к следующему этапу!");
                return false;
            }
        }

        private void DataBase_Click(object sender, RoutedEventArgs e)
        {
            db.Database.CloseConnection(); // Отключение от базы данных

            MainWindow window = new MainWindow();
            window.Show();
            var myWindow = Window.GetWindow(this);
            myWindow.Close();
        }
    }
}
