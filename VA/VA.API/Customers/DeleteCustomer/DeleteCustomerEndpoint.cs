
namespace Catalog.API.Customers.DeleteCustomer;

//public record DeleteCustomerRequest(Guid Id);
public record DeleteCustomerResponse(bool IsSuccess);

public class DeleteCustomerEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/Customers/{id}", async (Guid id, ISender sender) =>
        {
            var result = await sender.Send(new DeleteCustomerCommand(id));

            var response = result.Adapt<DeleteCustomerResponse>();

            return Results.Ok(response);
        })
        .WithName("DeleteCustomer")
        .Produces<DeleteCustomerResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .WithSummary("Delete Customer")
        .WithDescription("Delete Customer");
    }
}
