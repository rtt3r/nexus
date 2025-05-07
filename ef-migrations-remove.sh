#!/bin/bash

export ASPNETCORE_ENVIRONMENT=Migrations

# Function to update the database
function removeMigrations {
    local context=$1
    local project=$2
    local startupProject=$3
    local dbContext=$4

    echo "Removing last $context migration..."

    dotnet ef migrations remove \
        --project "$project" \
        --startup-project "$startupProject" \
        --context "$dbContext"
}

# Main logic
if [ "$#" -eq 0 ]; then
    echo "Enter context (Core/Hcm/EventSourcing)"
    read context

    case "$context" in
        Core)
            removeMigrations "Core" \
                "src/Core/Nexus.Core.Infra.Data/Nexus.Core.Infra.Data.csproj" \
                "src/Core/Nexus.Core.Web/Nexus.Core.Web.csproj" \
                "CoreDbContext"
            ;;
        Hcm)
            removeMigrations "Hcm" \
                "src/Hcm/Nexus.Hcm.Infra.Data/Nexus.Hcm.Infra.Data.csproj" \
                "src/Hcm/Nexus.Hcm.Web/Nexus.Hcm.Web.csproj" \
                "HcmDbContext"
            ;;
        EventSourcing)
            removeMigrations "EventSourcing" \
                "src/Infra/Nexus.Infra.Data.EventSourcing/Nexus.Infra.Data.EventSourcing.csproj" \
                "src/Core/Nexus.Core.Worker/Nexus.Core.Worker.csproj" \
                "EventSourcingDbContext"
            ;;
        *)
            echo "Error: Invalid context. Please enter 'Core', 'Hcm' or 'EventSourcing'."
            exit 1
            ;;
    esac
else
    case "$1" in
        Core)
            removeMigrations "Core" \
                "src/Core/Nexus.Core.Infra.Data/Nexus.Core.Infra.Data.csproj" \
                "src/Core/Nexus.Core.Web/Nexus.Core.Web.csproj" \
                "CoreDbContext"
            ;;
        Hcm)
            removeMigrations "Hcm" \
                "src/Hcm/Nexus.Hcm.Infra.Data/Nexus.Hcm.Infra.Data.csproj" \
                "src/Hcm/Nexus.Hcm.Web/Nexus.Hcm.Web.csproj" \
                "HcmDbContext"
            ;;
        EventSourcing)
            removeMigrations "EventSourcing" \
                "src/Infra/Nexus.Infra.Data.EventSourcing/Nexus.Infra.Data.EventSourcing.csproj" \
                "src/Core/Nexus.Core.Worker/Nexus.Core.Worker.csproj" \
                "EventSourcingDbContext"
            ;;
        *)
            echo "Error: Invalid context. Please specify 'Core', 'Hcm' or 'EventSourcing'."
            exit 1
            ;;
    esac
fi
