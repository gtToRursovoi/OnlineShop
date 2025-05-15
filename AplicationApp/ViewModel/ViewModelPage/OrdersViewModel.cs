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

namespace AplicationApp.ViewModel.ViewModelPage
{
    public class OrdersViewModel : BaseViewModel
    {
        private readonly SqlServerDbContext _context;

        public ObservableCollection<Order> Orders { get; set; }

        public OrdersViewModel()
        {
            _context = new SqlServerDbContext();
            Orders = new ObservableCollection<Order>();

            LoadOrders();
        }

        private void LoadOrders()
        {
            int userId = SessionService.CurrentUserId;

            var orders = _context.Orders
                .Where(o => o.UserId == userId)
                .OrderByDescending(o => o.OrderDate)
                .ToList();

            Orders.Clear();
            foreach (var order in orders)
            {
                // Чтобы подгрузить связанные OrderItems и продукты, можно использовать Include, если используешь EF Core
                // иначе здесь можно будет лениво загрузить коллекции

                _context.Entry(order).Collection(o => o.OrderItems).Load();
                foreach (var item in order.OrderItems)
                {
                    _context.Entry(item).Reference(i => i.Product).Load();
                }

                Orders.Add(order);
            }
        }
    }
}
