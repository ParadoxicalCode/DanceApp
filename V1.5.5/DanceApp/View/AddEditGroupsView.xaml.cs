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
        private bool CBSwitch;
        List<Pair> pairs;
        private int PageIndex = 1;
        int pageCount;
        public List<SelectedPairs> selectedPairs = new List<SelectedPairs>();
        public AddEditGroupsView(int id)
        {
            InitializeComponent();
            if (id != 0)
            {
                ID = id;
                var data = db.Dances.Where(u => u.ID == ID).FirstOrDefault();
                if (data.SportsDiscipline != null)
                    SportsDisciplineCB.SelectedItem = data.SportsDiscipline;
            }
            Category1CB.ItemsSource = db.AgeCategories.ToList(); // Здесь должны быть категории, которые есть среди пар

            ButtonSwitch("Назад", true);
            ButtonSwitch("Вперёд", true);
        }

        public class SelectedPairs
        {
            public string ID { get; set; }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void SportsDisciplineCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (SportsDisciplineCB.SelectedItem == SportsDisciplineCB.Items[0])
            {
                var query = from d in db.Dances
                            where d.SportsDiscipline == "Европейская программа"
                            select d;
                DancesDG.ItemsSource = query.ToList();
            }
            else
            {
                var query = from d in db.Dances
                    where d.SportsDiscipline == "Латиноамериканская программа"
                    select d;
                DancesDG.ItemsSource = query.ToList();
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void GroupsDG_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void DancesChB_Checked(object sender, RoutedEventArgs e)
        {
            Dance row = (Dance)((CheckBox)sender).DataContext;
            selectedPairs.Add(new SelectedPairs { ID = row.ID.ToString() });
        }

        private void DancesChB_Unchecked(object sender, RoutedEventArgs e)
        {
            Dance row = (Dance)((CheckBox)sender).DataContext;
            var delete = selectedPairs.Where(u => u.ID == row.ID.ToString()).FirstOrDefault();
            selectedPairs.Remove(delete);
        }

        private void PairsChB_Checked(object sender, RoutedEventArgs e)
        {
            Pair row = (Pair)((CheckBox)sender).DataContext;
            selectedPairs.Add(new SelectedPairs { ID = row.ID.ToString() });
        }

        private void PairsChB_Unchecked(object sender, RoutedEventArgs e)
        {
            Pair row = (Pair)((CheckBox)sender).DataContext;
            var delete = selectedPairs.Where(u => u.ID == row.ID.ToString()).FirstOrDefault();
            selectedPairs.Remove(delete);
        }

        // Поиск всех уникальных возрастных категорий с выбранным типом выступления
        private void PerformanceTypeCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ButtonSwitch("Назад", true);
            ButtonSwitch("Вперёд", true);

            CBSwitch = false;
            Category1CB.ItemsSource = null;
            PairsDG.ItemsSource = null;
            CBSwitch = true;
            var performanceType = ((ComboBoxItem)PerformanceTypeCB.SelectedValue).Content.ToString();

            // Находим все пары с выбранным типом выступления
            var query = from p in db.Pairs
                where p.PerformanceType == performanceType
                select p;

            // Поиск всех уникальных возрастных категорий
            List<int> ageCategories = query.Select(x => x.AgeCategoryID).Distinct().ToList();

            // Сортировка по возрастанию
            ageCategories.Sort();

            // Замена ID на название возрастной категории
            var pairs = db.Pairs.ToList();
            List<string> category1CBItems = new List<string>();

            foreach (var a in ageCategories)
            {
                var data = db.AgeCategories.Where(u => u.ID == a).FirstOrDefault();
                category1CBItems.Add(data.Title);
            }
            Category1CB.ItemsSource = category1CBItems.ToList();
            if (category1CBItems.Count() == 1)
                Category1CB.SelectedItem = Category1CB.Items[0];
        }

        // Поиск всех возрастных категорий, которые можно объединить с выбранной. А также поиск пар, подходящих этой группе
        private void Category1CB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CBSwitch == true)
            {
                PairsDG.ItemsSource = null;
                string selectAgeCategory;
                if (Category1CB.Items.Count == 1)
                    selectAgeCategory = Category1CB.Items[0].ToString();
                else
                    selectAgeCategory = Category1CB.SelectedItem.ToString();

                List<string> Category2CBItems = new List<string>();

                switch (selectAgeCategory)
                {
                    case "Дети 0":
                        Category2CBItems.Add(db.AgeCategories.Where(x => x.Title == "Дети 1").FirstOrDefault().Title);
                        break;
                    case "Дети 1":
                        Category2CBItems.Add(db.AgeCategories.Where(x => x.Title == "Дети 2").FirstOrDefault().Title);
                        break;
                    case "Юниоры 1":
                        Category2CBItems.Add(db.AgeCategories.Where(x => x.Title == "Юниоры 2").FirstOrDefault().Title);
                        break;
                    case "Юниоры 2":
                        Category2CBItems.Add(db.AgeCategories.Where(x => x.Title == "Взрослые").FirstOrDefault().Title);
                        break;
                }
                Category2CB.SelectedItem = Category2CBItems.ToList();

                // Поиск подходящих пар
                string performanceType = ((ComboBoxItem)PerformanceTypeCB.SelectedValue).Content.ToString();
                var data = db.AgeCategories.Where(u => u.Title == selectAgeCategory).FirstOrDefault();
                pairs = db.Pairs.Where(x => x.PerformanceType == performanceType && x.AgeCategoryID == data.ID).ToList();

                PairsDG.ItemsSource = pairs.Take(5);
                if (pairs.Count >= 5)
                    NumberOfRecords.Content = 5 + " из " + pairs.Count;
                else
                    NumberOfRecords.Content = pairs.Count + " из " + pairs.Count;

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

                if (pageCount > 1)
                    ButtonSwitch("Вперёд", false);
            }
        }

        // Пагинация
        private void BeginningBtn_Click(object sender, RoutedEventArgs e)
        {
            ButtonSwitch("Назад", true);
            ButtonSwitch("Вперёд", false);

            PairsDG.ItemsSource = null;
            PairsDG.ItemsSource = pairs.Take(5);
            if (pairs.Count >= 5)
                NumberOfRecords.Content = 5 + " из " + pairs.Count;
            else
                NumberOfRecords.Content = pairs.Count + " из " + pairs.Count;

            PageIndex = 1;
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            ButtonSwitch("Вперёд", false);
            PairsDG.ItemsSource = null;
            PageIndex--;

            if (PageIndex == 1) // Если предыдущая страница первая
            {
                PairsDG.ItemsSource = pairs.Take(5);
                NumberOfRecords.Content = 5 + " из " + pairs.Count;

                ButtonSwitch("Назад", true);
            }
            else
            {
                PairsDG.ItemsSource = pairs.Skip((PageIndex - 1) * 5).Take(5);
                NumberOfRecords.Content = (PageIndex * 5) + " из " + pairs.Count;
            }
        }

        private void NextBtn_Click(object sender, RoutedEventArgs e)
        {
            ButtonSwitch("Назад", false);

            PairsDG.ItemsSource = null;
            PairsDG.ItemsSource = pairs.Skip(PageIndex * 5).Take(5);
            PageIndex++;

            if (PageIndex == pageCount) // Если следующая страница последняя
            {
                NumberOfRecords.Content = pairs.Count + " из " + pairs.Count;
                ButtonSwitch("Вперёд", true);
            }
            else
                NumberOfRecords.Content = (PageIndex * 5) + " из " + pairs.Count;
        }
        
        private void EndBtn_Click(object sender, RoutedEventArgs e)
        {
            ButtonSwitch("Назад", false);
            ButtonSwitch("Вперёд", true);

            PairsDG.ItemsSource = null;
            PairsDG.ItemsSource = pairs.Skip((pageCount - 1) * 5).Take(5);
            NumberOfRecords.Content = pairs.Count + " из " + pairs.Count;

            PageIndex = pageCount;
        }

        private void ButtonSwitch(string buttonType, bool hide)
        {
            if (hide == true)
            {
                if (buttonType == "Назад")
                {
                    BackBtn.Visibility = Visibility.Hidden;
                    BeginningBtn.Visibility = Visibility.Hidden;
                }
                else
                {
                    NextBtn.Visibility = Visibility.Hidden;
                    EndBtn.Visibility = Visibility.Hidden;
                }
            }
            else
            {
                if (buttonType == "Назад")
                {
                    BackBtn.Visibility = Visibility.Visible;
                    BeginningBtn.Visibility = Visibility.Visible;
                }
                else
                {
                    NextBtn.Visibility = Visibility.Visible;
                    EndBtn.Visibility = Visibility.Visible;
                }
            }
        }
    }
}
