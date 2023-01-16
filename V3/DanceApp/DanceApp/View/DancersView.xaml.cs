using System;
using System.Windows;
using System.Windows.Controls;
using DanceApp.View;

namespace DanceApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            WindowState = WindowState.Maximized;
        }

        private int rowCount = 0;
        private void Update()
        {
            
        }

        private void Home_Click(object sender, RoutedEventArgs e)
        {
            StartView form = new StartView();
            form.Show(); this.Close();
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

        private void DanceResultClick(object sender, RoutedEventArgs e)
        {
            
        }

        private void TableCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }
    }
}
