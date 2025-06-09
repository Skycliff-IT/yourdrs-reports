using Yourdrs.CrossCutting.CQRS;
using Yourdrs.CrossCutting.Exceptions;

namespace Yourdrs.Reports.API.Features.Customers.DeleteCustomer;
internal class DeleteCustomerCommandHandler(ApplicationDbContext context)
    : ICommandHandler<DeleteCustomerCommand, DeleteCustomerResponse>
{
    public async Task<DeleteCustomerResponse> Handle(DeleteCustomerCommand command, CancellationToken cancellationToken)
    {
        var customer = await context.Customers.FindAsync(new object[] { command.Id }, cancellationToken);

        if (customer is null)
        {
            throw new NotFoundException(command.Id.ToString());
        }

        context.Customers.Remove(customer);
        await context.SaveChangesAsync(cancellationToken);

        return new DeleteCustomerResponse(true);
    }
}