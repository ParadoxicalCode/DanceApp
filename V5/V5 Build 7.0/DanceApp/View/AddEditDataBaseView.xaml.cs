using Microsoft.Extensions.FileSystemGlobbing.Internal.PathSegments;
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
using System.IO;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Reflection;
using Newtonsoft.Json;
using static System.Net.WebRequestMethods;
using Microsoft.EntityFrameworkCore;
using DanceApp.Model.Data;
using DanceApp.Model;

#nullable disable
namespace DanceApp.View
{
    /// <summary>
    /// Логика взаимодействия для AddEditDataBaseView.xaml
    /// </summary>
    public partial class AddEditDataBaseView : Window
    {
        public List<DataBases> DataBases22 = new List<Model.DataBases>();
        public string path;
        public bool addOrEdit;
        public string appDirectory = Directory.GetCurrentDirectory();
        public AddEditDataBaseView(string CurrentPath, string Title, List<Model.DataBases> db, bool AddOrEdit)
        {
            InitializeComponent();
            TitleTB.Text = Title;
            DataBases22 = db;
            path = CurrentPath;
            addOrEdit = AddOrEdit;
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (TitleTB.Text != "")
            {
                // Проверка базы данных на существование.
                bool fileExists = System.IO.File.Exists(System.IO.Path.Combine(appDirectory, TitleTB.Text + ".db"));
                if (fileExists == false)
                {
                    if (addOrEdit == false)
                    {
                        string newFileName = appDirectory + "\\" + TitleTB.Text + ".db";
                        string newFilePath = System.IO.Path.Combine(path, newFileName);

                        try
                        {
                            System.IO.File.Move(path, newFilePath);

                            MessageBox.Show("База данных успешно переименована!");
                            this.Close();
                        }
                        catch (Exception ex) { MessageBox.Show(ex.Message.ToString()); }
                    }
                    else
                    {
                        this.Close();

                        string connectionString = "Data Source=" + TitleTB.Text + ".db";
                        Json connect = new Json()
                        {
                            ConnectionString = connectionString
                        };

                        string serialized = JsonConvert.SerializeObject(connect);
                        System.IO.File.WriteAllText("AppSettings.json", serialized);

                        GlobalClass.db = new DataBaseContext();
                        GlobalClass.db.Database.CloseConnection(); // Отключение от базы данных
                    }
                }
                else { MessageBox.Show("Данное название уже занято!"); }
            }
            else { MessageBox.Show("Поле с названием базы данных не заполнено!"); }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
