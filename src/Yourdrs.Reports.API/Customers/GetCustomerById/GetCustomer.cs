using Yourdrs.CrossCutting.CQRS;

namespace Yourdrs.Reports.API.Customers.GetCustomerById;
public record GetCustomerByIdResponse(Customer Customer);
public class GetCustomerByIdQuery(Guid id) : IQuery<GetCustomerByIdResponse>
{
    public Guid Id { get; } = id;
}