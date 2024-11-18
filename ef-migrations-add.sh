#!/bin/bash

export ASPNETCORE_ENVIRONMENT=Migrations

# Function to add migration
function addMigration {
    local context=$1
    local name=$2
    local project=$3
    local startupProject=$4
    local dbContext=$5

    if [ -z "$name" ]; then
        echo "Error: missing parameter 'name'."
        exit 1
    fi

    echo "Adding migration '$name' to $context"

    dotnet ef migrations add "$name" \
        --project "$project" \
        --startup-project "$startupProject" \
        --context "$dbContext" \
        --output-dir Migrations
}

# Main logic
if [ "$#" -eq 0 ]; then
    echo "Enter context (Core/EventSourcing)"
    read context

    echo "Enter migration name"
    read name

    case "$context" in
        Core)
            addMigration "Core" "$name" \
                "src/Core/Nexus.Core.Infra.Data/Nexus.Core.Infra.Data.csproj" \
                "src/Core/Nexus.Core.Api/Nexus.Core.Api.csproj" \
                "CoreDbContext"
            ;;
        EventSourcing)
            addMigration "EventSourcing" "$name" \
                "src/Infra/Nexus.Infra.Data.EventSourcing/Nexus.Infra.Data.EventSourcing.csproj" \
                "src/Core/Nexus.Core.Worker/Nexus.Core.Worker.csproj" \
                "EventSourcingDbContext"
            ;;
        *)
            echo "Error: Invalid context. Please enter 'Core' or 'EventSourcing'."
            exit 1
            ;;
    esac
else
    case "$1" in
        Core)
            addMigration "Core" "$2" \
                "src/Core/Nexus.Core.Infra.Data/Nexus.Core.Infra.Data.csproj" \
                "src/Core/Nexus.Core.Api/Nexus.Core.Api.csproj" \
                "CoreDbContext"
            ;;
        EventSourcing)
            addMigration "EventSourcing" "$2" \
                "src/Infra/Nexus.Infra.Data.EventSourcing/Nexus.Infra.Data.EventSourcing.csproj" \
                "src/Core/Nexus.Core.Worker/Nexus.Core.Worker.csproj" \
                "EventSourcingDbContext"
            ;;
        *)
            echo "Error: Invalid context. Please specify 'Core' or 'EventSourcing'."
            exit 1
            ;;
    esac
fi
