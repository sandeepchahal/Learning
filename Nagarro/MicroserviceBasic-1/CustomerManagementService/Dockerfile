# Use the .NET SDK image for the build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy the .csproj file and restore dependencies
COPY ["CustomerManagementService.csproj", "."]
RUN dotnet restore "CustomerManagementService.csproj"

# Copy the rest of the project files and build the project
COPY . .
RUN dotnet build "CustomerManagementService.csproj" -c Release -o /app/build

# Publish the application
FROM build AS publish
RUN dotnet publish "CustomerManagementService.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Use the .NET ASP.NET runtime image for the final stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CustomerManagementService.dll"]
