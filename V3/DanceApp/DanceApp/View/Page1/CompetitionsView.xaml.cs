using DanceApp.Model.Data;
using System;
using System.Collections.Generic;
using System.Linq;
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

namespace DanceApp.View.Page1
{
    /// <summary>
    /// Логика взаимодействия для CompetitionsView.xaml
    /// </summary>
    public partial class CompetitionsView : Page
    {
        //public ApplicationContext db = new ApplicationContext();
        public CompetitionsView()
        {
            InitializeComponent();
            DBInteraction dbInteraction = new DBInteraction();
            string name = "Новосибирск";
            //bInteraction.CreateCity(name);
            //DG.ItemsSource = db.Competitions.ToList();

            name = "Новосибирск2";
            //dbInteraction.CreateCity(name);

            //DG.ItemsSource = db.Competitions.ToList();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            DBInteraction dbInteraction = new DBInteraction();
            int city = 1;
            string name = "Соревнование в честь 1 сентября"; 
            string manager = "Иванов И.И."; 
            //dbInteraction.CreateCompetition(name, manager, city);
            //DG.ItemsSource = db.Competitions.ToList();
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
