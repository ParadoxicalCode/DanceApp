using DanceApp.Model;
using DanceApp.Model.Data;
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
    /// Логика взаимодействия для AddEditGroupsView.xaml
    /// </summary>
    public partial class AddEditGroupsView : Window
    {
        DataBaseContext db = new DataBaseContext();
        public int Id;
        public AddEditGroupsView(int id)
        {
            InitializeComponent();
            Id = id;
            TypeOfPerformanceCB.ItemsSource = db.TypesOfPerformance.ToList();
            TypeOfPerformanceCB.SelectedValuePath = "TypeOfPerformanceId";

            if (Id != 0)
            {
                var data = db.Groups.Find(Id);
                NumberTB.Text = data.Number.ToString();
                TitleTB.Text = data.Title.ToString();
                ProgramTB.Text = data.Program.ToString();
                NumberOfOutputsTB.Text = data.NumberOfOutputs.ToString();
                DancersCountTB.Text = data.DancersCount.ToString();

                var data2 = db.TypesOfPerformance.Find(data.ID);
                TypeOfPerformanceCB.SelectedValue = data2.ID;
            }
        }

        public class DGItems
        {
            public int GroupId { get; set; }
            public int CompetitionId { get; set; }
            public string Number { get; set; }
            public string Title { get; set; }
            public string Program { get; set; }
            public string NumberOfOutputs { get; set; }
            public string DancersCount { get; set; }
            public string TypeOfPerformance { get; set; }
        }

        private void AddEdit_Click(object sender, RoutedEventArgs e)
        {
            if (NumberTB.Text == "" || TitleTB.Text == "" || ProgramTB.Text == "" || NumberOfOutputsTB.Text == "" ||
                DancersCountTB.Text == "" || TypeOfPerformanceCB.SelectedItem == null)
            {
                MessageBox.Show("Не все поля заполнены!");
            }
            else
            {
                string number = NumberTB.Text.ToString();
                string title = TitleTB.Text.ToString();
                string program = ProgramTB.Text.ToString();
                string numberofoutputs = NumberOfOutputsTB.Text.ToString();
                string dancerscount = DancersCountTB.Text.ToString();
                var performanceType = TypeOfPerformanceCB.SelectedItem as TypeOfPerformance;

                bool checkIsExist = db.Groups.Any(x => x.Number == number && x.Title == title);
                if (checkIsExist == true)
                {
                    MessageBox.Show("Группа с таким номером и названием уже есть!");
                }
                else
                {
                    if (Id == 0)
                    {
                        Group g = new Group();
                        g.Number = number;
                        g.Title = title;
                        g.Program = program;
                        g.NumberOfOutputs = numberofoutputs;
                        g.DancersCount = dancerscount;
                        g.ID = performanceType.ID;

                        db.Groups.Add(g);
                        try
                        {
                            db.SaveChanges();
                            MessageBox.Show("Запись добавлена!");
                        }
                        catch (Exception ex) { MessageBox.Show(ex.Message.ToString()); }
                    }
                    else
                    {
                        var data = db.Groups.Where(u => u.ID == Id).FirstOrDefault();
                        data.Number = number;
                        data.Title = title;
                        data.Program = program;
                        data.NumberOfOutputs = numberofoutputs;
                        data.DancersCount = dancerscount;
                        data.TypeOfPerformanceId = performanceType.ID;
                        try
                        {
                            db.SaveChanges();
                            MessageBox.Show("Запись изменена!");
                            this.Close();
                        }
                        catch (Exception ex) { MessageBox.Show(ex.Message.ToString()); }
                    }
                }
            }
        }
    }
}
