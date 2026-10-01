namespace Api.Contracts.Halls;

public sealed record UpdateHallRequest(
    int BranchId,
    string Name,
    HallType Type,
    int Capacity,
    int CleaningBufferMinutes);
