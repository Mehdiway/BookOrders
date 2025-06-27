# Deploy BookManagement to Kubernetes
param(
    [string]$Namespace = "bookmanagement"
)

Write-Host "Deploying BookManagement to Kubernetes..." -ForegroundColor Green

# Create namespace
Write-Host "Creating namespace..." -ForegroundColor Yellow
kubectl apply -f k8s/namespace.yaml

# Apply ConfigMaps and Secrets
Write-Host "Applying ConfigMaps and Secrets..." -ForegroundColor Yellow
kubectl apply -f k8s/configmaps/
kubectl apply -f k8s/secrets/

# Apply Storage
Write-Host "Applying Storage..." -ForegroundColor Yellow
kubectl apply -f k8s/storage/

# Deploy Infrastructure (Database and Message Broker)
Write-Host "Deploying Infrastructure..." -ForegroundColor Yellow
kubectl apply -f k8s/database/
kubectl apply -f k8s/messaging/

# Wait for infrastructure to be ready
Write-Host "Waiting for infrastructure to be ready..." -ForegroundColor Yellow
kubectl wait --for=condition=ready pod -l app=sqlserver -n $Namespace --timeout=300s
kubectl wait --for=condition=ready pod -l app=rabbitmq -n $Namespace --timeout=300s

# Deploy Services
Write-Host "Deploying Services..." -ForegroundColor Yellow
kubectl apply -f k8s/services/

# Deploy Gateway
Write-Host "Deploying API Gateway..." -ForegroundColor Yellow
kubectl apply -f k8s/gateway/

# Deploy Ingress
Write-Host "Deploying Ingress..." -ForegroundColor Yellow
kubectl apply -f k8s/ingress/

# Wait for all pods to be ready
Write-Host "Waiting for all pods to be ready..." -ForegroundColor Yellow
kubectl wait --for=condition=ready pod -l app=catalog-api -n $Namespace --timeout=300s
kubectl wait --for=condition=ready pod -l app=order-api -n $Namespace --timeout=300s
kubectl wait --for=condition=ready pod -l app=api-gateway -n $Namespace --timeout=300s

Write-Host "Deployment completed successfully!" -ForegroundColor Green
Write-Host "You can access the application at: http://bookmanagement.local" -ForegroundColor Cyan
Write-Host "RabbitMQ Management UI: http://bookmanagement.local/rabbitmq" -ForegroundColor Cyan

# Show status
Write-Host "`nPod Status:" -ForegroundColor Yellow
kubectl get pods -n $Namespace

Write-Host "`nServices:" -ForegroundColor Yellow
kubectl get services -n $Namespace 