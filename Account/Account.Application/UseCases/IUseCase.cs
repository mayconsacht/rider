namespace Account.Application.UseCases;

public interface IUseCase<in T, out TResult>
{
    public TResult Execute(T input);
}