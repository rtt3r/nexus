#!/bin/bash

export ASPNETCORE_ENVIRONMENT=Migrations

function runCoreCommand {
    local param=$1
    if [ -z "$param" ];
        then
            echo "Error: missing parameter 'name'."
            exit 1
        else
            echo "Adding migration to Core"

            dotnet ef migrations add "$param" \
                --project src/Core/Nexus.Core.Infra.Data/Nexus.Core.Infra.Data.csproj \
                --startup-project src/Core/Nexus.Core.Api/Nexus.Core.Api.csproj \
                --context CoreDbContext \
                --output-dir Migrations
    fi
}

function runEventSourcingCommand {
    local param=$1
    if [ -z "$param" ];
        then
            echo "Error: missing parameter 'name'."
            exit 1
        else
            echo "Adding migration to EventSourcing"

            dotnet ef migrations add "$param" \
                --project src/Infra/Nexus.Infra.EventSourcing/Nexus.Infra.EventSourcing.csproj \
                --startup-project src/Core/Nexus.Core.Worker/Nexus.Core.Worker.csproj \
                --context EventSourcingDbContext \
                --output-dir Migrations
    fi
}

if [ "$#" -eq 0 ];
    then
        echo "Enter context"
        read context

        echo "Enter migration name"
        read name

        if [ $context == "Core" ];
            then
                runCoreCommand "$name"
            else
                runEventSourcingCommand "$name"
        fi

    else
        if [ $1 == "Core" ];
            then
                runCoreCommand "$2"
            else
                runEventSourcingCommand "$2"
        fi
fi
