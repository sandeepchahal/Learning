# Base image for running the application
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Build image for compiling the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copy project files and restore dependencies
COPY APIGateway.csproj ./
RUN dotnet restore "APIGateway.csproj"

# Copy all source files and build the application
COPY . .
WORKDIR "/src"
RUN dotnet build "APIGateway.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Publish the application to a folder
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "APIGateway.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Final image with the application
FROM base AS final
WORKDIR /app

# Copy the published application and Ocelot configuration file
COPY --from=publish /app/publish .
COPY ocelot.Development.json /app/

# Set the entry point for the application
ENTRYPOINT ["dotnet", "APIGateway.dll"]
