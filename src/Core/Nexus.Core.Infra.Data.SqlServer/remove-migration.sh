export ASPNETCORE_ENVIRONMENT=Migrations
dotnet ef migrations remove --startup-project ../Nexus.Core.Api/Nexus.Core.Api.csproj --context SqlServerCoreDbContext
