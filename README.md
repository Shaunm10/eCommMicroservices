# eCommMicroservices
E-commerce Microservices implementation


## Docker Commands
##### Running Mongo Container
`> docker run -d -p 27017:27017 --name shopping-mongo mongo` 

##### Look at the logs for a particular container
`> docker logs -f shopping-mongo`

##### Open a terminal to the container
`> docker exec -it shopping-mongo /bin/bash`