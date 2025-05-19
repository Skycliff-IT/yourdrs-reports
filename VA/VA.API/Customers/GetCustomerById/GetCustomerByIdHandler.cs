

namespace VA.API.Customers.GetCustomerById;

public record GetCustomerByIdQuery(Guid Id) : IQuery<GetCustomerByIdResult>;
public record GetCustomerByIdResult(Customer Customer);

internal class GetCustomerByIdQueryHandler
    : IQueryHandler<GetCustomerByIdQuery, GetCustomerByIdResult>
{
    public async Task<GetCustomerByIdResult> Handle(GetCustomerByIdQuery query, CancellationToken cancellationToken)
    {
        //var Customer = await session.LoadAsync<Customer>(query.Id, cancellationToken);

        //if (Customer is null)
        //{
        //    throw new CustomerNotFoundException(query.Id);
        //}

        //return new GetCustomerByIdResult(Customer);
        return new GetCustomerByIdResult(null);
    }
}
