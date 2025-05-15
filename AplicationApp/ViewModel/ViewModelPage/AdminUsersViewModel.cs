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
    public class AdminUsersViewModel : BaseViewModel
    {
        private readonly SqlServerDbContext _context;

        private User _selectedUser;
        public User SelectedUser
        {
            get => _selectedUser;
            set
            {
                _selectedUser = value;
                OnPropertyChanged(nameof(SelectedUser));
               
            }
        }

        public ObservableCollection<User> Users { get; set; }

        public ICommand DeleteUserCommand { get; }

        public AdminUsersViewModel()
        {
            _context = new SqlServerDbContext();
            Users = new ObservableCollection<User>();
            DeleteUserCommand = new RelayCommand(DeleteUser);

            LoadUsers();
        }

        private void LoadUsers()
        {
            var users = _context.Users
                .OrderBy(u => u.Email)
                .ToList();

            Users = new ObservableCollection<User>(users);
            OnPropertyChanged(nameof(Users));
        }

        private bool CanDeleteUser(object obj)
        {
            return SelectedUser != null;
        }

        private void DeleteUser(object obj)
        {
            if (SelectedUser != null)
            {
                // Можно добавить подтверждение через MessageBox, если нужно
                _context.Users.Remove(SelectedUser);
                _context.SaveChanges();
                LoadUsers();
                SelectedUser = null;
            }
        }
    }
}
