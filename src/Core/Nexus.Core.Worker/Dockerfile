# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 80

# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY ["nuget.config", "."]
COPY ["src/Core/Nexus.Core.Worker/Nexus.Core.Worker.csproj", "src/Core/Nexus.Core.Worker/"]
COPY ["src/Infra/Nexus.Infra.Crosscutting/Nexus.Infra.Crosscutting.csproj", "src/Infra/Nexus.Infra.Crosscutting/"]
COPY ["src/Infra/Nexus.Infra.Data.Query/Nexus.Infra.Data.Query.csproj", "src/Infra/Nexus.Infra.Data.Query/"]
COPY ["src/Infra/Nexus.Infra.Data.EventSourcing/Nexus.Infra.Data.EventSourcing.csproj", "src/Infra/Nexus.Infra.Data.EventSourcing/"]
COPY ["src/Core/Nexus.Core.Application/Nexus.Core.Application.csproj", "src/Core/Nexus.Core.Application/"]
COPY ["src/Core/Nexus.Core.Model/Nexus.Core.Model.csproj", "src/Core/Nexus.Core.Model/"]
COPY ["src/Core/Nexus.Core.Domain/Nexus.Core.Domain.csproj", "src/Core/Nexus.Core.Domain/"]
COPY ["src/Core/Nexus.Core.Infra.Data/Nexus.Core.Infra.Data.csproj", "src/Core/Nexus.Core.Infra.Data/"]
COPY ["src/Core/Nexus.Core.Infra.Data.Query/Nexus.Core.Infra.Data.Query.csproj", "src/Core/Nexus.Core.Infra.Data.Query/"]
COPY ["src/Core/Nexus.Core.Infra.IoC/Nexus.Core.Infra.IoC.csproj", "src/Core/Nexus.Core.Infra.IoC/"]

RUN dotnet restore "src/Core/Nexus.Core.Worker/Nexus.Core.Worker.csproj"

COPY . .
WORKDIR "/src/src/Core/Nexus.Core.Worker"
RUN dotnet build "Nexus.Core.Worker.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Nexus.Core.Worker.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "Nexus.Core.Worker.dll"]
