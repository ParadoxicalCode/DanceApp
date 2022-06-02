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
        public List<ComboBox> CBFirstVertically;
        public GlobalClass gb = new GlobalClass();
        public DanceResult()
        {
            InitializeComponent();

            // Записываем имена combobox'ов с первой части таблицы по горизонтали.
            CBFirstHorizontally = new List<ComboBox>() {
            S1, S2, S3, S4, S5, S6, S7 };

            // Записываем имена combobox'ов с первой части таблицы по вертикали.
            CBFirstVertically = new List<ComboBox>() {
            P1, P2, P3, P4, P5, P6, P7, P8, P9, P10 };

            // Записываем имена textBox'ов с первой части таблицы по горизонтали.
            TBFirstHorizontally = new List<TextBox>() {
            S41, S42, S43, S44, S45, S46, S47,
            S51, S52, S53, S54, S55, S56, S57,
            S61, S62, S63, S64, S65, S66, S67,
            S71, S72, S73, S74, S75, S76, S77,
            S81, S82, S83, S84, S85, S86, S87,
            S91, S92, S93, S94, S95, S96, S97,
            S101, S102, S103, S104, S105, S106, S107 };

            // Записываем имена textBox'ов с первой части таблицы по вертикали.
            TBFirstVertically = new List<TextBox>() {
            S14, S24, S34, S44, S54, S64, S74, S84, S94, S104,
            S15, S25, S35, S45, S55, S65, S75, S85, S95, S105,
            S16, S26, S36, S46, S56, S66, S76, S86, S96, S106,
            S17, S27, S37, S47, S57, S67, S77, S87, S97, S107, };

            // Записываем имена textBox'ов со второй части таблицы по вертикали.
            TBSecondVertically = new List<TextBox>() {
            KM4, KM14, KM24, KM34, KM44, KM54, KM64, KM74, KM84, KM94, KM104,
            KM5, KM15, KM25, KM35, KM45, KM55, KM65, KM75, KM85, KM95, KM105,
            KM6, KM16, KM26, KM36, KM46, KM56, KM66, KM76, KM86, KM96, KM106,
            KM7, KM17, KM27, KM37, KM47, KM57, KM67, KM77, KM87, KM97, KM107,
            KM8, KM18, KM28, KM38, KM48, KM58, KM68, KM78, KM88, KM98, KM108,
            KM9, KM19, KM29, KM39, KM49, KM59, KM69, KM79, KM89, KM99, KM109,
            KM10, KM20, KM30, KM40, KM50, KM60, KM70, KM80, KM90, KM100, KM110 };

            // Записываем имена textBox'ов со второй части таблицы по горизонтали.
            TBSecondHorizontally = new List<TextBox>() {
            KM41, KM42, KM43, KM44, KM45, KM46, KM47, KM48, M4,
            KM51, KM52, KM53, KM54, KM55, KM56, KM57, KM58, M5,
            KM61, KM62, KM63, KM64, KM65, KM66, KM67, KM68, M6,
            KM71, KM72, KM73, KM74, KM75, KM76, KM77, KM78, M7,
            KM81, KM82, KM83, KM84, KM85, KM86, KM87, KM88, M8,
            KM91, KM92, KM93, KM94, KM95, KM96, KM97, KM98, M9,
            KM101, KM102, KM103, KM104, KM105, KM106, KM107, KM108, M10,}; // 18

            // Выводим данные из БД в comboBox'ы.
            DanceCB.ItemsSource = db.Dances.ToList();
            S1.ItemsSource = db.Judges.ToList();
            S2.ItemsSource = db.Judges.ToList();
            S3.ItemsSource = db.Judges.ToList();
            S4.ItemsSource = db.Judges.ToList();
            S5.ItemsSource = db.Judges.ToList();
            S6.ItemsSource = db.Judges.ToList();
            S7.ItemsSource = db.Judges.ToList();
            P1.ItemsSource = db.Pairs.ToList();
            P2.ItemsSource = db.Pairs.ToList();
            P3.ItemsSource = db.Pairs.ToList();
            P4.ItemsSource = db.Pairs.ToList();
            P5.ItemsSource = db.Pairs.ToList();
            P6.ItemsSource = db.Pairs.ToList();
            P7.ItemsSource = db.Pairs.ToList();
            P8.ItemsSource = db.Pairs.ToList();
            P9.ItemsSource = db.Pairs.ToList();
            P10.ItemsSource = db.Pairs.ToList();

            // Скрываем textBox'ы.
            switch (GlobalClass.NumberOfPairs)
            {
                case "3":
                    int a = 0; int b = 49; int c = 3; Hide(a, b, c);
                    a = 3; b = 10; c = 2; Hide(a, b, c);
                    a = 0; b = 77; c = 5; Hide(a, b, c);
                    a = 0; b = 63; c = 6; Hide(a, b, c); break;
                case "4":
                    a = 7; b = 49; c = 3; Hide(a, b, c);
                    a = 4; b = 10; c = 2; Hide(a, b, c);
                    a = 9; b = 77; c = 5; Hide(a, b, c);
                    a = 9; b = 63; c = 6; Hide(a, b, c); break;
                case "5":
                    a = 14; b = 49; c = 3; Hide(a, b, c);
                    a = 5; b = 10; c = 2; Hide(a, b, c);
                    a = 18; b = 77; c = 5; Hide(a, b, c);
                    a = 18; b = 63; c = 6; Hide(a, b, c); break;
                case "6":
                    a = 21; b = 49; c = 3; Hide(a, b, c);
                    a = 6; b = 10; c = 2; Hide(a, b, c);
                    a = 29; b = 77; c = 5; Hide(a, b, c);
                    a = 27; b = 63; c = 6; Hide(a, b, c); break;
                case "7":
                    a = 28; b = 49; c = 3; Hide(a, b, c);
                    a = 7; b = 10; c = 2; Hide(a, b, c);
                    a = 41; b = 77; c = 5; Hide(a, b, c);
                    a = 36; b = 63; c = 6; Hide(a, b, c); break;
                case "8":
                    a = 35; b = 49; c = 3; Hide(a, b, c);
                    a = 8; b = 10; c = 2; Hide(a, b, c);
                    a = 53; b = 77; c = 5; Hide(a, b, c);
                    a = 45; b = 63; c = 6; Hide(a, b, c); break;
                case "9":
                    a = 42; b = 49; c = 3; Hide(a, b, c);
                    a = 9; b = 10; c = 2; Hide(a, b, c);
                    a = 65; b = 77; c = 5; Hide(a, b, c);
                    a = 54; b = 63; c = 6; Hide(a, b, c); break;
            }

            // Скрываем comboBox'ы.
            switch (GlobalClass.NumberOfJudges)
            {
                case "3":
                    int a = 0; int b = 40; int c = 4; Hide(a, b, c);
                    a = 3; b = 7; c = 1; Hide(a, b, c); break;
                case "5":
                    a = 20; b = 40; c = 4; Hide(a, b, c);
                    a = 5; b = 7; c = 1; Hide(a, b, c); break;
            }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            main.Show();
            Hide();
        }

        private void Calculate_Click(object sender, RoutedEventArgs e)
        {
            // Записываем имена всех textBox'ов с первой части таблицы.
            List<TextBox> TBFirstAll;
            TBFirstAll = new List<TextBox>() {
            S11, S12, S13, S14, S15, S16, S17,
            S21, S22, S23, S24, S25, S26, S27,
            S31, S32, S33, S34, S35, S36, S37,
            S41, S42, S43, S44, S45, S46, S47,
            S51, S52, S53, S54, S55, S56, S57,
            S61, S62, S63, S64, S65, S66, S67,
            S71, S72, S73, S74, S75, S76, S77,
            S81, S82, S83, S84, S85, S86, S87,
            S91, S92, S93, S94, S95, S96, S97,
            S101, S102, S103, S104, S105, S106, S107 };

            // Записываем в массив textBox'ы из первой части таблицы
            TextBox[,] arrayFirst = new TextBox[10, 7];
            int TBIndex = 0;
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    arrayFirst[i, j] = TBFirstAll[TBIndex]; TBIndex++;
                }
            }

            // Записываем имена всех textBox'ов со второй части таблицы.
            List<TextBox> TBSecondAll;
            TBSecondAll = new List<TextBox>() {
            KM11, KM12, KM13, KM14, KM15, KM16, KM17, KM18, KM19, KM20,
            KM21, KM22, KM23, KM24, KM25, KM26, KM27, KM28, KM29, KM30,
            KM31, KM32, KM33, KM34, KM35, KM36, KM37, KM38, KM39, KM40,
            KM41, KM42, KM43, KM44, KM45, KM46, KM47, KM48, KM49, KM50,
            KM51, KM52, KM53, KM54, KM55, KM56, KM57, KM58, KM59, KM60,
            KM61, KM62, KM63, KM64, KM65, KM66, KM67, KM68, KM69, KM70,
            KM71, KM72, KM73, KM74, KM75, KM76, KM77, KM78, KM79, KM80,
            KM81, KM82, KM83, KM84, KM85, KM86, KM87, KM88, KM89, KM90,
            KM91, KM92, KM93, KM94, KM95, KM96, KM97, KM98, KM99, KM100,
            KM101, KM102, KM103, KM104, KM105, KM106, KM107, KM108, KM109, KM110 };

            // Записываем в массив textBox'ы из второй части таблицы
            TextBox[,] arraySecond = new TextBox[10, 10];
            TBIndex = 0;
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    arraySecond[i, j] = TBSecondAll[TBIndex]; TBIndex++;
                }
            }

            int vertical = Int32.Parse(GlobalClass.NumberOfPairs);
            int horizontal = Int32.Parse(GlobalClass.NumberOfJudges);
            TBIndex = 0;
            // Большинство голосов судей.
            int[] rs = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

            int rsIndex = 0;
            // Корректность введённых данных.
            int fill = 0;
            int i2 = 0;

            if (DanceCB.SelectedValue != null)
            {
                // Первичная проверка.
                for (int i = 0; i < vertical && fill != 1; i++)
                {
                    for (int j = 0; j < horizontal && fill != 1; j++)
                    {
                        // Окрашиваем textBox в белый цвет по умолчанию.
                        arrayFirst[i, j].Background = Brushes.White;

                        // Проверка на заполнение.
                        if (arrayFirst[i, j].Text != "")
                        {
                            // Проверка на корректность.
                            if (gb.StringIsDigits(arrayFirst[i, j].Text.TrimEnd()) == true 
                            && Int32.Parse(arrayFirst[i, j].Text) <= vertical 
                            && Int32.Parse(arrayFirst[i, j].Text) > 0)
                            {
                            }
                            else
                            {
                                MessageBox.Show("Введены некорректные данные");
                                arrayFirst[i, j].Background = Brushes.Red;
                                fill = 1;
                            }
                        }
                        else
                        {
                            MessageBox.Show("Не все поля заполнены");
                            arrayFirst[i, j].Background = Brushes.Red;
                            fill = 1;
                        }
                    }
                }
                // Как только сделана первичная проверка на корректность и заполнение, делаем проверку
                // на правильность распределения мест.

                // Места.
                string[] place = new string[10];
                int same = -1;

                // Проверка на правильность распределения мест.
                if (fill != 1)
                {
                    // Перебираем
                    for (int right = 0; right < horizontal; right++)
                    {
                        // Элемент i-той строки j-того столбца.
                        for (int down = 0; down < vertical; down++)
                        {
                            // Запишем все элементы данного столбца в массив.
                            place[down] = arrayFirst[down, right].Text;
                        }

                        // Сравниваем текущий элемент со всеми остальными в данном столбце.
                        for (int down = 0; down < horizontal; down++)
                        {
                            for (int placeIndex = 0; placeIndex < vertical; placeIndex++)
                            {
                                if (arrayFirst[down, right].Text == place[placeIndex])
                                {
                                    same++;
                                    if (same >= 1)
                                    {
                                        arrayFirst[down, right].Background = Brushes.Red;
                                        arrayFirst[placeIndex, right].Background = Brushes.Red;
                                    }
                                }
                            }
                            if (same >= 1)
                            {
                                MessageBox.Show("Места должны быть разными");
                                // Останавливаем алгоритм и ждём пока пользователь не введёт корректные данные.
                                fill = 1; right = horizontal; down = horizontal;
                            }
                            else
                            {
                                same = -1;
                            }
                        }
                    }
                }

                // Если введённые данные прошли все проверки, то запускаем вычисления
                if (fill != 1)
                {
                    for (int i = 0; i < vertical; i++)
                    {
                        // Расчёт количества большинства мест в диапазоне от 1 до 10.
                        for (int j = 0; j < horizontal; j++)
                        {
                            if (Int32.Parse(arrayFirst[i, j].Text) == 1) { rs[0]++; };
                            if (Int32.Parse(arrayFirst[i, j].Text) <= 2 &&
                                Int32.Parse(arrayFirst[i, j].Text) != 0) { rs[1]++; };
                            if (Int32.Parse(arrayFirst[i, j].Text) <= 3 &&
                                Int32.Parse(arrayFirst[i, j].Text) != 0) { rs[2]++; };
                            if (Int32.Parse(arrayFirst[i, j].Text) <= 4 &&
                                Int32.Parse(arrayFirst[i, j].Text) != 0) { rs[3]++; };
                            if (Int32.Parse(arrayFirst[i, j].Text) <= 5 &&
                                Int32.Parse(arrayFirst[i, j].Text) != 0) { rs[4]++; };
                            if (Int32.Parse(arrayFirst[i, j].Text) <= 6 &&
                                Int32.Parse(arrayFirst[i, j].Text) != 0) { rs[5]++; };
                            if (Int32.Parse(arrayFirst[i, j].Text) <= 7 &&
                                Int32.Parse(arrayFirst[i, j].Text) != 0) { rs[6]++; };
                            if (Int32.Parse(arrayFirst[i, j].Text) <= 8 &&
                                Int32.Parse(arrayFirst[i, j].Text) != 0) { rs[7]++; };
                            if (Int32.Parse(arrayFirst[i, j].Text) <= 9 &&
                                Int32.Parse(arrayFirst[i, j].Text) != 0) { rs[8]++; };
                            if (Int32.Parse(arrayFirst[i, j].Text) <= 10 &&
                                Int32.Parse(arrayFirst[i, j].Text) != 0) { rs[9]++; };
                        }

                        // Запишем значения переменных мест для i-той строки первой части таблицы
                        // в соответствующие textBox'ы i-той строки во второй части таблицы.
                        for (int j = 0; j < vertical; j++)
                        {
                            arraySecond[i2, j].Text = rs[rsIndex].ToString(); rsIndex++;
                        }
                        rsIndex = 0; i2++; rs[0] = 0; rs[1] = 0; rs[2] = 0; rs[3] = 0;
                        rs[4] = 0; rs[5] = 0; rs[6] = 0; rs[7] = 0; rs[8] = 0; rs[9] = 0;
                    }

                    // Когда я писал этот код, только Бог и я понимали, что он делает.
                    // Сейчас... знает только Бог.

                    // Записываем имена всех крайних textBox'ов со второй части таблицы.
                    List<TextBox> TBSecondPlaces;
                    TBSecondPlaces = new List<TextBox>() { M1, M2, M3, M4, M5, M6, M7, M8, M9, M10 };
                    int placeEnd = 1;

                    // Перебираем правую часть таблицы сверху вниз по столбцам.
                    for (int j = 0; j < vertical; j++)
                    {
                        // Запишем в массив places результаты участников.
                        for (int i = 0; i < vertical; i++)
                        {
                            // Если участник имеет необходимое большинство мест и ему не
                            // присвоено место, то присвоим ему место и перейдём к следующему столбцу.
                            if (Int32.Parse(arraySecond[i, j].Text) >= (horizontal + 2 - 1) / 2 
                                && TBSecondPlaces[i].Text == "")
                            {
                                TBSecondPlaces[i].Text = (placeEnd).ToString();
                                placeEnd++; i = vertical;
                                /*
                                switch (j)
                                {
                                    // Если участник с j-тым id занял какое-то место, то записываем в
                                    // соответствующий textBox место.
                                    case 1:
                                        TBSecondPlaces[0].Text = (i + 1).ToString(); break;
                                    case 2:
                                        TBSecondPlaces[1].Text = (i + 1).ToString(); break;
                                    case 3:
                                        TBSecondPlaces[2].Text = (i + 1).ToString(); break;
                                    case 4:
                                        TBSecondPlaces[3].Text = (i + 1).ToString(); break;
                                    case 5:
                                        TBSecondPlaces[4].Text = (i + 1).ToString(); break;
                                    case 6:
                                        TBSecondPlaces[5].Text = (i + 1).ToString(); break;
                                    case 7:
                                        TBSecondPlaces[6].Text = (i + 1).ToString(); break;
                                    case 8:
                                        TBSecondPlaces[7].Text = (i + 1).ToString(); break;
                                    case 9:
                                        TBSecondPlaces[8].Text = (i + 1).ToString(); break;
                                    case 10:
                                        TBSecondPlaces[9].Text = (i + 1).ToString(); break;
                                }
                                */
                            }
                            // Иначе переходим к следующему столбцу.
                            else
                            {
                                // Если в этом столбце уже есть участник с присвоенным местом, то
                                // переходим к следующему столбцу.
                                for (int index = 0; index < vertical; index++)
                                {
                                    if (TBSecondPlaces[index].Text != "")
                                    {
                                        i = vertical; index = vertical;
                                    }
                                }
                            }
                        }
                        //j = vertical;

                        // Создадим массив, где будут места.
                        //Первый попавшийся подходящий участник получает первое место
                        // Второй - второе и тд.

                        // Нужно исключить из выборки участника, которому присвоено место.
                    } 
                }
            }
            else
            {
                MessageBox.Show("Поле 'Танец' не заполнено");
            }  
        }

        // Метод, скрывающий textBox'ы и comboBox'ы в определённом диапазоне.
        private void Hide(int a, int b, int c)
        {
            for (int i = a; i < b; i++)
            {
                // a - левая граница, b - правая граница, c - id списка.
                switch (c)
                {
                    case 1:
                        CBFirstHorizontally[i].Visibility = Visibility.Hidden; break;
                    case 2:
                        CBFirstVertically[i].Visibility = Visibility.Hidden; break;
                    case 3:
                        TBFirstHorizontally[i].Visibility = Visibility.Hidden; break;
                    case 4:
                        TBFirstVertically[i].Visibility = Visibility.Hidden; break;
                    case 5:
                        TBSecondVertically[i].Visibility = Visibility.Hidden; break;
                    case 6:
                        TBSecondHorizontally[i].Visibility = Visibility.Hidden; break;
                }
            }
        }
    }
}



