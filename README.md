# eCommMicroservices

E-commerce Micro services implementation

## Docker Commands

##### Running Mongo Container

`> docker run -d -p 27017:27017 --name shopping-mongo mongo`

##### Running Redis Container

`> docker run -d -p 6379:6379 --name aspnetrun-redis redis`

##### Look at the logs for a particular container

`> docker logs -f shopping-mongo`

##### Open a terminal to the container

`> docker exec -it shopping-mongo/bin/bash`

## Mongo Commands

##### Show the local DB's

`> show dbs`

##### Creates a new catalog

`> use CatalogDb`

##### Creates a new catalog

`> db.createCollection('Products')`

##### Inserts Products into a collection

```
> db.Products.insertMany(
    [
        {
            "name":"value"
        },
        {
            "name":"value"
        },
        {
            "name":"value"
        }
    ]
)
```

## Redis Commands

##### Open a terminal to the running container

`> docker exec -it aspnetrun-redis /bin/bash`

##### Open the cli

```
> cd data
> redis-cli
```

##### Set a key's value

`> set key value`
`> set name Shaun`

##### Get a key's value

`> get key`
`> get name`

## DotNet Core Commands

#### Create a new WebApi Project (in the current Dir)

`> dotnet new webapi -n Discount.Api`

#### Add Project to solution (in the Dir of .sln)

`> dotnet sln add .\Utilities\EventBus.Messages`

#### Install a Nuget Package

`> dotnet add package MongoDB.Driver --version 2.14.1`

#### Add project to solution

`> dotnet sln add Services/Ordering/Ordering.Api/Ordering.Api.csproj`

#### Add project reference to current project

`> dotnet add reference ../Ordering.Domain/`

#### Add Entity Framework migration

(In directory of Infrastructure project)

`> dotnet ef migrations add Initial`

## PG Sql

#### Create discounts table

```
CREATE Table Discount(
	ID SERIAL PRIMARY KEY NOT NULL,
	ProductID VARCHAR(24) NOT NULL,
	Description TEXT,
	AMOUNT INT
);
```

#### Insert discounts

```
INSERT INTO Discount (productid, description, amount) Values ('602d2149e773f2a3990b47f5','IPhone Discount',150);
INSERT INTO Discount (productid, description, amount) Values ('602d2149e773f2a3990b47f6','Samsung 10 Discount',100);
```

## Docker Compose Commands

#### Run the docker compose files

`> docker-compose -f .\docker-compose.yml -f .\docker-compose.override.yml up -d`

#### Run the docker compose files on M1 MacOs

`> docker-compose -f docker-compose.yml -f docker-compose.override.yml -f docker-compose.override-M1.yml up -d`

#### Force the images to be rebuilt:

`> docker-compose -f .\docker-compose.yml -f .\docker-compose.override.yml up -d --build`

| Resource         | Url (In Docker-compose)                                                              |
| ---------------- | ------------------------------------------------------------------------------------ |
| Catalog.Api      | [http://localhost:8000/swagger/index.html](http://localhost:8000/swagger/index.html) |
| Basket.Api       | [http://localhost:8001/swagger/index.html](http://localhost:8001/swagger/index.html) |
| Discount.Api     | [http://localhost:8002/swagger/index.html](http://localhost:8002/swagger/index.html) |
| Order.Api        | [http://localhost:8004/swagger/index.html](http://localhost:8004/swagger/index.html) |
| Discount.Grpc    | [http://localhost:8003](http://localhost:8003)                                       |
| Portainer        | [http://localhost:9000](http://localhost:9000)               admin:password          |
| PGAdmin4         | [http://localhost:5050/login?next=%2F](http://localhost:5050/login?next=%2F)         |

| RabbitMQ Console | [http://localhost:15672/](http://localhost:15672/) guest:guest 
| OcelotGateway | [http://localhost:8010/](http://localhost:8010/)
| Shopping.Aggregator | [http://localhost:8005/swagger/index.html](http://localhost:8005/swagger/index.html)                        |
                      |


http://localhost:15672/
[Dapper Video](https://www.youtube.com/watch?v=dwMFg6uxQ0I)

## Side Tasks

Different enhancements to add.

- [ ] Version the Api for External consumers (Both Controllers and DTO/VM's)
  - [ ] [Api versioning Article](https://referbruv.com/blog/posts/integrating-aspnet-core-api-versions-with-swagger-ui)
  - [ ] [Additional Swagger enhancements] (https://www.dotnetnakama.com/blog/enriched-web-api-documentation-using-swagger-openapi-in-asp-dotnet-core/)
  - [ ] Basket.Api
  - [ ] Catalog.Api
  - [ ] Coupon.Api
  - [ ] Coupon.Grpc
- [x] Create a Discount.Business project to hold services and DTO's
- [ ] Dapper -> StoredProc in Coupon.Api?
- [ ] Improve Swagger DTO comments/usage.
  - [ ] [Additional Swagger enhancements] (https://www.dotnetnakama.com/blog/enriched-web-api-documentation-using-swagger-openapi-in-asp-dotnet-core/)
- [ ] Make the casing of private variables consistent.
- [ ] Add Cancellation Tokens to all Api's
- [ ] Add better exception validation responses to Order.Api
- [ ] Make a business service for the BasketController.Checkout()
- [ ] Add Version Response header all Api's
- [ ] Health checks
  - [ ] Alive
  - [ ] Dependencies
- [ ] Add Decimal dataType to the Grpc -> [Grpc Decimal](https://itnext.io/net-decimal-datatype-in-grpc-51c2ddb1c153)
- [ ] Document methods and DTO properties in Web.Api projects
- [ ] Add better Support for Visual Studio 2022 for Mac (`<LangVersion>10</LangVersion>`)
- [ ] Add a better Postgres DB Migration setup
- [ ] Dapac for Order Sql DB - [Dacpac in Docker](https://www.atmosera.com/blog/devops-sql-server-dacpac-docker/) [Dacpac](https://docs.microsoft.com/en-us/sql/azure-data-studio/extensions/sql-database-project-extension?view=sql-server-ver15)
- [ ] Make use of C# records for DTO's. Research -> [Record Video](https://www.youtube.com/watch?v=9Byvwa9yF-I)
- [ ] Make the EntityBase use Id's instead of Strings
- [ ] Create Draw.io Diagrams to show how these services interact
- [ ] Add Unit Test
  - [ ] Catalog.Api
  - [ ] Discount.Api
  - [ ] Discount.Grpc
  - [ ] Basket.Api
- [ ] Add Integration Test
  - [ ] Catalog.Api
  - [ ] Discount.Api
  - [ ] Discount.Grpc
  - [ ] Basket.Api
- [ ] Add Jagger distributed tracing
- [ ] Add Global Exception Handler/logger for all Api's
- [ ] Add Auto registration of DI services -> [NetCore.AutoRegisterDi](https://www.thereformedprogrammer.net/asp-net-core-fast-and-automatic-dependency-injection-setup/)
- [ ] Add GitHub CI
- [ ] Make all configuration strongly typed objects
- [ ] in the Shopping Aggerage controller refactor the code to be in a service. " put this all into a help service."
- [ ] Add more Rosslyn Code analyzers
- [ ] Add Serilog to all WebApi's
- [ ] Add Debugging capability to VsCode Docker-compose
- [ ] User Dapper -> [Dapper Video](https://www.youtube.com/watch?v=dwMFg6uxQ0I)
- [ ] Create Client Apps:
  - [ ] Angular + .net
  - [ ] React + Node Client
  - [ ] iOS App
  - [ ] Create Android App
