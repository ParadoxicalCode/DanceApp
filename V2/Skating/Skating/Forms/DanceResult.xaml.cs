using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Navigation;

namespace Skating.Forms
{
    /// <summary>
    /// Логика взаимодействия для DanceResult.xaml
    /// </summary>
    public partial class DanceResult : Window
    {
        public SkatingEntities db = new SkatingEntities();
        public List<TextBox> LeftMatrix;
        public List<TextBox> LeftMatrixTransposed;
        public List<TextBlock> RightMatrix;
        public List<TextBlock> RightMatrixTransposed;
        public List<TextBlock> PlacesMatrix;
        public GlobalClass gb = new GlobalClass();
        int pairs = int.Parse(GlobalClass.NumberOfPairs);
        int judges = int.Parse(GlobalClass.NumberOfJudges);
        int indexLeft, indexRight;
        int[] places = new int[10];
        int[] placesClone = new int[10];
        int[] place = {1,2,3,4,5,6,7,8,9,10};
        int placeIndex;
        int bgs;
        int one, two, three, four, five, six, seven, eight, nine, ten;
        int column = 0;

        public DanceResult()
        {
            InitializeComponent();

            LeftMatrix = new List<TextBox>() // /*6*/ - index.
            {
                TB001, TB011, TB021, TB031, TB041, TB051, TB061, /*6*/  
                TB002, TB012, TB022, TB032, TB042, TB052, TB062, /*13*/ 
                TB003, TB013, TB023, TB033, TB043, TB053, TB063, /*20*/ 
                TB004, TB014, TB024, TB034, TB044, TB054, TB064, /*27*/ 
                TB005, TB015, TB025, TB035, TB045, TB055, TB065, /*34*/ 
                TB006, TB016, TB026, TB036, TB046, TB056, TB066, /*41*/ 
                TB007, TB017, TB027, TB037, TB047, TB057, TB067, /*48*/
                TB008, TB018, TB028, TB038, TB048, TB058, TB068, /*55*/
                TB009, TB019, TB029, TB039, TB049, TB059, TB069, /*62*/
                TB010, TB020, TB030, TB040, TB050, TB060, TB070, /*69*/
            };

            LeftMatrixTransposed = new List<TextBox>() // /*9*/ - index.
            {
                TB001, TB002, TB003, TB004, TB005, TB006, TB007, TB008, TB009, TB010, /*9*/
                TB011, TB012, TB013, TB014, TB015, TB016, TB017, TB018, TB019, TB020, /*19*/
                TB021, TB022, TB023, TB024, TB025, TB026, TB027, TB028, TB029, TB030, /*29*/
                TB031, TB032, TB033, TB034, TB035, TB036, TB037, TB038, TB039, TB040, /*39*/
                TB041, TB042, TB043, TB044, TB045, TB046, TB047, TB048, TB049, TB050, /*49*/
                TB051, TB052, TB053, TB054, TB055, TB056, TB057, TB058, TB059, TB060, /*59*/
                TB061, TB062, TB063, TB064, TB065, TB066, TB067, TB068, TB069, TB070, /*69*/
            };

            RightMatrix = new List<TextBlock>()
            {
                TBK001, TBK002, TBK003, TBK004, TBK005, TBK006, TBK007, TBK008, TBK009, TBK010, /*9*/
                TBK011, TBK012, TBK013, TBK014, TBK015, TBK016, TBK017, TBK018, TBK019, TBK020, /*19*/
                TBK021, TBK022, TBK023, TBK024, TBK025, TBK026, TBK027, TBK028, TBK029, TBK030, /*29*/
                TBK031, TBK032, TBK033, TBK034, TBK035, TBK036, TBK037, TBK038, TBK039, TBK040, /*39*/
                TBK041, TBK042, TBK043, TBK044, TBK045, TBK046, TBK047, TBK048, TBK049, TBK050, /*49*/
                TBK051, TBK052, TBK053, TBK054, TBK055, TBK056, TBK057, TBK058, TBK059, TBK060, /*59*/
                TBK061, TBK062, TBK063, TBK064, TBK065, TBK066, TBK067, TBK068, TBK069, TBK070, /*69*/
                TBK071, TBK072, TBK073, TBK074, TBK075, TBK076, TBK077, TBK078, TBK079, TBK080, /*79*/
                TBK081, TBK082, TBK083, TBK084, TBK085, TBK086, TBK087, TBK088, TBK089, TBK090, /*89*/
                TBK091, TBK092, TBK093, TBK094, TBK095, TBK096, TBK097, TBK098, TBK099, TBK100, /*99*/
            };

            RightMatrixTransposed = new List<TextBlock>()
            {
                TBK001, TBK011, TBK021, TBK031, TBK041, TBK051, TBK061, TBK071, TBK081, TBK091, /*9*/
                TBK002, TBK012, TBK022, TBK032, TBK042, TBK052, TBK062, TBK072, TBK082, TBK092, /*19*/
                TBK003, TBK013, TBK023, TBK033, TBK043, TBK053, TBK063, TBK073, TBK083, TBK093, /*29*/
                TBK004, TBK014, TBK024, TBK034, TBK044, TBK054, TBK064, TBK074, TBK084, TBK094, /*39*/
                TBK005, TBK015, TBK025, TBK035, TBK045, TBK055, TBK065, TBK075, TBK085, TBK095, /*49*/
                TBK006, TBK016, TBK026, TBK036, TBK046, TBK056, TBK066, TBK076, TBK086, TBK096, /*59*/
                TBK007, TBK017, TBK027, TBK037, TBK047, TBK057, TBK067, TBK077, TBK087, TBK097, /*69*/
                TBK008, TBK018, TBK028, TBK038, TBK048, TBK058, TBK068, TBK078, TBK088, TBK098, /*79*/
                TBK009, TBK019, TBK029, TBK039, TBK049, TBK059, TBK069, TBK079, TBK089, TBK099, /*89*/
                TBK010, TBK020, TBK030, TBK040, TBK050, TBK060, TBK070, TBK080, TBK090, TBK100, /*99*/
            };

            PlacesMatrix = new List<TextBlock>()
            {
                place01, place02, place03, place04, place05, place06, place07, place08, place09, place10,
            };

            switch (GlobalClass.NumberOfJudges)
            {
                case "3": int y = 30; NotActiveVertical(y); break;
                case "5": y = 50; NotActiveVertical(y); break;
            }

            switch (GlobalClass.NumberOfPairs)
            {
                case "2": int x = 14; NotActiveHorizontal(x); break;
                case "3": x = 21; NotActiveHorizontal(x); break;
                case "4": x = 28; NotActiveHorizontal(x); break;
                case "5": x = 35;  NotActiveHorizontal(x); break;
                case "6": x = 42; NotActiveHorizontal(x); break;
                case "7": x = 49; NotActiveHorizontal(x); break;
                case "8": x = 56; NotActiveHorizontal(x); break;
                case "9": x = 63; NotActiveHorizontal(x); break;
            }
        }

