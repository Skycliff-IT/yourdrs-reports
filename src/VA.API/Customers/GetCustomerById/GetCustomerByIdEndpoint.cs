namespace VA.API.Customers.GetCustomerById;

//public record GetCustomerByIdRequest();
public record GetCustomerByIdResponse(Customer Customer);
public class GetCustomerByIdQuery(Guid id) : IQuery<GetCustomerByIdResponse>
{
    public Guid Id { get; } = id;
}
public class GetCustomerByIdEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/Customers/{id}", async (Guid id,
            IQueryHandler<GetCustomerByIdQuery, GetCustomerByIdResponse> handler,
            CancellationToken cancellationToken) =>
        {
            var result = await handler.Handle(new GetCustomerByIdQuery(id), cancellationToken);

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
