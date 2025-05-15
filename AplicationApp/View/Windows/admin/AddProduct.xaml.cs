using AplicationApp.ViewModel.ViewModelWindow;
using Database.Service;
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

namespace AplicationApp.View.Windows.admin
{
    /// <summary>
    /// Логика взаимодействия для AddProduct.xaml
    /// </summary>
    public partial class AddProduct : Window
    {
        private readonly ProductViewModel viewModel;

        public AddProduct(Product productToEdit = null)
        {
            InitializeComponent();
            viewModel = new ProductViewModel(productToEdit);
            this.DataContext = viewModel;

        }
    }
}
