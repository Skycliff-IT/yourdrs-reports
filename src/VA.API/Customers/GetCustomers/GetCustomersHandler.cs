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
        //todo: implement pagination
        await context.Customers.ToListAsync(cancellationToken);
        return new GetCustomersResult(context.Customers);
    }
}
