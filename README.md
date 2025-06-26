# BookManagement Microservices Course

## Overview

This repository is a hands-on microservices course project demonstrating Clean Architecture, Docker-based deployment, and both synchronous (HTTP, gRPC) and (in the future) asynchronous (RabbitMQ, MassTransit) communication patterns. The solution is built with .NET and is designed for learning scalable, maintainable distributed systems.

## Architecture

- **Clean Architecture**: Each service is split into Domain, Application, Infrastructure, and API layers, promoting separation of concerns and testability.
- **CQRS & Mediator Pattern**: Commands and queries are handled via MediatR, with validation and pipeline behaviors for cross-cutting concerns.
- **Shared Kernel**: Common DTOs, base entities, exceptions, and interfaces are shared across services.
- **API Gateway**: A reverse proxy (YARP) routes external requests to the appropriate microservice.
- **Synchronous Communication**: HTTP REST APIs and gRPC for inter-service calls.
- **Asynchronous Communication**: RabbitMQ and MassTransit integration planned for future lessons.

## Services

- **Catalog Service** (`catalog-api`)
  - Manages books (CRUD, quantity checks)
  - Exposes REST and gRPC endpoints
- **Order Service** (`order-api`)
  - Manages orders and checkout
  - Communicates with Catalog via gRPC
- **API Gateway** (`api-gateway`)
  - Single entry point for clients
  - Routes `/catalog/*` and `/order/*` to respective services
- **SQL Server** (`sqlserver`)
  - Shared database instance for persistence

## Solution Structure

```
BookManagement/
  Gateway/                # API Gateway (YARP reverse proxy)
  Services/
    Catalog/
      Catalog.API/        # Catalog HTTP/gRPC API
      Catalog.Domain/     # Domain models & logic
      Catalog.Application/# Application layer (CQRS)
      Catalog.Infrastructure/ # Data access, gRPC impl
    Order/
      Order.API/          # Order HTTP API
      Order.Domain/       # Domain models & logic
      Order.Infrastructure/ # Data access
  Shared/                 # Shared DTOs, base classes, interfaces
  docker-compose.yml      # Multi-container orchestration
```

## Running the Solution

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

### Order API
- `GET /order/api/orders` - List all orders
- `GET /order/api/orders/{id}` - Get order by ID
- `POST /order/api/orders/CheckoutBook` - Checkout books (create order)
  - Body: `{ "shippingAddress": "string", "orderItems": [ { "bookId": int, "quantity": int, "totalPrice": decimal } ] }`
- `PUT /order/api/orders/{id}` - Update an order
- `DELETE /order/api/orders/{id}` - Delete an order

## Communication Patterns

- **HTTP REST**: Used for client-to-service and some service-to-service calls
- **gRPC**: Used for high-performance inter-service communication (Catalog ↔ Order)
- **RabbitMQ/MassTransit**: To be added for asynchronous messaging

## Extending the Solution

- Add new microservices by following the Clean Architecture template
- Integrate MassTransit and RabbitMQ for event-driven patterns
- Expand the API Gateway for authentication, rate limiting, etc.

## License

This project is for educational purposes as part of a microservices course. 