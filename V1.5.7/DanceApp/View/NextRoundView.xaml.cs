using DanceApp.Model;
using DanceApp.Model.Data;
using Microsoft.Win32;
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

#nullable disable
namespace DanceApp.View
{
    /// <summary>
    /// Логика взаимодействия для NextRoundView.xaml
    /// </summary>
    public partial class NextRoundView : Window
    {
        List<Judge> SelectedJudges;
        public DataBaseContext db = GlobalClass.db;
        int RoundID;
        public NextRoundView(int roundID, List<Judge> selectedJudges)
        {
            InitializeComponent();
            RoundID = roundID;
            RoundText.Text = db.Round.Find(roundID).Title;
            SelectedJudges = selectedJudges;

            GroupCB.ItemsSource = db.Group.Where(x => x.RoundID == roundID).ToList();
            GroupCB.SelectedIndex = 0;
        }

        private void GetGroupsAndPerformances()
        {
            // Нам надо найти все группы в туре, затем все заходы в группе

        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            GetGroupsAndPerformances();

            // Нужно сохранять каждый раз, как  нажимаем на далее
        }

        private void Document2_Click(object sender, RoutedEventArgs e)
        {
            var fraction = db.Competition.Find(1).Fraction;

            var dialog = new OpenFileDialog
            {
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                Title = "Укажите путь для сохранения PDF файла",
                Filter = "Все папки (*.*)|*.*",
                CheckFileExists = false,
                CheckPathExists = true,
                FileName = "Протокол №2" + " (" + RoundText.Text + ")" + ".PDF"
            };

            // Теперь надо пролистать все группы в туре и все заходы в группе и передать в метод протокол2

            if (dialog.ShowDialog() == true)
            {
                //new CreatePDF().Protocol2(dialog.FileName, SelectedJudges, RoundText.Text);
            }
        }

        private void FinalDocument_Click(object sender, RoutedEventArgs e)
        {
            var GroupID = (GroupCB.SelectedItem as Group).ID;
            var GroupName = db.Group.Find(GroupID).Title;
            var RoundName = db.Round.Find(RoundID).Title;

            var dialog = new OpenFileDialog
            {
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                Title = "Укажите путь для сохранения PDF файла",
                Filter = "Все папки (*.*)|*.*",
                CheckFileExists = false,
                CheckPathExists = true,
                FileName = "Финальный отчёт" + " (" + RoundName + ", " + GroupName + ")" + ".PDF"
            };

            // Теперь надо пролистать все группы в туре и все заходы в группе и передать в метод протокол2

            if (dialog.ShowDialog() == true)
            {
                new CreatePDF().FinalProtocol(dialog.FileName, RoundID, GroupID);
            }
        }

        private void JudgeCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void GroupCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void PerformanceCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void GroupsDG_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void DancesChB_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void DancesChB_Unchecked(object sender, RoutedEventArgs e)
        {

        }

        private void Table_Click(object sender, RoutedEventArgs e)
        {
            Button clickedButton = (Button)sender;
            string buttonName = clickedButton.Name;

            if (clickedButton.Background.ToString() == "#FF8F8F8F")
            {
                clickedButton.Background = new SolidColorBrush(Color.FromArgb(0xFF, 0x00, 0xBF, 0x1D));
            }
            else
            {
                clickedButton.Background = new SolidColorBrush(Color.FromArgb(0xFF, 0x8F, 0x8F, 0x8F));
            }
        }
    }
}
