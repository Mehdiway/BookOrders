# BookManagement Microservices Course 📚

## Overview

This repository is a hands-on microservices course project demonstrating Clean Architecture, Docker-based deployment, and both synchronous (HTTP, gRPC) and asynchronous (RabbitMQ, MassTransit) communication patterns. The solution is built with .NET and is designed for learning scalable, maintainable distributed systems.

## Architecture

- **Clean Architecture**: Each service is split into Domain, Application, Infrastructure, and API layers, promoting separation of concerns and testability.
- **CQRS & Mediator Pattern**: Commands and queries are handled via MediatR, with validation and pipeline behaviors for cross-cutting concerns.
- **Shared Kernel**: Common DTOs, base entities, exceptions, and interfaces are shared across services.
- **API Gateway**: A reverse proxy (YARP) routes external requests to the appropriate microservice.
- **Synchronous Communication**: HTTP REST APIs and gRPC for inter-service calls.
- **Asynchronous Communication**: RabbitMQ and MassTransit are implemented for event-driven messaging between services. 🐇🚍

## Services

- **Catalog Service** (`catalog-api`)
  - Manages books (CRUD, quantity checks)
  - Exposes REST and gRPC endpoints
  - Publishes and consumes events via RabbitMQ/MassTransit
- **Order Service** (`order-api`)
  - Manages orders and checkout
  - Communicates with Catalog via gRPC
  - Publishes and consumes events via RabbitMQ/MassTransit
- **API Gateway** (`api-gateway`)
  - Single entry point for clients
  - Routes `/catalog/*` and `/order/*` to respective services
- **SQL Server** (`sqlserver`)
  - Shared database instance for persistence
- **RabbitMQ** (`rabbitmq`)
  - Message broker for asynchronous communication

## Solution Structure

```
BookManagement/
  Gateway/                # API Gateway (YARP reverse proxy)
  Services/
    Catalog/
      Catalog.API/        # Catalog HTTP/gRPC API
      Catalog.Domain/     # Domain models & logic
      Catalog.Application/# Application layer (CQRS)
      Catalog.Infrastructure/ # Data access, gRPC impl, event consumers/producers
    Order/
      Order.API/          # Order HTTP API
      Order.Domain/       # Domain models & logic
      Order.Infrastructure/ # Data access, event consumers/producers
  Shared/                 # Shared DTOs, base classes, interfaces
  docker-compose.yml      # Multi-container orchestration
```

## Running the Solution 🚀

### Prerequisites
- [.NET 9 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/9.0)
- [Docker](https://www.docker.com/get-started)

### Start All Services

```bash
docker-compose up --build
```

- API Gateway: http://localhost:5000
- Catalog API: Internal (proxied via gateway)
- Order API: Internal (proxied via gateway)
- SQL Server: Internal
- RabbitMQ Management UI: http://localhost:15672 (default user/pass: guest/guest)

### Development (Individual Service)

You can run each service individually using Visual Studio or `dotnet run` in the respective project directory. Update connection strings as needed.

## API Summary

### Catalog API
- `GET /catalog/api/books` - List all books
- `GET /catalog/api/books/{id}` - Get book by ID
- `POST /catalog/api/books` - Create a new book
  - Body: `{ "title": "string", "author": "string", "publicationDate": "YYYY-MM-DD", "quantity": int, "price": decimal }`
- `PUT /catalog/api/books/{id}` - Update a book
- `DELETE /catalog/api/books/{id}` - Delete a book
- `POST /catalog/api/books/BookIdsAllExist` - Check if all book IDs exist
- **gRPC**: `CatalogService` (see `Shared/Protos/catalog.proto`)
- **Events**: Publishes and consumes book-related events via RabbitMQ/MassTransit

### Order API
- `GET /order/api/orders` - List all orders
- `GET /order/api/orders/{id}` - Get order by ID
- `POST /order/api/orders/CheckoutBook` - Checkout books (create order)
  - Body: `{ "shippingAddress": "string", "orderItems": [ { "bookId": int, "quantity": int, "totalPrice": decimal } ] }`
- `PUT /order/api/orders/{id}` - Update an order
- `DELETE /order/api/orders/{id}` - Delete an order
- **Events**: Publishes and consumes order-related events via RabbitMQ/MassTransit

## Communication Patterns

- **HTTP REST**: Used for client-to-service and some service-to-service calls
- **gRPC**: Used for high-performance inter-service communication (Catalog ↔ Order)
- **RabbitMQ/MassTransit**: Implemented for asynchronous, event-driven messaging between services 🐇

## Extending the Solution

- Add new microservices by following the Clean Architecture template
- Integrate additional event-driven patterns or message consumers as needed
- Expand the API Gateway for authentication, rate limiting, etc.

## License 📄

This project is for educational purposes as part of a microservices course. 