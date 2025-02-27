
# используем базовый образ Nginx
FROM nginx:alpine

# копируем статические файлы angular в контейнер
COPY dist/gibdd.client/browser/ /usr/share/nginx/html

# копируем конфигурацию Nginx
COPY nginx.conf /etc/nginx/nginx.conf

# открываем порт 80
EXPOSE 80

# запускаем Nginx
CMD ["nginx", "-g", "daemon off;"]
