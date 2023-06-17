using DanceApp.Model;
using DanceApp.Model.Data;
using DanceApp.View;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;

#nullable disable
namespace DanceApp
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<DataBases> DataBases = new List<Model.DataBases>();
        public bool addOrEdit;
        public string appDirectory = Directory.GetCurrentDirectory();
        public MainWindow()
        {
            InitializeComponent();
            GetFiles();
        }

        // Вывод в DataGrid списка баз данных
        void GetFiles()
        {
            string[] allfiles = Directory.GetFiles(appDirectory, "*.db");
            DataBases.Clear();

            for (int i = 0; i < allfiles.Length; i++)
            {
                string title = Path.GetFileNameWithoutExtension(allfiles[i]);

                long length = new FileInfo(allfiles[i]).Length;
                string size;
                if (length >= 1024 && length < 1048576)
                {
                    length /= 1024;
                    size = length + " Кб";
                }
                else
                {
                    length = length / 1048576;
                    size = length + " Мб";
                }
                string path = allfiles[i];

                if (DataBases.Any(x => x.Title == title) == false)
                {
                    DataBases.Add(new DataBases { Title = title, Size = size, Path = path });
                }
            }
            DG.ItemsSource = DataBases.ToList();
        }

        private void Select_Click(object sender, RoutedEventArgs e)
        {
            string title = (string)((Button)sender).CommandParameter;
            GlobalClass.dataBaseName = title;
            var connectionString = "Data Source=" + title + ".db";

            Json connect = new Json()
            {
                ConnectionString = connectionString
            };
            string serialized = JsonConvert.SerializeObject(connect);
            File.WriteAllText("AppSettings.json", serialized);

            GlobalClass.db = new DataBaseContext();

            MainWindow2 window = new MainWindow2(title);
            window.Show(); this.Close();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            AddEditDataBaseView window = new AddEditDataBaseView("", "", false);
            window.ShowDialog(); GetFiles();
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            string title = (string)((Button)sender).CommandParameter;
            int index = DataBases.FindIndex(c => c.Title == title);
            string path = DataBases[index].Path;

            AddEditDataBaseView window = new AddEditDataBaseView(path, title, true);
            window.ShowDialog(); GetFiles();
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            string title = (string)((Button)sender).CommandParameter;
            var connectionString = "Data Source=" + title + ".db";

            Json connect = new Json()
            {
                ConnectionString = connectionString
            };
            string serialized = JsonConvert.SerializeObject(connect);

            File.WriteAllText("AppSettings.json", serialized);
            GlobalClass.db = new DataBaseContext();

            if (MessageBox.Show("Вы действительно хотите удалить базу данных?", "Уведомление", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    GlobalClass.db.Database.CloseConnection();
                    GlobalClass.db.Database.EnsureDeleted();
                    GetFiles();
                }
                catch
                {
                    MessageBox.Show("База данных используется приложением!");
                }
            }
        }

        private void OpenDirectory_Click(object sender, RoutedEventArgs e)
        {
            // Узнаём путь до папки проекта.
            string appDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            // Открываем папку проекта в проводнике.
            Process.Start("explorer.exe", appDirectory);
            GetFiles();
        }

        private void Info_Click(object sender, RoutedEventArgs e)
        {
            InfoView form = new InfoView();
            form.ShowDialog();
        }
    }
}