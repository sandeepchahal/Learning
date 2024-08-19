# Base image with the runtime environment
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

# Build image with the SDK for compiling and publishing the app
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy the class library project
COPY ["GlobalUtilities/GlobalUtilities.csproj", "GlobalUtilities/"]

# Copy the API project
COPY ["UserService/UserService.csproj", "UserService/"]

# Restore the dependencies for both projects
RUN dotnet restore "UserService/UserService.csproj"

# Copy all the source files to the build context
COPY . .

# Build the API project
WORKDIR "/src/UserService"
RUN dotnet build "UserService.csproj" -c Release -o /app/build

# Publish the API project
FROM build AS publish
RUN dotnet publish "UserService.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Final image with the runtime environment
FROM base AS final
WORKDIR /app

# Copy the published output from the publish stage
COPY --from=publish /app/publish .

# Set the entry point for the container
ENTRYPOINT ["dotnet", "UserService.dll"]
