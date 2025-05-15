using AplicationApp.ViewModel.Other;
using Database.Context;
using Database.Service;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AplicationApp.ViewModel.ViewModelPage
{
    public class AdminOrdersViewModel : BaseViewModel
    {
        private readonly SqlServerDbContext _context;

        private Order _selectedOrder;
        public Order SelectedOrder
        {
            get => _selectedOrder;
            set
            {
                _selectedOrder = value;
                OnPropertyChanged(nameof(SelectedOrder));
            }
        }

        public ObservableCollection<Order> Orders { get; set; }

        public ICommand ConfirmOrderCommand { get; }
        public ICommand CancelOrderCommand { get; }

        public AdminOrdersViewModel()
        {
            _context = new SqlServerDbContext();
            Orders = new ObservableCollection<Order>();

            ConfirmOrderCommand = new RelayCommand(ConfirmOrder);
            CancelOrderCommand = new RelayCommand(CancelOrder);

            LoadOrders();
        }

        private void LoadOrders()
        {
            var orders = _context.Orders
                .Include(o => o.User)
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.Product)
                .OrderByDescending(o => o.OrderDate)
                .ToList();

            Orders = new ObservableCollection<Order>(orders);
            OnPropertyChanged(nameof(Orders));
        }

        private bool CanChangeStatus(object obj)
        {
            return SelectedOrder != null;
        }

        private void ConfirmOrder(object obj)
        {
            if (SelectedOrder != null)
            {
                SelectedOrder.Status = "completed";
                _context.SaveChanges();
                RefreshOrders();
            }
        }

        private void CancelOrder(object obj)
        {
            if (SelectedOrder != null)
            {
                SelectedOrder.Status = "processing";
                _context.SaveChanges();
                RefreshOrders();
            }
        }

        private void RefreshOrders()
        {
            LoadOrders();
            // Опционально — сбросить выбор, чтобы избежать багов:
            SelectedOrder = null;
        }
    }
}
