using DanceApp.Model.Data;
using DanceApp.Model;
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

namespace DanceApp.View
{
    /// <summary>
    /// Логика взаимодействия для CompetitionView.xaml
    /// </summary>
    #nullable disable
    public partial class MainWindow2 : Window
    {
        public DataBaseContext db = GlobalClass.db;
        public MainWindow2(string DBName)
        {
            InitializeComponent();
            this.Title = DBName;
            var data = db.Competitions.Find(1);
            if (data.Title == null || data.Title == "")
            {
                data.Title = DBName;
                try { db.SaveChanges(); }
                catch (Exception ex) { MessageBox.Show(ex.Message.ToString()); }
            }

            WindowState = WindowState.Maximized;
        }

        private void OnNavigate(object sender, ExecutedRoutedEventArgs e)
        {
            if (e.Parameter is Uri uri)
                MainFrame.Navigate(uri);
        }

        public void ClosedWindow()
        {
            this.Close();
            Application.Current.MainWindow.Close();
        }
    }
}
