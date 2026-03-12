# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 80

# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY ["nuget.config", "."]
COPY ["src/Hcm/Nexus.Hcm.Worker/Nexus.Hcm.Worker.csproj", "src/Hcm/Nexus.Hcm.Worker/"]
COPY ["src/Infra/Nexus.Infra.Crosscutting/Nexus.Infra.Crosscutting.csproj", "src/Infra/Nexus.Infra.Crosscutting/"]
COPY ["src/Infra/Nexus.Infra.Data.Query/Nexus.Infra.Data.Query.csproj", "src/Infra/Nexus.Infra.Data.Query/"]
COPY ["src/Infra/Nexus.Infra.Data.EventSourcing/Nexus.Infra.Data.EventSourcing.csproj", "src/Infra/Nexus.Infra.Data.EventSourcing/"]
COPY ["src/Hcm/Nexus.Hcm.Application/Nexus.Hcm.Application.csproj", "src/Hcm/Nexus.Hcm.Application/"]
COPY ["src/Hcm/Nexus.Hcm.Model/Nexus.Hcm.Model.csproj", "src/Hcm/Nexus.Hcm.Model/"]
COPY ["src/Hcm/Nexus.Hcm.Domain/Nexus.Hcm.Domain.csproj", "src/Hcm/Nexus.Hcm.Domain/"]
COPY ["src/Hcm/Nexus.Hcm.Infra.Data/Nexus.Hcm.Infra.Data.csproj", "src/Hcm/Nexus.Hcm.Infra.Data/"]
COPY ["src/Hcm/Nexus.Hcm.Infra.Data.Query/Nexus.Hcm.Infra.Data.Query.csproj", "src/Hcm/Nexus.Hcm.Infra.Data.Query/"]
COPY ["src/Hcm/Nexus.Hcm.Infra.IoC/Nexus.Hcm.Infra.IoC.csproj", "src/Hcm/Nexus.Hcm.Infra.IoC/"]

RUN dotnet restore "src/Hcm/Nexus.Hcm.Worker/Nexus.Hcm.Worker.csproj"

COPY . .
WORKDIR "/src/src/Hcm/Nexus.Hcm.Worker"
RUN dotnet build "Nexus.Hcm.Worker.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Nexus.Hcm.Worker.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "Nexus.Hcm.Worker.dll"]
