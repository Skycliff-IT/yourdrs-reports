using Yourdrs.CrossCutting.CQRS;

namespace Yourdrs.Reports.API.Features.Customers.CreateCustomer;
internal class CreateCustomerCommandHandler(ApplicationDbContext context) : ICommandHandler<CreateCustomerCommand, CreateCustomerResponse>
{
    public async Task<CreateCustomerResponse> Handle(CreateCustomerCommand command, CancellationToken cancellationToken)
    {
        //todo: implement mapster
        var customer = new Customer
        {
            Id = Guid.NewGuid(),
            CustomerCode = command.CustomerCode,
            CustomerName = command.CustomerName
        };

        context.Customers.Add(customer);
        await context.SaveChangesAsync(cancellationToken);

        return new CreateCustomerResponse(customer.Id);
    }
}
