# Base image with the runtime environment
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

# Build image with the SDK for compiling and publishing the app
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy the class library project
COPY ["JWTConfiguration/JWTConfiguration.csproj", "JWTConfiguration/"]

# Copy the API project
COPY ["CartService/CartService.csproj", "CartService/"]

# Restore the dependencies for both projects
RUN dotnet restore "CartService/CartService.csproj"

# Copy all the source files to the build context
COPY . .

# Build the API project
WORKDIR "/src/CartService"
RUN dotnet build "CartService.csproj" -c Release -o /app/build

# Publish the API project
FROM build AS publish
RUN dotnet publish "CartService.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Final image with the runtime environment
FROM base AS final
WORKDIR /app

# Copy the published output from the publish stage
COPY --from=publish /app/publish .

# Set the entry point for the container
ENTRYPOINT ["dotnet", "CartService.dll"]
