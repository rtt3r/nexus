export ASPNETCORE_ENVIRONMENT=Migrations

dotnet ef migrations script \
    --project src/Core/Nexus.Core.Infra.Data.${1:-MySql}/Nexus.Core.Infra.Data.${1:-MySql}.csproj \
    --startup-project src/Core/Nexus.Core.Api/Nexus.Core.Api.csproj \
    --context ${1:-MySql}${2:-Core}DbContext
