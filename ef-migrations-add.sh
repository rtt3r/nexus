#!/bin/bash

export ASPNETCORE_ENVIRONMENT=Migrations

# Function to add migration
function addMigration {
    local context=$1
    local name=$2
    local migrationsProject=$3
    local startupProject=$4
    local dbContext=$5

    if [ -z "$name" ]; then
        echo "Error: missing parameter 'name'."
        exit 1
    fi

    echo "Adding migration '$name' to $context"

    dotnet ef migrations add "$name" \
        --project "$migrationsProject" \
        --startup-project "$startupProject" \
        --context "$dbContext" \
        --output-dir Migrations
}

# Input handling
if [ "$#" -lt 1 ]; then
    echo "Enter context (e.g. Core, Hcm, EventSourcing):"
    read context
    echo "Enter migration name:"
    read name
else
    context=$1
    name=$2
fi

dbContext="${context}DbContext"
migrationsProject="src/$context/Nexus.$context.Infra.Data/Nexus.$context.Infra.Data.csproj"
startupProject="src/$context/Nexus.$context.Web/Nexus.$context.Web.csproj"

if [ "$context" == "EventSourcing" ]; then
    migrationsProject="src/Infra/Nexus.Infra.Data.EventSourcing/Nexus.Infra.Data.EventSourcing.csproj"
    startupProject="src/Core/Nexus.Core.Worker/Nexus.Core.Worker.csproj"
    dbContext="EventSourcingDbContext"
fi

addMigration "$context" "$name" "$migrationsProject" "$startupProject" "$dbContext"
