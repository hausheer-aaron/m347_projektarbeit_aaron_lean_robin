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
 
## Cloud deployment

Die Web-Applikation wird über den Cloud-Provider Railway öffentlich bereitgestellt. Dabei werden dieselben Docker Images verwendet, die zuvor auf DockerHub veröffentlicht wurden (`hausheeraaron/docker-backend` und `hausheeraaron/docker-frontend`).

Um eine lokale Änderung der Applikation zu deployen, werden folgende Schritte durchgeführt:

1. Änderungen lokal im Projekt durchführen.
2. Änderungen committen und auf den `main` Branch pushen.
3. Die GitLab CI/CD Pipeline baut automatisch neue Docker Images.
4. Die Images werden auf DockerHub veröffentlicht.
5. Der Service wird in Railway erneut deployt, damit die neuen Images verwendet werden.

 - Backend: https://docker-backend-production-310a.up.railway.app/swagger/index.html

 - Frontend: https://docker-frontend-production-23d8.up.railway.app/
