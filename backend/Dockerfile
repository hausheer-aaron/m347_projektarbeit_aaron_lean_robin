FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY src/WeatherApp.API.csproj src/
RUN dotnet restore src/WeatherApp.API.csproj

COPY src/ src/
RUN dotnet publish src/WeatherApp.API.csproj -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

ENV ASPNETCORE_URLS=http://+:80
EXPOSE 80

COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "WeatherApp.API.dll"]