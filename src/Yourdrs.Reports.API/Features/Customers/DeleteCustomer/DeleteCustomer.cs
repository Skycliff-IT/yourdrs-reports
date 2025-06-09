using Yourdrs.CrossCutting.CQRS;

namespace Yourdrs.Reports.API.Features.Customers.DeleteCustomer;
public record DeleteCustomerResponse(bool IsSuccess);
public record DeleteCustomerCommand(Guid Id) : ICommand<DeleteCustomerResponse>;