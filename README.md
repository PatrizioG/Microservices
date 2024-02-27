# Microservices

A small project to test the functionality of a microservice architecture

## Prerequisites

- .NET 8.0 SDK
https://dotnet.microsoft.com/en-us/download/dotnet/8.0

- Docker
https://docs.docker.com/get-docker/

- Ef core tools
`dotnet tool install --global dotnet-ef`

- SQLite browsers
https://sqlitebrowser.org/
or
https://www.jetbrains.com/datagrip/

## Apply migrations doesn't needed
Orders project uses a sqlite db but due to the testing nature of the project, migrations are applied at the startup phase.
Other projects use in-memory databases with hardcoded data and therefore do not require migration.

## How to launch Microservices

- Launch RabbitMQ
`docker run -p 15672:15672 -p 5672:5672 masstransit/rabbitmq`

- Launch all the microservice (from different terminals)
`dotnet run --project Products`
`dotnet run --project Users`
`dotnet run --project Orders`

