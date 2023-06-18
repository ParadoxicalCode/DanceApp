using DanceApp.Model;
using DanceApp.Model.Data;
using DanceApp.Model.Groups;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

#nullable disable
namespace DanceApp.View
{
    /// <summary>
    /// Логика взаимодействия для GroupsView.xaml
    /// </summary>
    public partial class GroupsView : Page
    {
        private DataBaseContext db = GlobalClass.db;
        //private int selectedGroupsCount = 0;
        private int RoundID;
        private bool CBSwitch;

        public List<Pair> selectedPairs = new List<Pair>();
        public List<Judge> selectedJudges = new List<Judge>(); 

        public GroupsView()
        {
            InitializeComponent();

            CBSwitch = false;

            RoundCB.ItemsSource = db.Round.ToList();
            JudgesDG.ItemsSource = db.Judge.ToList();

            var ID = db.Competition.Find(1);
            var round = db.Round.Where(u => u.ID == ID.RoundID).FirstOrDefault();
            RoundID = round.ID;
            RoundCB.SelectedValue = round.Title;
            GetGroups();

            int freePairs = new GetPairs().Free(RoundID).Count;
            FreePairsCountText.Text = freePairs.ToString();

            RoundStatusText.Text = db.Round.Find(RoundID).Status;

            CBSwitch = true;
        }

        public class Items
        {
            public string Element { get; set; }
        }





        private void GetGroups()
        {
            if (db.Group.Where(u => u.RoundID == RoundID).ToList() != null)
            {
                GroupsDG.ItemsSource = db.Group.Where(u => u.RoundID == RoundID).ToList();
            }
            else
                GroupsDG.ItemsSource = null;
        }

        private void GetDances()
        {
            // В танец надо привязывать какие танцы есть в группе
            var groupID = (GroupsDG.SelectedItem as Group).ID;
            var dancesInGroup = db.DancesInGroup.Where(x => x.GroupID == groupID).Select(x => x.DanceID).ToList();
            List<Dance> dances = new List<Dance>();

            foreach (var d in dancesInGroup)
            {
                var dance = db.Dance.Find(d);
                dances.Add(new Dance
                {
                    ID = dance.ID,
                    Title = dance.Title,
                    ShortName = dance.ShortName
                });
            }

            CBSwitch = false;
            DanceCB.ItemsSource = dances;
            CBSwitch = true;
            DanceCB.SelectedIndex = 0;
        }

        private void GetPerformances()
        {
            CBSwitch = false;
            PerformanceCB.ItemsSource = null;
            PairsDG.ItemsSource = null;
            CBSwitch = true;

            var groupID = (GroupsDG.SelectedItem as Group).ID;

            PerformanceCB.ItemsSource = db.Performance.Where(x => x.GroupID == groupID).ToList();
            PerformanceCB.SelectedIndex = 0;

            GetPairs();
        }

        private void GetPairs()
        {
            PairsDG.ItemsSource = null;

            // Поиск пар
            var groupID = (GroupsDG.SelectedItem as Group).ID;
            var performance = (PerformanceCB.SelectedItem as Performance).ID;

            List<Pair> pairs = new List<Pair>();
            var pairsInPerformance = db.PairsInPerformance.Where(u => u.PerformanceID == performance).ToList();

            foreach (var p in pairsInPerformance)
            {
                var data = db.Pair.Where(u => u.ID == p.PairID).FirstOrDefault();
                pairs.Add(data);
            }
            PairsDG.ItemsSource = pairs.ToList();

            CBSwitch = true;
        }

        private void UpdateDataBase()
        {
            try { db.SaveChanges(); }
            catch (Exception ex) { MessageBox.Show(ex.InnerException.Message); }
        }

        private void DefaultValues()
        {
            GetGroups();
            CBSwitch = false;
            GroupsDG.SelectedItem = null;
            DanceCB.ItemsSource = null;
            PerformanceCB.ItemsSource = null;
            PairsDG.ItemsSource = null;
            JudgesDG.ItemsSource = null;
            JudgesDG.ItemsSource = db.Judge.ToList();
            CBSwitch = true;

            selectedPairs.Clear();
            selectedJudges.Clear();
            int freePairs = new GetPairs().Free(RoundID).Count;
            FreePairsCountText.Text = freePairs.ToString();

            GroupStatusText.Text = "";
            PerformanceStatusText.Text = "";
            DanceStatusText.Text = "";
        }

