export ASPNETCORE_ENVIRONMENT=Migrations
dotnet ef migrations script --startup-project ../Nexus.Core.$1/Nexus.Core.$1.csproj --context $2DbContext
