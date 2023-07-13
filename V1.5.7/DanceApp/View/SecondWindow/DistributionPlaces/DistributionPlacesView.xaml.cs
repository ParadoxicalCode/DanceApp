using DanceApp.Model.Data;
using DanceApp.Model;
using DanceApp.View;
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
using System.Windows.Shapes;
using DanceApp.Model.Groups;
using System.Collections.Immutable;
using iText.IO.Image;
using iText.Kernel.Colors;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Action;
using iText.Kernel.Pdf.Canvas.Draw;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using Microsoft.Win32;
using iText.Kernel.Font;
using static Org.BouncyCastle.Utilities.Test.FixedSecureRandom;

#nullable disable
namespace DanceApp.View
{
    /// <summary>
    /// Логика взаимодействия для DistributionPlacesView.xaml
    /// </summary>
    public partial class DistributionPlacesView : Window
    {
        public DataBaseContext db = GlobalClass.db;
        TextBlock[] PairsMatrix;
        TextBlock[] JudgesMatrix;
        TextBlock[] PlacesMatrix;
        TextBox[,] LeftMatrix;
        TextBlock[,] RightMatrix;

        List<Judge> SelectedJudges = new List<Judge>();
        List<Pair> SelectedPairs = new List<Pair>();

        public int pairsCount;
        public int judgesCount;
        int GroupID;
        int PerformanceNumber;
        int DanceID;

        string[] Places = new string[10];
        public int[,] LeftMatrix2;
        public int[,] RightMatrix2;

        public DistributionPlacesView(bool performanceStatus, int roundID, int groupID, int danceID, int performanceNumber, List<Judge> selectedJudges, List<Pair> selectedPairs)
        {
            InitializeComponent();

            GroupID = groupID;
            PerformanceNumber = performanceNumber;
            DanceID = danceID;
            int PerformanceID = db.Performance.Where(x => x.GroupID == GroupID && x.Number == PerformanceNumber).FirstOrDefault().ID;

            if (performanceStatus == true)
            {
                CalculateBtn.IsEnabled = false;
                RandomBtn.IsEnabled = false;
                Document1Text.IsEnabled = false;

                var judges = db.JudgesInDance.Where(x => x.PerformanceID == PerformanceID && x.DanceID == DanceID).Select(x => x.JudgeID).ToList();
                foreach (var j in judges)
                {
                    var judge = db.Judge.Find(j);
                    SelectedJudges.Add(judge);
                }
                judgesCount = judges.Count;

                
                var pairs = db.PairsInDance.Where(x => x.PerformanceID == PerformanceID && x.DanceID == DanceID).Select(x => x.PairID).ToList();
                foreach (var p in pairs)
                {
                    SelectedPairs.Add(db.Pair.Find(p));
                }
                pairsCount = pairs.Count;
            }
            else
            {
                pairsCount = selectedPairs.Count;
                judgesCount = selectedJudges.Count;

                SelectedJudges = selectedJudges;
                SelectedPairs = selectedPairs;
            }

            LeftMatrix2 = new int[pairsCount, judgesCount];
            RightMatrix2 = new int[pairsCount, pairsCount];

            RoundText.Text = db.Round.Find(roundID).Title;
            GroupText.Text = db.Group.Find(groupID).Title;
            DanceText.Text = db.Dance.Find(danceID).Title;
            PerformanceText.Text = performanceNumber.ToString();

            PairsMatrix = new TextBlock[10]{ Pair1Text, Pair2Text, Pair3Text, Pair4Text, Pair5Text, Pair6Text, Pair7Text, Pair8Text, Pair9Text, Pair10Text };
            for (int i = 0; i < pairsCount; i++)
            {
                PairsMatrix[i].Text = db.Pair.Find(SelectedPairs[i].ID).Number;
            }

            JudgesMatrix = new TextBlock[7] { Judge1Text, Judge2Text, Judge3Text, Judge4Text, Judge5Text, Judge6Text, Judge7Text };
            for (int i = 0; i < judgesCount; i++)
            {
                JudgesMatrix[i].Text = db.Judge.Find(SelectedJudges[i].ID).Character.ToString();
            }

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

            PlacesMatrix = new TextBlock[10] { p01, p02, p03, p04, p05, p06, p07, p08, p09, p10 };

            // Сокрытие выпадающих списков в левой части матрицы.
            for (int i = 0; i < 10; i++)
            {
                int j = (i < pairsCount ? judgesCount : 0);
                for (; j < 7; j++)
                {
                    LeftMatrix[j, i].IsReadOnly = true;
                    LeftMatrix[j, i].Background = Brushes.LightGray;
                }
            }

            if (performanceStatus == true)
            {
                var PairsInDance = db.PairsInDance.Where(x => x.PerformanceID == PerformanceID).ToList();
                Load(PerformanceID);
            }
        }

