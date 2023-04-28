using DanceApp.Model;
using DanceApp.View;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
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
namespace DanceApp
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<Model.DataBases> DataBases = new List<Model.DataBases>();
        public bool addOrEdit;
        public MainWindow()
        {
            InitializeComponent();
            DatabaseFacade facade = new DatabaseFacade(new Model.Data.DataBaseContext());
            facade.EnsureCreated();

            GetFiles();
            DG.ItemsSource = DataBases.ToList();
        }

        void GetFiles()
        {
            string PathToFolder = Directory.GetCurrentDirectory();
            string[] allfiles = Directory.GetFiles(PathToFolder, "*.db");

            DataBases.Clear();
            for (int i = 0; i < allfiles.Length; i++)
            {
                string title = System.IO.Path.GetFileNameWithoutExtension(allfiles[i]);

                long length = new FileInfo(allfiles[i]).Length;
                string size;
                if (length >= 1024 && length < 1048576)
                {
                    length /= 1024;
                    size = length + "КБ";
                }
                else if (length > 1048575 && length < 1073741824)
                {
                    length = length / 1048576;
                    size = length + "МБ";
                }
                else
                {
                    length = length / 1073741824;
                    size = length + "ГБ";
                }

                string path = allfiles[i];

                if (DataBases.Any(x => x.Title == title) == false)
                {
                    DataBases.Add(new Model.DataBases { Title = title, Size = size, Path = path });
                }
                DG.ItemsSource = DataBases.ToList();
            }
        }

        private void Next_Click(object sender, RoutedEventArgs e)
        {
            // Узнаём путь до папки проекта.
            string appDirectory = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            // Узнаём путь до базы данных.
            string title = (string)((Button)sender).CommandParameter;
            string[] files = Directory.GetFiles(appDirectory, title + ".db", SearchOption.AllDirectories);

            if (files.Length > 0)
            {
                if (files.Length > 0)
                {
                    string DBPath = files[0];
                    var connectionString = "Data Source=" + DBPath;

                    Json connect = new Json()
                    {
                        ConnectionString = connectionString
                    };

                    string serialized = JsonConvert.SerializeObject(connect);

                    File.WriteAllText("AppSettings.json", serialized);
                }

                CompetitionView window = new CompetitionView();
                window.Show(); this.Close();
            }
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            AddEditDataBaseView window = new AddEditDataBaseView("", "", null, true);
            window.ShowDialog(); GetFiles();
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            string title = (string)((Button)sender).CommandParameter;
            int index = DataBases.FindIndex(c => c.Title == title);
            string path = DataBases[index].Path;

            AddEditDataBaseView window = new AddEditDataBaseView(path, title, DataBases, false);
            window.ShowDialog();
            GetFiles();
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            string title = (string)((Button)sender).CommandParameter;
            int index = DataBases.FindIndex(c => c.Title == title);
            string path = DataBases[index].Path;

            if (File.Exists(path))
            {
                if (MessageBox.Show("Вы действительно хотите удалить базу данных?", "Уведомление", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    try
                    {
                        File.Delete(path);
                        MessageBox.Show("База данных успешно удалена!");
                    }
                    catch (Exception ex) { MessageBox.Show(ex.Message.ToString()); }
                }
            }
            else { MessageBox.Show("Такой базы данных не существует!"); }
            GetFiles();
        }

        private void OpenDirectory_Click(object sender, RoutedEventArgs e)
        {
            // Узнаём путь до папки проекта.
            string appDirectory = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            // Открываем папку проекта в проводнике.
            Process.Start("explorer.exe", appDirectory);
        }

        private void Info_Click(object sender, RoutedEventArgs e)
        {
            InfoView form = new InfoView();
            form.ShowDialog();
        }
    }
}