docker build -t hausheeraaron/docker-frontend:latest ./frontend
docker build -t hausheeraaron/docker-backend:latest ./backend

docker push hausheeraaron/docker-frontend:latest
docker push hausheeraaron/docker-backend:latest