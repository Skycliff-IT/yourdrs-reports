﻿using Yourdrs.CrossCutting.CQRS;

namespace Yourdrs.Reports.API.Customers.UpdateCustomer;
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
            .Produces<UpdateCustomerResponse>()
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithSummary("Update Customer")
            .WithDescription("Update Customer");
    }
}
