using Yourdrs.CrossCutting.CQRS;

namespace Yourdrs.Reports.API.Features.Reports.GetOfficeAppointmentCounts;

public class GetOfficeAppointmentCountsEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/Reports/OfficeAppointmentCounts",
            async (
                GetOfficeAppointmentCountsRequest request,
                [FromServices] IDispatcher dispatcher,
                CancellationToken cancellationToken) =>
            {
                // Adapt the request to the command
                var command = request.Adapt<GetOfficeAppointmentCountsCommand>();

                // Send the command via dispatcher and get the result
                var result = await dispatcher.Send<GetOfficeAppointmentCountsCommand, List<OfficeAppointmentCountResponse>>(command, cancellationToken);

                // Return the result as an HTTP 200 response
                return Results.Ok(result);
            })
            .WithName("GetOfficeAppointmentCounts")
            .Produces<List<OfficeAppointmentCountResponse>>()
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Get office appointment counts")
            .WithDescription("Retrieves the appointment counts by status for the given parameters.");
    }
}
