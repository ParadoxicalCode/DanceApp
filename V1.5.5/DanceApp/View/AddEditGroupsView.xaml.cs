using DanceApp.Model;
using DanceApp.Model.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using static DanceApp.View.CompetitionView;

#nullable disable
namespace DanceApp.View
{
    /// <summary>
    /// Логика взаимодействия для AddEditGroupsView.xaml
    /// </summary>
    public partial class AddEditGroupsView : Window
    {
        public DataBaseContext db = GlobalClass.db;
        public bool UpdateCompetition;
        public int ID;
        private bool CBSwitch = true;
        List<Pair> pairs;
        public List<CBItems> performanceTypeList = new List<CBItems>();
        public List<CBItems> sportsDisciplineList = new List<CBItems>();
        public List<CBItems> selectedPairs = new List<CBItems>();
        public List<CBItems> selectedDances = new List<CBItems>();
        List<AgeCategory> category1CBList = new List<AgeCategory>();

        public class CBItems
        {
            public string Element { get; set; }
        }

        public AddEditGroupsView(int id)
        {
            InitializeComponent();
            sportsDisciplineList.Add(new CBItems { Element = "Европейская программа" });
            sportsDisciplineList.Add(new CBItems { Element = "Латиноамериканская программа" });
            SportsDisciplineCB.ItemsSource = sportsDisciplineList;
            
            performanceTypeList.Add(new CBItems { Element = "Пара" });
            performanceTypeList.Add(new CBItems { Element = "Соло" });
            PerformanceTypeCB.ItemsSource = performanceTypeList;

            ID = id;
            if (id != 0)
            {
                var group = db.Groups.Where(u => u.ID == ID).FirstOrDefault();

                CBSwitch = false;
                SportsDisciplineCB.SelectedValue = group.SportsDiscipline;

                var dance = db.DancesInGroup.Where(u => u.GroupID == ID).ToList();
                DancesDG.ItemsSource = dance;
                CBSwitch = true;

                PerformanceTypeCB.SelectedValue = group.PerformanceType;
                Category1CB.SelectedValue = group.AgeCategory1;
                if (group.AgeCategory2 != null)
                    Category2CB.SelectedValue = group.AgeCategory2;
            }
            else
            {
                SportsDisciplineCB.SelectedIndex = 0;
                PerformanceTypeCB.SelectedIndex = 0;
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (selectedPairs.Count < 1 || selectedDances.Count < 1 || PerformanceTypeCB.SelectedItem == null ||
                Category1CB.SelectedItem == null || SportsDisciplineCB.SelectedItem == null) 
            {
                MessageBox.Show("Не все поля заполнены!");
                return;
            }

            string number = NumberTB.Text;
            if (TrimAndCheckNumber(ref number) == false && number != "")
            {
                MessageBox.Show("В поле номер обязательно должно быть число!");
                return;
            }

            var data = db.Competitions.Where(u => u.ID == 1).FirstOrDefault();
            var sportsDiscipline = (SportsDisciplineCB.SelectedItem as CBItems).Element;
            var performanceType = (PerformanceTypeCB.SelectedItem as CBItems).Element;
            var category1 = (Category1CB.SelectedItem as AgeCategory).Title;
            string category2;

            if (Category2CB.SelectedItem as AgeCategory != null)
                category2 = " +" + (Category2CB.SelectedItem as AgeCategory).Title;
            else
                category2 = "";

            var title = category1 + category2;
            if (performanceType == "Соло")
                title += " (" + performanceType + ")";

            string program;
            if (selectedDances.Count == 1)
                program = "1 танец";
            else if (selectedDances.Count < 5)
                program = selectedDances.Count.ToString() + " танца";
            else
                program = "5 танцев";

            if (ID == 0)
            {
                bool checkIsExist = db.Groups.Any(x => x.Number == number);
                int identical = db.Groups.Count(x => x.Number == number);

                // Валидация номера
                if (checkIsExist == true && number != "" && identical > 1)
                {
                    MessageBox.Show("Пара с таким номером уже есть!");
                    return;
                }

                var group = new Group();
                group.TourID = (int)data.TourID;
                group.Number = NumberTB.Text;
                group.Title = title;
                group.AgeCategory1 = category1;
                group.AgeCategory2 = category2;
                group.PerformanceType = performanceType;
                group.Program = program;
                group.SportsDiscipline = sportsDiscipline;
                group.PairsCount = selectedPairs.Count;

                db.Groups.Add(group);
                try
                {
                    db.SaveChanges();
                    MessageBox.Show("Запись добавлена!");
                    NumberTB.Text = "";
                    CBSwitch = false;
                    Category2CB.ItemsSource = null;
                    Category1CB.ItemsSource = null;
                    CBSwitch = true;

                    GetDances();
                    SportsDisciplineCB.SelectedIndex = 0;
                    GetPairs();
                    PerformanceTypeCB.SelectedIndex = 0;

                    

                    BackBtn.Visibility = Visibility.Hidden;
                    BeginningBtn.Visibility = Visibility.Hidden;
                    NextBtn.Visibility = Visibility.Hidden;
                    EndBtn.Visibility = Visibility.Hidden;
                    NumberOfRecords.Content = "";

                    // Удаление данных о выбранных танцах
                    var deleteDances = db.DancesInGroup.Where(x => x.GroupID == group.ID).ToList();
                    db.DancesInGroup.RemoveRange(deleteDances);
                    UpdateDataBase();

                    // Сохранение списка выбранных танцев

                    // Если выбрана такая-то программа, то создать список танцев из этой программы.
                    // Выбранным парам Select = true

                    foreach (var d in selectedDances)
                    {
                        var dancesInGroup = new DancesInGroup();
                        dancesInGroup.GroupID = group.ID;
                        dancesInGroup.DanceID = Int32.Parse(d.Element);
                        db.DancesInGroup.Add(dancesInGroup);
                        UpdateDataBase();
                    }

                    // Удаление данных о выбранных парах
                    var deletePairs = db.PairsInGroup.Where(x => x.GroupID == group.ID).ToList();
                    List<Pair> pairs = new List<Pair>();

                    foreach (var p in deletePairs)
                    {
                        var pair = db.Pairs.Where(u => u.ID == p.PairID).FirstOrDefault();
                        pairs.Add(pair);
                    }

                    foreach (var p in pairs)
                    {
                        var pair = db.PairsInTour.Where(u => u.TourID == (int)data.TourID && u.PairID == p.ID).FirstOrDefault();
                        pair.Select = false;
                        UpdateDataBase();
                    }
                    db.PairsInGroup.RemoveRange(deletePairs);
                    UpdateDataBase();

                    // Сохранение списка выбранных пар
                    foreach (var p in selectedPairs)
                    {
                        var pairsInGroup = new PairsInGroup();
                        pairsInGroup.GroupID = group.ID;
                        pairsInGroup.PairID = Int32.Parse(p.Element);
                        db.PairsInGroup.Add(pairsInGroup);

                        var pair = db.PairsInTour.Where(u => u.TourID == (int)data.TourID && u.PairID == Int32.Parse(p.Element)).FirstOrDefault();
                        pair.Select = true;

                        UpdateDataBase();
                    }
                }
                catch (Exception ex) { MessageBox.Show(ex.InnerException.Message); }
            }
            else
            {
                var group = db.Groups.Where(u => u.ID == ID).FirstOrDefault();

                try
                {
                    db.SaveChanges();
                    MessageBox.Show("Запись изменена!");
                    this.Close();
                }
                catch (Exception ex) { MessageBox.Show(ex.InnerException.Message); }
            }

            // Добавление выбранных танцев в текущую группу





            // Добавление выбранных пар в текущую группу
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }



        private void SportsDisciplineCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CBSwitch == true)
                GetDances();
        }

