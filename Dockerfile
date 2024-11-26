FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
#FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app
EXPOSE 80
EXPOSE 8081

RUN dotnet --info

# Копируем все файлы из текущей директории репозитория в папку /app
COPY . /app/
RUN cp /app/Shops/appsettings.json /app/publish/appsettings.json
# Проверяем содержимое папки /app после копирования
RUN ls -la /app

RUN ls -la /app
RUN ls -la /app/publish

# Debug the presence of `appsettings.json`
RUN ls -la /app/publish/appsettings.json
# Устанавливаем права на выполнение для скрипта запуска

# Удаляем все лишние файлы, оставляем только `publish` и `Scripts`
RUN rm -rf /app/Dockerfile \
           /app/ShopClassLibrary \
           /app/Shops \
           /app/Shops.Client \
           /app/Shops_Gateway \
           /app/StoreImage \
           /app/Store_Category \
           /app/Store_Orders \
           /app/Store_Products \
           /app/Store_Projects \
           /app/Store_Status \
           /app/Store_Tasks \
           /app/Store_Users \
           /app/Story_Test \
           /app/Shops.sln \
           /app/Store_Orders.csproj \
           /app/README.md
# Копируем содержимое папки publish в корень /app
RUN cp -r /app/publish/* /app/ && rm -rf /app/publish    
RUN chmod +x /app/Scripts/Start/run.sh

# Устанавливаем точку входа
ENTRYPOINT ["/app/Scripts/Start/run.sh"]
