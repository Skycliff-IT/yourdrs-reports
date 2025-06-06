﻿//using MediatR;

namespace Yourdrs.CrossCutting.CQRS;
public interface IQueryHandler<in TQuery, TResponse>
    where TQuery : IQuery<TResponse>
{
    Task<TResponse> Handle(TQuery query, CancellationToken cancellationToken);
}
