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
        private List<TourCBItems> tourCBItems = new List<TourCBItems>();
        private List<Group> selectedGroups = new List<Group>();
        private List<ToolTipItem> toolTipItems = new List<ToolTipItem>();
        //private int selectedGroupsCount = 0;
        private bool UpdateFlag;
        public GroupsView()
        {
            InitializeComponent();

            GetGroups();
            AddItemsToCB();
        }

        private class TourCBItems
        {
            public string TourName { get; set; }
        }

        private class ToolTipItem
        {
            public string AgeCategory { get; set; }
        }

        private void AddGroups()
        {
            var allAgeCategories = new string[2, db.Pairs.Count()];
            var ageCategories = new string[2, db.Pairs.Count()];
            var pairs = db.Pairs.ToList();
            int i = 0;

            // Выписываем в массив все возрастные категории и типы выступления
            foreach (var p in pairs)
            {
                allAgeCategories[0, i] = p.AgeCategoryID.ToString();
                allAgeCategories[1, i] = p.PerformanceType;
                i++;
            }

            // Поиск всех уникальных возрастных категорий (тип выступления не должен повторяться, если ID одинаковый)
            ageCategories[0, 0] = allAgeCategories[0, 0];
            ageCategories[1, 0] = allAgeCategories[1, 0];
            int a = 1;
            for (int x = 1; x < db.Pairs.Count(); x++)
            {
                bool result = false;
                for (int j = 0; j < db.Pairs.Count(); j++)
                {
                    bool first = false;
                    bool second = false;
                    if (allAgeCategories[0, x] == ageCategories[0, j] && ageCategories[0, j] != null)
                        first = true;
                    if (allAgeCategories[1, x] == ageCategories[1, j] && ageCategories[0, j] != null)
                        second = true;
                    if (first == true && second == true)
                    {
                        result = true;
                        break;
                    }
                }
                if (result == false)
                {
                    ageCategories[0, a] = allAgeCategories[0, x];
                    ageCategories[1, a] = allAgeCategories[1, x];
                    a++;
                }
            }

            // Сортировка от младших возрастных групп к старшим
            for (i = 0; i < db.Pairs.Count() - 1; i++)
            {
                for (int j = i + 1; j < db.Pairs.Count(); j++)
                {
                    if (ageCategories[0, i] == null || ageCategories[0, j] == null)
                        goto next;
                    else if (Int32.Parse(ageCategories[0, i]) > Int32.Parse(ageCategories[0, j])) // заменить CompareTo на что-то более понятное и предсказуемое
                    {
                        // меняем местами значения строк i и j
                        string temp1 = ageCategories[0, i];
                        string temp2 = ageCategories[1, i];
                        ageCategories[0, i] = ageCategories[0, j];
                        ageCategories[1, i] = ageCategories[1, j];
                        ageCategories[0, j] = temp1;
                        ageCategories[1, j] = temp2;
                    }
                }
            }
            next:

            // Сортировка по типу выступления: сначала соло, затем пара
            for (i = 0; i < db.Pairs.Count() - 1; i++)
            {
                if (ageCategories[0, i] == ageCategories[0, i + 1])
                {
                    string temp;
                    if (ageCategories[0, i] == "")
                    {
                        temp = ageCategories[0, i];
                        ageCategories[0, i] = ageCategories[0, i + 1];
                        ageCategories[0, i + 1] = temp;
                    }
                }
            }

            /*
            // Создание групп
            for (i = 0; i < db.Pairs.Count() && ageCategories[0, i] != null; i++)
            {
                var data = db.AgeCategories.Where(u => u.ID == Int32.Parse(ageCategories[0, i])).FirstOrDefault();
                Group g = new Group()
                {
                    Number = i + 1,
                    Title = data.Title + ageCategories[1, i],
                    AgeCategoryID = Int32.Parse(ageCategories[0, i]),
                    PerformanceType = ageCategories[1, i],
                    PairsCount = 0
                };
                db.Groups.Add(g);
                UpdateDataBase();
            }
            

            // Узнаём, к какой группе относится пара и увеличиваем счётчик количества пар в группе на 1
            foreach (var p in pairs)
            {
                // Находим для пары группу по ID и типу выступления
                var data = db.Groups.Where(u => u.AgeCategoryID == p.AgeCategoryID && u.PerformanceType == p.PerformanceType).FirstOrDefault();
                data.PairsCount += 1;
                UpdateDataBase();
            }
            */
            GetGroups();
        }

        private void GetGroups()
        {
            GroupsDG.ItemsSource = db.Groups.ToList();

            // Нужно посчитать количество пар, которым 
            toolTipItems.Add(new ToolTipItem { AgeCategory = "Дети 0" });
            toolTipItems.Add(new ToolTipItem { AgeCategory = "Взрослые" });
            ToolTipListBox.ItemsSource = toolTipItems.ToList();
        }

        private void UpdateDataBase()
        {
            try { db.SaveChanges(); }
            catch (Exception ex) { MessageBox.Show(ex.Message.ToString()); }
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            AddEditGroupsView x = new AddEditGroupsView(0);
            x.ShowDialog(); GetGroups();
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            int ID = (int)((Button)sender).CommandParameter;
            AddEditGroupsView x = new AddEditGroupsView(ID);
            x.ShowDialog(); GetGroups();
        }

        public class PairsDGItems
        {
            public string Number { get; set; }
            public string MaleSurname { get; set; }
            public string MaleName { get; set; }
            public string MalePatronymic { get; set; }
            public string MaleBirthday { get; set; }
            public string FemaleSurname { get; set; }
            public string FemaleName { get; set; }
            public string FemalePatronymic { get; set; }
            public string FemaleBirthday { get; set; }
            public string Club { get; set; }
        }

        // Вывод всех пар, которые состоят в выбранной группе в нижнюю таблицу
        private void GroupsDG_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            /*
            if (GroupsDG.SelectedItem != null)
            {
                Group selectedGroup = GroupsDG.SelectedItem as Group;

                var query = from p in db.Pairs
                    where p.GroupID == selectedGroup.ID
                    select p;
                PairsDG.ItemsSource = query.ToList();
            }
            */
        }

        private void AddItemsToCB()
        {
            int pairsCount = db.Pairs.Count();
            if (pairsCount <= 8)
                tourCBItems.Add(new TourCBItems { TourName = "Финал" });

            if (pairsCount >= 7 && pairsCount <= 15)
                tourCBItems.Add(new TourCBItems { TourName = "Полуфинал" });

            if (pairsCount >= 13 && pairsCount <= 30)
                tourCBItems.Add(new TourCBItems { TourName = "1/4" });

            if (pairsCount >= 25 && pairsCount <= 60)
                tourCBItems.Add(new TourCBItems { TourName = "1/8" });

            TourCB.ItemsSource = tourCBItems.ToList();

            UpdateFlag = false;
            if (tourCBItems.Count() == 1)
            {
                TourCB.SelectedItem = TourCB.Items[0];
            }
            else if (db.Tours.FirstOrDefault() != null)
            {
                TourCB.SelectedValue = db.Tours.FirstOrDefault().TourName;
            }
            UpdateFlag = true;
        }

        // Заполнение таблицы Tours начиная с выбранного тура и заканчивая туром "Финал"
        private void TourCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (UpdateFlag == true)
            {
                var data = db.Competitions.Where(u => u.ID == 1).FirstOrDefault();
                data.CurrentTour = null;

                var performances = db.Performances.ToList();
                db.Performances.RemoveRange(performances);
                UpdateDataBase();

                var tours = db.Tours.ToList();
                db.Tours.RemoveRange(tours);
                UpdateDataBase();

                // Начиная с первого тура заполнить таблицу до финального
                var tourName = TourCB.SelectedItem as TourCBItems;
                string selectTour = tourName.TourName;
                string[] allTours = new string[4] { "1/8", "1/4", "Полуфинал", "Финал" };
                int toursCount = 1;
                if (selectTour == "Полуфинал")
                    toursCount = 2;
                else if (selectTour == "1/4")
                    toursCount = 3;
                else if (selectTour == "1/8")
                    toursCount = 4;

                for (int i = 4 - toursCount; i < 4; i++)
                {
                    db.Tours.Add(new Tour { TourName = allTours[i] });
                    UpdateDataBase();
                }

                // Запоминаем начальный тур
                UpdateFlag = false;
                TourCB.SelectedValue = allTours[4 - toursCount];
                UpdateFlag = true;
                data.CurrentTour = allTours[4 - toursCount];
                UpdateDataBase();
            }
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {

        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {

        }
    }
}
