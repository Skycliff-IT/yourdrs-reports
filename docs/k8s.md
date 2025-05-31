# Managing Kubernetes Contexts and Deployments

This guide explains how to manage Kubernetes contexts and deploy resources using `kubectl`.

## 1. List Available Contexts

Lists all configured contexts and highlights the current one:

```sh
kubectl config get-contexts
```

**Example Output:**
```
CURRENT   NAME              CLUSTER           AUTHINFO                                    NAMESPACE
*         aks-weatherdemo   aks-weatherdemo   clusterUser_rg-docker-k8s_aks-weatherdemo
          docker-desktop    docker-desktop    docker-desktop
```

## 2. Switch Context

Switches to Docker Desktop:

```sh
kubectl config use-context <context-name>
```

**Example:**
```sh
kubectl config use-context docker-desktop
Switched to context "docker-desktop".
```

## 3. Confirm the Active Context

Verify the context switch:

```sh
kubectl config current-context
```

**Example Output:**
```
docker-desktop
```

## 4. Apply Deployment and Service Manifests

Deploy resources defined in YAML files:

```sh
kubectl apply -f k8s/deployment.yml
kubectl apply -f k8s/service.yml
```

**Example Output:**
```
deployment.apps/va-api created
service/va-api-service created
```

## 5. Verify Service Status

Check the status of the deployed service:

```sh
kubectl get service va-api-service
```

**Example Output:**
```
NAME             TYPE           CLUSTER-IP     EXTERNAL-IP   PORT(S)        AGE
va-api-service   LoadBalancer   10.96.138.88   <pending>     80:32649/TCP   8s
```

> **Note:**  
> When using the `LoadBalancer` service type in a local Kubernetes environment (such as Docker Desktop), the `EXTERNAL-IP` may remain in `<pending>` state. Local clusters typically do not provision external load balancers automatically.

## 6. Example: Inspecting Services and Pods

Below are example commands and outputs for inspecting services, describing a service, checking pod status, and viewing logs.

### List All Services

```sh
kubectl get services
```

**Example Output:**
```
NAME              TYPE           CLUSTER-IP     EXTERNAL-IP   PORT(S)        AGE
kubernetes        ClusterIP      10.96.0.1      <none>        443/TCP        49d
va-api-service    LoadBalancer   10.96.138.88   <pending>     80:32649/TCP   18m
weather-service   LoadBalancer   10.99.99.87    localhost     80:32606/TCP   25d
```

### Describe a Service

```sh
kubectl describe service va-api-service
```

**Example Output:**
```
Name:                     va-api-service
Namespace:                default
Labels:                   <none>
Annotations:              <none>
Selector:                 app=va-api
Type:                     LoadBalancer
IP Family Policy:         SingleStack
IP Families:              IPv4
IP:                       10.96.138.88
IPs:                      10.96.138.88
Port:                     <unset>  80/TCP
TargetPort:               8080/TCP
NodePort:                 <unset>  32649/TCP
Endpoints:
Session Affinity:         None
External Traffic Policy:  Cluster
Events:                   <none>
```

### Check Pod Status

```sh
kubectl get pods
```

**Example Output:**
```
NAME                               READY   STATUS             RESTARTS           AGE
va-api-7b6d99866d-dwxmk            0/1     ImagePullBackOff   0                  21m
```

### View Pod Logs

```sh
kubectl logs va-api-7b6d99866d-dwxmk
```