        private void NotActiveVertical(int y)
        {
            for (; y < 70; y++)
                LeftMatrixTransposed[y].Visibility = Visibility.Hidden;
        }

        private void NotActiveHorizontal(int x)
        {
            for (; x < 70; x++)
                LeftMatrix[x].Visibility = Visibility.Hidden;
        }

        private void Calculate_Click(object sender, RoutedEventArgs e)
        {
            if (Validation() == true)
            {
                // Когда я писал этот код, только Бог и я понимали, что он делает.
                // Сейчас... знает только Бог.

                PlaceCount();
                for (; column < pairs; column++)
                {
                    Dispute(); indexRight += 10;
                }
            }
        }

        // Для пары, которая получила место расставить чёрточки.
        private void Erase(int f)
        {
            for (int a = 0; a < pairs - 1; a++)
            {
                if (a + column != pairs - 1 && (ten + a + column < 101))
                {
                    switch (f)
                    {
                        case 0: RightMatrixTransposed[one + a + column].Text = "-"; break;
                        case 1: RightMatrixTransposed[two + a + column].Text = "-"; break;
                        case 2: RightMatrixTransposed[three + a + column].Text = "-"; break;
                        case 3: RightMatrixTransposed[four + a + column].Text = "-"; break;
                        case 4: RightMatrixTransposed[five + a + column].Text = "-"; break;
                        case 5: RightMatrixTransposed[six + a + column].Text = "-"; break;
                        case 6: RightMatrixTransposed[seven + a + column].Text = "-"; break;
                        case 7: RightMatrixTransposed[eight + a + column].Text = "-"; break;
                        case 8: RightMatrixTransposed[nine + a + column].Text = "-"; break;
                        case 9: RightMatrixTransposed[ten + a + column].Text = "-"; break;
                    }
                }
            }
        }

