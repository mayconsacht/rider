using Payment.Domain.Enums;
using Shared.Domain;

namespace Payment.Domain.Entities;

public class Transaction : Entity
{
  public Guid RideId { get; private set; }
  public double Amount {  get; private set; }
  public DateTime Date { get; private set; }
  public TransactionStatus Status { get; private set; }

  public Transaction(Guid id, Guid rideId, double amount, DateTime date, TransactionStatus status)
  {
    Id = id;
    RideId = rideId;
    Amount = amount;
    Date = date;
    Status = status;
  }

  public static Transaction Create(Guid rideId, double amount)
  {
    return new Transaction(Guid.NewGuid(), rideId, amount, DateTime.Now, TransactionStatus.WaitingPayment);
  }

  public void Pay()
  {
    Status = TransactionStatus.Paid;
  }
}
