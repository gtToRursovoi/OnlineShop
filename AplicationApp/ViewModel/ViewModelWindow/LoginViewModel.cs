using AplicationApp.Services;
using AplicationApp.View.Windows;
using AplicationApp.ViewModel.Other;
using Database.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace AplicationApp.ViewModel.ViewModelWindow
{
    public class LoginViewModel : BaseViewModel
    {
        private readonly SqlServerDbContext _context;

        public string Login { get; set; }
        public string Password { get; set; }

        public ICommand LoginCommand { get; }

        public event Action<string> OnError;
        public event Action<string> OnSuccess;
        public event Action OpenAdminWindow;
        public event Action<int, string> OnLoginSuccess;

        public LoginViewModel()
        {
            _context = new SqlServerDbContext();
            LoginCommand = new RelayCommand(LoginExecute);
        }

        private void LoginExecute(object obj)
        {
            if (string.IsNullOrWhiteSpace(Login) || string.IsNullOrWhiteSpace(Password))
            {
                OnError?.Invoke("Логин и пароль не могут быть пустыми.");
                return;
            }

            var trimmedLogin = Login.Trim();
            var trimmedPassword = Password.Trim();

            var currentUser = _context.Users.FirstOrDefault(u =>
                (u.Username == trimmedLogin || u.Email == trimmedLogin) &&
                u.Password == trimmedPassword);

            if (currentUser == null)
            {
                OnError?.Invoke("Неверный логин или пароль.");
                return;
            }

            // Сохраняем данные в SessionService
            SessionService.CurrentUserId = currentUser.UserId;
            SessionService.CurrentUserLogin = currentUser.Username;

            if (currentUser.Role == "admin")
            {
                OnSuccess?.Invoke("Добро пожаловать, Админ!");
                OpenAdminWindow?.Invoke();

                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                Application.Current.Windows[0].Close();
            }
            else
            {
                OnSuccess?.Invoke("Добро пожаловать!");
                UserWindow userWindow = new UserWindow();
                userWindow.Show();
                Application.Current.Windows[0].Close();
            }
        }
    }

}
