# Weather Dashboard Application

Веб-приложение для мониторинга погоды с графиками и историей данных.

## Функциональность
- Получение данных о погоде через OpenWeather API
- Отображение графиков температуры, влажности и скорости ветра
- Хранение истории погодных данных в базе данных SQLite
- Возможность просмотра данных для любого города
## Установка и запуск
git clone https://github.com/your-username/weather-dashboard.git
cd weather-dashboard
Получите API ключ на [OpenWeather](https://openweathermap.org/api)
В файле `appsettings.json` замените `YOUR_API_KEY_HERE` на ваш API ключ:
```json
"OpenWeather": {
    "ApiKey": "ваш_ключ_здесь"
}
Установите зависимости и запустите приложение:
```bash
dotnet restore
dotnet build
dotnet run
Откройте браузер и перейдите по адресу:
   - https://localhost:5001 (HTTPS)
   - http://localhost:5000 (HTTP)

Ошибка 401 при запросе к OpenWeather API:
   - Проверьте правильность API ключа
   - Убедитесь, что ключ активирован(может занять пару часов)