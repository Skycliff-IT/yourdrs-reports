using Yourdrs.CrossCutting.CQRS;
using Yourdrs.CrossCutting.Pagination;

namespace Yourdrs.Reports.API.Features.Customers.GetCustomers;
public class GetCustomersQueryHandler
(ApplicationDbContext context)
    : IQueryHandler<GetCustomersQuery, GetCustomersResponse>
{
    public async Task<GetCustomersResponse> Handle(GetCustomersQuery query, CancellationToken cancellationToken)
    {
        var pageIndex = query.Request.PageIndex;
        var pageSize = query.Request.PageSize;

        var totalCount = await context.Customers.LongCountAsync(cancellationToken);

        var customers = await context.Customers
            .OrderBy(o => o.CustomerName)
            .Skip(pageSize * pageIndex)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        var customerDtos = customers.ToCustomerDtoList();


        return new GetCustomersResponse(
            new PaginatedResult<CustomerDto>(
                pageIndex,
                pageSize,
                totalCount,
                customerDtos));
    }
}
