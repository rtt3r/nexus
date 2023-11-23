export ASPNETCORE_ENVIRONMENT=Migrations
dotnet ef migrations add "$1" --startup-project ../Nexus.Core.Api/Nexus.Core.Api.csproj --context MySqlCoreDbContext --output-dir Migrations/Core
