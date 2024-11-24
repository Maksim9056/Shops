FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Отладка содержимого
RUN ls -la /publish

# Копируем опубликованные файлы
COPY /publish /app/

# Копируем скрипт запуска
COPY /Scripts/Start/run.sh /app/run.sh
RUN chmod +x /app/run.sh

ENTRYPOINT ["/app/run.sh"]
