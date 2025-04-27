# Weather Dashboard Application

Веб-приложение для мониторинга погоды с графиками и историей данных.

## Функциональность

- Получение данных о погоде через OpenWeather API
- Отображение графиков температуры, влажности и скорости ветра
- Хранение истории погодных данных в базе данных SQLite
- Возможность просмотра данных для любого города

## Технологии

- ASP.NET Core 9.0
- Entity Framework Core
- SQLite
- Chart.js
- OpenWeather API

## Требования

- .NET 9.0 SDK
- OpenWeather API ключ

## Установка и запуск

1. Клонируйте репозиторий:
```bash
git clone https://github.com/your-username/weather-dashboard.git
cd weather-dashboard
```

2. Получите API ключ на [OpenWeather](https://openweathermap.org/api)

3. В файле `appsettings.json` замените `YOUR_API_KEY_HERE` на ваш API ключ:
```json
"OpenWeather": {
    "ApiKey": "ваш_ключ_здесь"
}
```

4. Установите зависимости и запустите приложение:
```bash
dotnet restore
dotnet build
dotnet run
```

5. Откройте браузер и перейдите по адресу:
   - https://localhost:5001 (HTTPS)
   - http://localhost:5000 (HTTP)

## Структура проекта

- `Controllers/` - API контроллеры
- `Models/` - Модели данных
- `Services/` - Сервисы для работы с API
- `Data/` - Контекст базы данных
- `wwwroot/` - Статические файлы и фронтенд

## Возможные проблемы и решения

1. Ошибка 401 при запросе к OpenWeather API:
   - Проверьте правильность API ключа
   - Убедитесь, что ключ активирован

2. Ошибка подключения к базе данных:
   - Проверьте права доступа к файлу базы данных
   - Убедитесь, что SQLite установлен

3. Ошибка при запуске приложения:
   - Проверьте версию .NET SDK
   - Убедитесь, что все зависимости установлены

## Лицензия

MIT