export ASPNETCORE_ENVIRONMENT=Migrations

dotnet ef migrations add "${2:-Core}_$1" \
    --project src/Core/Nexus.Core.Infra.Data/Nexus.Core.Infra.Data.csproj \
    --startup-project src/Core/Nexus.Core.Api/Nexus.Core.Api.csproj \
    --context ${2:-Core}DbContext \
    --output-dir Migrations/${2:-Core}
