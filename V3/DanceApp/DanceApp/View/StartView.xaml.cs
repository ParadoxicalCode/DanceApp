using System;
using System.Windows;
using System.Windows.Controls;
using DanceApp.View;


namespace DanceApp.View
{
    /// <summary>
    /// Логика взаимодействия для StartView.xaml
    /// </summary>
    public partial class StartView : Window
    {
        public StartView()
        {
            InitializeComponent();
            WindowState = WindowState.Maximized;
        }

        private int rowCount = 0;
        private void Update()
        {

        }

        private void Form1_Load()
        {
            Model.GlobalClass.DataChanged += new EventHandler(ApplicationEvents_DataChanged);
        }

        private void ApplicationEvents_DataChanged(object sender, EventArgs e)
        {

        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {

        }

        private void UpdateDataGrid(object sender, EventArgs e)
        {

        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void Next_Click(object sender, RoutedEventArgs e)
        {
            MainWindow form = new MainWindow();
            form.Show(); this.Close();
        }

        private void Info_Click(object sender, RoutedEventArgs e)
        {
            InfoView form = new InfoView();
            form.Show();
        }

        private void DanceResultClick(object sender, RoutedEventArgs e)
        {

        }

        private void TableCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
