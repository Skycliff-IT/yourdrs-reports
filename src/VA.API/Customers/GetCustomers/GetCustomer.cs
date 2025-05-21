using VA.Shared.Pagination;

namespace VA.API.Customers.GetCustomers
{
    public class GetCustomersQuery : IQuery<GetCustomersResponse>
    {
        public PaginationRequest Request { get; }

        public GetCustomersQuery(PaginationRequest request)
        {
            Request = request;
        }
    }
    public record GetCustomersResponse(PaginatedResult<CustomerDto> Customers);

}
