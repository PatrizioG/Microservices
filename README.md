# Microservices

A small project to test the functionality of a microservice architecture
## Prerequisites

- Docker
https://docs.docker.com/get-docker/

- Ef core tools
`dotnet tool install --global dotnet-ef`

## Apply migrations
`cd Microservices`
`cd Products`
`dotnet ef database update`

## How to launch Microservices

- Launch RabbitMQ
`docker run -p 15672:15672 -p 5672:5672 masstransit/rabbitmq`

- Launch all the microservice (from different terminals)
`dotnet run --project Products`
`dotnet run --project Users`
`dotnet run --project Orders`

