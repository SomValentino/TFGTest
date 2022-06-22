# TFGTest

Inorder to run the project. I have created a docker compose file to run both mongoDB database and api swagger endpoint.
## How to run application
First install docker desktop on your comupter for mac go to https://docs.docker.com/desktop/mac/install/ 
and for windows go to https://docs.docker.com/desktop/windows/install/

Goto to the root folder of the solution where the docker-compose.yml file leaves and run the below commands:

```
docker-compose -f docker-compose.yml -f docker-compose.override.yml up -d
```

The docker compose will create two containers for the database and api. You can now access api at:

```
http://localhost:5001/swagger
```
The solution has four projects and they are descibed as follows:

## TFG.API
The project that contains the api controller endpoints

## TFG.Application
The project that contains interface contracts and services implementations of the business logic of the application.

## TFG.Infrastructure
The project that contains the data accces layer implementation. Here i have used NoSql database which MongDB a document database that stores data as JSON.

## TFG.Domain
The project that contains the domain entities of the application such Customer model.

## TFG.Unit.Tests
An xUnit project for testing endpoints in account and customer controllers.
