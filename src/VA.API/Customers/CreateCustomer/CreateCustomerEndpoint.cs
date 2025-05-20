namespace VA.API.Customers.CreateCustomer;

public record CreateCustomerRequest(string CustomerCode, string CustomerName);

public record CreateCustomerResponse(Guid Id);

public class CreateCustomerEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/Customers",
            async (CreateCustomerRequest request, ISender sender) =>
        {
            var command = request.Adapt<CreateCustomerCommand>();

            var result = await sender.Send(command);

            var response = result.Adapt<CreateCustomerResponse>();

            return Results.Created($"/Customers/{response.Id}", response);

        })
        .WithName("CreateCustomer")
        .Produces<CreateCustomerResponse>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Create Customer")
        .WithDescription("Create Customer");
    }
}
