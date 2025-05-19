
namespace VA.API.Customers.UpdateCustomer;

public record UpdateCustomerRequest(Guid Id, string Name, List<string> Category, string Description, string ImageFile, decimal Price);
public record UpdateCustomerResponse(bool IsSuccess);

public class UpdateCustomerEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/Customers",
            async (UpdateCustomerRequest request, ISender sender) =>
            {
                var command = request.Adapt<UpdateCustomerCommand>();

                var result = await sender.Send(command);

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
