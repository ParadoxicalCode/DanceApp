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
    public partial class MainWindow2 : Window
    {
        public MainWindow2()
        {
            InitializeComponent();
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
