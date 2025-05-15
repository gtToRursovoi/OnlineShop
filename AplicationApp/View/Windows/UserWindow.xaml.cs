using AplicationApp.View.Pagess.User;
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

namespace AplicationApp.View.Windows
{
    /// <summary>
    /// Логика взаимодействия для UserWindow.xaml
    /// </summary>
    public partial class UserWindow : Window
    {
        public UserWindow()
        {
            InitializeComponent();
        }

        private void OpenPRoductPage(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new ProductPageUser(MainFrame));
        }

        private void OpenBasketPAge(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new basketPageUser(MainFrame));
        }

        private void OpenOrderClick(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new OrdersPageUser(MainFrame));
        }
    }
}
