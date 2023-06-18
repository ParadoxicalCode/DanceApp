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
        }

        private void GetGroupsAndPerformances()
        {
            // Нам надо найти все группы в туре, затем все заходы в группе

        }

        private void NextBtn_Click(object sender, RoutedEventArgs e)
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
    }
}
