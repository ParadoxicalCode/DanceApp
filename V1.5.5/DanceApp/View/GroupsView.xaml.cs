using DanceApp.Model;
using DanceApp.Model.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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
        private List<Items> tourCBItems = new List<Items>();
        private List<Group> selectedGroups = new List<Group>();
        private List<Items> toolTipItems = new List<Items>();
        //private int selectedGroupsCount = 0;
        private bool CBSwitch;
        public GroupsView()
        {
            InitializeComponent();
            TourCB.ItemsSource = db.Tours.ToList();
            JudgesDG.ItemsSource = db.Judges.ToList();

            var ID = db.Competitions.Where(u => u.ID == 1).FirstOrDefault();
            var tour = db.Tours.Where(u => u.ID == ID.TourID).FirstOrDefault();
            TourCB.SelectedValue = tour.Title;
            GetGroups();
        }

        private class Items
        {
            public string Element { get; set; }
        }

        private void GetGroups()
        {
            var tour = db.Tours.Where(u => u.Title == (TourCB.SelectedItem as Tour).Title).FirstOrDefault();
            GroupsDG.ItemsSource = db.Groups.Where(u => u.TourID == tour.ID).ToList();

            // Нужно посчитать количество пар, которым 
            //toolTipItems.Add(new Items { Element = "Дети 0" });
            //toolTipItems.Add(new Items { Element = "Взрослые" });
            //ToolTipListBox.ItemsSource = toolTipItems.ToList();
        }

        // Вывод всех пар, которые состоят в выбранной группе в нижнюю таблицу
        private void GetPairs(int groupID)
        {
            List<Pair> pairs = new List<Pair>();
            var pairsInGroup = db.PairsInGroup.Where(u => u.GroupID == groupID).ToList();

            foreach (var p in pairsInGroup)
            {
                var data = db.Pairs.Where(u => u.ID == p.PairID).FirstOrDefault();
                pairs.Add(data);
            }
            PairsDG.ItemsSource = pairs.ToList();
        }

        private void UpdateDataBase()
        {
            try { db.SaveChanges(); }
            catch (Exception ex) { MessageBox.Show(ex.InnerException.Message); }
        }



        private void Add_Click(object sender, RoutedEventArgs e)
        {
            AddEditGroupsView x = new AddEditGroupsView(0);
            x.ShowDialog(); 

            GetGroups();
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            int groupID = (int)((Button)sender).CommandParameter;
            AddEditGroupsView x = new AddEditGroupsView(groupID);
            x.ShowDialog(); 

            GetGroups();
            GetPairs(groupID);
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            // Удалить связанные с группой данные, саму группу и сделать все пары, состоящие в этой группе нераспределёнными
        }





        private void GroupsDG_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int selectedGroup = (GroupsDG.SelectedItem as Group).ID;
            GetPairs(selectedGroup);


            // Нужно в ComboBox вывести танцы выбранной группы
        }

        private void TourCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            GetGroups();
        }

        private void DanceCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Нужно в ComboBox вывести заходы выбранного танца
        }

        private void GroupCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void PerformanceCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }





        private void PairsChB_Checked(object sender, RoutedEventArgs e)
        {
            
        }

        private void PairsChB_Unchecked(object sender, RoutedEventArgs e)
        {
            
        }

        private void JudgesChB_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void JudgesChB_Unchecked(object sender, RoutedEventArgs e)
        {

        }
    }
}
