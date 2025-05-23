﻿FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["APIGateway.csproj", "."]
RUN dotnet restore "APIGateway.csproj"

COPY . .
RUN dotnet build "APIGateway.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "APIGateway.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app

# Copy the published application and Ocelot configuration file
COPY --from=publish /app/publish .
COPY ocelot.Development.json /app/

ENTRYPOINT ["dotnet", "APIGateway.dll"]
