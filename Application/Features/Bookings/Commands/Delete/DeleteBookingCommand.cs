using Application.Common.Models;

namespace Application.Features.Bookings.Commands.Delete;

public sealed record DeleteBookingCommand(int Id) : IRequest<ApiResponse<EmptyResponse?>>;
