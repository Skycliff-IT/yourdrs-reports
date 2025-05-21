using VA.Shared.Pagination;

namespace VA.API.Customers.GetCustomers;

public class GetCustomersQuery : IQuery<GetCustomersResponse>
{
    public PaginationRequest Request { get; }

    public GetCustomersQuery(PaginationRequest request)
    {
        Request = request;
    }
}
public record GetCustomersResponse(PaginatedResult<CustomerDto> Customers);

public class GetCustomersEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/Customers", async ([AsParameters] PaginationRequest request,
                IQueryHandler<GetCustomersQuery,GetCustomersResponse> handler, CancellationToken cancellationToken) =>
        {
            //var query = request.Adapt<GetCustomersQuery>();
            //var result = await handler.Handle(new GetCustomersQuery(request));
            var result = await handler.Handle(new GetCustomersQuery(request), cancellationToken);

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
