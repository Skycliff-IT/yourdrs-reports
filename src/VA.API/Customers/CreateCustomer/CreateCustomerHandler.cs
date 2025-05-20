
namespace VA.API.Customers.CreateCustomer;

public record CreateCustomerCommand(string CustomerCode, string CustomerName)
    : ICommand<CreateCustomerResult>;
public record CreateCustomerResult(Guid Id);

public class CreateCustomerCommandValidator : AbstractValidator<CreateCustomerCommand>
{
    public CreateCustomerCommandValidator()
    {
        RuleFor(x => x.CustomerCode)
            .NotEmpty()
            .WithMessage("Customer code is required.")
            .Matches(@"^CUST-\d{3}$")
            .WithMessage("Customer code must follow the format 'CUST-xxx', where 'xxx' are three digits (e.g., CUST-002).");

        RuleFor(x => x.CustomerName)
            .NotEmpty()
            .WithMessage("Name is required")
            .Length(2, 50).WithMessage("Name must be between 2 and 50 characters");
    }
}

internal class CreateCustomerCommandHandler(CustomerContext context)
    : ICommandHandler<CreateCustomerCommand, CreateCustomerResult>
{
    public async Task<CreateCustomerResult> Handle(CreateCustomerCommand command, CancellationToken cancellationToken)
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

        return new CreateCustomerResult(customer.Id);
    }
}
