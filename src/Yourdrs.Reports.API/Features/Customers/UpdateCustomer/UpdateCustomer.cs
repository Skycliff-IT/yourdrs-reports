using Yourdrs.CrossCutting.CQRS;

namespace Yourdrs.Reports.API.Features.Customers.UpdateCustomer;
public record UpdateCustomerRequest(Guid Id, string CustomerCode, string CustomerName);
public record UpdateCustomerResponse(bool IsSuccess, string? ErrorMessage = null, Customer? UpdatedCustomer = null);
public record UpdateCustomerCommand(Guid Id, string CustomerCode, string CustomerName)
    : ICommand<UpdateCustomerResponse>;