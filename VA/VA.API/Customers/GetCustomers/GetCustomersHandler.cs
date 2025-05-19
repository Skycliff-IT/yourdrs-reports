using VA.API.Models;
using VA.Shared.CQRS;

namespace Catalog.API.Customers.GetCustomers;

public record GetCustomersQuery(int? PageNumber = 1, int? PageSize = 10) : IQuery<GetCustomersResult>;
public record GetCustomersResult(IEnumerable<Customer> Customers);

internal class GetCustomersQueryHandler
    : IQueryHandler<GetCustomersQuery, GetCustomersResult>
{
    public async Task<GetCustomersResult> Handle(GetCustomersQuery query, CancellationToken cancellationToken)
    {
        //var Customers = await session.Query<Customer>()
        //    .ToPagedListAsync(query.PageNumber ?? 1, query.PageSize ?? 10, cancellationToken);

        //return new GetCustomersResult(Customers);
        return null;
    }
}
