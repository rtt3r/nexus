export ASPNETCORE_ENVIRONMENT=Migrations

dotnet ef migrations remove \
    --project src/Core/Nexus.Core.Infra.Data.MySql/Nexus.Core.Infra.Data.MySql.csproj \
    --startup-project src/Core/Nexus.Core.Api/Nexus.Core.Api.csproj \
    --context MySql${1:-Core}DbContext

dotnet ef migrations remove \
    --project src/Core/Nexus.Core.Infra.Data.Npgsql/Nexus.Core.Infra.Data.Npgsql.csproj \
    --startup-project src/Core/Nexus.Core.Api/Nexus.Core.Api.csproj \
    --context Npgsql${1:-Core}DbContext

dotnet ef migrations remove \
    --project src/Core/Nexus.Core.Infra.Data.SqlServer/Nexus.Core.Infra.Data.SqlServer.csproj \
    --startup-project src/Core/Nexus.Core.Api/Nexus.Core.Api.csproj \
    --context SqlServer${1:-Core}DbContext
