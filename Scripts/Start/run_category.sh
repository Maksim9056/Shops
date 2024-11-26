#!/bin/bash

CONFIG_FILE="/app/appsettings.json"

# Проверяем наличие файла конфигурации
if [ ! -f "$CONFIG_FILE" ]; then
    echo "Файл конфигурации $CONFIG_FILE не найден!"
    ls -la /app/publish
    exit 1
fi

# Проверяем и заменяем целевую строку
TARGET_STRING="Host=localhost;Port=5432;Database=CloudStorage;Username=postgres;Password=1"
if grep -q "$TARGET_STRING" "$CONFIG_FILE"; then
    echo "Целевая строка найдена. Выполняется замена на переменные окружения."
    sed -i "s#$TARGET_STRING#Host=$PG_HOST;Port=$PG_PORT;Database=$PG_DATABASE;Username=$PG_USERNAME;Password=$PG_USERPASSWORD#g" "$CONFIG_FILE"
else
    echo "Целевая строка не найдена. Замена не требуется."
fi

# Запускаем приложение
dotnet /app/Store_Category.dll --urls http://0.0.0.0:$PORT_WEB &

# Оставляем контейнер в режиме ожидания
tail -f /dev/null
