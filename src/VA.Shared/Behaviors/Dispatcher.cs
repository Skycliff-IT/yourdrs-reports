using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VA.Shared.CQRS;

namespace VA.Shared.Behaviors;

public interface IDispatcher
{
    Task<TResponse> Send<TRequest, TResponse>(TRequest request, CancellationToken cancellationToken = default)
        where TRequest : ICommand<TResponse>;
}

public class Dispatcher : IDispatcher
{
    private readonly IServiceProvider _serviceProvider;

    public Dispatcher(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task<TResponse> Send<TRequest, TResponse>(TRequest request, CancellationToken cancellationToken = default)
        where TRequest : ICommand<TResponse>
    {
        var behaviors = _serviceProvider.GetServices<IPipelineBehavior<TRequest, TResponse>>().Reverse().ToList();
        var handler = _serviceProvider.GetRequiredService<ICommandHandler<TRequest, TResponse>>();

        RequestHandlerDelegate<TResponse> handlerDelegate = _ => handler.Handle(request, cancellationToken);

        foreach (var behavior in behaviors)
        {
            var next = handlerDelegate;
            handlerDelegate = ct => behavior.Handle(request, next, ct);
        }

        return await handlerDelegate(cancellationToken);
    }
}