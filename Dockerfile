FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base

WORKDIR /app
EXPOSE 8080
EXPOSE 8081

RUN dotnet --info

# Копируем все файлы из текущей директории репозитория в папку /app
COPY . /app/

RUN dotnet build -c Release
RUN dotnet publish -c Release -o /app/publish
# Проверяем содержимое папки /app после копирования
RUN ls -la /app

# Устанавливаем права на выполнение для скрипта запуска
RUN chmod +x /app/Scripts/Start/run.sh

# Устанавливаем точку входа
ENTRYPOINT ["/app/Scripts/Start/run.sh"]
