# Build and Push Docker Images for BookManagement
param(
    [string]$Registry = "bookmanagement",
    [string]$Tag = "latest"
)

Write-Host "Building and pushing Docker images for BookManagement..." -ForegroundColor Green

# Build Catalog API
Write-Host "Building Catalog API..." -ForegroundColor Yellow
docker build -f Services/Catalog/Catalog.API/Dockerfile -t $Registry/catalog-api:$Tag .

# Build Order API
Write-Host "Building Order API..." -ForegroundColor Yellow
docker build -f Services/Order/Order.API/Dockerfile -t $Registry/order-api:$Tag .

# Build API Gateway
Write-Host "Building API Gateway..." -ForegroundColor Yellow
docker build -f Gateway/Dockerfile -t $Registry/api-gateway:$Tag .

# Push images (uncomment if you have a registry)
# Write-Host "Pushing images to registry..." -ForegroundColor Yellow
# docker push $Registry/catalog-api:$Tag
# docker push $Registry/order-api:$Tag
# docker push $Registry/api-gateway:$Tag

Write-Host "Docker images built successfully!" -ForegroundColor Green
Write-Host "Images created:"
Write-Host "  - $Registry/catalog-api:$Tag"
Write-Host "  - $Registry/order-api:$Tag"
Write-Host "  - $Registry/api-gateway:$Tag" 