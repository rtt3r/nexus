#!/bin/bash

export ASPNETCORE_ENVIRONMENT=Migrations

# Function to update the database
function updateDatabase {
    local context=$1
    local project=$2
    local startupProject=$3
    local dbContext=$4

    echo "Updating $context Database"

    dotnet ef database update \
        --project "$project" \
        --startup-project "$startupProject" \
        --context "$dbContext"
}

# Main logic
if [ "$#" -eq 0 ]; then
    echo "Enter context (e.g. Core, Hcm, EventSourcing):"
    read context
else
    context=$1
fi

# Compose paths and context name based on input
dbContext="${context}DbContext"
migrationsProject="src/$context/Nexus.$context.Infra.Data/Nexus.$context.Infra.Data.csproj"
startupProject="src/$context/Nexus.$context.Api/Nexus.$context.Api.csproj"

# Handle special context exceptions
if [ "$context" == "EventSourcing" ]; then
    migrationsProject="src/Infra/Nexus.Infra.Data.EventSourcing/Nexus.Infra.Data.EventSourcing.csproj"
    startupProject="src/Core/Nexus.Core.Worker/Nexus.Core.Worker.csproj"
    dbContext="EventSourcingDbContext"
fi

# Call update
updateDatabase "$context" "$migrationsProject" "$startupProject" "$dbContext"
