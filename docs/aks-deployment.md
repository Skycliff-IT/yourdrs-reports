# Deploying .NET 9 API and MySQL to Azure Kubernetes Service (AKS)

## 1. Push Docker Image to Azure Container Registry (ACR)

**Create ACR (if needed):**
```sh
az acr create --resource-group <your-resource-group> --name <youracrname> --sku Basic
```

**Login to ACR:**
```sh
az acr login --name <youracrname>
```

**Tag your image:**
```sh
docker tag va-api:latest <youracrname>.azurecr.io/va-api:latest
```

**Push your image:**
```sh
docker push <youracrname>.azurecr.io/va-api:latest
```

---

## 2. Update Deployment YAML

Edit `k8s/deployment.yml` and set the image:

```yaml
image: <youracrname>.azurecr.io/va-api:latest
imagePullPolicy: Always
```

---

## 3. Create AKS Cluster (if needed)

```sh
az aks create --resource-group <your-resource-group> --name <your-aks-name> --node-count 1 --enable-addons monitoring --generate-ssh-keys
```

---

## 4. Connect kubectl to AKS

```sh
az aks get-credentials --resource-group <your-resource-group> --name <your-aks-name>
```

---

## 5. Allow AKS to Pull from ACR

```sh
az aks update -n <your-aks-name> -g <your-resource-group> --attach-acr <youracrname>
```

---

## 6. Deploy MySQL and API to AKS

```sh
kubectl apply -f k8s/mysql.yml
kubectl apply -f k8s/deployment.yml
kubectl apply -f k8s/service.yml
```

---

## 7. Get the External IP

```sh
kubectl get svc va-api-service
```

- Wait for the `EXTERNAL-IP` to be assigned.
- Access your API at `http://<EXTERNAL-IP>/swagger`.

---

**Tip:** For production, consider using Azure Database for MySQL instead of running MySQL in a pod.
