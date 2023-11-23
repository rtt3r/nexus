export ASPNETCORE_ENVIRONMENT=Migrations
dotnet ef database update --startup-project ../Nexus.Core.Api/Nexus.Core.Api.csproj --context Npgsql${2:-Core}DbContext
