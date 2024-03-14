ARG NET_IMAGE=7.0
FROM mcr.microsoft.com/dotnet/sdk:${NET_IMAGE} AS build
WORKDIR /src

COPY *.sln . 
COPY src/Services/Web/*.csproj ./app
RUN dotnet restore

COPY . ./app
WORKDIR /src/app
RUN dotnet publish -c Release -o /app

FROM mcr.microsoft.com/dotnet/aspnet:${NET_IMAGE}
WORKDIR /app 
COPY --from=build /app ./
#ENTRYPOINT ["dotnet", "Loop.SmartPOS.Services.Catalog.API.dll"]