ARG NET_IMAGE=7.0
FROM mcr.microsoft.com/dotnet/aspnet:${NET_IMAGE} AS base
WORKDIR /app 
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:${NET_IMAGE} AS build
WORKDIR /src

COPY src/Services/Web ./src/Services/Web
RUN dotnet restore "PixieFit.sln"

# COPY src/BuildingBlocks ./src/BuildingBlocks/
# COPY src/Services ./src/Services/
# COPY src/ApiGateways ./src/ApiGateways/
# COPY ["docker-compose.dcproj", "NuGet.config", "SmartPOS.sln", "./"]
# RUN dotnet restore "SmartPOS.sln"

COPY . .
WORKDIR "/src/src/Services/Web"

FROM build AS publish
RUN dotnet publish "Catalog.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Loop.SmartPOS.Services.Catalog.API.dll"]

RUN apt-get update && apt-get install -y --allow-unauthenticated libc6-dev libgdiplus libx11-dev