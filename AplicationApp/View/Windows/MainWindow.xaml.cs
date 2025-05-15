using AplicationApp.View.Pagess;
using AplicationApp.View.Pagess.Admin;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AplicationApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void OpenProductClick(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new ProductPage(MainFrame));
        }

        private void OpenOrdersClick(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new AdminOrderPage(MainFrame));
        }

        private void OpenUserClick(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new AdminUsersPage(MainFrame));  
        }
    }
}