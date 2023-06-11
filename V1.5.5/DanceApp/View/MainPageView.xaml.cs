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
using System.Windows.Markup;

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
            var competition = db.Competitions.Find(1);
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
                    {
                        if (competition.Date != null && competition.Date != "")
                            Frame.Content = new PairsView();
                        else
                        {
                            MessageBoxView messageBox = new MessageBoxView("Заполните данные соревнования!", "Уведомление", 1);
                            messageBox.ShowDialog();
                        } 
                    }
                    break;
                case "GroupsBtn":
                    if (!(Frame.Content is GroupsView))
                    {
                        if (competition.RegistrationStatus == true)
                        {
                            if (Next() == true)
                                Frame.Content = new GroupsView();
                        }
                        else
                            Frame.Content = new GroupsView();
                    }
                    break;
                case "DocumentsBtn":

                    break;
            }
        }

        private void DataBases_Click(object sender, RoutedEventArgs e)
        {
            // Отключение от базы данных
            //GlobalClass.db.Database.GetDbConnection().Close();
            //GlobalClass.db.Dispose();

            MainWindow window = new MainWindow();
            window.Show();
            var myWindow = Window.GetWindow(this);
            myWindow.Close();
        }

        private bool Next()
        {
            var pair = db.Pairs.Where(u => u.Number == "" || u.Number == null).FirstOrDefault();
            if (pair == null)
            {
                CloseRegistrationView window = new CloseRegistrationView();

                if (window.ShowDialog() == true)
                {
                    var competition = db.Competitions.Find(1);
                    competition.RegistrationStatus = false;
                    try { db.SaveChanges(); }
                    catch (Exception ex) { MessageBox.Show(ex.InnerException.Message); }
                    return true;
                }
                return false;
            }
            else
            {
                MessageBoxView messageBox = new MessageBoxView("Не всем парам присвоены номера!", "Уведомление", 1);
                messageBox.ShowDialog();
            }
            return false;
        }
    }
}
