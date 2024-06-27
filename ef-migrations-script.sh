export ASPNETCORE_ENVIRONMENT=Migrations

dotnet ef migrations script \
    --project src/Core/Nexus.Core.Infra.Data.${1:-Npgsql}/Nexus.Core.Infra.Data.${1:-Npgsql}.csproj \
    --startup-project src/Core/Nexus.Core.Api/Nexus.Core.Api.csproj \
    --context ${1:-Npgsql}${2:-Core}DbContext
