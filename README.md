# microservises-example
Based on Udemy course "Microservices Architecture and Implementation on .NET 5"
https://github.com/aspnetrun/run-aspnetcore-microservices

## Start development environment
1. Navigate to the root directory which include **docker-compose.yml** files.
2. Run following docker-compose command:
```csharp
docker-compose -f docker-compose.yml -f docker-compose.override.yml up -d
```
OR (when need to rebuild the images)
```csharp
docker-compose -f docker-compose.yml -f docker-compose.override.yml up --build -d
```

## Access microservises:
* **Catalog API -> http://localhost:8000/swagger/index.html**
* **Basket API -> http://localhost:8001/swagger/index.html**
* **Portainer -> http://localhost:9000/**   --admin/admin123
* **Discount API -> http://localhost:8002/swagger/index.html**
* **pgAdmin PostgreSQL -> http://localhost:5050**   -- admin@aspnetrun.com/admin123
* **Shopping.Aggregator -> http://locahlhost:8005/swagger/index.html**
* **API Gateway -> http://localhost:8010/Catalog**
* **Rabbit Management Dashboard -> http://localhost:15672**   -- guest/guest
