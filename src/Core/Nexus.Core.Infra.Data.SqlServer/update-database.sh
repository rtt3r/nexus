export ASPNETCORE_ENVIRONMENT=Migrations
dotnet ef database update --startup-project ../Nexus.Core.Api/Nexus.Core.Api.csproj --context SqlServerCoreDbContext
