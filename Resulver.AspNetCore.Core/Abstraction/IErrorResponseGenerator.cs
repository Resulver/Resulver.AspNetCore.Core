namespace Resulver.AspNetCore.Core.Abstraction;

public interface IErrorResponseGenerator<out TResponse>
{
    public TResponse MakeResponse(ResultError error);
}