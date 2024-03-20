ARG NET_IMAGE=7.0
FROM mcr.microsoft.com/dotnet/sdk:${NET_IMAGE} AS build
WORKDIR /src

COPY *.sln . 
COPY *.csproj .
RUN dotnet restore "PixieFit.sln"

COPY . .
RUN dotnet publish "Web.csproj" -c Release -o /app

FROM mcr.microsoft.com/dotnet/aspnet:${NET_IMAGE}
WORKDIR /app 
COPY --from=build /app ./
ENTRYPOINT ["dotnet", "Web.dll"]