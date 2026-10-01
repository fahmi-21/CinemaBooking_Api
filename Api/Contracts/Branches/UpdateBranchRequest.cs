namespace Api.Contracts.Branches;

public sealed record UpdateBranchRequest(
    string Name,
    string Address,
    double? Latitude,
    double? Longitude,
    string GoogleMapsUrl);