        private void UpdateStatus(string Type)
        {
            
        }





        private void NextRound_Click(object sender, RoutedEventArgs e)
        {
            /*
            if (RoundStatusText.Text == "Не завершено")
            {
                MessageBoxView messageBox = new MessageBoxView("Статус тура \"Не завершено\"!", "Уведомление", 1);
                messageBox.ShowDialog();
            }
            else
            {
                NextRoundView nextRound = new NextRoundView();
                nextRound.ShowDialog();
            }

            // Ещё надо хотя бы 3 судьи чтобы было
            */

            NextRoundView nextRound = new NextRoundView(RoundID, selectedJudges);
            nextRound.ShowDialog();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            if (FreePairsCountText.Text == "0")
            {
                new MessageBoxView("Создать группу нельзя, так как нет свободных пар!", "Уведомление", 1).ShowDialog();
            }
            else
            {
                AddEditGroupsView x = new AddEditGroupsView(RoundID, 0);
                x.ShowDialog();

                CBSwitch = false;
                GetGroups();
                DefaultValues();
                CBSwitch = true;
            }
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            int groupID = (int)((Button)sender).CommandParameter;
            AddEditGroupsView x = new AddEditGroupsView(RoundID, groupID);
            x.ShowDialog();

            CBSwitch = false;
            GetGroups();
            DefaultValues();
            CBSwitch = true;
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Удалить запись?", "Уведомление", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                int groupID = (int)((Button)sender).CommandParameter;

                // Делаем пары нараспределёнными
                var pairsInRound = db.PairsInRound.Where(x => x.RoundID == RoundID).ToList();
                foreach (var p in pairsInRound)
                {
                    p.Select = false;
                    UpdateDataBase();
                }

                // Удаление всех связанных данных с удаляемой группой



                // Поиск танцев в группе
                var dancesInGroup = db.DancesInGroup.Where(x => x.GroupID == groupID).ToList();
                foreach (var d in dancesInGroup)
                {
                    // Поиск заходов в танце
                    var performancesInDance = db.Performance.Where(x => x.GroupID == d.GroupID).ToList();
                    foreach (var p in performancesInDance)
                    {
                        // Поиск пар в заходе
                        var pairsInPerformance = db.PairsInPerformance.Where(x => x.PerformanceID == p.ID).ToList();
                        db.PairsInPerformance.RemoveRange(pairsInPerformance);

                        db.Performance.Remove(p);
                        UpdateDataBase();
                    }
                    db.DancesInGroup.Remove(d);
                    UpdateDataBase();
                }

                // Удаление данных о парах в группе
                var pairsInGroup = db.PairsInGroup.Where(x => x.GroupID == groupID).ToList();
                db.PairsInGroup.RemoveRange(pairsInGroup);

                // Удаление группы
                var group = db.Group.Where(x => x.ID == groupID).ToList();
                db.Group.RemoveRange(group);

                try
                {
                    db.SaveChanges();
                    CBSwitch = false;
                    DefaultValues();
                    RoundStatusText.Text = new Model.Groups.UpdateStatus().Round(RoundID);
                    CBSwitch = true;
                }
                catch (Exception ex) { MessageBox.Show(ex.InnerException.Message); }
            }
        }

