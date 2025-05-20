using VA.Shared.Exceptions;

namespace VA.API.Customers.DeleteCustomer;

public record DeleteCustomerCommand(Guid Id) : ICommand<DeleteCustomerResult>;
public record DeleteCustomerResult(bool IsSuccess);

public class DeleteCustomerCommandValidator : AbstractValidator<DeleteCustomerCommand>
{
    public DeleteCustomerCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Customer ID is required");
    }
}

internal class DeleteCustomerCommandHandler(CustomerContext context)
    : ICommandHandler<DeleteCustomerCommand, DeleteCustomerResult>
{
    public async Task<DeleteCustomerResult> Handle(DeleteCustomerCommand command, CancellationToken cancellationToken)
    {
        var customer = await context.Customers.FindAsync(new object[] { command.Id }, cancellationToken);

        if (customer is null)
        {
            throw new NotFoundException(command.Id.ToString());
        }

        context.Customers.Remove(customer);
        await context.SaveChangesAsync(cancellationToken);

        return new DeleteCustomerResult(true);
    }
}