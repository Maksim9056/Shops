FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base

WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Копируем опубликованные файлы из папки /workspace/publish
COPY /publish /app/

# Проверяем содержимое папки /app после копирования
RUN ls -la /app

# Копируем скрипт запуска
COPY /Scripts/Start/run.sh /app/run.sh
RUN chmod +x /app/run.sh

# Устанавливаем точку входа
ENTRYPOINT ["/app/run.sh"]
