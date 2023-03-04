namespace CleanArch.Application.AggregatesModel.OrderAggregates;

/// <summary>
/// Value Object
/// </summary>
public record Address(
    string Street, 
    string City, 
    string State, string ZipCode);
