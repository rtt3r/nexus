# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 80
EXPOSE 443

# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY ["nuget.config", "."]
COPY ["src/Finance/Nexus.Finance.Api/Nexus.Finance.Api.csproj", "src/Finance/Nexus.Finance.Api/"]
COPY ["src/Infra/Nexus.Infra.Http/Nexus.Infra.Http.csproj", "src/Infra/Nexus.Infra.Http/"]
COPY ["src/Infra/Nexus.Infra.Crosscutting/Nexus.Infra.Crosscutting.csproj", "src/Infra/Nexus.Infra.Crosscutting/"]
COPY ["src/Infra/Nexus.Infra.Data.Query/Nexus.Infra.Data.Query.csproj", "src/Infra/Nexus.Infra.Data.Query/"]
COPY ["src/Infra/Nexus.Infra.Data.EventSourcing/Nexus.Infra.Data.EventSourcing.csproj", "src/Infra/Nexus.Infra.Data.EventSourcing/"]
COPY ["src/Finance/Nexus.Finance.Application/Nexus.Finance.Application.csproj", "src/Finance/Nexus.Finance.Application/"]
COPY ["src/Finance/Nexus.Finance.Model/Nexus.Finance.Model.csproj", "src/Finance/Nexus.Finance.Model/"]
COPY ["src/Finance/Nexus.Finance.Domain/Nexus.Finance.Domain.csproj", "src/Finance/Nexus.Finance.Domain/"]
COPY ["src/Finance/Nexus.Finance.Infra.Data/Nexus.Finance.Infra.Data.csproj", "src/Finance/Nexus.Finance.Infra.Data/"]
COPY ["src/Finance/Nexus.Finance.Infra.Data.Query/Nexus.Finance.Infra.Data.Query.csproj", "src/Finance/Nexus.Finance.Infra.Data.Query/"]
COPY ["src/Finance/Nexus.Finance.Infra.IoC/Nexus.Finance.Infra.IoC.csproj", "src/Finance/Nexus.Finance.Infra.IoC/"]

RUN dotnet restore "src/Finance/Nexus.Finance.Api/Nexus.Finance.Api.csproj"

COPY . .
WORKDIR "/src/src/Finance/Nexus.Finance.Api"
RUN dotnet build "Nexus.Finance.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Nexus.Finance.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "Nexus.Finance.Api.dll"]
