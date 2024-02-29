# Microservices
A small project to test the functionality of a microservice architecture
## Prerequisites
- .NET 8.0 SDK
https://dotnet.microsoft.com/en-us/download/dotnet/8.0
- Docker
https://docs.docker.com/get-docker/
- SQLite browsers
https://sqlitebrowser.org/
or
https://www.jetbrains.com/datagrip/
## Don't apply migrations
- In Orders microservice are automatically applied at startup (due to the testing nature of the project).
- Other microservices uses in-memory databases with hardcoded data.
## How to launch Microservices
- Launch RabbitMQ
  - `docker run -p 15672:15672 -p 5672:5672 masstransit/rabbitmq`
- Launch all the microservice and API (from different terminals)
  - `dotnet run --project Products`
  - `dotnet run --project Users`
  - `dotnet run --project Orders`
  - `dotnet run --project API`
## Launch tests
`dotnet test Products.Tests`
