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
    /// Логика взаимодействия для PairsJudges.xaml
    /// </summary>
    public partial class PairsJudges : Window
    {
        public SkatingEntities db = new SkatingEntities();
        public PairsJudges()
        {
            InitializeComponent();
            DanceCB.ItemsSource = db.Dances.ToList();
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void Dancers_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mw = new MainWindow();
            mw.Show(); this.Hide();
        }

        private void DanceResult_Click(object sender, RoutedEventArgs e)
        {
            // Проверка на заполнение.
            if (NumberOfPairsCB.SelectedValue == null || NumberOfJudgesCB.SelectedValue == null || DanceCB.SelectedValue == null)
            {
                MessageBox.Show("Не все поля заполнены");
            }
            else
            {
                GlobalClass.NumberOfPairs = NumberOfPairsCB.Text;
                GlobalClass.NumberOfJudges = NumberOfJudgesCB.Text;
                DanceResult dr = new DanceResult();
                dr.Show(); this.Hide();
            }
        }
    }
}
