using DanceApp.Model.Data;
using DanceApp.Model;
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
using System.Windows.Shapes;
using static DanceApp.View.CompetitionView;

namespace DanceApp.View
{
    /// <summary>
    /// Логика взаимодействия для CloseRegistrationView.xaml
    /// </summary>
    #nullable disable
    public partial class CloseRegistrationView : Window
    {
        private DataBaseContext db = GlobalClass.db;
        private List<Tour> TourCBItemsList = new List<Tour>();
        bool CBSwitch;
        public CloseRegistrationView()
        {
            InitializeComponent();
            AddItemsToCB();
        }

        private void AddItemsToCB()
        {
            int pairsCount = db.Pairs.Count();
            if (pairsCount <= 8)
                TourCBItemsList.Add(new Tour { ID = 1, Title = "Финал" });

            if (pairsCount >= 7 && pairsCount <= 15)
                TourCBItemsList.Add(new Tour { ID = 2, Title = "Полуфинал" });

            if (pairsCount >= 13 && pairsCount <= 30)
                TourCBItemsList.Add(new Tour { ID = 3, Title = "1/4" });

            if (pairsCount >= 25 && pairsCount <= 60)
                TourCBItemsList.Add(new Tour { ID = 4, Title = "1/8" });

            CBSwitch = false;
            TourCB.ItemsSource = TourCBItemsList.ToList();
            CBSwitch = true;

            if (TourCBItemsList.Count() == 1)
                TourCB.SelectedIndex = 0;
        }

        private void Next_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void TourCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CBSwitch == true)
            {
                var data = db.Competitions.Where(u => u.ID == 1).FirstOrDefault();
                var firstTour = (TourCB.SelectedItem as Tour).Title;

                // Начиная с первого тура заполнить таблицу до финального
                string[] allTours = new string[4] { "1/8", "1/4", "Полуфинал", "Финал" };
                int toursCount = 1;
                if (firstTour == "Полуфинал")
                    toursCount = 2;
                else if (firstTour == "1/4")
                    toursCount = 3;
                else if (firstTour == "1/8")
                    toursCount = 4;

                var tours = db.Tours.ToList();
                db.Tours.RemoveRange(tours);

                for (int i = 4 - toursCount; i < 4; i++)
                {
                    db.Tours.Add(new Tour { Title = allTours[i] });
                    UpdateDataBase();
                }
                data.TourID = db.Tours.FirstOrDefault().ID;
                UpdateDataBase();

                // Пары в первом туре

                var pairs = db.PairsInTour.ToList();
                db.PairsInTour.RemoveRange(pairs);

                foreach (var p in db.Pairs)
                {
                    var pairsInTour = new PairsInTour();
                    pairsInTour.TourID = db.Tours.FirstOrDefault().ID;
                    pairsInTour.PairID = p.ID;
                    pairsInTour.Select = false;
                    db.PairsInTour.Add(pairsInTour);

                    try { db.SaveChanges(); }
                    catch (Exception ex) { MessageBox.Show(ex.InnerException.Message); }
                }
            }
        }

        private void UpdateDataBase()
        {
            try { db.SaveChanges(); }
            catch (Exception ex) { MessageBox.Show(ex.InnerException.Message); }
        }
    }
}
