using MediatR;

namespace Payment.Application.UseCases.Commands;

public record ProcessPaymentCommand(Guid RideId, double Amount, string CardHolder, int CardNumber, DateOnly CardExpDate, int CardCVV) : IRequest;
