using Yourdrs.CrossCutting.CQRS;

namespace Yourdrs.Reports.API.Customers.DeleteCustomer;
public record DeleteCustomerResponse(bool IsSuccess);
public record DeleteCustomerCommand(Guid Id) : ICommand<DeleteCustomerResponse>;