        // Распределяет места.
        private void Dispute()
        {
            int u = 0;
            for (int ri = indexRight; ri < indexRight + pairs; ri++)
            {
                if (RightMatrix[ri].Text != "-")
                {
                    places[u] = Int32.Parse(RightMatrix[ri].Text);
                } 
                u++;
            }

            int first = 0; int second = 0; int third = 0;
            int f = 0; int s = 1;
            for (; f < 10 && first == 0; f++) 
            { 
                if (places[f] >= (judges + 1) / 2) { first = places[f]; places[f] = 0; } 
            }

            if (column == pairs - 1) 
            {
                Places(f - 1);
            }
            else
            {
                switch (BGS(indexRight))
                {
                    case 1: { Places(f - 1); Erase(f - 1); break; }
                    case 2:
                        for (; second == 0; s++)
                        { if (places[s] >= (judges + 1) / 2) { second = placesClone[s]; } }

                        //for (int y = 0; y < 10; y++) { placesClone[y] = 0; }

                        // Правило 6.
                        if (first > second) { Places(f); }
                        else if (first < second) { Places(s); }
                        else
                        {
                            // Правило 7.

                        }

                        break;
                    case 3:

                        break;
                }
            }
        }

        // Присуждает места.
        private void Places(int e)
        {
            switch (e)
            {
                case 0: PlacesMatrix[0].Text = place[placeIndex].ToString(); break;
                case 1: PlacesMatrix[1].Text = place[placeIndex].ToString(); break;
                case 2: PlacesMatrix[2].Text = place[placeIndex].ToString(); break;
                case 3: PlacesMatrix[3].Text = place[placeIndex].ToString(); break;
                case 4: PlacesMatrix[4].Text = place[placeIndex].ToString(); break;
                case 5: PlacesMatrix[5].Text = place[placeIndex].ToString(); break;
                case 6: PlacesMatrix[6].Text = place[placeIndex].ToString(); break;
                case 7: PlacesMatrix[7].Text = place[placeIndex].ToString(); break;
                case 8: PlacesMatrix[8].Text = place[placeIndex].ToString(); break;
                case 9: PlacesMatrix[9].Text = place[placeIndex].ToString(); break;
            }
            placeIndex++;
        }

        // Проверяет, есть ли в текущем столбце пары, набравшие большинство голосов судей.
        private int BGS(int indexRight)
        {
            bgs = 0;
            for (int i = indexRight; i < (indexRight + pairs); i++)
            {
                if (RightMatrix[i].Text != "-")
                {
                    // Если участник набрал необходимое большинство голосов судей.
                    if (Int32.Parse(RightMatrix[i].Text) >= (judges + 1) / 2) { bgs++; }
                }
            } 
            for (int q = 0; q < 10; q++) { placesClone[q] = places[q]; }
            return bgs;
        }

