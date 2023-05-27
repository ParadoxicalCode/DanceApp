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

        private void MenuButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            if (button != null)
            {
                switch (button.Name)
                {
                    case "CompetitionBtn":
                        CompetitionView x = new CompetitionView();
                        x.ShowDialog();
                        break;
                    case "JudgesBtn":
                        if (!(Frame.Content is JudgesView))
                            Frame.Content = new JudgesView();
                        break;
                    case "PairsBtn":
                        if (!(Frame.Content is PairsView))
                            Frame.Content = new PairsView();
                        break;
                    case "GroupsBtn":
                        if (!(Frame.Content is GroupsView))
                        {
                            var data = db.Competitions.Where(u => u.ID == 1).FirstOrDefault();
                            if (data.RegistrationStatus == true)
                            {
                                if (MessageBox.Show("После перехода на страницу \"Группа\" регистрация закроется. Вы уверены?", "Уведомление", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                                {
                                    data.RegistrationStatus = false;
                                    try { db.SaveChanges(); }
                                    catch (Exception ex) { MessageBox.Show(ex.Message.ToString()); }
                                    Frame.Content = new GroupsView();
                                }
                            }
                            else
                                Frame.Content = new GroupsView();
                        }
                        break;
                    case "PlacesBtn":
                        //if (Next() == true)
                        //{
                            if (!(Frame.Content is PlacesView))
                            {
                                Frame.Content = new PlacesView();
                            }
                        //}
                        break;
                    case "DocumentsBtn":

                        break;
                }
            }
        }

        private void DataBases_Click(object sender, RoutedEventArgs e)
        {
            db.Database.CloseConnection(); // Отключение от базы данных

            MainWindow window = new MainWindow();
            window.Show();
            var myWindow = Window.GetWindow(this);
            myWindow.Close();
        }

        private bool Next()
        {
            var data = db.Competitions.Where(u => u.ID == 1).FirstOrDefault();

            if (data.RegistrationStatus == false) { return false; }
            else
            {
                MessageBox.Show("Завершите регистрацию прежде чем переходить к следующему этапу!");
                return false;
            }
        }
    }
}
