using DanceApp.Model;
using DanceApp.Model.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
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
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System.Text.RegularExpressions;

#nullable disable
namespace DanceApp.View
{
    /// <summary>
    /// Логика взаимодействия для AddEditGroupsView.xaml
    /// </summary>
    public partial class AddEditGroupsView : Window
    {
        public DataBaseContext db = GlobalClass.db;
        public int ID;
        private bool CBSwitch = false;

        public List<CBItems> performanceTypeList = new List<CBItems>();
        public List<CBItems> sportsDisciplineList = new List<CBItems>();

        public List<CBItems> selectedPairs = new List<CBItems>();
        public List<dancesDGClass> selectedDances = new List<dancesDGClass>();
        public List<Pair> freePairs = new List<Pair>();
        public AddEditGroupsView(int id)
        {
            InitializeComponent();
            AddItemsToComboBox();
            GetFreePairs();

            ID = id;
            if (id == 0)
                AddData();
            else
                LoadData();

            CBSwitch = true;
        }

        public class CBItems
        {
            public string Element { get; set; }
        }

        public class dancesDGClass
        {
            public int ID { get; set; }
            public string Title { get; set; }
            public string ShortName { get; set; }
            public bool Select { get; set; }
        }

        public class pairsDGClass
        {
            public int ID { get; set; }
            public string Number { get; set; }
            public string MaleSurname { get; set; }
            public string MaleName { get; set; }
            public string MalePatronymic { get; set; }
            public string MaleBirthday { get; set; }
            public string FemaleSurname { get; set; }
            public string FemaleName { get; set; }
            public string FemalePatronymic { get; set; }
            public string FemaleBirthday { get; set; }
            public bool Select { get; set; }
        }

        private void AddItemsToComboBox()
        {
            sportsDisciplineList.Add(new CBItems { Element = "Европейская программа" });
            sportsDisciplineList.Add(new CBItems { Element = "Латиноамериканская программа" });
            SportsDisciplineCB.ItemsSource = sportsDisciplineList;

            performanceTypeList.Add(new CBItems { Element = "Пара" });
            performanceTypeList.Add(new CBItems { Element = "Соло" });
            PerformanceTypeCB.ItemsSource = performanceTypeList;
        }

        private void GetFreePairs()
        {
            // Получаем ID нераспределённых пар
            var query = db.PairsInTour
                .Where(x => x.Select == false)
                .Select(x => x.PairID)
                .ToList();

            // Поиск полученных ID в таблице Pairs
            int i = 0;
            foreach (var p in query)
            {
                var pair = db.Pairs.Where(u => u.ID == query[i]).FirstOrDefault();
                freePairs.Add(pair);
                i++;
            }
        }

        // Сброс параметров в исходное состояние
        private void AddData()
        {
            NumberTB.Text = "";
            Category2CB.ItemsSource = null;
            Category1CB.ItemsSource = null;

            SportsDisciplineCB.SelectedIndex = 0;
            GetDances();
            
            PerformanceTypeCB.SelectedIndex = 0;

            GetCategories1();
            GetCategories2();
        }

        // Поиск подходящих пар. Привязка данных к PairsDG
        private void GetPairs()
        {
            var selectAgeCategory = (Category1CB.SelectedItem as AgeCategory).Title;
            var performanceType = (PerformanceTypeCB.SelectedItem as CBItems).Element;

            var data = db.AgeCategories.Where(u => u.Title == selectAgeCategory).FirstOrDefault();
            PairsDG.ItemsSource = db.Pairs.Where(x => x.PerformanceType == performanceType && x.AgeCategoryID == data.ID).ToList();
        }

