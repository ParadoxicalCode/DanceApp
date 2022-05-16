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

namespace SkatingSystem.Forms
{
    /// <summary>
    /// Логика взаимодействия для Judges.xaml
    /// </summary>
    public partial class Judges : Window
    {
        public Judges()
        {
            InitializeComponent();
            TestDG.ItemsSource = SkatingEntities.GetContext().SudeyskiySostav.ToList();
            SkatingEntities db = new SkatingEntities();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            main.Show();
            Hide();
        }

    }
}
