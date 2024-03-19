ARG NET_IMAGE=7.0
FROM mcr.microsoft.com/dotnet/sdk:${NET_IMAGE} AS build
WORKDIR /src

COPY ../../../*.sln . 
COPY *.csproj .
#WORKDIR /src/app
RUN dotnet restore

COPY . .
RUN dotnet publish -c Release -o /app

FROM mcr.microsoft.com/dotnet/aspnet:${NET_IMAGE}
WORKDIR /app 
COPY --from=build /app ./
ENTRYPOINT ["dotnet", "Web.dll"]