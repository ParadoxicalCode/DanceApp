using Skating.Forms;
using System;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace Skating
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public SkatingEntities db = new SkatingEntities();
        
        public MainWindow()
        {
            InitializeComponent();
            DG.ItemsSource = db.Dancers.ToList();
            TableCB.SelectedIndex = 0;
            Update();
        }

        void Form1_Load()
        {
            GlobalClass.DataChanged += new EventHandler(ApplicationEvents_DataChanged);
        }
        void ApplicationEvents_DataChanged(object sender, EventArgs e)
        {
            DG.ItemsSource = db.Dancers.ToList();
        }

        private int rowCount = 0;
        public void Update()
        {
            rowCount = db.Dancers.Count();
            numberOfEntries.Text = rowCount.ToString();
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void Image_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Hide_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            // Получаем id выбранной записи.
            Dancers path = DG.SelectedItem as Dancers;

            if (MessageBox.Show("Удалить запись?", "Уведомление", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                int id = path.DancerId;

                // Удаляем запись.
                var delete = db.Dancers.Where(u => u.DancerId.Equals(id)).FirstOrDefault();
                db.Dancers.Remove(delete);
                db.SaveChanges();

                // Обновляем таблицу.
                DG.ItemsSource = db.Dancers.ToList(); Update();
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            
            db.SaveChanges();
        }

        private void UpdateDataGrid(object sender, EventArgs e)
        {
            DG.ItemsSource = db.Dancers.ToList(); Update();
        }


        private void Add_Click(object sender, RoutedEventArgs e)
        {
            var da = new DancerAdd();
            da.ButtonClicked += UpdateDataGrid;
            da.Show();
        }

        private void DanceResultClick(object sender, RoutedEventArgs e)
        {
            PairsJudges pj = new PairsJudges();
            pj.Show(); this.Hide();
        }

        private void TableCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (TableCB.SelectedIndex)
            {
                case 1:
                    Club club = new Club();
                    club.Show(); this.Hide();
                    break;
                case 2:
                    Group group = new Group();
                    group.Show(); this.Hide();
                    break;
                case 3:
                    Judge judge = new Judge();
                    judge.Show(); this.Hide();
                    break;
                case 4:
                    Dance dance = new Dance();
                    dance.Show(); this.Hide();
                    break;
            }
        }

        private void TextBox_TextChanged(object sender, RoutedEventArgs e)
        {
            TextBox t = (TextBox)sender;
            string filter = t.Text;
            ICollectionView cv = CollectionViewSource.GetDefaultView(DG.ItemsSource);
            if (filter == "") cv.Filter = null;
            else
            {
                cv.Filter = o =>
                {
                    Dancers p = o as Dancers;
                    if (t.Name == "SearchT")
                        return (p.DancerId == Convert.ToInt32(filter));
                    return (p.Gender.Name.ToUpper().StartsWith(filter.ToUpper()));
                };
            }
        }
    }
}
