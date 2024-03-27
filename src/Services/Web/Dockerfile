ARG NET_IMAGE=7.0
FROM mcr.microsoft.com/dotnet/aspnet:${NET_IMAGE} AS base
WORKDIR /app 
#EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:${NET_IMAGE} AS build
WORKDIR /src

COPY *.sln .
COPY src/Services ./src/Services
RUN dotnet restore "PixieFit.sln"

COPY . .
WORKDIR src/Services/Web
RUN dotnet publish "Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app 
COPY --from=build /app/publish .
#CMD ["sh"]
ENTRYPOINT ["dotnet", "Web.dll"]
HEALTHCHECK CMD curl --fail http://localhost:8080/healthz || exit