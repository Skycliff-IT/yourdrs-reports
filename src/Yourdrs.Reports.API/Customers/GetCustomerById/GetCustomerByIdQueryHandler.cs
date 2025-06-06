using Yourdrs.CrossCutting.CQRS;
using Yourdrs.CrossCutting.Exceptions;

namespace Yourdrs.Reports.API.Customers.GetCustomerById;
internal class GetCustomerByIdQueryHandler
    (CustomerContext context)
    : IQueryHandler<GetCustomerByIdQuery, GetCustomerByIdResponse>
{
    public Task<GetCustomerByIdResponse> Handle(GetCustomerByIdQuery query, CancellationToken cancellationToken)
    {
        var customer = context.Customers.FirstOrDefault(x => x.Id == query.Id);
        if (customer is null)
        {
            throw new NotFoundException(query.Id.ToString());
        }
        return Task.FromResult(new GetCustomerByIdResponse(customer));
    }
}
