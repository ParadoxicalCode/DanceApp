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
        public DataBaseContext db = GlobalClass.db;
        public int Id;
        public AddEditGroupsView(int id)
        {
            InitializeComponent();
            Id = id;
            TypeOfPerformanceCB.SelectedValuePath = "TypeOfPerformanceId";

            if (Id != 0)
            {

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
            
        }
    }
}
