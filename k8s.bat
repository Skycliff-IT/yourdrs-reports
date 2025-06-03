docker build -t va-api:latest -f src/VA.API/Dockerfile src

kubectl apply -f k8s/mysql.yml
kubectl apply -f k8s/deployment.yml
kubectl apply -f k8s/service.yml
kubectl get svc va-api-service

kubectl get pods
kubectl describe pod <va-api-pod-name>

kubectl delete pod -l app=va-api

kubectl get svc
kubectl logs deployment/va-api

kubectl describe pod va-api-844dbb9c-dn8ww