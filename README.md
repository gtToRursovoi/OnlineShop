Отлично! Раз программа полностью написана на **WPF (.NET Desktop)** — это значит, что это десктопное приложение, а не веб. Вот адаптированная версия `README.md` для **онлайн-магазина на WPF**:

---

# 🛒 WPF Online Store

Это десктопное приложение онлайн-магазина, разработанное на платформе **.NET с использованием WPF (Windows Presentation Foundation)**. Программа предназначена для локального использования или в составе клиент-серверной архитектуры (если реализовано подключение к API или БД).

## 📌 Основные функции

* Просмотр каталога товаров
* Поиск и фильтрация по категориям
* Добавление товаров в корзину
* Оформление заказов
* Регистрация и авторизация пользователей
* Личный кабинет (профиль, история покупок)
* Панель администратора (CRUD для товаров и заказов)
* Работа с базой данных (через Entity Framework)

## 🛠️ Технологии и стек

* **.NET / .NET Core / .NET 6+**
* **WPF (MVVM архитектура)**
* **Entity Framework Core**
* **SQL Server / SQLite / PostgreSQL**
* **Material Design in XAML Toolkit** *(если используется)*
* **Dependency Injection**, **ICommand**, **ObservableCollection** и прочие WPF-паттерны

## 💻 Требования

* Windows 10/11
* .NET 6 SDK или выше
* Visual Studio 2022 или новее
* SQL Server / SQLite установлен (если используется внешняя БД)

## 🚀 Установка и запуск

1. **Клонируйте репозиторий**

   ```bash
   git clone https://github.com/your-username/wpf-online-store.git
   cd wpf-online-store
   ```

2. **Откройте проект в Visual Studio**

3. **Настройте строку подключения к БД в `appsettings.json` или `App.config`:**

   ```xml
   <connectionStrings>
     <add name="DefaultConnection" 
          connectionString="Server=localhost;Database=OnlineStoreDb;Trusted_Connection=True;" 
          providerName="System.Data.SqlClient" />
   </connectionStrings>
   ```

4. **Примените миграции и создайте базу данных (если используется EF Core):**

   ```bash
   dotnet ef database update
   ```

5. **Запустите приложение в Visual Studio (F5)**

## 📂 Структура проекта

```
/Models           - модели данных
/Views            - XAML представления
/ViewModels       - логика представлений (MVVM)
/Services         - работа с данными, API, бизнес-логика
/Commands         - реализация ICommand
/App.xaml         - настройки приложения
/MainWindow.xaml  - стартовое окно
```

## 🧪 Тестирование

Если присутствует модульная логика, можно использовать `xUnit`, `NUnit` или `MSTest`:

```bash
dotnet test
```

## 📄 Лицензия

Проект распространяется под лицензией MIT. См. [LICENSE](./LICENSE).


