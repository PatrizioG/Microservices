# Prerequisites

## Docker
https://docs.docker.com/get-docker/

## Ef core tools
`dotnet tool install --global dotnet-ef`

# Apply migrations
`cd Microservices`
`cd Products`
`dotnet ef database update`

# Launch the Microservices

## Launch RabbitMQ
`docker run -p 15672:15672 -p 5672:5672 masstransit/rabbitmq`

## Launch Products microservice
`cd Microservices`
`cd Products`
`dotnet run`

