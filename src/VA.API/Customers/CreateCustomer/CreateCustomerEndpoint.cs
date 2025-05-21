using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using VA.API.Customers.UpdateCustomer;
using VA.Shared.Behaviors;

namespace VA.API.Customers.CreateCustomer;

public record CreateCustomerRequest(string CustomerCode, string CustomerName);

public record CreateCustomerResponse(Guid Id);

//public class CreateCustomerEndpoint : ICarterModule
//{
//    public void AddRoutes(IEndpointRouteBuilder app)
//    {
//        app.MapPost("/Customers", async (
//                CreateCustomerRequest request,
//                [FromServices] IDispatcher dispatcher,
//                //ICommandHandler<CreateCustomerCommand,
//                    CreateCustomerResponse> handler) =>
//        {
//            var command = request.Adapt<CreateCustomerCommand>();
//            var response = await dispatcher.Send<UpdateCustomerCommand, UpdateCustomerResponse>(command, cancellationToken);

//            //var result = await handler.Handle(command);

//            var response = result.Adapt<CreateCustomerResponse>();

//            return Results.Created($"/Customers/{response.Id}", response);

//        })
//        .WithName("CreateCustomer")
//        .Produces<CreateCustomerResponse>(StatusCodes.Status201Created)
//        .ProducesProblem(StatusCodes.Status400BadRequest)
//        .WithSummary("Create Customer")
//        .WithDescription("Create Customer");
//    }
//}

public class CreateCustomerEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/Customers",
                async (
                    CreateCustomerRequest request,
                    [FromServices] IDispatcher dispatcher,
                    CancellationToken cancellationToken) =>
                {
                    var command = request.Adapt<CreateCustomerCommand>();
                    var result = await dispatcher.Send<CreateCustomerCommand, CreateCustomerResponse>(command, cancellationToken);

                    return Results.Created($"/Customers/{result.Id}", result);
                })
            .WithName("CreateCustomer")
            .Produces<CreateCustomerResponse>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Create Customer")
            .WithDescription("Create Customer");
    }
}
