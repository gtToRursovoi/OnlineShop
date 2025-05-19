using AplicationApp.Services;
using AplicationApp.View.Pagess.User;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

        private void AnloginClick(object sender, RoutedEventArgs e)
        {
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Show();
            this.Close();
        }

        private void Hyperlink_RequestNavigate(object sender, System.Windows.Navigation.RequestNavigateEventArgs e)
        {
            System.Diagnostics.Process.Start(new ProcessStartInfo
            {
                FileName = e.Uri.AbsoluteUri,
                UseShellExecute = true
            });
            e.Handled = true;
        }

        private void OpenTGK(object sender, System.Windows.Navigation.RequestNavigateEventArgs e)
        {
            System.Diagnostics.Process.Start(new ProcessStartInfo
            {
                FileName = e.Uri.AbsoluteUri,
                UseShellExecute = true
            });
            e.Handled = true;
        }

        private void OpenProfilePage(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new UserProfilePage(MainFrame));
        }
        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string query = (sender as TextBox)?.Text ?? string.Empty;

            if (MainFrame.Content is Page page && page.DataContext is ISearchablePage searchableViewModel)
            {
                searchableViewModel.ApplySearch(query);
            }
        }
    }
}
