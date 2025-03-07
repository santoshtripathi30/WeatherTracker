																																											

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:8.0-alpine AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["WeatherTracker/WeatherTracker.csproj", "WeatherTracker/"]
RUN dotnet restore "./WeatherTracker/WeatherTracker.csproj" --disable-parallel # Optimize restore
COPY . . 
WORKDIR "/src/WeatherTracker"
RUN dotnet build "./WeatherTracker.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./WeatherTracker.csproj" -c $BUILD_CONFIGURATION -o /app/publish --no-restore /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine AS final
WORKDIR /app
COPY --from=publish /app/publish .
# Strip debug symbols to reduce size further (optional)
RUN dotnet publish --self-contained false -c Release -o /app/publish /p:PublishTrimmed=true
ENTRYPOINT ["dotnet", "WeatherTracker.dll"]