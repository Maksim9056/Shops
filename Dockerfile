FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

RUN dotnet --info

# Копируем все файлы из текущей директории репозитория в папку /app
COPY . /app/

# Проверяем содержимое папки /app после копирования
RUN ls -la /app

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Shops.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false
#RUN dotnet restore 
#RUN dotnet build -c Release
#RUN dotnet publish -c Release -o /app/publish
# Проверяем содержимое папки /app после копирования
RUN ls -la /app

# Устанавливаем права на выполнение для скрипта запуска
RUN chmod +x /app/Scripts/Start/run.sh

# Устанавливаем точку входа
ENTRYPOINT ["/app/Scripts/Start/run.sh"]
