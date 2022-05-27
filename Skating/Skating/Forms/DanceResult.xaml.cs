using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Skating.Forms
{
    /// <summary>
    /// Логика взаимодействия для DanceResult.xaml
    /// </summary>
    public partial class DanceResult : Window
    {
        public SkatingEntities db = new SkatingEntities();
        public List<ComboBox> CBFirstHorizontally;
        public List<TextBox> TBFirstHorizontally;
        public List<TextBox> TBFirstVertically;
        public List<TextBox> TBSecondVertically;
        public List<TextBox> TBSecondHorizontally;
        public DanceResult()
        {
            InitializeComponent();

            // Записываем имена combobox'ов с первой части таблицы по горизонтали.
            CBFirstHorizontally = new List<ComboBox>() {
            S4, S5, S6, S7 };

            // Записываем имена textBox'ов с первой части таблицы по горизонтали.
            TBFirstHorizontally = new List<TextBox>() {
            P4, S41, S42, S43, S44, S45, S46, S47,
            P5, S51, S52, S53, S54, S55, S56, S57,
            P6, S61, S62, S63, S64, S65, S66, S67,
            P7, S71, S72, S73, S74, S75, S76, S77,
            P8, S81, S82, S83, S84, S85, S86, S87 };

            // Записываем имена textBox'ов с первой части таблицы по вертикали.
            TBFirstVertically = new List<TextBox>() {
            S14, S24, S34, S44, S54, S64, S74, S84,
            S15, S25, S35, S45, S55, S65, S75, S85,
            S16, S26, S36, S46, S56, S66, S76, S86,
            S17, S27, S37, S47, S57, S67, S77, S87 };

            // Записываем имена textBox'ов со второй части таблицы по вертикали.
            TBSecondVertically = new List<TextBox>() {
            KM4, KM14, KM24, KM34, KM44, KM54, KM64, KM74, KM84,
            KM5, KM15, KM25, KM35, KM45, KM55, KM65, KM75, KM85,
            KM6, KM16, KM26, KM36, KM46, KM56, KM66, KM76, KM86,
            KM7, KM17, KM27, KM37, KM47, KM57, KM67, KM77, KM87,
            KM8, KM18, KM28, KM38, KM48, KM58, KM68, KM78, KM88, };

            // Записываем имена textBox'ов со второй части таблицы по горизонтали.
            TBSecondHorizontally = new List<TextBox>() {
            KM41, KM42, KM43, KM44, KM45, KM46, KM47, KM48, M4,
            KM51, KM52, KM53, KM54, KM55, KM56, KM57, KM58, M5,
            KM61, KM62, KM63, KM64, KM65, KM66, KM67, KM68, M6,
            KM71, KM72, KM73, KM74, KM75, KM76, KM77, KM78, M7,
            KM81, KM82, KM83, KM84, KM85, KM86, KM87, KM88, M8, };



            DanceCB.ItemsSource = db.Dances.ToList();
            S1.ItemsSource = db.Judges.ToList();
            S2.ItemsSource = db.Judges.ToList();
            S3.ItemsSource = db.Judges.ToList();
            S4.ItemsSource = db.Judges.ToList();
            S5.ItemsSource = db.Judges.ToList();
            S6.ItemsSource = db.Judges.ToList();
            S7.ItemsSource = db.Judges.ToList();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            main.Show();
            Hide();
        }

        private void Calculate_Click(object sender, RoutedEventArgs e)
        {
            /*
            int r1 = 0;
            int r2 = 0;
            int r3 = 0;
            int r4 = 0;
            int r5 = 0;
            int r6 = 0;
            int r7 = 0;
            int r8 = 0;
            int a11, a12, a13, a14, a15, a16, a17;
            a11 = Int32.Parse(S11.Text);
            a12 = Int32.Parse(S12.Text);
            a13 = Int32.Parse(S13.Text);
            a14 = Int32.Parse(S14.Text);
            a15 = Int32.Parse(S15.Text);
            a16 = Int32.Parse(S16.Text);
            a17 = Int32.Parse(S17.Text);
            int[] array = { a11, a12, a13, a14, a15, a16, a17 };
            for (int i = 0; i < 7; i++)
            {
                if (array[i] == 1) { r1++; }
                if (array[i] <= 2) { r2++; }
                if (array[i] <= 3) { r3++; }
                if (array[i] <= 4) { r4++; }
                if (array[i] <= 5) { r5++; }
                if (array[i] <= 6) { r6++; }
                if (array[i] <= 7) { r7++; }
                if (array[i] <= 8) { r8++; }
            }
            KM11.Text = r1.ToString();
            KM12.Text = r2.ToString();
            KM13.Text = r3.ToString();
            KM14.Text = r4.ToString();
            KM15.Text = r5.ToString();
            KM16.Text = r6.ToString();
            KM17.Text = r7.ToString();
            KM18.Text = r8.ToString();

            r1 = 0; r2 = 0; r3 = 0; r4 = 0; r5 = 0; r6 = 0; r7 = 0; r8 = 0;
            a11 = 0; a12 = 0; a13 = 0; a14 = 0; a15 = 0; a16 = 0; a17 = 0;
            a11 = Int32.Parse(S21.Text);
            a12 = Int32.Parse(S22.Text);
            a13 = Int32.Parse(S23.Text);
            a14 = Int32.Parse(S24.Text);
            a15 = Int32.Parse(S25.Text);
            a16 = Int32.Parse(S26.Text);
            a17 = Int32.Parse(S27.Text);
            for (int i = 0; i < 7; i++)
            {
                if (array[i] == 1) { r1++; }
                if (array[i] <= 2) { r2++; }
                if (array[i] <= 3) { r3++; }
                if (array[i] <= 4) { r4++; }
                if (array[i] <= 5) { r5++; }
                if (array[i] <= 6) { r6++; }
                if (array[i] <= 7) { r7++; }
                if (array[i] <= 8) { r8++; }
            }
            KM21.Text = r1.ToString();
            KM22.Text = r2.ToString();
            KM23.Text = r3.ToString();
            KM24.Text = r4.ToString();
            KM25.Text = r5.ToString();
            KM26.Text = r6.ToString();
            KM27.Text = r7.ToString();
            KM28.Text = r8.ToString();

            r1 = 0; r2 = 0; r3 = 0; r4 = 0; r5 = 0; r6 = 0; r7 = 0; r8 = 0;
            a11 = 0; a12 = 0; a13 = 0; a14 = 0; a15 = 0; a16 = 0; a17 = 0;
            a11 = Int32.Parse(S31.Text);
            a12 = Int32.Parse(S32.Text);
            a13 = Int32.Parse(S33.Text);
            a14 = Int32.Parse(S34.Text);
            a15 = Int32.Parse(S35.Text);
            a16 = Int32.Parse(S36.Text);
            a17 = Int32.Parse(S37.Text);
            for (int i = 0; i < 7; i++)
            {
                if (array[i] == 1) { r1++; }
                if (array[i] <= 2) { r2++; }
                if (array[i] <= 3) { r3++; }
                if (array[i] <= 4) { r4++; }
                if (array[i] <= 5) { r5++; }
                if (array[i] <= 6) { r6++; }
                if (array[i] <= 7) { r7++; }
                if (array[i] <= 8) { r8++; }
            }
            KM31.Text = r1.ToString();
            KM32.Text = r2.ToString();
            KM33.Text = r3.ToString();
            KM34.Text = r4.ToString();
            KM35.Text = r5.ToString();
            KM36.Text = r6.ToString();
            KM37.Text = r7.ToString();
            KM38.Text = r8.ToString();

            r1 = 0; r2 = 0; r3 = 0; r4 = 0; r5 = 0; r6 = 0; r7 = 0; r8 = 0;
            a11 = 0; a12 = 0; a13 = 0; a14 = 0; a15 = 0; a16 = 0; a17 = 0;
            a11 = Int32.Parse(S41.Text);
            a12 = Int32.Parse(S42.Text);
            a13 = Int32.Parse(S43.Text);
            a14 = Int32.Parse(S44.Text);
            a15 = Int32.Parse(S45.Text);
            a16 = Int32.Parse(S46.Text);
            a17 = Int32.Parse(S47.Text);
            for (int i = 0; i < 7; i++)
            {
                if (array[i] == 1) { r1++; }
                if (array[i] <= 2) { r2++; }
                if (array[i] <= 3) { r3++; }
                if (array[i] <= 4) { r4++; }
                if (array[i] <= 5) { r5++; }
                if (array[i] <= 6) { r6++; }
                if (array[i] <= 7) { r7++; }
                if (array[i] <= 8) { r8++; }
            }
            KM41.Text = r1.ToString();
            KM42.Text = r2.ToString();
            KM43.Text = r3.ToString();
            KM44.Text = r4.ToString();
            KM45.Text = r5.ToString();
            KM46.Text = r6.ToString();
            KM47.Text = r7.ToString();
            KM48.Text = r8.ToString();

            r1 = 0; r2 = 0; r3 = 0; r4 = 0; r5 = 0; r6 = 0; r7 = 0; r8 = 0;
            a11 = 0; a12 = 0; a13 = 0; a14 = 0; a15 = 0; a16 = 0; a17 = 0;
            a11 = Int32.Parse(S51.Text);
            a12 = Int32.Parse(S52.Text);
            a13 = Int32.Parse(S53.Text);
            a14 = Int32.Parse(S54.Text);
            a15 = Int32.Parse(S55.Text);
            a16 = Int32.Parse(S56.Text);
            a17 = Int32.Parse(S57.Text);
            for (int i = 0; i < 7; i++)
            {
                if (array[i] == 1) { r1++; }
                if (array[i] <= 2) { r2++; }
                if (array[i] <= 3) { r3++; }
                if (array[i] <= 4) { r4++; }
                if (array[i] <= 5) { r5++; }
                if (array[i] <= 6) { r6++; }
                if (array[i] <= 7) { r7++; }
                if (array[i] <= 8) { r8++; }
            }
            KM51.Text = r1.ToString();
            KM52.Text = r2.ToString();
            KM53.Text = r3.ToString();
            KM54.Text = r4.ToString();
            KM55.Text = r5.ToString();
            KM56.Text = r6.ToString();
            KM57.Text = r7.ToString();
            KM58.Text = r8.ToString();

            r1 = 0; r2 = 0; r3 = 0; r4 = 0; r5 = 0; r6 = 0; r7 = 0; r8 = 0;
            a11 = 0; a12 = 0; a13 = 0; a14 = 0; a15 = 0; a16 = 0; a17 = 0;
            a11 = Int32.Parse(S61.Text);
            a12 = Int32.Parse(S62.Text);
            a13 = Int32.Parse(S63.Text);
            a14 = Int32.Parse(S64.Text);
            a15 = Int32.Parse(S65.Text);
            a16 = Int32.Parse(S66.Text);
            a17 = Int32.Parse(S67.Text);
            for (int i = 0; i < 7; i++)
            {
                if (array[i] == 1) { r1++; }
                if (array[i] <= 2) { r2++; }
                if (array[i] <= 3) { r3++; }
                if (array[i] <= 4) { r4++; }
                if (array[i] <= 5) { r5++; }
                if (array[i] <= 6) { r6++; }
                if (array[i] <= 7) { r7++; }
                if (array[i] <= 8) { r8++; }
            }
            KM61.Text = r1.ToString();
            KM62.Text = r2.ToString();
            KM63.Text = r3.ToString();
            KM64.Text = r4.ToString();
            KM65.Text = r5.ToString();
            KM66.Text = r6.ToString();
            KM67.Text = r7.ToString();
            KM68.Text = r8.ToString();

            r1 = 0; r2 = 0; r3 = 0; r4 = 0; r5 = 0; r6 = 0; r7 = 0; r8 = 0;
            a11 = 0; a12 = 0; a13 = 0; a14 = 0; a15 = 0; a16 = 0; a17 = 0;
            a11 = Int32.Parse(S71.Text);
            a12 = Int32.Parse(S72.Text);
            a13 = Int32.Parse(S73.Text);
            a14 = Int32.Parse(S74.Text);
            a15 = Int32.Parse(S75.Text);
            a16 = Int32.Parse(S76.Text);
            a17 = Int32.Parse(S77.Text);
            for (int i = 0; i < 7; i++)
            {
                if (array[i] == 1) { r1++; }
                if (array[i] <= 2) { r2++; }
                if (array[i] <= 3) { r3++; }
                if (array[i] <= 4) { r4++; }
                if (array[i] <= 5) { r5++; }
                if (array[i] <= 6) { r6++; }
                if (array[i] <= 7) { r7++; }
                if (array[i] <= 8) { r8++; }
            }
            KM71.Text = r1.ToString();
            KM72.Text = r2.ToString();
            KM73.Text = r3.ToString();
            KM74.Text = r4.ToString();
            KM75.Text = r5.ToString();
            KM76.Text = r6.ToString();
            KM77.Text = r7.ToString();
            KM78.Text = r8.ToString();

            r1 = 0; r2 = 0; r3 = 0; r4 = 0; r5 = 0; r6 = 0; r7 = 0; r8 = 0;
            a11 = 0; a12 = 0; a13 = 0; a14 = 0; a15 = 0; a16 = 0; a17 = 0;
            a11 = Int32.Parse(S81.Text);
            a12 = Int32.Parse(S82.Text);
            a13 = Int32.Parse(S83.Text);
            a14 = Int32.Parse(S84.Text);
            a15 = Int32.Parse(S85.Text);
            a16 = Int32.Parse(S86.Text);
            a17 = Int32.Parse(S87.Text);
            for (int i = 0; i < 7; i++)
            {
                if (array[i] == 1) { r1++; }
                if (array[i] <= 2) { r2++; }
                if (array[i] <= 3) { r3++; }
                if (array[i] <= 4) { r4++; }
                if (array[i] <= 5) { r5++; }
                if (array[i] <= 6) { r6++; }
                if (array[i] <= 7) { r7++; }
                if (array[i] <= 8) { r8++; }
            }
            KM81.Text = r1.ToString();
            KM82.Text = r2.ToString();
            KM83.Text = r3.ToString();
            KM84.Text = r4.ToString();
            KM85.Text = r5.ToString();
            KM86.Text = r6.ToString();
            KM87.Text = r7.ToString();
            KM88.Text = r8.ToString();
            
            // Массив, вкотором храним имена Texbox'ов.
            string[] array = {"P3", "S31", "S32", "S33", "S34", "S35", "S36", "S37",
            "P4", "S41", "S42", "S43", "S44", "S45", "S46", "S47",
            "P5", "S51", "S52", "S53", "S54", "S55", "S56", "S57",
            "P6", "S61", "S62", "S63", "S64", "S65", "S66", "S67",
            "P7", "S71", "S72", "S73", "S74", "S75", "S76", "S77",
            "P8", "S81", "S82", "S83", "S84", "S85", "S86", "S87"};
            */

            // Проверка на заполнение.
            if (DanceCB.SelectedValue == null || NumberOfPairsCB.SelectedValue == null ||
                NumberOfJudgesCB.SelectedValue == null)
            {
                MessageBox.Show("Не все поля заполнены");
            }
            else
            {
                switch (NumberOfPairsCB.Text)
                {
                    case "3":
                        int a = 0; int b = 39; TBVisibilityFirstHorizontally(a, b);
                        a = 0; b = 45; TBVisibilitySecondVertically(a, b);
                        a = 0; b = 45; TBVisibilitySecondHorizontally(a, b); break;
                    case "4":
                        a = 8; b = 39; TBVisibilityFirstHorizontally(a, b);
                        a = 9; b = 45; TBVisibilitySecondVertically(a, b);
                        a = 9; b = 45; TBVisibilitySecondHorizontally(a, b); break;
                    case "5":
                        a = 16; b = 39; TBVisibilityFirstHorizontally(a, b);
                        a = 18; b = 45; TBVisibilitySecondVertically(a, b);
                        a = 18; b = 45; TBVisibilitySecondHorizontally(a, b); break;
                    case "6":
                        a = 24; b = 39; TBVisibilityFirstHorizontally(a, b);
                        a = 27; b = 45; TBVisibilitySecondVertically(a, b);
                        a = 27; b = 45; TBVisibilitySecondHorizontally(a, b); break;
                    case "7":
                        a = 32; b = 39; TBVisibilityFirstHorizontally(a, b);
                        a = 36; b = 45; TBVisibilitySecondVertically(a, b);
                        a = 36; b = 45; TBVisibilitySecondHorizontally(a, b); break;
                }

                switch (NumberOfJudgesCB.Text)
                {
                    case "3":
                        int a = 0; int b = 32; TBVisibilityFirstVertically(a, b);
                        a = 0; b = 4; CBVisibilityFirstHorizontally(a, b); break;
                    case "5":
                        a = 16; b = 32; TBVisibilityFirstVertically(a, b);
                        a = 2; b = 4; CBVisibilityFirstHorizontally(a, b); break;
                }
            }

            /*
            // Возвращаемся в исходное состояние.
            for (int i = 0; i < 48; i++)
            {
                textboxesFirstHorizontally[i].Visibility = Visibility.Visible;
            }
            for (int i = 0; i < 30; i++)
            {
                textboxesFirstVertically[i].Visibility = Visibility.Visible;
            }
            */
        }

        // Метод, скрывающий comboBox'ы в определённом диапазоне.
        private void CBVisibilityFirstHorizontally(int a, int b)
        {
            for (int i = a; i < b; i++)
            {
                CBFirstHorizontally[i].Visibility = Visibility.Hidden;
            }
        }

        // Методы, скрывающие textBox'ы в определённом диапазоне.
        private void TBVisibilityFirstHorizontally(int a, int b)
        {
            for (int i = a; i < b; i++)
            {
                TBFirstHorizontally[i].Visibility = Visibility.Hidden;
            }
        }

        private void TBVisibilityFirstVertically(int a, int b)
        {
            for (int i = a; i < b; i++)
            {
                TBFirstVertically[i].Visibility = Visibility.Hidden;
            }
        }

        private void TBVisibilitySecondVertically(int a, int b)
        {
            for (int i = a; i < b; i++)
            {
                TBSecondVertically[i].Visibility = Visibility.Hidden;
            }
        }

        private void TBVisibilitySecondHorizontally(int a, int b)
        {
            for (int i = a; i < b; i++)
            {
                TBSecondHorizontally[i].Visibility = Visibility.Hidden;
            }
        }
    }
}
