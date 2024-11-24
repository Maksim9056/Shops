FROM mcr.microsoft.com/dotnet/aspnet:8.0 
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

COPY  workspace/app/publish /app/

# Копируем скрипт из вашей папки
COPY Scripts/Start/run.sh /app/run.sh
RUN chmod +x /app/run.sh

# Устанавливаем точку входа
ENTRYPOINT ["/app/run.sh"]