        private void GetDances()
        {
            DancesDG.ItemsSource = null;

            if (SportsDisciplineCB.SelectedItem == SportsDisciplineCB.Items[0])
            {
                DancesDG.ItemsSource = db.Dances.Where(u => u.SportsDiscipline == "Европейская программа").ToList();
            }
            else
                DancesDG.ItemsSource = db.Dances.Where(u => u.SportsDiscipline == "Латиноамериканская программа").ToList();
        }


        // Поиск всех уникальных возрастных категорий с выбранным типом выступления
        private void PerformanceTypeCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CBSwitch == true)
                GetPairs();
        }

        private void GetPairs()
        {
            PairsDG.ItemsSource = null;

            CBSwitch = false;
            Category1CB.ItemsSource = null;
            category1CBList.Clear();
            CBSwitch = true;
            PairsDG.ItemsSource = null;

            var performanceType = PerformanceTypeCB.SelectedItem as CBItems;

            // Находим все пары с выбранным типом выступления
            var query = db.Pairs.Where(u => u.PerformanceType == performanceType.Element).ToList();

            // Поиск всех уникальных возрастных категорий
            List<int> ageCategories = query.Select(x => x.AgeCategoryID).Distinct().ToList();

            // Сортировка по возрастанию
            ageCategories.Sort();

            // Замена ID на название возрастной категории
            var pairs = db.Pairs.ToList();

            foreach (var a in ageCategories)
            {
                var data = db.AgeCategories.Where(u => u.ID == a).FirstOrDefault();
                category1CBList.Add(data);
            }
            Category1CB.ItemsSource = category1CBList.ToList();

            if (category1CBList.Count == 1)
                Category1CB.SelectedIndex = 0;
        }

        // Поиск всех возрастных категорий, которые можно объединить с выбранной. А также поиск пар, подходящих этой группе
        private void Category1CB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CBSwitch == true)
            {
                var selectAgeCategory = (Category1CB.SelectedItem as AgeCategory).Title;
                var performanceType = (PerformanceTypeCB.SelectedItem as CBItems).Element;

                List<AgeCategory> Category2CBItems = new List<AgeCategory>();

                switch (selectAgeCategory) // Проверить, есть ли такие пары на самом  деле
                {
                    case "Дети 0":
                        if (db.Pairs.Where(x => x.AgeCategoryID == 2 && x.PerformanceType == performanceType).FirstOrDefault() != null)
                            Category2CBItems.Add(db.AgeCategories.Where(x => x.Title == "Дети 1").FirstOrDefault());
                        break;
                    case "Дети 1":
                        if (db.Pairs.Where(x => x.AgeCategoryID == 3 && x.PerformanceType == performanceType).FirstOrDefault() != null)
                            Category2CBItems.Add(db.AgeCategories.Where(x => x.Title == "Дети 2").FirstOrDefault());
                        break;
                    case "Юниоры 1":
                        if (db.Pairs.Where(x => x.AgeCategoryID == 5 && x.PerformanceType == performanceType).FirstOrDefault() != null)
                            Category2CBItems.Add(db.AgeCategories.Where(x => x.Title == "Юниоры 2").FirstOrDefault());
                        break;
                    case "Юниоры 2":
                        if (db.Pairs.Where(x => x.AgeCategoryID == 7 && x.PerformanceType == performanceType).FirstOrDefault() != null)
                            Category2CBItems.Add(db.AgeCategories.Where(x => x.Title == "Взрослые").FirstOrDefault());
                        break;
                }
                CBSwitch = false;
                Category2CB.ItemsSource = null;
                Category2CB.ItemsSource = Category2CBItems.ToList();
                CBSwitch = true;

                // Поиск подходящих пар
                var data = db.AgeCategories.Where(u => u.Title == selectAgeCategory).FirstOrDefault();
                pairs = db.Pairs.Where(x => x.PerformanceType == performanceType && x.AgeCategoryID == data.ID).ToList();

                PagesCount();
            }
        }

        private void Category2CB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CBSwitch == true)
            {
                // Поиск подходящих пар (или категория 1, или категория 2)
                CBSwitch = false;
                var category1 = (Category1CB.SelectedItem as AgeCategory).Title;
                var category2 = (Category2CB.SelectedItem as AgeCategory).Title;
                var performanceType = (PerformanceTypeCB.SelectedItem as CBItems).Element;
                CBSwitch = true;

                var data1 = db.AgeCategories.Where(u => u.Title == category1).FirstOrDefault();
                var data2 = db.AgeCategories.Where(u => u.Title == category2).FirstOrDefault();
                pairs = db.Pairs.Where(x => x.PerformanceType == performanceType).ToList();
                pairs = pairs.Where(x => x.AgeCategoryID == data1.ID || x.AgeCategoryID == data2.ID).ToList();

                PagesCount();
            }
        }



        private void DancesChB_Checked(object sender, RoutedEventArgs e)
        {
            Dance row = (Dance)((CheckBox)sender).DataContext;
            selectedDances.Add(new CBItems { Element = row.ID.ToString() });
        }

        private void DancesChB_Unchecked(object sender, RoutedEventArgs e)
        {
            Dance row = (Dance)((CheckBox)sender).DataContext;
            var delete = selectedDances.Where(u => u.Element == row.ID.ToString()).FirstOrDefault();
            selectedPairs.Remove(delete);
        }

        private void PairsChB_Checked(object sender, RoutedEventArgs e)
        {
            Pair row = (Pair)((CheckBox)sender).DataContext;
            selectedPairs.Add(new CBItems { Element = row.ID.ToString() });
        }

        private void PairsChB_Unchecked(object sender, RoutedEventArgs e)
        {
            Pair row = (Pair)((CheckBox)sender).DataContext;
            var delete = selectedPairs.Where(u => u.Element == row.ID.ToString()).FirstOrDefault();
            selectedPairs.Remove(delete);
        }


        

        public bool TrimAndCheckNumber(ref string str)
        {
            str = str.Trim();
            int number;
            if (int.TryParse(str, out number))
                return true;
            else
                return false;
        }

        private void UpdateDataBase()
        {
            try { db.SaveChanges(); }
            catch (Exception ex) { MessageBox.Show(ex.InnerException.Message); }
        }



        // Пагинация

        private int PageIndex = 1;
        private int pageCount;
        private void PaginationButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            switch (button.Name)
            {
                case "BeginningBtn":
                    PageIndex = 1;
                    break;
                case "BackBtn":
                    PageIndex--;
                    break;
                case "NextBtn":
                    PageIndex++;
                    break;
                case "EndBtn":
                    PageIndex = pageCount;
                    break;
            }
            Update();
        }

        private void PagesCount()
        {
            // Узнаём количество страниц
            if (pairs.Count > 5)
            {
                if (pairs.Count % 5 == 0)
                    pageCount = pairs.Count / 5;
                else
                    pageCount = (pairs.Count / 5) + 1;
            }
            else
                pageCount = 1;

            Update();
        }

        private void Update()
        {
            PairsDG.ItemsSource = null;
            PairsDG.Items.Clear();

            if (PageIndex == 1)
            {
                BackBtn.Visibility = Visibility.Hidden;
                BeginningBtn.Visibility = Visibility.Hidden;
                PairsDG.ItemsSource = pairs.Take(5);

                if (pageCount == 1)
                {
                    NextBtn.Visibility = Visibility.Hidden;
                    EndBtn.Visibility = Visibility.Hidden;
                    NumberOfRecords.Content = pairs.Count + " из " + pairs.Count;
                }
                else
                {
                    NextBtn.Visibility = Visibility.Visible;
                    EndBtn.Visibility = Visibility.Visible;
                    NumberOfRecords.Content = 5 + " из " + pairs.Count;
                }
            }
            else
            {
                BackBtn.Visibility = Visibility.Visible;
                BeginningBtn.Visibility = Visibility.Visible;
                NextBtn.Visibility = Visibility.Hidden;
                EndBtn.Visibility = Visibility.Hidden;

                PairsDG.ItemsSource = pairs.Skip((PageIndex - 1) * 5).Take(5);

                // Если открыта последняя страница
                if (PageIndex >= pageCount && pageCount >= 2)
                {
                    PageIndex = pageCount;
                    NumberOfRecords.Content = pairs.Count + " из " + pairs.Count;
                }
                else // Если открыта страница между первой и последней
                    NumberOfRecords.Content = PageIndex * 5 + " из " + pairs.Count;
            }
        }
    }
}
