namespace Domain.Enums;

public enum NotificationType : byte
{
    BookingConfirmed = 0,
    BookingCancelled = 1,
    PaymentSucceeded = 2,
    PaymentFailed = 3,
    Promotion = 4,
    System = 5
}