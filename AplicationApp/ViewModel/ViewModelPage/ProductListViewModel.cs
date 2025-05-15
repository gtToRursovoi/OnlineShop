using AplicationApp.Services;
using AplicationApp.ViewModel.Other;
using Database.Context;
using Database.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AplicationApp.ViewModel.ViewModelPage
{
    public class ProductListViewModel : BaseViewModel
    {
        private readonly SqlServerDbContext _context;

        public ObservableCollection<Product> Products { get; set; }
        public Dictionary<int, int> ProductQuantities { get; set; } = new(); // <ProductId, Quantity>
        public ICommand AddToCartCommand { get; }

        public ProductListViewModel()
        {
            _context = new SqlServerDbContext();
            Products = new ObservableCollection<Product>(_context.Products.ToList());

            // Заполнить словарь начальными значениями (1 по умолчанию)
            foreach (var product in Products)
            {
                ProductQuantities[product.ProductId] = 1;
            }

            AddToCartCommand = new RelayCommand(AddToCart);
        }



        private void AddToCart(object obj)
        {
            if (obj is Product product)
            {
                int userId = SessionService.CurrentUserId;
                int quantity = ProductQuantities.ContainsKey(product.ProductId) ? ProductQuantities[product.ProductId] : 1;

                if (quantity <= 0)
                {
                    return; // Нельзя добавить 0 или меньше
                }

                var existingItem = _context.CartItems.FirstOrDefault(c =>
                    c.UserId == userId && c.ProductId == product.ProductId);

                if (existingItem != null)
                {
                    existingItem.Quantity += quantity;
                }
                else
                {
                    var cartItem = new CartItem
                    {
                        UserId = userId,
                        ProductId = product.ProductId,
                        Quantity = quantity
                    };

                    _context.CartItems.Add(cartItem);
                }

                _context.SaveChanges();
            }
        }
    }
}
