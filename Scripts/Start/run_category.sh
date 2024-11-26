#!/bin/bash

CONFIG_FILE="/app/appsettings.json"

# Проверяем наличие файла конфигурации
if [ ! -f "$CONFIG_FILE" ]; then
    echo "Файл конфигурации $CONFIG_FILE не найден!"
    ls -la /app
    exit 1
fi

# Проверяем и заменяем целевую строку
TARGET_STRING="Host=localhost;Database=StoreShop;Username=postgres;Password=1"
REPLACEMENT_STRING="Host=${PG_HOST};Database=${PG_DATABASE};Username=${PG_USERNAME};Password=${PG_USERPASSWORD}"

# Проверяем, содержит ли файл целевую строку
if grep -q "$TARGET_STRING" "$CONFIG_FILE"; then
    echo "Целевая строка найдена. Выполняется замена на актуальные параметры."
    sed -i "s#${TARGET_STRING}#${REPLACEMENT_STRING}#g" "$CONFIG_FILE"
else
    echo "Целевая строка не найдена. Замена не требуется."
fi

# Проверка результата замены
echo "Содержимое $CONFIG_FILE после замены:"
cat "$CONFIG_FILE"

# Запускаем приложение
dotnet /app/Store_Category.dll --urls http://0.0.0.0:$PORT_WEB &

# Оставляем контейнер в режиме ожидания
tail -f /dev/null
