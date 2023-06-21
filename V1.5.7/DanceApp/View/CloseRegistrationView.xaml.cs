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
        private List<Round> RoundCBItemsList = new List<Round>();
        bool CBSwitch;
        public CloseRegistrationView()
        {
            InitializeComponent();
            AddItemsToCB();
        }

        private void AddItemsToCB()
        {
            int pairsCount = db.Pair.Count();
            if (pairsCount <= 8)
                RoundCBItemsList.Add(new Round { ID = 1, Title = "Финал" });

            if (pairsCount >= 7 && pairsCount <= 15)
                RoundCBItemsList.Add(new Round { ID = 2, Title = "Полуфинал" });

            if (pairsCount >= 13 && pairsCount <= 30)
                RoundCBItemsList.Add(new Round { ID = 3, Title = "1/4" });

            if (pairsCount >= 25 && pairsCount <= 60)
                RoundCBItemsList.Add(new Round { ID = 4, Title = "1/8" });

            CBSwitch = false;
            RoundCB.ItemsSource = RoundCBItemsList.ToList();
            CBSwitch = true;

            if (RoundCBItemsList.Count() == 1)
                RoundCB.SelectedIndex = 0;
        }

        private void Next_Click(object sender, RoutedEventArgs e)
        {
            if (RoundCB.SelectedIndex >= 0)
            {
                this.DialogResult = true;
            }
            else
            {
                MessageBox.Show("Выберите начальный тур!");
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void RoundCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CBSwitch == true)
            {
                var data = db.Competition.Where(u => u.ID == 1).FirstOrDefault();
                var firstRound = (RoundCB.SelectedItem as Round).Title;

                // Начиная с первого тура заполнить таблицу до финального
                string[] allRounds = new string[4] { "1/8", "1/4", "Полуфинал", "Финал" };
                int roundsCount = 1;
                if (firstRound == "Полуфинал")
                    roundsCount = 2;
                else if (firstRound == "1/4")
                    roundsCount = 3;
                else if (firstRound == "1/8")
                    roundsCount = 4;

                var rounds = db.Round.ToList();
                db.Round.RemoveRange(rounds);

                for (int i = 4 - roundsCount; i < 4; i++)
                {
                    var round = new Round();
                    round.Title = allRounds[i];
                    round.Status = false;

                    db.Round.Add(round);
                    UpdateDataBase();
                }
                data.RoundID = db.Round.FirstOrDefault().ID;
                UpdateDataBase();

                // Пары в первом туре

                var pairs = db.PairsInRound.ToList();
                db.PairsInRound.RemoveRange(pairs);

                foreach (var p in db.Pair)
                {
                    var pairsInRound = new PairsInRound();
                    pairsInRound.RoundID = db.Round.FirstOrDefault().ID;
                    pairsInRound.PairID = p.ID;
                    pairsInRound.Select = false;
                    db.PairsInRound.Add(pairsInRound);

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
