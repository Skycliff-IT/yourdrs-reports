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
                var command = request.Adapt<GetOfficeAppointmentCountsCommand>();

                var result = await dispatcher.Send<GetOfficeAppointmentCountsCommand, List<OfficeAppointmentCountDto>>(command, cancellationToken);

                return Results.Ok(result);
            })
            .WithName("GetOfficeAppointmentCounts")
            .Produces<List<OfficeAppointmentCountDto>>()
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Get office appointment counts")
            .WithDescription("Retrieves the appointment counts by status for the given parameters.");
    }
}
