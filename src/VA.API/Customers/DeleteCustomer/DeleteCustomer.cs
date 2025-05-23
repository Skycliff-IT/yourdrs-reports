using VA.CrossCutting.CQRS;

namespace VA.API.Customers.DeleteCustomer;
public record DeleteCustomerResponse(bool IsSuccess);
public record DeleteCustomerCommand(Guid Id) : ICommand<DeleteCustomerResponse>;