        private void LoadData()
        {
            // Загрузка данных в выпадающие списки
            var group = db.Groups.Where(u => u.ID == ID).FirstOrDefault();
            SportsDisciplineCB.SelectedValue = group.SportsDiscipline;

            PerformanceTypeCB.SelectedValue = group.PerformanceType;

            GetCategories1();
            Category1CB.SelectedValue = group.AgeCategory1;

            if (group.AgeCategory2 != null)
            {
                GetCategories2();
                Category2CB.SelectedValue = group.AgeCategory2;
            }

            // Привязка данных к DancesDG
            List<dancesDGClass> DancesDGItems = new List<dancesDGClass>();

            foreach (var d in db.DancesInGroup)
            {
                var dance = db.Dances.Where(u => u.ID == d.ID).FirstOrDefault();
                var dances = new dancesDGClass();
                dances.ID = dance.ID;
                dances.Title = dance.Title;
                dances.ShortName = dance.ShortName;
                dances.Select = d.Select;
                
                DancesDGItems.Add(dances);
            }
            DancesDG.ItemsSource = DancesDGItems.ToList();








            


            // Объединить список нераспределённых пар и список отмеченных пар, а затем загрузить полученные
            // данные в PairsDG

            // Нужно создать новый список и плюс добавить туда Select,

            /*
            List<pairsDGClass> pairsDGItems = new List<pairsDGClass>();
            int i = 0;
            foreach (var p in freePairs)
            {
                var query = freePairs.Select(x => x.ID).ToList();
                var data = db.PairsInGroup.Where(u => u.PairID == query[i]).FirstOrDefault();

                pairsDGItems.Add(new pairsDGClass
                {
                    ID = p.ID,
                    Number = p.Number,
                    MaleSurname = p.MaleSurname,
                    MaleName = p.MaleName,
                    MalePatronymic = p.MalePatronymic,
                    MaleBirthday = p.MaleBirthday,
                    FemaleSurname = p.FemaleSurname,
                    FemaleName = p.FemaleName,
                    FemalePatronymic = p.FemalePatronymic,
                    FemaleBirthday = p.FemaleBirthday,
                    Select = data.Select
                });
                i++;
            }
            */
        }

        private void GetDances()
        {
            List<Dance> dances = new List<Dance>();
            if (SportsDisciplineCB.SelectedIndex == 0)
            {
                dances = db.Dances.Where(u => u.SportsDiscipline == "Европейская программа").ToList();
            }
            else
            {
                dances = db.Dances.Where(u => u.SportsDiscipline == "Латиноамериканская программа").ToList();
            }

            // Привязка данных к DancesDG
            List<dancesDGClass> DancesDGItems = new List<dancesDGClass>();

            foreach (var d in dances)
            {
                var dance = new dancesDGClass();
                dance.ID = d.ID;
                dance.Title = d.Title;
                dance.ShortName = d.ShortName;
                dance.Select = false;

                DancesDGItems.Add(dance);
            }
            DancesDG.ItemsSource = DancesDGItems.ToList();
        }

        // Поиск категорий с выбранным типом выступления. Привязка данных к Category1CB
        private void GetCategories1()
        {
            List<AgeCategory> Category2CBItems = new List<AgeCategory>();
            var performanceType = PerformanceTypeCB.SelectedItem as CBItems;
            var query = freePairs.Where(u => u.PerformanceType == performanceType.Element).ToList();

            // Поиск всех уникальных возрастных категорий
            List<int> ageCategories = query.Select(x => x.AgeCategoryID).Distinct().ToList();

            // Сортировка по возрастанию
            ageCategories.Sort();

            // Замена ID на название возрастной категории
            var pairs = db.Pairs.ToList();

            foreach (var a in ageCategories)
            {
                var data = db.AgeCategories.Where(u => u.ID == a).FirstOrDefault();
                Category2CBItems.Add(data);
            }
            Category1CB.ItemsSource = Category2CBItems;

            if (Category1CB.Items.Count >= 1)
                Category1CB.SelectedIndex = 0;

            GetPairs();
        }

