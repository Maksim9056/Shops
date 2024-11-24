FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

#FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
#ARG BUILD_CONFIGURATION=Release
#WORKDIR /src

# Копируем .csproj файлы относительно корневой директории
#COPY ["Shops/Shops.csproj", "Shops/"]
#COPY ["ShopClassLibrary/ShopClassLibrary.csproj", "ShopClassLibrary/"]
#COPY ["Shops.Client/Shops.Client.csproj", "Shops.Client/"]

# Выполняем восстановление зависимостей
#RUN dotnet restore "./Shops/Shops.csproj"

# Копируем весь проект для сборки
#COPY . .
#WORKDIR "/src/Shops"
#RUN dotnet build "./Shops.csproj" -c $BUILD_CONFIGURATION -o /app/build

#FROM build AS publish
#ARG BUILD_CONFIGURATION=Release
#RUN dotnet publish "./Shops.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=base /app/publish .

# Копируем скрипт из вашей папки
COPY Scripts/Start/run.sh /app/run.sh
RUN chmod +x /app/run.sh

# Устанавливаем точку входа
ENTRYPOINT ["/app/run.sh"]
