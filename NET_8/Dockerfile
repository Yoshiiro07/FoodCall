# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy project files
COPY FoodCall.API/FoodCall.API.csproj FoodCall.API/
COPY FoodCall.Application/FoodCall.Application.csproj FoodCall.Application/
COPY FoodCall.Domain/FoodCall.Domain.csproj FoodCall.Domain/
COPY FoodCall.Infrastructure/FoodCall.Infrastructure.csproj FoodCall.Infrastructure/

# Restore dependencies
RUN dotnet restore FoodCall.API/FoodCall.API.csproj

# Copy all source code
COPY . .

# Build the application
WORKDIR /src/FoodCall.API
RUN dotnet build -c Release -o /app/build

# Publish the application
FROM build AS publish
RUN dotnet publish -c Release -o /app/publish /p:UseAppHost=false

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

# Copy published files from publish stage
COPY --from=publish /app/publish .

# Expose port
EXPOSE 8080
EXPOSE 8081

# Set environment variables
ENV ASPNETCORE_URLS=http://+:8080
ENV ASPNETCORE_ENVIRONMENT=Production

# Run the application
ENTRYPOINT ["dotnet", "FoodCall.API.dll"]
