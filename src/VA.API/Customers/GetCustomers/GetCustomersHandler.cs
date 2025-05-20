using Microsoft.EntityFrameworkCore;
using VA.Shared.Pagination;

namespace VA.API.Customers.GetCustomers;

public record GetCustomersQuery(PaginationRequest PaginationRequest) : IQuery<GetCustomersResult>;
public record GetCustomersResult(PaginatedResult<CustomerDto> Customers);

internal class GetCustomersQueryHandler
(CustomerContext context)
    : IQueryHandler<GetCustomersQuery, GetCustomersResult>
{
    public async Task<GetCustomersResult> Handle(GetCustomersQuery query, CancellationToken cancellationToken)
    {

        var pageIndex = query.PaginationRequest.PageIndex;
        var pageSize = query.PaginationRequest.PageSize;
        
        var totalCount = await context.Customers.LongCountAsync(cancellationToken);

        var customers = await context.Customers
            .OrderBy(o => o.CustomerName)
            .Skip(pageSize * pageIndex)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        var customerDtos = customers.ToCustomerDtoList();


        return new GetCustomersResult(
            new PaginatedResult<CustomerDto>(
                pageIndex,
                pageSize,
                totalCount,
                customerDtos));
    }
}
