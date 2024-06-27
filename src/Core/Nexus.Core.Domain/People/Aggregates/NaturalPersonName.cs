﻿namespace Nexus.Core.Domain.People.Aggregates;

public record NaturalPersonName(string FirstName, string LastName)
{
    public string FirstName { get; init; } = FirstName = FirstName.Trim();
    public string LastName { get; init; } = LastName = LastName.Trim();

    public string GetFullName()
        => $"{FirstName} {LastName}";
}
