#!/bin/bash

export ASPNETCORE_ENVIRONMENT=Migrations

# Function to remove last migration
function removeMigrations {
    local context=$1
    local migrationsProject=$2
    local startupProject=$3
    local dbContext=$4

    echo "Removing last $context migration..."

    dotnet ef migrations remove \
        --project "$migrationsProject" \
        --startup-project "$startupProject" \
        --context "$dbContext"
}

# Input handling
if [ "$#" -eq 0 ]; then
    echo "Enter context (e.g. Core, Hcm, EventSourcing):"
    read context
else
    context=$1
fi

dbContext="${context}DbContext"
migrationsProject="src/$context/Nexus.$context.Infra.Data/Nexus.$context.Infra.Data.csproj"
startupProject="src/$context/Nexus.$context.Api/Nexus.$context.Api.csproj"

if [ "$context" == "EventSourcing" ]; then
    migrationsProject="src/Infra/Nexus.Infra.Data.EventSourcing/Nexus.Infra.Data.EventSourcing.csproj"
    startupProject="src/Core/Nexus.Core.Worker/Nexus.Core.Worker.csproj"
    dbContext="EventSourcingDbContext"
fi

removeMigrations "$context" "$migrationsProject" "$startupProject" "$dbContext"
