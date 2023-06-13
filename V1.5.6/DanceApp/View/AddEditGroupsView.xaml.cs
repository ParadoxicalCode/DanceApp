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
using System.Windows.Controls.Primitives;
using DanceApp.Model.Skating;
using static DanceApp.View.AddEditGroupsView;
using System.Diagnostics;
using DanceApp.Model.Groups;

#nullable disable
namespace DanceApp.View
{
    /// <summary>
    /// Логика взаимодействия для AddEditGroupsView.xaml
    /// </summary>
    public partial class AddEditGroupsView : Window
    {
        public DataBaseContext db = GlobalClass.db;

        public List<CBItems> performanceTypeList = new List<CBItems>();
        public List<CBItems> sportsDisciplineList = new List<CBItems>();

        public List<ClassDances> selectedDances = new List<ClassDances>();
        public List<ClassPairs> selectedPairs = new List<ClassPairs>();
        public List<Pair> freePairs = new List<Pair>();

        private bool CBSwitch = true;
        public int GroupID;
        public int TourID;
        public AddEditGroupsView(int tourID, int groupID)
        {
            InitializeComponent();
            AddItemsToComboBox();

            freePairs = new GetPairs().Free(TourID);

            TourID = tourID;
            GroupID = groupID;
            if (GroupID == 0)
            {
                DefaultValues();
            }  
            else
                LoadData();
        }

        public class CBItems
        {
            public string Element { get; set; }
        }





        private void DefaultValues()
        {
            NumberTB.Text = "";
            CBSwitch = false;
            Category1CB.ItemsSource = null;
            Category2CB.ItemsSource = null;
            CBSwitch = true;

            selectedDances.Clear();
            selectedPairs.Clear();
            freePairs = new GetPairs().Free(TourID);

            SportsDisciplineCB.SelectedIndex = 0;
            DancesDG.ItemsSource = new GetDances().Add(0);

            PerformanceTypeCB.SelectedIndex = 0;
            GetCategory1();

            Category1CB.ItemsSource = new GetCategories().Get1(GroupID, "Пара", freePairs);

            if (Category1CB.Items.Count >= 1)
                Category1CB.SelectedIndex = 0;
        }

