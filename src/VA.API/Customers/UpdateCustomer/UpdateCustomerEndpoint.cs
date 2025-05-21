

namespace VA.API.Customers.UpdateCustomer;

public record UpdateCustomerRequest(Guid Id, string CustomerCode, string CustomerName);
public record UpdateCustomerResponse(bool IsSuccess, string? ErrorMessage = null, Customer? UpdatedCustomer = null);

public class UpdateCustomerEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/Customers",
            async (UpdateCustomerRequest request,
                ICommandHandler<UpdateCustomerCommand, UpdateCustomerResponse> handler) =>
            {
                var command = request.Adapt<UpdateCustomerCommand>();

                var result = await handler.Handle(command);

                var response = result.Adapt<UpdateCustomerResponse>();

                return Results.Ok(response);
            })
            .WithName("UpdateCustomer")
            .Produces<UpdateCustomerResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithSummary("Update Customer")
            .WithDescription("Update Customer");
    }
}
