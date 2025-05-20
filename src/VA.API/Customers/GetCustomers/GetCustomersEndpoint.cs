using VA.Shared.Pagination;

namespace VA.API.Customers.GetCustomers;

public record GetCustomersResponse(PaginatedResult<CustomerDto> Customers);

public class GetCustomersEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/Customers", async ([AsParameters] PaginationRequest request, ISender sender) =>
        {
            //var query = request.Adapt<GetCustomersQuery>();
            var result = await sender.Send(new GetCustomersQuery(request));

            var response = result.Adapt<GetCustomersResponse>();

            return Results.Ok(response);
        })
        .WithName("GetCustomers")
        .Produces<GetCustomersResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .WithSummary("Get Customers")
        .WithDescription("Get Customers");
    }
}
