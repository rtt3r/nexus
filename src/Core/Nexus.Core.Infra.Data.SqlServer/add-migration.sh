export ASPNETCORE_ENVIRONMENT=Migrations
dotnet ef migrations add "$1" --startup-project ../Nexus.Core.$2/Nexus.Core.$2.csproj --context SqlServer$3DbContext --output-dir Migrations/$3
