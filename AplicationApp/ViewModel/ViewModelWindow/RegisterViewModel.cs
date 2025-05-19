using AplicationApp.ViewModel.Other;
using Database.Context;
using Database.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace AplicationApp.ViewModel.ViewModelWindow
{
    public class RegisterViewModel : BaseViewModel
    {
        private string _username;
        private string _email;
        private string _password;
        private string _confirmPassword;
        private string _statusMessage;

        public string Username
        {
            get => _username;
            set { _username = value; OnPropertyChanged(); }
        }

        public string Email
        {
            get => _email;
            set { _email = value; OnPropertyChanged(); }
        }

        public string Password
        {
            get => _password;
            set { _password = value; OnPropertyChanged(); }
        }

        public string ConfirmPassword
        {
            get => _confirmPassword;
            set { _confirmPassword = value; OnPropertyChanged(); }
        }

        public string StatusMessage
        {
            get => _statusMessage;
            set { _statusMessage = value; OnPropertyChanged(); }
        }

        public ICommand RegisterCommand { get; }

        public RegisterViewModel()
        {
            RegisterCommand = new RelayCommand(_ => Register());
        }

        private void Register()
        {
            if (Password != ConfirmPassword)
            {
                StatusMessage = "Пароли не совпадают.";
                MessageBox.Show("Пароли не совпадают.");
                return;
            }

            if (string.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(Email) || string.IsNullOrWhiteSpace(Password))
            {
                StatusMessage = "Все поля обязательны.";
                MessageBox.Show("Все поля обязательны.");
                return;
            }

            using var db = new SqlServerDbContext();

            if (db.Users.Any(u => u.Username == Username || u.Email == Email))
            {
                StatusMessage = "Пользователь с таким логином или email уже существует.";
                MessageBox.Show("Пользователь с таким логином или email уже существует.");
                return;
            }

            var user = new User
            {
                Username = Username,
                Email = Email,
                Password = Password // ❗ Хешировать пароль в реальном проекте
            };

            db.Users.Add(user);
            db.SaveChanges();

            StatusMessage = "Регистрация прошла успешно.";
            MessageBox.Show("Регистрация прошла успешно.");
            // Можно здесь вызвать переход на LoginView
        }
    }
}
