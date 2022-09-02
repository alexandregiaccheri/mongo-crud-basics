# E-commerce (Pet Shop) API
Back-end para uma loja (e-commerce) do setor de Pet Shop. API ASP.NET integrando com banco de dados NoSQL (MongoDB).

Link SwaggerUI versão live: https://petshopapi.azurewebsites.net/swagger/index.html

Para rodar o app localmente, basta adicionar o seguinte código ao seu appsettings.json (crie se não existir) e substitua o campo "connection string" de acordo com as necessidades do seu ambiente.

```json
"MongoDbSettings": {
    "ConnectionString": "<connection_string>",
    "DatabaseName": "pet_shop_api",
    "CategoryCollection": "categories",
    "OrderCollection" : "orders",
    "ProductCollection": "products",
    "UserCollection" : "users"
  }
```
