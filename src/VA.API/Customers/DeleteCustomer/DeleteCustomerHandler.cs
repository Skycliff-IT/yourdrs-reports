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

internal class DeleteCustomerCommandHandler
    : ICommandHandler<DeleteCustomerCommand, DeleteCustomerResult>
{
    public async Task<DeleteCustomerResult> Handle(DeleteCustomerCommand command, CancellationToken cancellationToken)
    {
        //session.Delete<Customer>(command.Id);
        //await session.SaveChangesAsync(cancellationToken);

        return new DeleteCustomerResult(true);
    }
}
