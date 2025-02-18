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
    echo "Enter context (Core/EventSourcing)"
    read context

    case "$context" in
        Core)
            updateDatabase "Core" \
                "src/Core/Nexus.Core.Infra.Data/Nexus.Core.Infra.Data.csproj" \
                "src/Core/Nexus.Core.Api/Nexus.Core.Api.csproj" \
                "CoreDbContext"
            ;;
        EventSourcing)
            updateDatabase "EventSourcing" \
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
            updateDatabase "Core" \
                "src/Core/Nexus.Core.Infra.Data/Nexus.Core.Infra.Data.csproj" \
                "src/Core/Nexus.Core.Api/Nexus.Core.Api.csproj" \
                "CoreDbContext"
            ;;
        EventSourcing)
            updateDatabase "EventSourcing" \
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
