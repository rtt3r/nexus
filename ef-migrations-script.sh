#!/bin/bash

export ASPNETCORE_ENVIRONMENT=Migrations

# Function to generate migration SQL script
function generateScript {
    local context=$1
    local migrationsProject=$2
    local startupProject=$3
    local dbContext=$4

    echo "Generating $context migrations script..."

    dotnet ef migrations script \
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
startupProject="src/$context/Nexus.$context.Web/Nexus.$context.Web.csproj"

if [ "$context" == "EventSourcing" ]; then
    migrationsProject="src/Infra/Nexus.Infra.Data.EventSourcing/Nexus.Infra.Data.EventSourcing.csproj"
    startupProject="src/Core/Nexus.Core.Worker/Nexus.Core.Worker.csproj"
    dbContext="EventSourcingDbContext"
fi

generateScript "$context" "$migrationsProject" "$startupProject" "$dbContext"
