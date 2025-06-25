# Build stage
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app

# Copy solution and restore dependencies
COPY *.sln ./
COPY Shared/Shared.csproj ./Shared/
COPY Services/Order/Order.API/Order.API.csproj ./Services/Order/Order.API/
COPY Services/Order/Order.Domain/Order.Domain.csproj ./Services/Order/Order.Domain/
COPY Services/Order/Order.Infrastructure/Order.Infrastructure.csproj ./Services/Order/Order.Infrastructure/

RUN dotnet restore ./Services/Order/Order.API/Order.API.csproj

# Copy everything else and build
COPY . ./
WORKDIR /app/Services/Order/Order.API
RUN dotnet publish -c Release -o /out

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app
COPY --from=build /out ./

ENTRYPOINT ["dotnet", "Order.API.dll"]
