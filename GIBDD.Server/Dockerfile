# используем базовый образ .NET SDK для сборки
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# копируем файлы проекта и восстанавливаем зависимости
COPY . .
RUN dotnet restore

# собираем проект
RUN dotnet publish -c Release -o /app

# используем базовый образ .NET Runtime для запуска
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

ENV ASPNETCORE_ENVIRONMENT=Production

# копируем собранные файлы
COPY --from=build /app .

# открываем порт
EXPOSE 8080

# используем скрипт как точку входа
ENTRYPOINT ["dotnet", "GIBDD.Server.dll"]
