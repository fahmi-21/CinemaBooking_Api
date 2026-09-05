namespace Domain.Enums;

public enum BookingStatus : byte
{
    PendingPayment = 0,
    Confirmed = 1,
    Cancelled = 2,
    Expired = 3,
    Completed = 4
}