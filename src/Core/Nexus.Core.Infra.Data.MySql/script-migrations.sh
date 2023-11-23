export ASPNETCORE_ENVIRONMENT=Migrations
dotnet ef migrations script --startup-project ../Nexus.Core.Api/Nexus.Core.Api.csproj --context MySql${2:-Core}DbContext
