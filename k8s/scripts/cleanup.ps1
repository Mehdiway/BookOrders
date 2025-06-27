# Cleanup BookManagement from Kubernetes
param(
    [string]$Namespace = "bookmanagement"
)

Write-Host "Cleaning up BookManagement from Kubernetes..." -ForegroundColor Green

# Delete all resources in the namespace
Write-Host "Deleting all resources in namespace '$Namespace'..." -ForegroundColor Yellow
kubectl delete namespace $Namespace

# Wait for namespace deletion
Write-Host "Waiting for namespace deletion..." -ForegroundColor Yellow
kubectl wait --for=delete namespace/$Namespace --timeout=300s

Write-Host "Cleanup completed successfully!" -ForegroundColor Green 