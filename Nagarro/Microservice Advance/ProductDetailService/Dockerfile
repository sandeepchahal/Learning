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
COPY ["GlobalUtilities/GlobalUtilities.csproj", "GlobalUtilities/"]

# Copy the API project
COPY ["ProductDetailService/ProductDetailService.csproj", "ProductDetailService/"]

# Restore the dependencies for both projects
RUN dotnet restore "ProductDetailService/ProductDetailService.csproj"

# Copy all the source files to the build context
COPY . .

# Build the API project
WORKDIR "/src/ProductDetailService"
RUN dotnet build "ProductDetailService.csproj" -c Release -o /app/build

# Publish the API project
FROM build AS publish
RUN dotnet publish "ProductDetailService.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Final image with the runtime environment
FROM base AS final
WORKDIR /app

# Copy the published output from the publish stage
COPY --from=publish /app/publish .

# Set the entry point for the container
ENTRYPOINT ["dotnet", "ProductDetailService.dll"]
