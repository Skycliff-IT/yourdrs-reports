using Microsoft.EntityFrameworkCore;

namespace VA.API.Customers.GetCustomers;

public record GetCustomersQuery(int? PageNumber = 1, int? PageSize = 10) : IQuery<GetCustomersResult>;
public record GetCustomersResult(IEnumerable<Customer> Customers);

internal class GetCustomersQueryHandler
(CustomerContext context)
    : IQueryHandler<GetCustomersQuery, GetCustomersResult>
{
    public async Task<GetCustomersResult> Handle(GetCustomersQuery query, CancellationToken cancellationToken)
    {
        //var Customers = await session.Query<Customer>()
        //    .ToPagedListAsync(query.PageNumber ?? 1, query.PageSize ?? 10, cancellationToken);

        //return new GetCustomersResult(Customers);
        await context.Customers.ToListAsync(cancellationToken);

        return new GetCustomersResult(context.Customers);
    }
}
