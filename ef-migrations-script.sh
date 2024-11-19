#!/bin/bash

export ASPNETCORE_ENVIRONMENT=Migrations

# Function to update the database
function generateScript {
    local context=$1
    local project=$2
    local startupProject=$3
    local dbContext=$4

    echo "Generating $context migrations script..."

    dotnet ef migrations script \
        --project "$project" \
        --startup-project "$startupProject" \
        --context "$dbContext"
}

# Main logic
if [ "$#" -eq 0 ]; then
    echo "Enter context (Finance/EventSourcing)"
    read context

    case "$context" in
        Finance)
            generateScript "Finance" \
                "src/Finance/Nexus.Finance.Infra.Data/Nexus.Finance.Infra.Data.csproj" \
                "src/Finance/Nexus.Finance.Api/Nexus.Finance.Api.csproj" \
                "FinanceDbContext"
            ;;
        EventSourcing)
            generateScript "EventSourcing" \
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
            generateScript "Finance" \
                "src/Finance/Nexus.Finance.Infra.Data/Nexus.Finance.Infra.Data.csproj" \
                "src/Finance/Nexus.Finance.Api/Nexus.Finance.Api.csproj" \
                "FinanceDbContext"
            ;;
        EventSourcing)
            generateScript "EventSourcing" \
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
