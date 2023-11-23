export ASPNETCORE_ENVIRONMENT=Migrations

dotnet ef database update \
    --project src/Core/Nexus.Core.Infra.Data.${1:-MySql}/Nexus.Core.Infra.Data.${1:-MySql}.csproj \
    --startup-project src/Core/Nexus.Core.Api/Nexus.Core.Api.csproj \
    --context ${1:-MySql}${1:-Core}DbContext
