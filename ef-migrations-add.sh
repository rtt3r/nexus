export ASPNETCORE_ENVIRONMENT=Migrations

dotnet ef migrations add "$1" \
    --project src/Core/Nexus.Core.Infra.Data.MySql/Nexus.Core.Infra.Data.MySql.csproj \
    --startup-project src/Core/Nexus.Core.Api/Nexus.Core.Api.csproj \
    --context MySql${2:-Core}DbContext \
    --output-dir Migrations/${2:-Core}

export ASPNETCORE_ENVIRONMENT=Migrations
dotnet ef migrations add "$1" \
    --project src/Core/Nexus.Core.Infra.Data.Npgsql/Nexus.Core.Infra.Data.Npgsql.csproj \
    --startup-project src/Core/Nexus.Core.Api/Nexus.Core.Api.csproj \
    --context Npgsql${2:-Core}DbContext \
    --output-dir Migrations/${2:-Core}

export ASPNETCORE_ENVIRONMENT=Migrations
dotnet ef migrations add "$1" \
    --project src/Core/Nexus.Core.Infra.Data.SqlServer/Nexus.Core.Infra.Data.SqlServer.csproj \
    --startup-project src/Core/Nexus.Core.Api/Nexus.Core.Api.csproj \
    --context SqlServer${2:-Core}DbContext \
    --output-dir Migrations/${2:-Core}
