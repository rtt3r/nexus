#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["nuget.config", "."]
COPY ["src/Core/Nexus.Core.Worker/Nexus.Core.Worker.csproj", "src/Core/Nexus.Core.Worker/"]
COPY ["src/Core/Nexus.Core.Application/Nexus.Core.Application.csproj", "src/Core/Nexus.Core.Application/"]
COPY ["src/Infra/Nexus.Infra.Crosscutting/Nexus.Infra.Crosscutting.csproj", "src/Infra/Nexus.Infra.Crosscutting/"]
COPY ["src/Infra/Nexus.Infra.Data.Query/Nexus.Infra.Data.Query.csproj", "src/Infra/Nexus.Infra.Data.Query/"]
COPY ["src/Core/Nexus.Core.Domain/Nexus.Core.Domain.csproj", "src/Core/Nexus.Core.Domain/"]
COPY ["src/Core/Nexus.Core.Infra.Data.Query/Nexus.Core.Infra.Data.Query.csproj", "src/Core/Nexus.Core.Infra.Data.Query/"]
COPY ["src/Core/Nexus.Core.Model/Nexus.Core.Model.csproj", "src/Core/Nexus.Core.Model/"]
COPY ["src/Core/Nexus.Core.Infra.Data/Nexus.Core.Infra.Data.csproj", "src/Core/Nexus.Core.Infra.Data/"]
COPY ["src/Core/Nexus.Core.Infra.IoC/Nexus.Core.Infra.IoC.csproj", "src/Core/Nexus.Core.Infra.IoC/"]

RUN dotnet restore "src/Core/Nexus.Core.Worker/Nexus.Core.Worker.csproj"

COPY . .
WORKDIR "/src/src/Core/Nexus.Core.Worker"
RUN dotnet build "Nexus.Core.Worker.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Nexus.Core.Worker.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "Nexus.Core.Worker.dll"]
