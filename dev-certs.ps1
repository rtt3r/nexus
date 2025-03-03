dotnet dev-certs https --clean
dotnet dev-certs https -ep $Env:APPDATA/ASP.NET/Https/Nexus.Core.Web.pfx
dotnet dev-certs https --trust