        // Считает количество мест в правой части таблицы.
        private void PlaceCount()
        {
            one = 1; two = 11; three = 21; four = 31; five = 41;
            six = 51; seven = 61; eight = 71; nine = 81; ten = 91;
            for (int n = 0; n < 10; n++) { PlacesMatrix[n].Text = ""; }
            indexLeft = 0; indexRight = 0; int sum; placeIndex = 0; column = 0;
            for (int y = 0; y < 10; y++) { places[y] = 0; }
            for (int j = 0; j < pairs; j++)
            {
                for (int u = indexLeft; u < (indexLeft + judges); u++)
                {
                    switch (LeftMatrix[u].Text)
                    {
                        case "1": places[0] += 1; break;
                        case "2": places[1] += 1; break;
                        case "3": places[2] += 1; break;
                        case "4": places[3] += 1; break;
                        case "5": places[4] += 1; break;
                        case "6": places[5] += 1; break;
                        case "7": places[6] += 1; break;
                        case "8": places[7] += 1; break;
                        case "9": places[8] += 1; break;
                        case "10": places[9] += 1; break;
                    }
                }

                int g = 0; sum = places[0];
                for (int h = indexRight; h < (indexRight + pairs); h++)
                {
                    if (sum == 0) { RightMatrixTransposed[h].Text = "-"; }
                    else { RightMatrixTransposed[h].Text = sum.ToString(); }

                    g++; if (g != 10) { sum += places[g]; }
                }
                for (int y = 0; y < 10; y++) { places[y] = 0; }
                indexLeft += 7; indexRight += 10;
            }
            indexRight = 0;
        }

        private bool Validation()
        {
            // Правило 3.
            bool stop = true;
            for (int i = 0; i < 70 && stop == true; i++)
            {
                LeftMatrixTransposed[i].Background = Brushes.White;
                if (LeftMatrixTransposed[i].Visibility != Visibility.Hidden)
                {
                    if (LeftMatrixTransposed[i].Text == "")
                    {
                        MessageBox.Show("Не все поля заполнены!");
                        LeftMatrixTransposed[i].Background = Brushes.Red;
                        stop = false;
                    }
                    else
                    {
                        LeftMatrixTransposed[i].Background = Brushes.White;
                        if (gb.StringIsDigits(LeftMatrixTransposed[i].Text.TrimEnd()) == false ||
                            Int32.Parse(LeftMatrixTransposed[i].Text) > pairs ||
                            Int32.Parse(LeftMatrixTransposed[i].Text) <= 0)
                        {
                            MessageBox.Show("Введены некорректные данные!");
                            LeftMatrixTransposed[i].Background = Brushes.Red;
                            stop = false;
                        }
                    }
                }    
            }

            // Проверка распределения мест.
            int p = 1; int index = 0; int dynamic = pairs;
            for (int j = 0; j < judges && stop == true; j++)
            {
                for (int c = 0; c < (pairs - 1) && stop == true; c++)
                {
                    for (; p < dynamic; p++)
                    {
                        if (LeftMatrixTransposed[index].Text == LeftMatrixTransposed[p].Text)
                        {
                            LeftMatrixTransposed[index].Background = Brushes.Red;
                            LeftMatrixTransposed[p].Background = Brushes.Red;
                            stop = false;
                        }
                    }
                    index++; p = index + 1;
                }
                index += (11 - pairs); dynamic += 10; p = index + 1;
                if (stop == false) { MessageBox.Show("Места должны быть разными!"); }
            }
            if (stop == true) { return true; } else { return false;}
        }

        private void Random_Click(object sender, RoutedEventArgs e)
        {
            for (int y = 0; y < 10; y++) { PlacesMatrix[y].Text = ""; }

            Random r = new Random(); int index = 0;
            for (int b = 0; b < judges; b++)
            {
                List<int> list = Enumerable.Range(1, pairs).OrderBy(j => r.Next()).ToList();
                for (int u = 0; u < pairs; u++)
                {
                    LeftMatrixTransposed[index + u].Text = list[u].ToString();
                }
                index += 10;
            }
            Calculate_Click(sender, e);
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void DataBaseClick(object sender, RoutedEventArgs e)
        {
            MainWindow mw = new MainWindow();
            mw.Show(); this.Hide();
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}