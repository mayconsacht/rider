using Payment.Domain.Entities;

namespace Payment.Application.Gateways;

public interface IPaymentProcessorGateway
{
  Task<Transaction> ProcessPayment(string CardHolder, int CardNumber, DateOnly CardExpDate, int CardCVV, double Amount);
}
