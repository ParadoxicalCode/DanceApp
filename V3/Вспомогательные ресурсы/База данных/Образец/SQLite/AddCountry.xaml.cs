using SQLite.Data;
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

namespace SQLite
{
    /// <summary>
    /// Логика взаимодействия для AddCountry.xaml
    /// </summary>
    public partial class AddCountry : Window
    {
        Data.AppContext db = new Data.AppContext();
        public AddCountry()
        {
            InitializeComponent();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            if (CountryTB.Text == "")
            {
                MessageBox.Show("Не все поля заполнены!");
            }
            else
            {
                string title = CountryTB.Text.ToString();
                bool checkIsExist = db.Countries.Any(x => x.Title == title);

                if (checkIsExist == true)
                {
                    MessageBox.Show("Такая страна уже есть!");
                }
                else
                {
                    Country c = new Country()
                    {
                        Title = title
                    };

                    db.Countries.Add(c);

                    try
                    {
                        db.SaveChanges();
                        MessageBox.Show("Запись добавлена!");
                    }
                    catch (Exception ex) { MessageBox.Show(ex.Message.ToString()); }
                }
            }
        }
    }
}
