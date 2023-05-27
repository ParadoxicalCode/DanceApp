using System.Linq;
using System.Windows;

namespace Probe
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ProductDbContext dbContext;
        Product NewProduct = new Product();

        public MainWindow(ProductDbContext dbContext)
        {
            this.dbContext = dbContext;
            InitializeComponent();
            GetProducts();

            AddNewProductGrid.DataContext = NewProduct;
        }

        private void GetProducts()
        {
            ProductDG.ItemsSource = dbContext.Products.ToList();
        }

        private void AddProduct(object s, RoutedEventArgs e)
        {
            dbContext.Products.Add(NewProduct);
            dbContext.SaveChanges();
            GetProducts();
            NewProduct = new Product();
            AddNewProductGrid.DataContext = NewProduct;
        }

        Product selectedProduct = new Product();
        private void UpdateProductForEdit(object s, RoutedEventArgs e)
        {
            selectedProduct = (s as FrameworkElement).DataContext as Product;
            UpdateProductGrid.DataContext = selectedProduct;
        }

        private void UpdateProduct(object s, RoutedEventArgs e)
        {
            dbContext.Update(selectedProduct);
            dbContext.SaveChanges();
            GetProducts();
        }

        private void DeleteProduct(object s, RoutedEventArgs e)
        {
            var productToBeDeleted = (s as FrameworkElement).DataContext as Product;
            dbContext.Products.Remove(productToBeDeleted);
            dbContext.SaveChanges();
            GetProducts();
        }
    }
}
