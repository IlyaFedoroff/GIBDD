# Используем официальный образ для ASP.NET для финальной сборки
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

# Используем SDK для сборки
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Копируем файл проекта в рабочую директорию
COPY ["GIBDD.Server/GIBDD.Server.csproj", "GIBDD.Server/"]

# Восстанавливаем зависимости
RUN dotnet restore "GIBDD.Server/GIBDD.Server.csproj"

# Копируем остальные файлы проекта
COPY . .

# Переключаемся на директорию с проектом
WORKDIR "/src/GIBDD.Server"

# Собираем проект
RUN dotnet build "GIBDD.Server.csproj" -c Release -o /app/build

# Подготовка для публикации
FROM build AS publish
RUN dotnet publish "GIBDD.Server.csproj" -c Release -o /app/publish

# Финальный образ
FROM base AS final
WORKDIR /app

# Копируем публикацию из промежуточного контейнера
COPY --from=publish /app/publish .

# Устанавливаем точку входа
ENTRYPOINT ["dotnet", "GIBDD.Server.dll"]
