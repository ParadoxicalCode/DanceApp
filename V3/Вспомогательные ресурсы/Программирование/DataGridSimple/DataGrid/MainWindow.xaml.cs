using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DataGrid
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        public MainWindow()
        {
            InitializeComponent();          
        }

        //Загрузка содержимого таблицы
        private void grid_Loaded(object sender, RoutedEventArgs e)
        {
            List<MyTable> result = new List<MyTable>(3);
            result.Add(new MyTable(1, "Майкл Джексон", "Thriller", 1982));
            result.Add(new MyTable(2, "AC/DC", "Back in Black", 1980));
            result.Add(new MyTable(3, "Bee Gees", "Saturday Night Fever", 1977));
            result.Add(new MyTable(4, "Pink Floyd", "The Dark Side of the Moon", 1973));
            grid.ItemsSource = result;
        }

        //Получаем данные из таблицы
        private void grid_MouseUp(object sender, MouseButtonEventArgs e)
        {
            MyTable path = grid.SelectedItem as MyTable;
            MessageBox.Show(" ID: " + path.Id + "\n Исполнитель: " + path.Vocalist + "\n Альбом: " + path.Album
                + "\n Год: " + path.Year);
        }
    }

    class MyTable
    {
        public MyTable(int Id, string Vocalist, string Album, int Year)
        {
            this.Id = Id;
            this.Vocalist = Vocalist;
            this.Album = Album;
            this.Year = Year;
        }
        public int Id { get; set; }
        public string Vocalist { get; set; }
        public string Album { get; set; }
        public int Year { get; set; }
    }

}
