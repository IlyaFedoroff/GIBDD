# Используем базовый образ Node.js для сборки Angular приложения
FROM node:18 AS build

# Устанавливаем рабочую директорию
WORKDIR /app

# Копируем package.json и package-lock.json для установки зависимостей
COPY package*.json ./

# Устанавливаем зависимости
RUN npm install

# Копируем исходный код Angular
COPY . .

# Выполняем сборку проекта Angular в продакшн режиме
RUN npm run build -- --configuration production

# Используем базовый образ Nginx для сервера
FROM nginx:alpine

# Копируем собранные статические файлы в директорию Nginx
COPY --from=build /app/dist/gibdd.client/browser/ /usr/share/nginx/html

# Копируем конфигурацию Nginx
COPY nginx.conf /etc/nginx/nginx.conf

# Открываем порт 80
EXPOSE 80

# Запускаем Nginx
CMD ["nginx", "-g", "daemon off;"]
