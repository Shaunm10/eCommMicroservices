# eCommMicroservices
E-commerce Microservices implementation


## Docker Commands
##### Running Mongo Container
`> docker run -d -p 27017:27017 --name shopping-mongo mongo` 

##### Look at the logs for a particular container
`> docker logs -f shopping-mongo`

##### Open a terminal to the container
`> docker exec -it shopping-mongo /bin/bash`


## Mongo Commands

##### Show the local DB's
`> show dbs`

##### Creates a new catalog
`> use CatalogDb`

##### Creates a new catalog
`> db.createCollection('Products')`

##### Inserts Products ito a collection
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

## .net Core Commands

#### Install a Nuget Package
`> dotnet add package MongoDB.Driver --version 2.14.1`

## Docker Compose Commands

#### Run the docker compose files
`> docker-compose -f .\docker-compose.yml -f .\docker-compose.override.yml up -d`

[Dapper Video](https://www.youtube.com/watch?v=dwMFg6uxQ0I)
