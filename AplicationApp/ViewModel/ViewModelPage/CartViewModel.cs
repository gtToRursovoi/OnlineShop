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
using System.Windows;
using System.Windows.Input;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AplicationApp.ViewModel.ViewModelPage
{
    public class CartViewModel : BaseViewModel
    {
        private readonly SqlServerDbContext _context;

        public ObservableCollection<CartItem> CartItems { get; set; }

        public decimal TotalPrice => CartItems.Sum(item => item.Product.Price * item.Quantity);

        public ICommand IncreaseCommand { get; }
        public ICommand DecreaseCommand { get; }
        public ICommand RemoveItemCommand { get; }
        public ICommand IncreaseQuantityCommand { get; }
        public ICommand DecreaseQuantityCommand { get; }
        public ICommand PlaceOrderCommand { get; }

        public CartViewModel()
        {
            _context = new SqlServerDbContext();
            CartItems = new ObservableCollection<CartItem>();

            IncreaseCommand = new RelayCommand(IncreaseQuantity);
            DecreaseCommand = new RelayCommand(DecreaseQuantity);
            RemoveItemCommand = new RelayCommand(RemoveCartItem);
            IncreaseQuantityCommand = new RelayCommand(IncreaseCartQuantity);
            DecreaseQuantityCommand = new RelayCommand(DecreaseCartQuantity);
            PlaceOrderCommand = new RelayCommand(PlaceOrder);

            LoadCartItems();
        }
        private void IncreaseCartQuantity(object obj)
        {
            if (obj is CartItem item)
            {
                item.Quantity++;
                UpdateAndSave(item);
            }
        }

        private void DecreaseCartQuantity(object obj)
        {
            if (obj is CartItem item && item.Quantity > 1)
            {
                item.Quantity--;
                UpdateAndSave(item);
            }
        }

        private void RemoveCartItem(object obj)
        {
            if (obj is CartItem item)
            {
                var dbItem = _context.CartItems.FirstOrDefault(ci => ci.CartItemId == item.CartItemId);
                if (dbItem != null)
                {
                    _context.CartItems.Remove(dbItem);
                    _context.SaveChanges();
                    CartItems.Remove(item);

                    OnPropertyChanged(nameof(CartItems));
                    OnPropertyChanged(nameof(TotalPrice));
                }
            }
        }

        private void PlaceOrder(object obj)
        {
            if (!CartItems.Any())
            {
                MessageBox.Show("Корзина пуста.");
                return;
            }

            int userId = SessionService.CurrentUserId;

            // Создаем новый заказ
            var order = new Order
            {
                UserId = userId,
                OrderDate = DateTime.Now,
                Status = "processing",
                TotalAmount = TotalPrice,
                OrderItems = CartItems.Select(ci => new OrderItem
                {
                    ProductId = ci.ProductId,
                    Quantity = ci.Quantity,
                    PricePerUnit = ci.Product.Price
                }).ToList()
            };

            // Добавляем заказ в контекст
            _context.Orders.Add(order);

            // Удаляем все элементы корзины этого пользователя из БД
            var cartItemsToRemove = _context.CartItems.Where(ci => ci.UserId == userId);
            _context.CartItems.RemoveRange(cartItemsToRemove);

            _context.SaveChanges();

            // Очищаем локальную коллекцию и обновляем UI
            CartItems.Clear();
            OnPropertyChanged(nameof(TotalPrice));

            MessageBox.Show("Заказ успешно оформлен!");
        }



        private void LoadCartItems()
        {
            int userId = SessionService.CurrentUserId;

            var items = _context.CartItems
                .Where(c => c.UserId == userId)
                .Select(c => new CartItem
                {
                    CartItemId = c.CartItemId,
                    ProductId = c.ProductId,
                    Quantity = c.Quantity,
                    Product = c.Product,
                    UserId = c.UserId
                })
                .ToList();

            CartItems = new ObservableCollection<CartItem>(items);

            OnPropertyChanged(nameof(CartItems));
            OnPropertyChanged(nameof(TotalPrice));
        }

        private void IncreaseQuantity(object obj)
        {
            if (obj is CartItem item)
            {
                item.Quantity++;
                UpdateAndSave(item);
            }
        }

        private void DecreaseQuantity(object obj)
        {
            if (obj is CartItem item && item.Quantity > 1)
            {
                item.Quantity--;
                UpdateAndSave(item);
            }
        }

        private void UpdateAndSave(CartItem item)
        {
            var dbItem = _context.CartItems.FirstOrDefault(ci => ci.CartItemId == item.CartItemId);
            if (dbItem != null)
            {
                dbItem.Quantity = item.Quantity;
                _context.SaveChanges();

                OnPropertyChanged(nameof(TotalPrice));
            }
        }
    }
}
