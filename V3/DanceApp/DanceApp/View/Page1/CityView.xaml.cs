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
    /// Логика взаимодействия для CityView.xaml
    /// </summary>
    public partial class CityView : Page
    {
        public ApplicationContext db = new ApplicationContext();
        public CityView()
        {
            InitializeComponent();
            DG.ItemsSource = db.Cities.ToList();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            DBInteraction dbInteraction = new DBInteraction();
            string name = CityTB.Text.ToString();
            dbInteraction.CreateCity(name);
            DG.ItemsSource = db.Cities.ToList();
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