        private void SelectPerformance_Click(object sender, RoutedEventArgs e)
        {
            // Валидация
            if (GroupsDG.SelectedItem == null || DanceCB.SelectedItem == null || PerformanceCB.SelectedItem == null)
            {
                new MessageBoxView("Не все поля заполнены!", "Уведомление", 1).ShowDialog();
                return;
            }

            if (FreePairsCountText.Text != "0")
            {
                new MessageBoxView("Не все пары распределены по группам!", "Уведомление", 1).ShowDialog();
                return;
            }

            var groups = db.Group.Where(u => u.RoundID == RoundID).ToList();
            var checkNumber = groups.Where(u => u.Number == "" || u.Number == null).FirstOrDefault();
            if (checkNumber != null)
            {
                new MessageBoxView("Не всем группам присвоены номера!", "Уведомление", 1).ShowDialog();
                return;
            }

            if (DanceStatusText.Text == "Не завершено")
            {
                if (selectedPairs.Count < 2)
                {
                    new MessageBoxView("Необходимо выбрать хотя бы две пары!", "Уведомление", 1).ShowDialog();
                    return;
                }

                if (selectedJudges.Count < 3 || selectedJudges.Count > 7)
                {
                    new MessageBoxView("Количество судей должно быть не менее трёх и не более семи!", "Уведомление", 1).ShowDialog();
                    return;
                }

                if (selectedJudges.Count % 2 == 0)
                {
                    new MessageBoxView("Количество судей должно быть нечётным!", "Уведомление", 1).ShowDialog();
                    return;
                }
            }

            var GroupID = (GroupsDG.SelectedItem as Group).ID;
            var DanceID = (DanceCB.SelectedItem as Dance).ID;
            var PerformanceNumber = (PerformanceCB.SelectedItem as Performance).Number;

            DistributionPlacesView places = new DistributionPlacesView(DanceStatusText.Text, RoundID, GroupID, DanceID, PerformanceNumber, selectedJudges, selectedPairs);
            places.ShowDialog();

            DanceStatusText.Text = new Model.Groups.UpdateStatus().Dance(GroupID, PerformanceNumber, DanceID);
            PerformanceStatusText.Text = new Model.Groups.UpdateStatus().Performance(GroupID, PerformanceNumber, DanceID);
            GroupStatusText.Text = new Model.Groups.UpdateStatus().Group(GroupID);
            RoundStatusText.Text = new Model.Groups.UpdateStatus().Round(RoundID);
        }





        private void RoundCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CBSwitch == true)
            {
                PairsDG.ItemsSource = null;
                GetGroups();
                
                RoundID = (RoundCB.SelectedItem as Round).ID;
                RoundStatusText.Text = new Model.Groups.UpdateStatus().Round(RoundID);
                selectedPairs.Clear();
            }
        }

        private void GroupsDG_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CBSwitch == true)
            {
                GetPerformances();

                int groupID = (GroupsDG.SelectedItem as Group).ID;
                GroupStatusText.Text = new Model.Groups.UpdateStatus().Group(groupID);
                selectedPairs.Clear();
            } 
        }

        private void PerformanceCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CBSwitch == true)
            {
                GetPairs();

                int groupID = (GroupsDG.SelectedItem as Group).ID;
                int performanceNumber = (PerformanceCB.SelectedItem as Performance).Number;
                selectedPairs.Clear();

                GetDances();
                var DanceID = (DanceCB.SelectedItem as Dance).ID;

                PerformanceStatusText.Text = new Model.Groups.UpdateStatus().Performance(groupID, performanceNumber, DanceID);
            }
        }

        private void DanceCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CBSwitch == true)
            {
                int groupID = (GroupsDG.SelectedItem as Group).ID;
                int performanceNumber = (PerformanceCB.SelectedItem as Performance).Number;
                int danceID = (DanceCB.SelectedItem as Dance).ID;
                DanceStatusText.Text = new Model.Groups.UpdateStatus().Dance(groupID, performanceNumber, danceID);
            }
        }





        private void PairsChB_Checked(object sender, RoutedEventArgs e)
        {
            Pair row = (Pair)((CheckBox)sender).DataContext;
            var pair = db.Pair.Find(row.ID);
            selectedPairs.Add(pair);
        }

        private void PairsChB_Unchecked(object sender, RoutedEventArgs e)
        {
            Pair row = (Pair)((CheckBox)sender).DataContext;
            var delete = selectedPairs.Where(u => u.ID == row.ID).FirstOrDefault();
            selectedPairs.Remove(delete);
        }

        private void JudgesChB_Checked(object sender, RoutedEventArgs e)
        {
            Judge row = (Judge)((CheckBox)sender).DataContext;
            var judge = db.Judge.Find(row.ID);
            selectedJudges.Add(judge);
        }

        private void JudgesChB_Unchecked(object sender, RoutedEventArgs e)
        {
            Judge row = (Judge)((CheckBox)sender).DataContext;
            var delete = selectedJudges.Where(u => u.ID == row.ID).FirstOrDefault();
            selectedJudges.Remove(delete);
        }
    }
}
