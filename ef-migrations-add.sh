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
    echo "Enter context (Finance/EventSourcing)"
    read context

    echo "Enter migration name"
    read name

    case "$context" in
        Finance)
            addMigration "Finance" "$name" \
                "src/Finance/Nexus.Finance.Infra.Data/Nexus.Finance.Infra.Data.csproj" \
                "src/Finance/Nexus.Finance.Api/Nexus.Finance.Api.csproj" \
                "FinanceDbContext"
            ;;
        EventSourcing)
            addMigration "EventSourcing" "$name" \
                "src/Infra/Nexus.Infra.Data.EventSourcing/Nexus.Infra.Data.EventSourcing.csproj" \
                "src/Finance/Nexus.Finance.Worker/Nexus.Finance.Worker.csproj" \
                "EventSourcingDbContext"
            ;;
        *)
            echo "Error: Invalid context. Please enter 'Finance' or 'EventSourcing'."
            exit 1
            ;;
    esac
else
    case "$1" in
        Finance)
            addMigration "Finance" "$2" \
                "src/Finance/Nexus.Finance.Infra.Data/Nexus.Finance.Infra.Data.csproj" \
                "src/Finance/Nexus.Finance.Api/Nexus.Finance.Api.csproj" \
                "FinanceDbContext"
            ;;
        EventSourcing)
            addMigration "EventSourcing" "$2" \
                "src/Infra/Nexus.Infra.Data.EventSourcing/Nexus.Infra.Data.EventSourcing.csproj" \
                "src/Finance/Nexus.Finance.Worker/Nexus.Finance.Worker.csproj" \
                "EventSourcingDbContext"
            ;;
        *)
            echo "Error: Invalid context. Please specify 'Finance' or 'EventSourcing'."
            exit 1
            ;;
    esac
fi
