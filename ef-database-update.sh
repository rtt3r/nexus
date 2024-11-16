#!/bin/bash

export ASPNETCORE_ENVIRONMENT=Migrations

function runCoreCommand {
    echo "Updating Core Database"

    dotnet ef database update \
        --project src/Core/Nexus.Core.Infra.Data/Nexus.Core.Infra.Data.csproj \
        --startup-project src/Core/Nexus.Core.Api/Nexus.Core.Api.csproj \
        --context CoreDbContext
}

function runEventSourcingCommand {
    echo "Updating EventSourcing Database"

    dotnet ef database update \
        --project src/Infra/Nexus.Infra.EventSourcing/Nexus.Infra.EventSourcing.csproj \
        --startup-project src/Core/Nexus.Core.Worker/Nexus.Core.Worker.csproj \
        --context EventSourcingDbContext
}

if [ "$#" -eq 0 ];
    then
        echo "Enter context"
        read context

        if [ $context == "Core" ];
            then
                runCoreCommand
            else
                runEventSourcingCommand
        fi

    else
        if [ $1 == "Core" ];
            then
                runCoreCommand
            else
                runEventSourcingCommand
        fi
fi
