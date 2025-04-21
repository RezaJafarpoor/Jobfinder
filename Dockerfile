# stage 1 : Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app
COPY . ./
WORKDIR /app/Jobfinder.Api
RUN dotnet restore

RUN dotnet publish -c Release -o /app/publish

# stage 2 Runtime

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .
EXPOSE 80
ENTRYPOINT ["dotnet", "Jobfinder.Api.dll"]
