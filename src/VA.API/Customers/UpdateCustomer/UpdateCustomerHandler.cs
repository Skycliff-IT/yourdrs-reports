using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using VA.Shared.Exceptions;

namespace VA.API.Customers.UpdateCustomer;

public record UpdateCustomerCommand(Guid Id, string CustomerCode, string CustomerName)
    : ICommand<UpdateCustomerResult>;
//public record UpdateCustomerResult(bool IsSuccess);
public record UpdateCustomerResult(bool IsSuccess, string? ErrorMessage = null, Customer? UpdatedCustomer = null);
public class UpdateCustomerCommandValidator : AbstractValidator<UpdateCustomerCommand>
{
    public UpdateCustomerCommandValidator()
    {
        RuleFor(command => command.Id).NotEmpty().WithMessage("Customer ID is required");

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

internal class UpdateCustomerCommandHandler(CustomerContext context)
    : ICommandHandler<UpdateCustomerCommand, UpdateCustomerResult>
{
    public async Task<UpdateCustomerResult> Handle(UpdateCustomerCommand command, CancellationToken cancellationToken)
    {
        var customer = await context.Customers.FindAsync([command.Id], cancellationToken);

        if (customer is null)
        {
            throw new NotFoundException(command.Id.ToString());
        }

        customer.CustomerCode = command.CustomerCode;
        customer.CustomerName = command.CustomerName;

        context.Customers.Update(customer);
        await context.SaveChangesAsync(cancellationToken);

        return new UpdateCustomerResult(true, null, customer);
    }
}
