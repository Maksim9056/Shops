#!/bin/bash

# Путь к файлу конфигурации
CONFIG_FILE="/app/publish/appsettings.json"

# Ожидаемая строка для поиска
TARGET_STRING="Host=localhost;Port=5432;Database=CloudStorage;Username=postgres;Password=1"

# Проверяем наличие строки в файле
if grep -q "$TARGET_STRING" "$CONFIG_FILE"; then
    echo "Строка найдена. Выполняем замену."
    sed -i "s#$TARGET_STRING#Host=$PG_HOST;Port=$PG_PORT;Database=$PG_DATABASE;Username=$PG_USERNAME;Password=$PG_USERPASSWORD#g" "$CONFIG_FILE"
else
    echo "Строка не найдена. Замена не требуется."
fi

# Запускаем веб-приложение
dotnet  /app/publish/Shops.dll --urls http://0.0.0.0:$PORT_WEB &

# Оставляем контейнер в режиме ожидания
tail -f /dev/null
