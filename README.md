# Weather Dashboard Application

Веб-приложение для мониторинга погоды с графиками и историей данных.
- Получение данных о погоде через OpenWeather API
- Отображение графиков температуры, влажности и скорости ветра
- Хранение истории погодных данных в базе данных
- Возможность просмотра данных для любого города

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
```
Откройте браузер и перейдите по адресу:
   - https://localhost:5001 (HTTPS)
   - http://localhost:5000 (HTTP)