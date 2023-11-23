export ASPNETCORE_ENVIRONMENT=Migrations
dotnet ef migrations script --startup-project ../Nexus.Core.Api/Nexus.Core.Api.csproj --context SqlServer${2:-Core}DbContext
