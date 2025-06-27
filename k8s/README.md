# BookManagement Kubernetes Setup

This directory contains all the Kubernetes manifests and scripts needed to deploy the BookManagement microservices application to Kubernetes.

## Prerequisites

1. **Kubernetes Cluster**: A running Kubernetes cluster (local or cloud)
2. **kubectl**: Kubernetes command-line tool
3. **Docker**: For building container images
4. **NGINX Ingress Controller**: For external access (install if not present)

## Architecture

The application consists of the following components:

- **API Gateway**: Entry point for all external requests
- **Catalog API**: Manages book catalog and inventory
- **Order API**: Handles order processing
- **SQL Server**: Database for both services
- **RabbitMQ**: Message broker for inter-service communication

## Directory Structure

```
k8s/
├── namespace.yaml                 # Application namespace
├── configmaps/                    # Configuration
│   └── app-config.yaml
├── secrets/                       # Sensitive data
│   └── database-secrets.yaml
├── storage/                       # Persistent storage
│   ├── sqlserver-pvc.yaml
│   └── rabbitmq-pvc.yaml
├── database/                      # Database deployment
│   └── sqlserver-deployment.yaml
├── messaging/                     # Message broker
│   └── rabbitmq-deployment.yaml
├── services/                      # Microservices
│   ├── catalog-deployment.yaml
│   └── order-deployment.yaml
├── gateway/                       # API Gateway
│   └── gateway-deployment.yaml
├── ingress/                       # External access
│   └── ingress.yaml
└── scripts/                       # Deployment scripts
    ├── build-and-push-images.ps1
    ├── deploy.ps1
    └── cleanup.ps1
```

## Quick Start

### 1. Build Docker Images

```powershell
# Build all images
.\k8s\scripts\build-and-push-images.ps1

# Or with custom registry and tag
.\k8s\scripts\build-and-push-images.ps1 -Registry "your-registry" -Tag "v1.0.0"
```

### 2. Deploy to Kubernetes

```powershell
# Deploy the entire application
.\k8s\scripts\deploy.ps1

# Or with custom namespace
.\k8s\scripts\deploy.ps1 -Namespace "my-bookmanagement"
```

### 3. Access the Application

After deployment, you can access:

- **API Gateway**: http://bookmanagement.local
- **RabbitMQ Management**: http://bookmanagement.local/rabbitmq
- **Direct Catalog API**: http://bookmanagement.local/api/catalog
- **Direct Order API**: http://bookmanagement.local/api/orders

### 4. Cleanup

```powershell
# Remove all resources
.\k8s\scripts\cleanup.ps1
```

## Manual Deployment

If you prefer to deploy manually:

```bash
# 1. Create namespace
kubectl apply -f k8s/namespace.yaml

# 2. Apply configuration
kubectl apply -f k8s/configmaps/
kubectl apply -f k8s/secrets/

# 3. Deploy storage
kubectl apply -f k8s/storage/

# 4. Deploy infrastructure
kubectl apply -f k8s/database/
kubectl apply -f k8s/messaging/

# 5. Deploy services
kubectl apply -f k8s/services/
kubectl apply -f k8s/gateway/

# 6. Deploy ingress
kubectl apply -f k8s/ingress/
```

## Configuration

### Environment Variables

The application uses the following environment variables:

- `ASPNETCORE_URLS`: HTTP binding (default: http://+:80)
- `ASPNETCORE_ENVIRONMENT`: Environment (default: Production)
- `ConnectionStrings__DefaultConnection`: Database connection string
- `RabbitMQ`: RabbitMQ connection string

### Secrets

Sensitive data is stored in Kubernetes secrets:

- `sa-password`: SQL Server SA password
- `rabbitmq-user`: RabbitMQ username
- `rabbitmq-password`: RabbitMQ password

### Resource Limits

Each service has defined resource limits:

- **Catalog API**: 1Gi memory, 1000m CPU
- **Order API**: 1Gi memory, 1000m CPU
- **API Gateway**: 512Mi memory, 500m CPU
- **SQL Server**: 4Gi memory, 2000m CPU
- **RabbitMQ**: 1Gi memory, 1000m CPU

## Health Checks

All services include health checks:

- **Liveness Probe**: Checks if the service is alive
- **Readiness Probe**: Checks if the service is ready to receive traffic

## Scaling

To scale services:

```bash
# Scale Catalog API to 3 replicas
kubectl scale deployment catalog-api --replicas=3 -n bookmanagement

# Scale Order API to 3 replicas
kubectl scale deployment order-api --replicas=3 -n bookmanagement

# Scale API Gateway to 3 replicas
kubectl scale deployment api-gateway --replicas=3 -n bookmanagement
```

## Monitoring

Check the status of your deployment:

```bash
# View all pods
kubectl get pods -n bookmanagement

# View services
kubectl get services -n bookmanagement

# View ingress
kubectl get ingress -n bookmanagement

# View logs
kubectl logs -f deployment/catalog-api -n bookmanagement
kubectl logs -f deployment/order-api -n bookmanagement
kubectl logs -f deployment/api-gateway -n bookmanagement
```

## Troubleshooting

### Common Issues

1. **Images not found**: Ensure Docker images are built and available
2. **Database connection issues**: Check if SQL Server pod is running
3. **RabbitMQ connection issues**: Check if RabbitMQ pod is running
4. **Ingress not working**: Ensure NGINX Ingress Controller is installed

### Debug Commands

```bash
# Describe pods for detailed information
kubectl describe pod <pod-name> -n bookmanagement

# Check events
kubectl get events -n bookmanagement

# Port forward for direct access
kubectl port-forward service/api-gateway 8080:80 -n bookmanagement
```

## Security Considerations

1. **Secrets**: Use proper secret management in production
2. **Network Policies**: Implement network policies for service-to-service communication
3. **RBAC**: Configure proper role-based access control
4. **TLS**: Enable TLS for production deployments
5. **Image Security**: Use signed images and scan for vulnerabilities

## Production Recommendations

1. **Use a proper container registry** (Azure Container Registry, Docker Hub, etc.)
2. **Implement proper monitoring and logging** (Prometheus, Grafana, ELK stack)
3. **Set up automated backups** for databases
4. **Configure horizontal pod autoscaling**
5. **Use managed services** for databases and message brokers in production
6. **Implement proper CI/CD pipelines** 