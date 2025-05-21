using VA.Shared.Exceptions;

namespace VA.API.Customers.DeleteCustomer;

public record DeleteCustomerCommand(Guid Id) : ICommand<DeleteCustomerResponse>;
//public record DeleteCustomerResult(bool IsSuccess);

public class DeleteCustomerCommandValidator : AbstractValidator<DeleteCustomerCommand>
{
    public DeleteCustomerCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Customer ID is required");
    }
}

internal class DeleteCustomerCommandHandler(CustomerContext context)
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