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
    /// Логика взаимодействия для MessageBoxView.xaml
    /// </summary>
    public partial class MessageBoxView : Window
    {
        public MessageBoxView(string Message, string Title, int buttonCount)
        {
            InitializeComponent();
            MessageTBK.Text = Message;
            this.Title = Title;

            if (buttonCount == 1)
            {
                YesBtn.HorizontalAlignment = HorizontalAlignment.Center;
                YesBtn.Content = "Ок";
                YesBtn.Margin = new Thickness(0, 0, 0, 0);
                NoBtn.Visibility= Visibility.Hidden;
            }
        }

        private void Yes_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        private void No_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        /*
        MyMessageBox messageBox = new MyMessageBox("Вы действительно хотите продолжить?");
        if (messageBox.ShowDialog() == true)
        {
            // пользователь нажал "ОК"
        }
        else
        {
            // пользователь нажал "Отмена"
        }

        */
    }
}
