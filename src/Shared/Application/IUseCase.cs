namespace Shared.Application;

public interface IUseCase<in T, out TResult>
{
    public TResult Execute(T request);
}