using MediatR;

namespace VA.Shared.CQRS;
public interface IQuery<out TResponse> : IRequest<TResponse>
    where TResponse : notnull
{
}
