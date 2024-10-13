namespace Payment.Domain.Enums;

public enum TransactionStatus
{
  Unknown = 0,
  WaitingPayment = 1,
  Approved = 2,
  Paid = 3
}
