# Deploy BookManagement using Helm
param(
    [string]$ReleaseName = "bookmanagement",
    [string]$Namespace = "bookmanagement",
    [string]$ValuesFile = "k8s/helm/values.yaml"
)

Write-Host "Deploying BookManagement using Helm..." -ForegroundColor Green

# Check if Helm is installed
try {
    helm version --short | Out-Null
    Write-Host "Helm is installed" -ForegroundColor Green
} catch {
    Write-Host "Helm is not installed. Please install Helm first." -ForegroundColor Red
    exit 1
}

# Create namespace if it doesn't exist
Write-Host "Creating namespace '$Namespace'..." -ForegroundColor Yellow
kubectl create namespace $Namespace --dry-run=client -o yaml | kubectl apply -f -

# Deploy using Helm
Write-Host "Deploying with Helm..." -ForegroundColor Yellow
helm upgrade --install $ReleaseName ./k8s/helm `
    --namespace $Namespace `
    --values $ValuesFile `
    --wait `
    --timeout 10m

# Check deployment status
Write-Host "Checking deployment status..." -ForegroundColor Yellow
helm status $ReleaseName --namespace $Namespace

# Wait for all pods to be ready
Write-Host "Waiting for all pods to be ready..." -ForegroundColor Yellow
kubectl wait --for=condition=ready pod -l app.kubernetes.io/instance=$ReleaseName -n $Namespace --timeout=300s

Write-Host "Helm deployment completed successfully!" -ForegroundColor Green
Write-Host "You can access the application at: http://bookmanagement.local" -ForegroundColor Cyan

# Show status
Write-Host "`nPod Status:" -ForegroundColor Yellow
kubectl get pods -n $Namespace

Write-Host "`nServices:" -ForegroundColor Yellow
kubectl get services -n $Namespace 