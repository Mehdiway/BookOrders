# Build stage
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app

# Copy solution and restore dependencies
COPY *.sln ./
COPY Shared/Shared.csproj ./Shared/
COPY Services/Catalog/Catalog.API/Catalog.API.csproj ./Services/Catalog/Catalog.API/
COPY Services/Catalog/Catalog.Application/Catalog.Application.csproj ./Services/Catalog/Catalog.Application/
COPY Services/Catalog/Catalog.Domain/Catalog.Domain.csproj ./Services/Catalog/Catalog.Domain/
COPY Services/Catalog/Catalog.Infrastructure/Catalog.Infrastructure.csproj ./Services/Catalog/Catalog.Infrastructure/

RUN dotnet restore ./Services/Catalog/Catalog.API/Catalog.API.csproj

# Copy everything else and build
COPY *.sln ./
COPY Directory.Build.props ./
COPY Shared/ ./Shared/
COPY Services/Catalog/Catalog.API/ ./Services/Catalog/Catalog.API/
COPY Services/Catalog/Catalog.Application/ ./Services/Catalog/Catalog.Application/
COPY Services/Catalog/Catalog.Domain/ ./Services/Catalog/Catalog.Domain/
COPY Services/Catalog/Catalog.Infrastructure/ ./Services/Catalog/Catalog.Infrastructure/
WORKDIR /app/Services/Catalog/Catalog.API
RUN dotnet publish -c Release -o /out

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app
COPY --from=build /out ./

ENTRYPOINT ["dotnet", "Catalog.API.dll"]
