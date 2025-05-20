namespace VA.API.Customers.GetCustomers;

public record GetCustomersRequest(int? PageNumber = 1, int? PageSize = 10);
public record GetCustomersResponse(IEnumerable<Customer> Customers);
//public record GetCustomersResponse(PaginatedResult<GetCustomersRequest> Customers);

public class GetCustomersEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/Customers", async ([AsParameters] GetCustomersRequest request, 
                ISender sender, 
                CancellationToken cancellationToken) =>
        {
            var query = request.Adapt<GetCustomersQuery>();

            var result = await sender.Send(query, cancellationToken);

            var response = result.Adapt<GetCustomersResponse>();

            return Results.Ok(response);
        })
        .WithName("GetCustomers")
        .Produces<GetCustomersResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Get Customers")
        .WithDescription("Get Customers");
    }
}
