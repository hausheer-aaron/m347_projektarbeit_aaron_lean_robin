FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app
 
COPY . .
RUN dotnet publish ./src/*.csproj -c Release -o /out
 
FROM nginx:alpine
COPY --from=build /out/wwwroot /usr/share/nginx/html
 
EXPOSE 80
 
CMD ["nginx", "-g", "daemon off;"]