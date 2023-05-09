using DanceApp.Model;
using DanceApp.Model.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

#nullable disable
namespace DanceApp.View
{
    /// <summary>
    /// Логика взаимодействия для GroupsView.xaml
    /// </summary>
    public partial class GroupsView : Page
    {
        public DataBaseContext db = GlobalClass.db;
        public GroupsView()
        {
            InitializeComponent();
            var data = db.Competitions.Where(u => u.ID == 1).FirstOrDefault();

            // Если изменяли данные в таблицах Pairs и Judges, то обновить список групп
            if (data.UpdateGroups == true )
                GetGroups();
            else
                GroupsDG.ItemsSource = db.Groups.ToList();
        }

        void GetGroups()
        {
            // Удаление всех групп
            var delete = db.Groups.ToList();
            db.Groups.RemoveRange(delete);
            try { db.SaveChanges(); }
            catch (Exception ex) { MessageBox.Show(ex.Message.ToString()); }

            var pairs = db.Pairs.ToList();
            int groupID = 0;
            string title;
            int groupNumber = 1;

            foreach (var p in pairs)
            {
                var groups = db.Groups.ToList();
                var c = db.Competitions.Where(u => u.ID == 1).FirstOrDefault();
                title = p.AgeCategory + p.PerformanceType;
                bool checkIsExist = false;

                foreach (var g in groups)
                {
                    if (title == g.Title)
                    {
                        checkIsExist = true;
                        groupID = g.ID;
                    }
                }

                // Создать группу, если такой нет
                if (checkIsExist == false) 
                {
                    Group g = new Group();
                    g.Title = title;
                    g.PairsCount += 1;
                    g.Number = groupNumber;

                    db.Groups.Add(g);
                    groupNumber++;
                }
                else
                {
                    var gr = db.Groups.Where(u => u.ID == groupID).FirstOrDefault();
                    gr.PairsCount += 1;
                }

                try { db.SaveChanges(); }
                catch (Exception ex) { MessageBox.Show(ex.Message.ToString()); }

                var x = db.Groups.Where(u => u.Title == title).FirstOrDefault();
                p.GroupID = x.ID;

                try { db.SaveChanges(); }
                catch (Exception ex) { MessageBox.Show(ex.Message.ToString()); }
            }
            GroupsDG.ItemsSource = db.Groups.ToList();

            // Запрещаем обновлять список групп, пока данные в таблице Pairs или Judges не изменятся
            var data = db.Competitions.Where(u => u.ID == 1).FirstOrDefault();
            data.UpdateGroups = false;
            try { db.SaveChanges(); }
            catch (Exception ex) { MessageBox.Show(ex.Message.ToString()); }
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            int ID = (int)((Button)sender).CommandParameter;
            AddEditGroupsView x = new AddEditGroupsView(ID);
            x.ShowDialog(); GetGroups();
        }

        public class PairsDGItems
        {
            public string Number { get; set; }
            public string MaleSurname { get; set; }
            public string MaleName { get; set; }
            public string MalePatronymic { get; set; }
            public string MaleBirthday { get; set; }
            public string FemaleSurname { get; set; }
            public string FemaleName { get; set; }
            public string FemalePatronymic { get; set; }
            public string FemaleBirthday { get; set; }
            public string Club { get; set; }
        }

        // Вывод всех пар в нижнюю таблицу, которые состоят в выбранной группе
        private void GroupsDG_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (GroupsDG.SelectedItem != null)
            {
                Group selectedGroup = GroupsDG.SelectedItem as Group;

                var query = from p in db.Pairs
                    where p.GroupID == selectedGroup.ID
                    select p;
                PairsDG.ItemsSource = query.ToList();
            }
        }
    }
}
