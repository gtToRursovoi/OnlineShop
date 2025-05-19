using AplicationApp.Services;
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
    public class UserProfileViewModel : BaseViewModel
    {
        private readonly SqlServerDbContext _context;

        public UserProfileViewModel()
        {
            _context = new SqlServerDbContext();
            LoadUserInfo();
            LoadUserOrders();

            UpdateCommand = new RelayCommand(UpdateUserInfo);
        }

        private User _user;
        public User User
        {
            get => _user;
            set { _user = value; OnPropertyChanged(); }
        }

        public ObservableCollection<Order> Orders { get; set; } = new ObservableCollection<Order>();

        public ICommand UpdateCommand { get; }

        private void LoadUserInfo()
        {
            var userId = SessionService.CurrentUserId;
            User = _context.Users.FirstOrDefault(u => u.UserId == userId);
        }

        private void LoadUserOrders()
        {
            var userId = SessionService.CurrentUserId;
            var orders = _context.Orders
                .Where(o => o.UserId == userId)
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .OrderByDescending(o => o.OrderDate)
                .ToList();

            Orders = new ObservableCollection<Order>(orders);
            OnPropertyChanged(nameof(Orders));
        }

        private void UpdateUserInfo(object obj)
        {
            var dbUser = _context.Users.FirstOrDefault(u => u.UserId == User.UserId);
            if (dbUser != null)
            {
                dbUser.Username = User.Username;
                dbUser.Email = User.Email;
              
                _context.SaveChanges();
            }
        }
    }
}
