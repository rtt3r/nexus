export ASPNETCORE_ENVIRONMENT=Migrations
dotnet ef migrations add "$1" --startup-project ../Nexus.Core.Api/Nexus.Core.Api.csproj --context MySql${2:-Core}DbContext --output-dir Migrations/${2:-Core}
