namespace Domain.Enums;

public enum PaymentStatus : byte
{
    Pending = 0,
    Succeeded = 1,
    Failed = 2,
    Refunded = 3
}