        private void Load(int performanceID)
        {
            // Загрузить оценки судей из базы данных и произвести расчёты
            for (int i = 0; i < pairsCount; i++)
            {
                for (int j = 0; j < judgesCount; j++)
                {
                    // В запросе должен быть список судей с индексом j
                    var value = db.JudgesAssesment.Where(x => x.PerformanceID == performanceID && x.DanceID == DanceID && 
                        x.JudgeID == SelectedJudges[j].ID && x.PairID == SelectedPairs[i].ID).FirstOrDefault().Value;
                    LeftMatrix[j, i].Text = value;
                }
            }
            Calculate();
        }

        private void Save()
        {
            int performanceID = db.Performance.Where(x => x.GroupID == GroupID && x.Number == PerformanceNumber).FirstOrDefault().ID;
            
            for (int i = 0; i < pairsCount; i++)
            {
                // Сохранение оценок судей
                for (int j = 0; j < judgesCount; j++)
                {
                    JudgesAssesment a = new JudgesAssesment();
                    a.PerformanceID = performanceID;
                    a.DanceID = DanceID;
                    a.JudgeID = SelectedJudges[j].ID;
                    a.PairID = SelectedPairs[i].ID;
                    a.Value = LeftMatrix[j, i].Text;

                    db.JudgesAssesment.Add(a);
                }

                // Сохранение результатов танца
                IntermediateResult ir = new IntermediateResult();
                ir.PerformanceID = performanceID;
                ir.DanceID = DanceID;
                ir.PairID = SelectedPairs[i].ID;
                ir.Value = Places[i];

                db.IntermediateResult.Add(ir);
            }

            // Сохранение судей
            foreach (var j in SelectedJudges)
            {
                var judge = new JudgesInDance();
                judge.PerformanceID = performanceID;
                judge.DanceID = DanceID;
                judge.JudgeID = j.ID;

                db.JudgesInDance.Add(judge);

                try { db.SaveChanges(); }
                catch (Exception ex) { MessageBox.Show(ex.InnerException.Message); }
            }

            // Сохранение пар
            foreach (var p in SelectedPairs)
            {
                var pair = new PairsInDance();
                pair.PerformanceID = performanceID;
                pair.DanceID = DanceID;
                pair.PairID = p.ID;

                db.PairsInDance.Add(pair);

                try { db.SaveChanges(); }
                catch (Exception ex) { MessageBox.Show(ex.InnerException.Message); }
            }
        }

        private void Calculate_Click(object sender, RoutedEventArgs e)
        {
            bool block = Calculate();
            if (block == true)
            {
                Save();
                //this.Close();
            }
        }

        private bool Calculate()
        {
            if (Validation() == true)
            {
                CopyLeftMatrix();

                // Расчёты и вывод результатов в GUI.
                Output();
                return true;
            }
            return false;
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
                PlacesMatrix[i].Text = result[i, pairsCount];
                Places[i] = result[i, pairsCount];
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

        private string FractionToText(string Round)
        {
            switch (Round)
            {
                case "1/8":
                    return "одна восьмая финала";
                case "1/4":
                    return "четверть финала";
            }
            return Round;
        }

        private void Document1_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog
            {
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                Title = "Укажите путь для сохранения PDF файла",
                Filter = "Все папки (*.*)|*.*",
                CheckFileExists = false,
                CheckPathExists = true,
                FileName = "Протокол №1" + " (" + FractionToText(RoundText.Text) + ", " + GroupText.Text + ", " + "заход " + PerformanceText.Text + ", " + DanceText.Text + ")" + ".PDF"
            };

            if (dialog.ShowDialog() == true)
            {
                new CreatePDF().Protocol1(dialog.FileName, SelectedPairs, SelectedJudges, RoundText.Text, GroupText.Text, DanceText.Text, PerformanceText.Text);
            }
        }
    }
}
