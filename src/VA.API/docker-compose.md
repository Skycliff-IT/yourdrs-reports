# Build images defined in the compose file
docker-compose build

# Start services in the background (detached mode)
docker-compose up -d

# Start services in the foreground (shows logs)
docker-compose up

# Stop services
docker-compose down

# Stop services but keep containers
docker-compose stop

# Start stopped services
docker-compose start

# View running services
docker-compose ps

# View logs for all services
docker-compose logs

# View logs for a specific service (e.g., va.api)
docker-compose logs va.api

# Run a one-off command in a service container (e.g., bash in va.api)
docker-compose run va.api bash

# Remove stopped containers, networks, images, and volumes
docker-compose down --volumes --rmi all


cd.. (parent folder, where there is sln file contains)
docker build -f VA.API/Dockerfile -t va-api .