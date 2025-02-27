# GIBDD
Конструирование 3 лаба

Этот проект — веб-приложение, работающее на **Angular**, **Docker**, **.NET 8.0** и **Nginx**.

## 📌 Зависимости
Перед запуском убедитесь, что у вас установлены:
- **Node.js** (рекомендуемая версия: LTS) → [Скачать](https://nodejs.org/)
- **Angular CLI** → устанавливается командой:
  ```bash
  npm install -g @angular/cli
  ```
- **Docker** и **Docker Compose** → [Скачать](https://www.docker.com/products/docker-desktop/)


## 🚀 Инструкция по развертыванию


### 1️⃣ Склонировать репозиторий
```bash
git clone https://github.com/IlyaFedoroff/GIBDD.git
cd GIBDD
```

### 2️⃣ Установить зависимости
```bash
npm install
```

### 3️⃣ Собрать Angular-приложение
```bash
ng build
```

### 4️⃣ Запустить контейнеры через Docker Compose
```bash
docker-compose up --build
```

### 5️⃣ Открыть приложение в браузере
Перейдите по адресу:
```
http://localhost
```

---
🎉 Теперь ваше приложение запущено и готово к использованию!