        // Поиск категорий, которые совместимы с первой выбранной категорией. Привязка данных к Category2CB
        private void GetCategories2()
        {
            var selectAgeCategory = (Category1CB.SelectedItem as AgeCategory).Title;
            var performanceType = (PerformanceTypeCB.SelectedItem as CBItems).Element;

            List<AgeCategory> Category2CBItems = new List<AgeCategory>();

            switch (selectAgeCategory) // Проверить, есть ли такие пары на самом деле
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
            Category2CB.ItemsSource = Category2CBItems.ToList();
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



        private void Save_Click(object sender, RoutedEventArgs e)
        {
            // Для сохранения надо сделать запись в таблицы DancesInGroup и PairsInGroup, а также записать состояние выпадающих списков и создать саму группу
            CBSwitch = false;

            // Валидация
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


            // Данные группы
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
                // Валидация номера
                bool checkIsExist = db.Groups.Any(x => x.Number == number);
                if (checkIsExist == true && number != "")
                {
                    MessageBox.Show("Пара с таким номером уже есть!");
                    CBSwitch = true;
                    return;
                }

                // Проверка на существование группы
                if (db.Groups.Where(u => u.Title == title).FirstOrDefault() != null)
                {
                    MessageBox.Show("Группа с выбранными параметрами уже есть!");
                    CBSwitch = true;
                    return;
                }

                var group = new Model.Data.Group();
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
                }
                catch (Exception ex) { MessageBox.Show(ex.InnerException.Message); }

                var groupID = db.Groups.Where(u => u.Title == title).FirstOrDefault();
                if (groupID != null)
                {
                    // Удаление данных о выбранных танцах
                    var deleteDances = db.DancesInGroup.Where(x => x.GroupID == groupID.ID).ToList();
                    db.DancesInGroup.RemoveRange(deleteDances);

                    // Удаление данных о выбранных парах
                    var deletePairs = db.PairsInGroup.Where(x => x.GroupID == groupID.ID).ToList();
                    db.PairsInGroup.RemoveRange(deletePairs);

                    UpdateDataBase();

                    // Сохранение выбранных танцев в базу данных
                    var selectDance = selectedDances.Select(x => x.ID).ToList();

                    var allDances = db.Dances.Where(u => u.SportsDiscipline == sportsDiscipline).ToList();
                    foreach (var d in allDances)
                    {
                        bool select = false;
                        for (int i = 0; i < selectDance.Count; i++)
                        {
                            if (d.ID == selectDance[i])
                                select = true;
                        }

                        var dancesInGroup = new DancesInGroup();
                        dancesInGroup.GroupID = groupID.ID;
                        dancesInGroup.DanceID = d.ID;
                        dancesInGroup.Select = select;

                        db.DancesInGroup.Add(dancesInGroup);
                        UpdateDataBase();
                    }
                    AddData();
                    GetPairs();
                }
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
            CBSwitch = true;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        


        // Поиск танцев с выбранной спортивной дисциплиной
        private void SportsDisciplineCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CBSwitch == true)
                GetDances();
        }

        // Поиск всех уникальных возрастных категорий с выбранным типом выступления
        private void PerformanceTypeCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CBSwitch == true)
                GetCategories1();
        }

        // Поиск всех возрастных категорий, которые можно объединить с выбранной. А также поиск пар, подходящих этой группе
        private void Category1CB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CBSwitch == true)
                GetCategories2();
        }

        private void Category2CB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CBSwitch == true)
            {
                GetCategories2();
                GetPairs();
            }  
        }



        private void DancesChB_Checked(object sender, RoutedEventArgs e)
        {
            dancesDGClass row = (dancesDGClass)((CheckBox)sender).DataContext;
            selectedDances.Add(new dancesDGClass
            {
                ID = row.ID,
                Title = row.Title,
                ShortName = row.ShortName,
                Select = true
            }); 
        }

        private void DancesChB_Unchecked(object sender, RoutedEventArgs e)
        {
            dancesDGClass row = (dancesDGClass)((CheckBox)sender).DataContext;
            var delete = selectedDances.Where(u => u.ID == row.ID).FirstOrDefault();
            selectedDances.Remove(delete);
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
    }
}
