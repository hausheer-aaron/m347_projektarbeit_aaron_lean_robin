# M347_Projektarbeit_Aaron_Lean_Robin



# Local Development

## Images Dockerhub

- Backend: https://hub.docker.com/repository/docker/hausheeraaron/my-csharp-api

## Build

- Backend: `docker build -t my-csharp-api .`

- Frontend: `docker build -t frontend-app .`

## Run

- Backend: `docker run --rm -p 5000:80 my-csharp-api`

  Open in Browser: localhost:5000/swagger  

- Frontend: `docker run --rm -p 8080:80 frontend-app`

  Open in Browser: localhost:8080

 # Local Development with Compose
 ## Build
 - `docker compose --build -d`
 ## Run
 - `docker compose up -d`
  
 