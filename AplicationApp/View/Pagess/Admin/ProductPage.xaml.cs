using AplicationApp.Services;
using AplicationApp.View.Windows.admin;
using AplicationApp.ViewModel.ViewModelWindow;
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
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace AplicationApp.View.Pagess
{
    /// <summary>
    /// Логика взаимодействия для ProductPage.xaml
    /// </summary>
    public partial class ProductPage : UserControl, ISearchablePage
    {
        Frame MainFrame;
        ProductViewModel viewModel;
        public ProductPage(Frame mainFrame)
        {
            InitializeComponent();
            MainFrame = mainFrame;
            viewModel = new ProductViewModel();
            this.DataContext = viewModel;
        }

        public void ApplySearch(string query)
        {
            if (DataContext is ProductViewModel vm)
            {
                vm.ApplySearch(query);
            }
        }

        private void AddProductButton_Click(object sender, RoutedEventArgs e)
        {
            var addWindow = new AddProduct(); // передаём null — значит добавление
            bool? result = addWindow.ShowDialog();

           
        }
        private void EditProductButton_Click(object sender, RoutedEventArgs e)
        {
            if (viewModel.SelectedProduct != null)
            {
                var editWindow = new AddProduct(viewModel.SelectedProduct);
                bool? result = editWindow.ShowDialog();

               
            }
            else
            {
                MessageBox.Show("Сначала выберите товар для редактирования.");
            }
        }


    }
}
