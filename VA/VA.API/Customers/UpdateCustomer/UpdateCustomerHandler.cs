namespace VA.API.Customers.UpdateCustomer;

public record UpdateCustomerCommand(Guid Id, string Name, List<string> Category, string Description, string ImageFile, decimal Price)
    : ICommand<UpdateCustomerResult>;
public record UpdateCustomerResult(bool IsSuccess);

public class UpdateCustomerCommandValidator : AbstractValidator<UpdateCustomerCommand>
{
    public UpdateCustomerCommandValidator()
    {
        RuleFor(command => command.Id).NotEmpty().WithMessage("Customer ID is required");

        RuleFor(command => command.Name)
            .NotEmpty().WithMessage("Name is required")
            .Length(2, 150).WithMessage("Name must be between 2 and 150 characters");

        RuleFor(command => command.Price)
            .GreaterThan(0).WithMessage("Price must be greater than 0");
    }
}

internal class UpdateCustomerCommandHandler
    : ICommandHandler<UpdateCustomerCommand, UpdateCustomerResult>
{
    public async Task<UpdateCustomerResult> Handle(UpdateCustomerCommand command, CancellationToken cancellationToken)
    {
        //var Customer = await session.LoadAsync<Customer>(command.Id, cancellationToken);

        //if (Customer is null)
        //{
        //    throw new CustomerNotFoundException(command.Id);
        //}

        //Customer.Name = command.Name;
        //Customer.Category = command.Category;
        //Customer.Description = command.Description;
        //Customer.ImageFile = command.ImageFile;
        //Customer.Price = command.Price;

        //session.Update(Customer);
        //await session.SaveChangesAsync(cancellationToken);

        return new UpdateCustomerResult(true);
    }
}
