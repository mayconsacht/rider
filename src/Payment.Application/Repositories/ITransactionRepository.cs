using Payment.Domain.Entities;

namespace Payment.Application.Repositories;

public interface ITransactionRepository
{
  Task Save(Transaction transaction);
}
