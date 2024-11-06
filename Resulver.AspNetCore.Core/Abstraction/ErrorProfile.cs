using Resulver.AspNetCore.Core.ErrorHandling;

namespace Resulver.AspNetCore.Core.Abstraction;

public abstract class ErrorProfile<TResponse>
{
    public List<ErrorResponseHandler<TResponse>> ErrorResponses => [];

    protected abstract ErrorResponseHandler<TResponse> AddError<TError>()
        where TError : ResultError;

    public abstract void Configure();
}