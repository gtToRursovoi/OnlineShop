# 🛒 WPF Online Store

Это десктопное приложение онлайн-магазина, разработанное на платформе **.NET с использованием WPF (Windows Presentation Foundation)**. Программа предназначена для локального использования или как часть клиент-серверного решения.

## 📌 Основные функции

* Просмотр каталога товаров
* Корзина и оформление заказа
* Регистрация и авторизация
* Админ-панель для управления товарами и заказами
* Поддержка ролей пользователей
* Работа с БД через Entity Framework Core

## 🛠️ Стек технологий

* .NET 8 
* WPF с паттерном MVVM
* Entity Framework Core
* SQL Server 


## 💻 Требования

* Windows 10/11
* .NET SDK (8.0 или выше)
* Visual Studio 2022+
* Установленная СУБД Microsoft Sql Server

## 🚀 Установка и запуск

1. **Клонируйте репозиторий**

   ```bash
   git clone https://github.com/gtToRursovoi/OnlineShop/tree/Debug
   cd wpf-online-store
   ```

2. **Откройте проект в Visual Studio**

3. **Настройте строку подключения**

   Строка подключения задаётся вручную в конструкторе класса `ApplicationDbContext.cs`.

   ```csharp
   protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
   {
       if (!optionsBuilder.IsConfigured)
       {
           optionsBuilder.UseSqlServer("Server=localhost;Database=OnlineStoreDb;Trusted_Connection=True;");
       }
   }
   ```

   > 💡 Рекомендуется выносить строку подключения в конфигурационный файл или переменные окружения для продакшена.

4. **Примените миграции (если необходимо):**

   ```bash
   dotnet ef database update
   ```

5. **Запустите приложение (F5 в Visual Studio)**

## 🧪 Тестирование

```bash
dotnet test
```

## 📁 Структура проекта

```
/Models          - классы данных
/ViewModels      - MVVM логика
/Views           - XAML интерфейсы
/Services        - бизнес-логика, работа с БД
/ApplicationDbContext.cs - контекст базы данных
```

## 📄 Лицензия

MIT License — см. [LICENSE](./LICENSE)
