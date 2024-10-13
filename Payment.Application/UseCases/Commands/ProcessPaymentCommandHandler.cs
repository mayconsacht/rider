using MediatR;
using Microsoft.Extensions.Logging;
using Payment.Application.Gateways;
using Payment.Application.Repositories;
using Payment.Domain.Entities;

namespace Payment.Application.UseCases.Commands;

public class ProcessPaymentCommandHandler(IPaymentProcessorGateway _paymentProcessor, 
  ITransactionRepository _transactionRepository, ILogger<ProcessPaymentCommandHandler> _logger) : IRequestHandler<ProcessPaymentCommand>
{
    public async Task Handle(ProcessPaymentCommand request, CancellationToken cancellationToken)
    {
      var transaction = Transaction.Create(request.RideId, request.Amount);
      try {
        _logger.LogInformation($"Trying to pay. TransactionId: {transaction.Id}");
        var outTransaction = await _paymentProcessor.ProcessPayment(request.CardHolder, request.CardNumber, request.CardExpDate, request.CardCVV, request.Amount);
        if (outTransaction.Status == Domain.Enums.TransactionStatus.Approved)
        {
          outTransaction.Pay();
          await _transactionRepository.Save(outTransaction);
          _logger.LogInformation($"Paid successfully. TransactionId: {transaction.Id}");
        }
      } 
      catch (Exception ex)
      {
        _logger.LogWarning($"Payment failed. TransactionId: {transaction.Id} - {ex.Message}");
      }
    }
}
