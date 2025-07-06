# Consulte https://aka.ms/customizecontainer para aprender a personalizar su contenedor de depuración y cómo Visual Studio usa este Dockerfile para compilar sus imágenes para una depuración más rápida.

# Esta fase se usa cuando se ejecuta desde VS en modo rápido (valor predeterminado para la configuración de depuración)
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 80
EXPOSE 443


# Esta fase se usa para compilar el proyecto de servicio
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["GtMotive.Estimate.Microservice.Host/GtMotive.Estimate.Microservice.Host.csproj", "GtMotive.Estimate.Microservice.Host/"]
COPY ["GtMotive.Estimate.Microservice.Api/GtMotive.Estimate.Microservice.Api.csproj", "GtMotive.Estimate.Microservice.Api/"]
COPY ["GtMotive.Estimate.Microservice.ApplicationCore/GtMotive.Estimate.Microservice.ApplicationCore.csproj", "GtMotive.Estimate.Microservice.ApplicationCore/"]
COPY ["GtMotive.Estimate.Microservice.Domain/GtMotive.Estimate.Microservice.Domain.csproj", "GtMotive.Estimate.Microservice.Domain/"]
COPY ["GtMotive.Estimate.Microservice.Infrastructure/GtMotive.Estimate.Microservice.Infrastructure.csproj", "GtMotive.Estimate.Microservice.Infrastructure/"]
RUN dotnet restore "./GtMotive.Estimate.Microservice.Host/GtMotive.Estimate.Microservice.Host.csproj"
COPY . .
WORKDIR "/src/GtMotive.Estimate.Microservice.Host"
RUN dotnet build "./GtMotive.Estimate.Microservice.Host.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Esta fase se usa para publicar el proyecto de servicio que se copiará en la fase final.
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./GtMotive.Estimate.Microservice.Host.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Esta fase se usa en producción o cuando se ejecuta desde VS en modo normal (valor predeterminado cuando no se usa la configuración de depuración)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GtMotive.Estimate.Microservice.Host.dll"]