        private void LoadData()
        {
            // Загрузка данных в выпадающие списки
            var group = db.Group.Find(GroupID);

            SportsDisciplineCB.SelectedValue = group.SportsDiscipline;
            PerformanceTypeCB.SelectedValue = group.PerformanceType;

            Category1CB.ItemsSource = new GetCategories().Get1(group.ID, group.PerformanceType, freePairs);
            Category1CB.SelectedValue = group.AgeCategory1;

            if (group.AgeCategory2 != null && group.AgeCategory2 != "")
            {
                Category2CB.ItemsSource = new GetCategories().Get2(group.PerformanceType, group.AgeCategory1);
                Category2CB.SelectedValue = group.AgeCategory2;
            }

            // Загрузка данных в таблицы
            var performanceType = (PerformanceTypeCB.SelectedItem as CBItems).Element;
            var ageCategory1 = (Category1CB.SelectedItem as AgeCategory).ID;

            int ageCategory2;
            if (Category2CB.SelectedItem != null)
                ageCategory2 = (Category2CB.SelectedItem as AgeCategory).ID;
            else
                ageCategory2 = 0;

            DancesDG.ItemsSource = new GetDances().Load(group.ID);
            PairsDG.ItemsSource = null;
            PairsDG.Items.Clear();
            PairsDG.ItemsSource = new GetPairs().Load(group.ID, performanceType, ageCategory1, ageCategory2);
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

        private void GetCategory1()
        {
            CBSwitch = false;
            Category1CB.ItemsSource = null;
            Category2CB.ItemsSource = null;
            PairsDG.ItemsSource = null;
            CBSwitch = true;

            var performanceType = (PerformanceTypeCB.SelectedItem as CBItems).Element;

            Category1CB.ItemsSource = new GetCategories().Get1(GroupID, performanceType, freePairs);

            if (Category1CB.Items.Count >= 1)
                Category1CB.SelectedIndex = 0;
        }





        private void Save_Click(object sender, RoutedEventArgs e)
        {
            // Валидация
            if (selectedPairs.Count < 1 || selectedDances.Count < 1 || PerformanceTypeCB.SelectedItem == null ||
                Category1CB.SelectedItem == null || SportsDisciplineCB.SelectedItem == null)
            {
                MessageBox.Show("Не все поля заполнены!");
                return;
            }

            string number = NumberTB.Text;
            var x = new GlobalClass();
            if (x.TrimAndCheckNumber(ref number) == false && number != "")
            {
                MessageBox.Show("В поле номер обязательно должно быть число!");
                return;
            }

            var sportsDiscipline = (SportsDisciplineCB.SelectedItem as CBItems).Element;
            var performanceType = (PerformanceTypeCB.SelectedItem as CBItems).Element;
            var ageCategory1 = (Category1CB.SelectedItem as AgeCategory).Title;

            string ageCategory2;
            if (Category2CB.SelectedItem != null)
                ageCategory2 = (Category2CB.SelectedItem as AgeCategory).Title;
            else
                ageCategory2 = "";

            // Добавление/изменение данных группы
            var newGroupID = new SaveData().Save(TourID, GroupID, number, sportsDiscipline, performanceType, ageCategory1, ageCategory2, selectedDances, selectedPairs);

            if (newGroupID != 0)
            {
                // Распределение пар по заходам
                new Model.Performances.PairsToPerformances().Distribution(newGroupID, selectedDances, selectedPairs);

                if (GroupID == 0)
                    DefaultValues();
                else
                    this.Close();
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        




        // Поиск танцев с выбранной спортивной дисциплиной
        private void SportsDisciplineCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DancesDG.ItemsSource = new GetDances().Add(SportsDisciplineCB.SelectedIndex);
        }

        // Поиск всех уникальных возрастных категорий с выбранным типом выступления
        private void PerformanceTypeCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            GetCategory1();
        }

        // Поиск всех возрастных категорий, которые можно объединить с выбранной. А также поиск пар, подходящих этой группе
        private void Category1CB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CBSwitch == true)
            {
                Category2CB.ItemsSource = null;

                var performanceType = (PerformanceTypeCB.SelectedItem as CBItems).Element;
                var ageCategory1 = Category1CB.SelectedItem as AgeCategory;

                Category2CB.ItemsSource = new GetCategories().Get2(performanceType, ageCategory1.Title);

                PairsDG.ItemsSource = new GetPairs().Get(TourID, performanceType, ageCategory1.ID, 0);
            }
        }

        private void Category2CB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CBSwitch == true)
            {
                var performanceType = (PerformanceTypeCB.SelectedItem as CBItems).Element;
                var ageCategory1 = (Category2CB.SelectedItem as AgeCategory).ID;
                var ageCategory2 = (Category2CB.SelectedItem as AgeCategory).ID;

                PairsDG.ItemsSource = new GetPairs().Get(TourID, performanceType, ageCategory1, ageCategory2);
            }
        }





        private void DancesChB_Checked(object sender, RoutedEventArgs e)
        {
            ClassDances row = (ClassDances)((CheckBox)sender).DataContext;
            selectedDances.Add(new ClassDances
            {
                ID = row.ID,
                Title = row.Title,
                ShortName = row.ShortName,
                Select = true
            }); 
        }

        private void DancesChB_Unchecked(object sender, RoutedEventArgs e)
        {
            ClassDances row = (ClassDances)((CheckBox)sender).DataContext;
            var delete = selectedDances.Where(u => u.ID == row.ID).FirstOrDefault();
            selectedDances.Remove(delete);
        }

        private void PairsChB_Checked(object sender, RoutedEventArgs e)
        {
            ClassPairs row = (ClassPairs)((CheckBox)sender).DataContext;
            selectedPairs.Add(new ClassPairs
            {
                ID = row.ID,
                Number = row.Number,
                MaleSurname = row.MaleSurname,
                MaleName = row.MaleName,
                MalePatronymic = row.MalePatronymic,
                MaleBirthday = row.MaleBirthday,
                FemaleSurname = row.FemaleSurname,
                FemaleName = row.FemaleName,
                FemalePatronymic = row.FemalePatronymic,
                FemaleBirthday = row.FemaleBirthday,
                Select = true
            });
        }

        private void PairsChB_Unchecked(object sender, RoutedEventArgs e)
        {
            ClassPairs row = (ClassPairs)((CheckBox)sender).DataContext;
            var delete = selectedPairs.Where(u => u.ID == row.ID).FirstOrDefault();
            selectedPairs.Remove(delete);
        }
    }
}
