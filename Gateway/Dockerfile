# Build stage
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app

# Copy solution and restore dependencies
COPY *.sln ./
COPY Gateway/Gateway.csproj ./Gateway/

RUN dotnet restore ./Gateway/Gateway.csproj

# Copy everything else and build
COPY . ./
WORKDIR /app/Gateway
RUN dotnet publish -c Release -o /out

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app
COPY --from=build /out ./

ENTRYPOINT ["dotnet", "Gateway.dll"]
