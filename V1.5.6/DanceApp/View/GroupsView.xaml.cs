﻿using DanceApp.Model;
using DanceApp.Model.Data;
using DanceApp.Model.Groups;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
//using System.Text.RegularExpressions;
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
        public List<Dance> selectedDances = new List<Dance>();
        private List<Items> toolTipItems = new List<Items>();
        //private int selectedGroupsCount = 0;
        private int TourID;
        private bool CBSwitch;

        public List<Items> selectedPairs = new List<Items>();
        public List<Items> selectedJudges = new List<Items>(); 

        public GroupsView()
        {
            InitializeComponent();
            TourCB.ItemsSource = db.Tour.ToList();
            JudgesDG.ItemsSource = db.Judge.ToList();

            var ID = db.Competition.Find(1);
            var tour = db.Tour.Where(u => u.ID == ID.TourID).FirstOrDefault();
            TourID = tour.ID;
            TourCB.SelectedValue = tour.Title;
            GetGroups();
        }

        public class Items
        {
            public string Element { get; set; }
        }





        private void GetGroups()
        {
            if (db.Group.Where(u => u.TourID == TourID).ToList() != null)
            {
                GroupsDG.ItemsSource = db.Group.Where(u => u.TourID == TourID).ToList();
            }
            else
                GroupsDG.ItemsSource = null;
        }

        private void GetDances()
        {
            CBSwitch = false;
            selectedDances.Clear();
            DanceCB.ItemsSource = null;
            PerformanceCB.ItemsSource = null;
            PairsDG.ItemsSource = null;

            int groupID = (GroupsDG.SelectedItem as Group).ID;
            var dancesInGroup = db.DancesInGroup.Where(x => x.GroupID == groupID && x.Select == true).ToList();

            foreach (var d in dancesInGroup)
            {
                var dance = db.Dance.Find(d.DanceID);
                selectedDances.Add(dance);
            }
            
            DanceCB.ItemsSource = selectedDances.ToList();
            DanceCB.SelectedIndex = 0;

            GetPerformances();
        }

        private void GetPerformances()
        {
            CBSwitch = false;
            PerformanceCB.ItemsSource = null;
            PairsDG.ItemsSource = null;
            CBSwitch = true;

            var groupID = (GroupsDG.SelectedItem as Group).ID;
            var danceID = (DanceCB.SelectedItem as Dance).ID;

            PerformanceCB.ItemsSource = db.Performance.Where(x => x.GroupID == groupID && x.DanceID == danceID).ToList();
            PerformanceCB.SelectedIndex = 0;

            GetPairs();
        }

        private void GetPairs()
        {
            PairsDG.ItemsSource = null;

            // Поиск пар
            var groupID = (GroupsDG.SelectedItem as Group).ID;
            var danceID = (DanceCB.SelectedItem as Dance).ID;
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
            CBSwitch = true;
        }





        private void Add_Click(object sender, RoutedEventArgs e)
        {
            AddEditGroupsView x = new AddEditGroupsView(TourID, 0);
            x.ShowDialog();

            CBSwitch = false;
            GetGroups();
            DefaultValues();
            CBSwitch = true;
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            int groupID = (int)((Button)sender).CommandParameter;
            AddEditGroupsView x = new AddEditGroupsView(TourID, groupID);
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
                var pairsInTour = db.PairsInTour.Where(x => x.TourID == TourID).ToList();
                foreach (var p in pairsInTour)
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
                    var performancesInDance = db.Performance.Where(x => x.GroupID == d.GroupID && x.DanceID == d.DanceID).ToList();
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
                MessageBox.Show("Не все поля заполнены!");
                return;
            }

            var GroupID = (GroupsDG.SelectedItem as Group).ID;
            var DanceID = (DanceCB.SelectedItem as Dance).ID;
            var PerformanceID = (PerformanceCB.SelectedItem as Performance).Number;

            DistributionPlacesView places = new DistributionPlacesView(TourID, GroupID, DanceID, PerformanceID);
            places.ShowDialog();
        }





        private void TourCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CBSwitch = false;
            PairsDG.ItemsSource = null;
            GetGroups();
            CBSwitch = true;
            TourID = (TourCB.SelectedItem as Tour).ID;

            TourStatusText.Text = db.Tour.Find(TourID).Status;
        }

        private void GroupsDG_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CBSwitch == true)
            {
                GetDances();

                int groupID = (GroupsDG.SelectedItem as Group).ID;
                GroupStatusText.Text = db.Group.Find(groupID).Status;
            }
                
        }

        private void DanceCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CBSwitch == true)
                GetPerformances();
        }

        private void PerformanceCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CBSwitch == true)
            {
                GetPairs();

                int groupID = (GroupsDG.SelectedItem as Group).ID;
                int danceID = (DanceCB.SelectedItem as Dance).ID;
                int performanceNumber = (PerformanceCB.SelectedItem as Performance).Number;
                
                PerformanceStatusText.Text = db.Performance.Where(x => x.GroupID == groupID && x.DanceID == danceID && x.Number == performanceNumber).FirstOrDefault().Status;
            }
        }





        private void PairsChB_Checked(object sender, RoutedEventArgs e)
        {
            Pair row = (Pair)((CheckBox)sender).DataContext;
            selectedPairs.Add(new Items
            {
                Element = row.ID.ToString()
            });
        }

        private void PairsChB_Unchecked(object sender, RoutedEventArgs e)
        {
            Pair row = (Pair)((CheckBox)sender).DataContext;
            var delete = selectedPairs.Where(u => u.Element == row.ID.ToString()).FirstOrDefault();
            selectedPairs.Remove(delete);
        }

        private void JudgesChB_Checked(object sender, RoutedEventArgs e)
        {
            Judge row = (Judge)((CheckBox)sender).DataContext;
            selectedJudges.Add(new Items
            {
                Element = row.ID.ToString()
            });
        }

        private void JudgesChB_Unchecked(object sender, RoutedEventArgs e)
        {
            Judge row = (Judge)((CheckBox)sender).DataContext;
            var delete = selectedJudges.Where(u => u.Element == row.ID.ToString()).FirstOrDefault();
            selectedJudges.Remove(delete);
        }

        private void NextTour_Click(object sender, RoutedEventArgs e)
        {
            NextTourView nextTour = new NextTourView();
            nextTour.ShowDialog();
        }
    }
}
