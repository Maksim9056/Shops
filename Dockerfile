FROM mcr.microsoft.com/dotnet/aspnet:8.0 
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

<<<<<<< HEAD
COPY  workspace/app/publish /app/

# Копируем скрипт из вашей папки
COPY Scripts/Start/run.sh /app/run.sh
=======
# Отладка содержимого
RUN ls -la /workspace
RUN ls -la /workspace/publish
RUN ls -la /workspace/Scripts

# Копируем опубликованные файлы приложения
COPY /workspace/publish /app/

# Копируем скрипт запуска
COPY /workspace/Scripts/Start/run.sh /app/run.sh
>>>>>>> 1e3871f (комент 1)
RUN chmod +x /app/run.sh

ENTRYPOINT ["/app/run.sh"]
