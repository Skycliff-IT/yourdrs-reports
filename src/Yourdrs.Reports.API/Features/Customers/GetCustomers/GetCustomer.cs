using Yourdrs.CrossCutting.CQRS;
using Yourdrs.CrossCutting.Pagination;

namespace Yourdrs.Reports.API.Features.Customers.GetCustomers;
public class GetCustomersQuery(PaginationRequest request) : IQuery<GetCustomersResponse>
{
    public PaginationRequest Request { get; } = request;
}
public record GetCustomersResponse(PaginatedResult<CustomerDto> Customers);