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

namespace AplicationApp.View.Pagess.Admin
{
    /// <summary>
    /// Логика взаимодействия для AdminOrderPage.xaml
    /// </summary>
    public partial class AdminOrderPage : UserControl
    {
        AdminOrdersViewModel viewModel;
        Frame MainFrame;
        public AdminOrderPage(Frame mainFrame)
        {
            InitializeComponent();
            MainFrame = mainFrame;
            viewModel = new AdminOrdersViewModel();
            this.DataContext = viewModel;
        }
    }
}
