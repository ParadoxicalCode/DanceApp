using DanceApp.Model;
using DanceApp.Model.Data;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DanceApp.View
{
    /// <summary>
    /// Логика взаимодействия для PlacesView.xaml
    /// </summary>
    public partial class PlacesView : Page
    {
        public DataBaseContext db = GlobalClass.db;
        TextBlock[] PlacesMatrix;
        TextBox[,] LeftMatrix;
        TextBlock[,] RightMatrix;

        public int pairsCount = GlobalClass.NumberOfPairs;
        public int judgesCount = GlobalClass.NumberOfJudges;
        public int[,] LeftMatrix2 = new int[GlobalClass.NumberOfPairs, GlobalClass.NumberOfJudges];
        public int[,] RightMatrix2 = new int[GlobalClass.NumberOfPairs, GlobalClass.NumberOfPairs];

        public PlacesView()
        {
            InitializeComponent();
            TourCB.ItemsSource = db.Tours.ToList();
            GroupCB.ItemsSource = db.Groups.ToList();
            DanceCB.ItemsSource = db.Dances.ToList();

            LeftMatrix = new TextBox[7, 10]{
                { TB01, TB02, TB03, TB04, TB05, TB06, TB07, TB08, TB09, TB10 },
                { TB11, TB12, TB13, TB14, TB15, TB16, TB17, TB18, TB19, TB20 },
                { TB21, TB22, TB23, TB24, TB25, TB26, TB27, TB28, TB29, TB30 },
                { TB31, TB32, TB33, TB34, TB35, TB36, TB37, TB38, TB39, TB40 },
                { TB41, TB42, TB43, TB44, TB45, TB46, TB47, TB48, TB49, TB50 },
                { TB51, TB52, TB53, TB54, TB55, TB56, TB57, TB58, TB59, TB60 },
                { TB61, TB62, TB63, TB64, TB65, TB66, TB67, TB68, TB69, TB70 }};

            RightMatrix = new TextBlock[10, 10]{
                { TBK001, TBK011, TBK021, TBK031, TBK041, TBK051, TBK061, TBK071, TBK081, TBK091 },
                { TBK002, TBK012, TBK022, TBK032, TBK042, TBK052, TBK062, TBK072, TBK082, TBK092 },
                { TBK003, TBK013, TBK023, TBK033, TBK043, TBK053, TBK063, TBK073, TBK083, TBK093 },
                { TBK004, TBK014, TBK024, TBK034, TBK044, TBK054, TBK064, TBK074, TBK084, TBK094 },
                { TBK005, TBK015, TBK025, TBK035, TBK045, TBK055, TBK065, TBK075, TBK085, TBK095 },
                { TBK006, TBK016, TBK026, TBK036, TBK046, TBK056, TBK066, TBK076, TBK086, TBK096 },
                { TBK007, TBK017, TBK027, TBK037, TBK047, TBK057, TBK067, TBK077, TBK087, TBK097 },
                { TBK008, TBK018, TBK028, TBK038, TBK048, TBK058, TBK068, TBK078, TBK088, TBK098 },
                { TBK009, TBK019, TBK029, TBK039, TBK049, TBK059, TBK069, TBK079, TBK089, TBK099 },
                { TBK010, TBK020, TBK030, TBK040, TBK050, TBK060, TBK070, TBK080, TBK090, TBK100 }};

            PlacesMatrix = new TextBlock[10]{ p01, p02, p03, p04, p05, p06, p07, p08, p09, p10 };

            // Сокрытие TextBox'ов в левой части матрицы.
            for (int i = 0; i < 10; i++)
            {
                int j = (i < pairsCount ? judgesCount : 0);
                for (; j < 7; j++)
                {
                    LeftMatrix[j, i].IsReadOnly = true;
                    LeftMatrix[j, i].Background = Brushes.LightGray;
                }
            }
        }

        private void Calculate_Click(object sender, RoutedEventArgs e)
        {
            Calculate();
        }

        private void Calculate()
        {
            if (Validation() == true)
            {
                CopyLeftMatrix();

                // Расчёты и вывод результатов в GUI.
                Output();
            }
        }

        private void CopyLeftMatrix()
        {
            for (int i = 0; i < pairsCount; i++)
            {
                for (int j = 0; j < judgesCount; j++)
                {
                    LeftMatrix2[i, j] = Int32.Parse(LeftMatrix[j, i].Text);
                }
            }
        }

        private void Output()
        {
            Model.Skating.DistributionOfPlaces dop = new Model.Skating.DistributionOfPlaces();
            string[,] result = dop.Distribution(LeftMatrix2, pairsCount, judgesCount);

            for (int i = 0; i < pairsCount; i++)
            {
                for (int j = 0; j < pairsCount; j++)
                {
                    RightMatrix[i, j].Text = result[i, j];
                }

                PlacesMatrix[i].Text =  result[i, pairsCount];
            }
        }

        private void Random_Click(object sender, RoutedEventArgs e)
        {
            Clear(); 
            Random r = new Random();
            for (int i = 0; i < judgesCount; i++)
            {
                List<int> list = Enumerable.Range(1, pairsCount).OrderBy(x => r.Next()).ToList();
                for (int j = 0; j < pairsCount; j++)
                {
                    LeftMatrix[i, j].Text = list[j].ToString();
                }
            }
            Calculate();
        }

        private bool Validation()
        {
            // Проверка на корректность.
            bool stop = false;
            for (int j = 0; j < judgesCount && stop == false; j++)
            {
                for (int i = 0; i < pairsCount; i++)
                {
                    LeftMatrix[j, i].Background = Brushes.White;
                    if (!Regex.IsMatch(LeftMatrix[j, i].Text, @"^[1-9][0-9]*$") ||
                        Int32.Parse(LeftMatrix[j, i].Text) > pairsCount)
                    {
                        LeftMatrix[j, i].Background = Brushes.Red;
                        MessageBox.Show("Введены некорректные данные либо ячейка пустая!");
                        stop = true; break;
                    }
                }
            }

            // Проверка распределения мест.
            for (int j = 0; j < judgesCount && stop == false; j++)
            {
                for (int i = 0; i < pairsCount && stop == false && i < (pairsCount - 1); i++)
                {
                    LeftMatrix[j, i].Background = Brushes.White;

                    for (int x = i + 1; x < pairsCount && i < (pairsCount - 1); x++)
                    {
                        if (LeftMatrix[j, i].Text == LeftMatrix[j, x].Text)
                        {
                            LeftMatrix[j, i].Background = Brushes.Red;
                            LeftMatrix[j, x].Background = Brushes.Red;
                            stop = true; break;
                        }
                    }
                }
                if (stop == true) { MessageBox.Show("Места должны быть разными!"); }
            }
            if (stop == true) { return false; } else { return true; }
        }

        private void Clear()
        {
            for (int i = 0; i < pairsCount; i++)
            {
                for (int j = 0; j < judgesCount; j++)
                {
                    LeftMatrix[j, i].Text = "";
                    LeftMatrix[j, i].Background = Brushes.White;
                }

                for (int j = 0; j < pairsCount; j++)
                {
                    RightMatrix[i, j].Text = "";
                }

                PlacesMatrix[i].Text = "";
            }
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            Clear();
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {

        }

        // При переходе на данную страницу раскидываем все пары по заходам
        public void pairsToPerformances()
        {
            /*
            var data = db.Competitions.Where(u => u.ID == 1).FirstOrDefault();
            int siteCapacity = data.SiteCapacity;
            var groups = db.Groups.ToList();
            int m = 0;

            // Перебираем все группы
            foreach (var g in groups)
            {
                var query = from p in db.Pairs
                            where p.GroupID == g.ID
                            select p;

                // Выписываем в массив ID всех пар, состоящих в текущей группе
                double divideResult = Math.Ceiling(Convert.ToDouble(g.PairsCount) / Convert.ToDouble(siteCapacity));
                int performanceCount = Convert.ToInt32(divideResult);
                int[,] pairsInPerformance = new int[performanceCount, siteCapacity];
                int[] randomizedPairs = new int[g.PairsCount];

                int k = 0;
                foreach (var p in query)
                {
                    randomizedPairs[k] = p.ID;
                    k++;
                }

                // Перемешиваем ID пар в массиве
                Random rnd = new Random();
                for (int i = 0; i < randomizedPairs.Length; i++)
                {
                    int j = rnd.Next(randomizedPairs.Length);
                    int temp = randomizedPairs[i];
                    randomizedPairs[i] = randomizedPairs[j];
                    randomizedPairs[j] = temp;
                }

                // Равномерно делим пары по заходам
                for (int i = 0; i < performanceCount; i++)
                {
                    for (int j = 0; j < g.PairsCount; i++)
                    {
                        pairsInPerformance[i, j] = randomizedPairs[j];
                    }
                }

                // Создаём заход и переносим ID пар из массива в базу данных
                for (int y = 0; y < performanceCount; y++)
                {

                }
                m++;
            }
            */
        }
    }
}