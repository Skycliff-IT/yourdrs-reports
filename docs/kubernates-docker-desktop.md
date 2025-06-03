# Deploying on Docker Desktop with Kubernetes

## Prerequisites

- [Docker Desktop](https://www.docker.com/products/docker-desktop/) installed and running
- Kubernetes enabled in Docker Desktop (Settings > Kubernetes > Enable Kubernetes)
- [kubectl](https://kubernetes.io/docs/tasks/tools/) installed

## Steps

1. **Build the Docker Image**

    Open a terminal in your project directory and run:

    ```sh
    docker build -t va-api:latest -f src/VA.API/Dockerfile src
    ```

2. **Start Docker Desktop and Enable Kubernetes**

    - Open Docker Desktop.
    - Go to Settings > Kubernetes.
    - Check "Enable Kubernetes" and apply.

3. **Apply Kubernetes Manifests**

    ```sh
    kubectl apply -f k8s/mysql.yml
    kubectl apply -f k8s/deployment.yml
    kubectl apply -f k8s/service.yml
    ```

4. **Check Services**

    ```sh
    kubectl get svc va-api-service
    ```

5. **Check Pods**

    ```sh
    kubectl get pods
    ```

6. **Port Forward the Service**

    Forward local port 8080 to service port 80:

    ```sh
    kubectl port-forward svc/va-api-service 8080:80
    ```

7. **Describe a Pod**

    Replace `<va-api-pod-name>` with the actual pod name:

    ```sh
    kubectl describe pod <va-api-pod-name>
    ```

8. **Delete Pods (if needed)**

    ```sh
    kubectl delete pod -l app=va-api
    ```

9. **Get All Services**

    ```sh
    kubectl get svc
    ```

10. **View Logs**

    ```sh
    kubectl logs deployment/va-api
    ```

11. **Describe a Specific Pod**

    Replace `va-api-844dbb9c-dn8ww` with your pod name:

    ```sh
    kubectl describe pod va-api-844dbb9c-dn8ww
    ```

---

**Note:**  
- Make sure your YAML files are correct and in the `k8s` folder.
- Use `kubectl get pods` to find the actual pod names.
- If your deployment uses the local Docker image, ensure the imagePullPolicy is set to `IfNotPresent` or `Never`.