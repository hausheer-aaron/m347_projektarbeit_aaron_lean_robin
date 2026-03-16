# M347_Projektarbeit_Aaron_Lean_Robin



# Local Development

## Images Dockerhub

- Backend: https://hub.docker.com/repository/docker/hausheeraaron/docker-backend

- Frontend: https://hub.docker.com/repository/docker/hausheeraaron/docker-frontend

## Build

- Backend: `docker build -t docker-backend .`

- Frontend: `docker build -t docker-frontend .`

## Run

- Backend: `docker run --rm -p 5000:80 docker-backend`

  Open in Browser: localhost:5000/swagger  

- Frontend: `docker run --rm -p 8080:80 docker-frontend`

  Open in Browser: localhost:8080

 # Local Development with Compose
 ## Build
 - `docker compose --build -d`
 ## Run
 - `docker compose up -d`


 # Deployment
 ## Build and publish images
 
Die Docker Images für das Frontend und Backend werden auf DockerHub veröffentlicht.

Zum manuellen Builden und Pushen der Images wird das Script `build-and-push.sh` verwendet. Dieses Script baut die beiden Images und pusht sie auf DockerHub (`hausheeraaron/docker-frontend` und `hausheeraaron/docker-backend`).

Zusätzlich ist eine GitLab CI/CD Pipeline eingerichtet. Immer wenn Änderungen in den `main` Branch gepusht werden, baut die Pipeline automatisch die Docker Images und pusht sie ebenfalls auf DockerHub. Für den Login werden die GitLab CI/CD Variablen `DOCKERHUB_USERNAME` und `DOCKERHUB_TOKEN` verwendet.
 