FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base

WORKDIR /app
EXPOSE 8080
EXPOSE 8081

RUN ls -la /workspace
RUN ls -la /workspace/publish
RUN ls -la /workspace/Scripts/Start

# Проверяем содержимое папки /workspace/publish
RUN ls -la /workspace/publish || echo "Папка /workspace/publish отсутствует"

# Копируем опубликованные файлы из папки /workspace/publish
COPY /workspace/publish /app/

# Проверяем содержимое папки /app после копирования
RUN ls -la /app

# Копируем скрипт запуска
COPY /workspace/Scripts/Start/run.sh /app/run.sh
RUN chmod +x /app/run.sh

# Устанавливаем точку входа
ENTRYPOINT ["/app/run.sh"]
