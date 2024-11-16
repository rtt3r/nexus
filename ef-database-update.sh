export ASPNETCORE_ENVIRONMENT=Migrations

dotnet ef database update \
    --project src/Core/Nexus.Core.Infra.Data/Nexus.Core.Infra.Data.csproj \
    --startup-project src/Core/Nexus.Core.Api/Nexus.Core.Api.csproj \
    --context ${1:-Core}DbContext
