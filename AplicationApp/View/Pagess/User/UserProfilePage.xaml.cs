using AplicationApp.ViewModel.ViewModelPage;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AplicationApp.View.Pagess.User
{
    /// <summary>
    /// Логика взаимодействия для UserProfilePage.xaml
    /// </summary>
    public partial class UserProfilePage : Page
    {
        UserProfileViewModel viewModel;
        Frame MainFrame;
        public UserProfilePage(Frame mainFrame)
        {
            InitializeComponent();
            viewModel = new UserProfileViewModel();
            this.DataContext = viewModel;
            MainFrame = mainFrame;
        }
    }
}
