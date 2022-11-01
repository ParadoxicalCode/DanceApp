using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Skating.Forms
{
    /// <summary>
    /// Логика взаимодействия для DanceResult.xaml
    /// </summary>
    public partial class DanceResult : Window
    {
        public SkatingEntities db = new SkatingEntities();
        public List<TextBox> Matrix;
        public List<TextBox> TransposedMatrix;
        public GlobalClass gb = new GlobalClass();
        public DanceResult()
        {
            InitializeComponent();

            Matrix = new List<TextBox>() 
            {
                TB001, TB011, TB021, TB031, TB041, TB051, TB061,    /*6*/     TB071, TB081, TB091, TB101, TB111, TB121, TB131, TB141, TB151, TB161,   TB171, /*17*/
                TB002, TB012, TB022, TB032, TB042, TB052, TB062,    /*24*/    TB072, TB082, TB092, TB102, TB112, TB122, TB132, TB142, TB152, TB162,   TB172, /*35*/
                TB003, TB013, TB023, TB033, TB043, TB053, TB063,    /*42*/    TB073, TB083, TB093, TB103, TB113, TB123, TB133, TB143, TB153, TB163,   TB173, /*53*/
                TB004, TB014, TB024, TB034, TB044, TB054, TB064,    /*60*/    TB074, TB084, TB094, TB104, TB114, TB124, TB134, TB144, TB154, TB164,   TB174, /*71*/
                TB005, TB015, TB025, TB035, TB045, TB055, TB065,    /*78*/    TB075, TB085, TB095, TB105, TB115, TB125, TB135, TB145, TB155, TB165,   TB175, /*89*/
                TB006, TB016, TB026, TB036, TB046, TB056, TB066,    /*96*/    TB076, TB086, TB096, TB106, TB116, TB126, TB136, TB146, TB156, TB166,   TB176, /*107*/
                TB007, TB017, TB027, TB037, TB047, TB057, TB067,    /*114*/   TB077, TB087, TB097, TB107, TB117, TB127, TB137, TB147, TB157, TB167,   TB177, /*125*/
                TB008, TB018, TB028, TB038, TB048, TB058, TB068,    /*132*/   TB078, TB088, TB098, TB108, TB118, TB128, TB138, TB148, TB158, TB168,   TB178, /*143*/
                TB009, TB019, TB029, TB039, TB049, TB059, TB069,    /*150*/   TB079, TB089, TB099, TB109, TB119, TB129, TB139, TB149, TB159, TB169,   TB179, /*161*/
                TB010, TB020, TB030, TB040, TB050, TB060, TB070,    /*168*/   TB080, TB090, TB100, TB110, TB120, TB130, TB140, TB150, TB160, TB170,   TB180  /*179*/
            };

            TransposedMatrix = new List<TextBox>()
            {
                TB001, TB002, TB003, TB004, TB005, TB006, TB007, TB008, TB009, TB010, /*9*/
                TB011, TB012, TB013, TB014, TB015, TB016, TB017, TB018, TB019, TB020, /*19*/
                TB021, TB022, TB023, TB024, TB025, TB026, TB027, TB028, TB029, TB030, /*29*/
                TB031, TB032, TB033, TB034, TB035, TB036, TB037, TB038, TB039, TB040, /*39*/
                TB041, TB042, TB043, TB044, TB045, TB046, TB047, TB048, TB049, TB050, /*49*/
                TB051, TB052, TB053, TB054, TB055, TB056, TB057, TB058, TB059, TB060, /*59*/
                TB061, TB062, TB063, TB064, TB065, TB066, TB067, TB068, TB069, TB070, /*69*/

                TB071, TB072, TB073, TB074, TB075, TB076, TB077, TB078, TB079, TB080,
                TB081, TB082, TB083, TB084, TB085, TB086, TB087, TB088, TB089, TB090,
                TB091, TB092, TB093, TB094, TB095, TB096, TB097, TB098, TB099, TB100,
                TB101, TB102, TB103, TB104, TB105, TB106, TB107, TB108, TB109, TB110,
                TB111, TB112, TB113, TB114, TB115, TB116, TB117, TB118, TB119, TB120,
                TB121, TB122, TB123, TB124, TB125, TB126, TB127, TB128, TB129, TB130,
                TB131, TB132, TB133, TB134, TB135, TB136, TB137, TB138, TB139, TB140,
                TB141, TB142, TB143, TB144, TB145, TB146, TB147, TB148, TB149, TB150,
                TB151, TB152, TB153, TB154, TB155, TB156, TB157, TB158, TB159, TB160,
                TB161, TB162, TB163, TB164, TB165, TB166, TB167, TB168, TB169, TB170,
                TB171, TB172, TB173, TB174, TB175, TB176, TB177, TB178, TB179, TB180
            };

            switch (GlobalClass.NumberOfPairs)
            {
                case "3": int index = 54; NotActiveTBHorizontal(index); break;
                case "4": index = 72; NotActiveTBHorizontal(index); break;
                case "5": index = 90; NotActiveTBHorizontal(index); break;
                case "6": index = 108; NotActiveTBHorizontal(index); break;
                case "7": index = 126; NotActiveTBHorizontal(index); break;
                case "8": index = 144; NotActiveTBHorizontal(index); break;
                case "9": index = 162; NotActiveTBHorizontal(index); break;
            }

            switch (GlobalClass.NumberOfJudges)
            {
                case "3": int index = 30; NotActiveTBVertical(index); break;
                case "5": index = 50; NotActiveTBVertical(index); break;
            }
        }

        int pairs = int.Parse(GlobalClass.NumberOfPairs);
        int judges = int.Parse(GlobalClass.NumberOfJudges);

        private void NotActiveTBHorizontal(int index)
        {
            for (; index < 180; index++)
            {
                //Matrix[index].IsEnabled = false;
                Matrix[index].Visibility = Visibility.Hidden;
            }
        }
        private void NotActiveTBVertical(int index)
        {
            for (; index < 70; index++)
            {
                //Matrix[index].IsEnabled = false;
                TransposedMatrix[index].Visibility = Visibility.Hidden;
            }
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Hide_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void DanceResultClick(object sender, RoutedEventArgs e)
        {
            Matrix[0].Background = Brushes.Red;
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

        private void Random_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}



