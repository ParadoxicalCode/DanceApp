﻿using Microsoft.Extensions.FileSystemGlobbing.Internal.PathSegments;
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
using static DanceApp.View.PairsView;
using System.Globalization;
using System.Windows.Markup;

#nullable disable
namespace DanceApp.View
{
    /// <summary>
    /// Логика взаимодействия для AddEditDataBaseView.xaml
    /// </summary>
    #nullable disable
    public partial class AddEditDataBaseView : Window
    {
        public DataBaseContext db = GlobalClass.db;
        public string path;
        public bool Edit;
        public string appDirectory = Directory.GetCurrentDirectory();
        public AddEditDataBaseView(string CurrentPath, string Title, bool edit)
        {
            InitializeComponent();

            if (edit == true)
                TitleTB.Text = Title;

            path = CurrentPath;
            Edit = edit;
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (TitleTB.Text != "")
            {
                // Проверка базы данных на существование.
                bool fileExists = System.IO.File.Exists(System.IO.Path.Combine(appDirectory, TitleTB.Text + ".db"));
                if (fileExists == false)
                {
                    if (Edit == true)
                    {
                        string newFileName = appDirectory + "\\" + TitleTB.Text + ".db";
                        string newFilePath = System.IO.Path.Combine(path, newFileName);
                        try
                        {
                            System.IO.File.Move(path, newFilePath);

                            Connect();

                            var data = GlobalClass.db.Competition.Find(1);
                            data.Title = TitleTB.Text;
                            try
                            {
                                GlobalClass.db.SaveChanges();
                                MessageBox.Show("База данных успешно переименована!");
                            }
                            catch (Exception ex) { MessageBox.Show(ex.InnerException.Message); }
                        }
                        catch (Exception ex) { throw new Exception("Чтобы переименовать базу данных перезапустите приложение!", ex); }
                    }
                    else
                        Connect();

                    this.Close();
                }
                else { MessageBox.Show("Данное название уже занято!"); }
            }
            else { MessageBox.Show("Поле с названием базы данных не заполнено!"); }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Connect()
        {
            string connectionString = "Data Source=" + TitleTB.Text + ".db";
            Json connect = new Json()
            {
                ConnectionString = connectionString
            };

            string serialized = JsonConvert.SerializeObject(connect);
            System.IO.File.WriteAllText("AppSettings.json", serialized);

            GlobalClass.db = new DataBaseContext();
        }
    }
}