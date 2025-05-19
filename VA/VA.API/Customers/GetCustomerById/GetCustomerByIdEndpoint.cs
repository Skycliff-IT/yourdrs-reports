namespace Catalog.API.Customers.GetCustomerById;

//public record GetCustomerByIdRequest();
public record GetCustomerByIdResponse(Customer Customer);

public class GetCustomerByIdEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/Customers/{id}", async (Guid id, ISender sender) =>
        {
            var result = await sender.Send(new GetCustomerByIdQuery(id));

            var response = result.Adapt<GetCustomerByIdResponse>();

            return Results.Ok(response);
        })
        .WithName("GetCustomerById")
        .Produces<GetCustomerByIdResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Get Customer By Id")
        .WithDescription("Get Customer By Id");
    }
}
