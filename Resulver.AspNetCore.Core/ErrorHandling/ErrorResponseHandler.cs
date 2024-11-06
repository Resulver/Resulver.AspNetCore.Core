namespace Resulver.AspNetCore.Core.Core.ErrorHandling;

public abstract class ErrorResponseHandler<TResponse>
{
    protected int StatusCode { get; private set; } = 400;
    public abstract Func<ResultError, TResponse> Handler { get; protected set; }
    public abstract Type ErrorType { get; }

    public void WithStatusCode(int statusCode)
    {
        StatusCode = statusCode;
    }

    public ErrorResponseHandler<TResponse> HandleWith(Func<ResultError, TResponse> handler)
    {
        Handler = handler;

        return this;
    }
}