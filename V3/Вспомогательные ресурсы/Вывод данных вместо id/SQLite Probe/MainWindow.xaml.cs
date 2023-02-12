using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Linq;

namespace SQLite_Probe
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly ProductContext db = new ProductContext();

        private CollectionViewSource ProductViewSource;

        public MainWindow()
        {
            InitializeComponent();
            //ProductViewSource = (CollectionViewSource)FindResource(nameof(ProductViewSource));
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            var query =
                from p in db.Products
                join c in db.Categories on p.ProductId equals c.CategoryId
                where p.ProductId != null
                select p;

            productsDataGrid.ItemsSource = query.ToList();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // all changes are automatically tracked, including
            // deletes!
            db.SaveChanges();

            // this forces the grid to refresh to latest values
            productsDataGrid.Items.Refresh();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            // clean up database connections
            db.Dispose();
            base.OnClosing(e);
        }
    }
}