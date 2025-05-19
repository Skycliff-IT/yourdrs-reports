using VA.API.Models;
using VA.Shared.CQRS;

namespace Catalog.API.Customers.CreateCustomer;

public record CreateCustomerCommand(string Code, string Name)
    : ICommand<CreateCustomerResult>;
public record CreateCustomerResult(Guid Id);

public class CreateCustomerCommandValidator : AbstractValidator<CreateCustomerCommand>
{
    public CreateCustomerCommandValidator()
    {
        RuleFor(x => x.Code).NotEmpty().WithMessage("Code is required");
        RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
    }
}

internal class CreateCustomerCommandHandler
    : ICommandHandler<CreateCustomerCommand, CreateCustomerResult>
{
    public async Task<CreateCustomerResult> Handle(CreateCustomerCommand command, CancellationToken cancellationToken)
    {
        //create Customer entity from command object
        //save to database
        //return CreateCustomerResult result               

        var Customer = new Customer
        {
            Id = Guid.NewGuid(),
            CustomerCode= command.Code,
            CustomerName = command.Name
        };
        
        //save to database
        //session.Store(Customer);
        //await session.SaveChangesAsync(cancellationToken);

        //return result
        return new CreateCustomerResult(Customer.Id);
    }
}
