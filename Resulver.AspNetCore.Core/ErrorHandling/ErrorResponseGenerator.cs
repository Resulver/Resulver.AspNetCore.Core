using Microsoft.Extensions.DependencyInjection;
using Resulver.AspNetCore.Core.Core.Abstraction;

namespace Resulver.AspNetCore.Core.Core.ErrorHandling;

public class ErrorResponseGenerator<TResponse> : IErrorResponseGenerator<TResponse>
{
    private readonly Dictionary<Type, ErrorResponseHandler<TResponse>> _errorResponses = [];

    public ErrorResponseGenerator(IServiceProvider serviceProvider)
    {
        foreach (var errorProfile in serviceProvider.GetServices<ErrorProfile<TResponse>>())
        foreach (var response in errorProfile.ErrorResponses)
            _errorResponses.Add(response.ErrorType, response);
    }

    public TResponse MakeResponse(ResultError error)
    {
        var errorResponse = _errorResponses.GetValueOrDefault(error.GetType())
                            ?? throw new Exception($"Error profile for '{error.GetType().Name}' is not defined !");

        return errorResponse.Handler(error);
    }
}