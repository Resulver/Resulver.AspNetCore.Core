using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Resulver.AspNetCore.Core.Abstraction;
using Resulver.AspNetCore.Core.ErrorHandling;

namespace Resulver.AspNetCore.Core;

public static class DependencyInjection
{
    public static IServiceCollection AddErrorProfile<TErrorProfile, TResponse>(this IServiceCollection services)
        where TErrorProfile : ErrorProfile<TResponse>
    {
        services.AddSingleton<TErrorProfile>();

        return services;
    }

    public static IServiceCollection AddErrorProfilesFromAssembly<TResponse>(
        this IServiceCollection services, Assembly assembly)
    {
        var errorProfiles = assembly
            .GetTypes()
            .Where(type =>
                type is { IsAbstract: false, IsInterface: false } &&
                type.IsAssignableTo(typeof(ErrorProfile<TResponse>)))
            .Select(profileType => (Activator.CreateInstance(profileType) as ErrorProfile<TResponse>)!)
            .ToList();

        foreach (var errorProfile in errorProfiles) errorProfile.Configure();

        var serviceDescriptors = errorProfiles.Select(ServiceDescriptor.Singleton);

        services.TryAddEnumerable(serviceDescriptors);

        return services;
    }

    public static IServiceCollection AddErrorResponseGenerator<TResponse>(this IServiceCollection services)
    {
        services.AddScoped<IErrorResponseGenerator<TResponse>, ErrorResponseGenerator<TResponse>>();

        return services;
    }
}