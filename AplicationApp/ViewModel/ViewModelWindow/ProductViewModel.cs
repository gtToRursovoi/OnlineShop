using AplicationApp.Services;
using AplicationApp.View.Windows.admin;
using AplicationApp.ViewModel.Other;
using Database.Context;
using Database.Service;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace AplicationApp.ViewModel.ViewModelWindow
{
    public class ProductViewModel : BaseViewModel, ISearchablePage
    {
        private readonly SqlServerDbContext _context;
        private bool _isEditMode;

        public int ProductId { get; set; } // Для редактирования

        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public string ImageUrl { get; set; }

        private Product _selectedProduct;
        public Product SelectedProduct
        {
            get => _selectedProduct;
            set
            {
                _selectedProduct = value;
                OnPropertyChanged();

                if (_selectedProduct != null)
                {
                    // Переключаемся в режим редактирования
                    _isEditMode = true;
                    ProductId = _selectedProduct.ProductId;
                    Name = _selectedProduct.Name;
                    Description = _selectedProduct.Description;
                    Price = _selectedProduct.Price;
                    Stock = _selectedProduct.Stock;
                    ImageUrl = _selectedProduct.ImageUrl;
                }
            }
        }

        public ObservableCollection<Product> Products { get; set; }
        public ObservableCollection<Product> AllProducts { get; set; } = new ObservableCollection<Product>();
        public ObservableCollection<Product> FilteredProducts { get; set; } = new ObservableCollection<Product>();


        public ICommand SaveCommand { get; set; }
        public ICommand CloseCommand { get; set; }
        public ICommand LoadProductsCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand SelectImageCommand { get; set; }

        public event Action<string> OnError;
        public event Action<string> OnSuccess;
        public event Action CloseWindow;

        public ProductViewModel(Product existingProduct = null)
        {
            _context = new SqlServerDbContext();
            Products = new ObservableCollection<Product>();

            if (existingProduct != null)
            {
                _isEditMode = true;
                ProductId = existingProduct.ProductId;
                Name = existingProduct.Name;
                Description = existingProduct.Description;
                Price = existingProduct.Price;
                Stock = existingProduct.Stock;
                ImageUrl = existingProduct.ImageUrl;
            }

            SaveCommand = new RelayCommand(SaveProductExecute);
            CloseCommand = new RelayCommand(obj => CloseWindow?.Invoke());
            LoadProductsCommand = new RelayCommand(LoadProducts); 
            DeleteCommand = new RelayCommand(DeleteProductExecute);
            SelectImageCommand = new RelayCommand(SelectImageExecute); 

            LoadProducts(null);
        }

        private void SaveProductExecute(object obj)
        {
            
            if (string.IsNullOrWhiteSpace(Name) || Price <= 0 || Stock < 0)
            {
                OnError?.Invoke("Пожалуйста, заполните все поля.");
                return;
            }

            if (_isEditMode)
            {
                var product = _context.Products.FirstOrDefault(p => p.ProductId == ProductId);
                if (product == null)
                {
                    OnError?.Invoke("Продукт не найден.");
                    return;
                }

                product.Name = Name;
                product.Description = Description;
                product.Price = Price;
                product.Stock = Stock;
                product.ImageUrl = ImageUrl; // Сохраняем путь к изображению

                _context.SaveChanges();
                OnSuccess?.Invoke("Продукт успешно обновлён.");
            }
            else
            {
                var newProduct = new Product
                {
                    Name = Name,
                    Description = Description,
                    Price = Price,
                    Stock = Stock,
                    ImageUrl = ImageUrl // Сохраняем путь к изображению
                };

                _context.Products.Add(newProduct);
                _context.SaveChanges();
                OnSuccess?.Invoke("Продукт успешно добавлен.");
                ClearFields();
            }
        }

        private void DeleteProductExecute(object obj)
        {
            if (SelectedProduct == null)
            {
                OnError?.Invoke("Выберите продукт для удаления.");
                return;
            }

            _context.Products.Remove(SelectedProduct);
            _context.SaveChanges();
            Products.Remove(SelectedProduct);
            SelectedProduct = null;

            OnSuccess?.Invoke("Продукт успешно удалён.");
            ClearFields();
        }

        private void ClearFields()
        {
            Name = string.Empty;
            Description = string.Empty;
            Price = 0;
            Stock = 0;
            ImageUrl = string.Empty;
        }

        private void LoadProducts(object obj)
        {
            var productsList = _context.Products.ToList();
            AllProducts.Clear();
            FilteredProducts.Clear();

            if (productsList != null)
            {
                foreach (var product in productsList)
                {
                    AllProducts.Add(product);
                    FilteredProducts.Add(product);
                }
            }
            else
            {
                OnError?.Invoke("Не удалось загрузить продукты.");
            }

            OnPropertyChanged(nameof(AllProducts));
            OnPropertyChanged(nameof(FilteredProducts));
        }

        // Метод для открытия диалога выбора изображения
        private void SelectImageExecute(object obj)
        {
            string selectedImagePath = OpenFileDialog();
            if (!string.IsNullOrEmpty(selectedImagePath))
            {
                ImageUrl = selectedImagePath;
            }
        }

        // Метод для открытия диалога выбора файла изображения
        private string OpenFileDialog()
        {
            OpenFileDialog dlg = new OpenFileDialog
            {
                DefaultExt = ".png",
                Filter = "Изображения (*.jpeg;*.png;*.jpg;*.gif)|*.jpeg;*.png;*.jpg;*.gif"
            };

            return dlg.ShowDialog() == true ? dlg.FileName : null;
        }
        public void ApplySearch(string query)
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                FilteredProducts = new ObservableCollection<Product>(AllProducts);
            }
            else
            {
                FilteredProducts = new ObservableCollection<Product>(
                    AllProducts.Where(p =>
                        p.Name.Contains(query, StringComparison.OrdinalIgnoreCase) ||
                        p.Description.Contains(query, StringComparison.OrdinalIgnoreCase)));
            }

            OnPropertyChanged(nameof(FilteredProducts));
        }
    }
}
