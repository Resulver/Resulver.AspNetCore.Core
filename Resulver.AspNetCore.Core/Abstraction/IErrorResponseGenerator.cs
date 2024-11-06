namespace Resulver.AspNetCore.Core.Core.Abstraction;

public interface IErrorResponseGenerator<out TResponse>
{
    public TResponse MakeResponse(ResultError error);
}