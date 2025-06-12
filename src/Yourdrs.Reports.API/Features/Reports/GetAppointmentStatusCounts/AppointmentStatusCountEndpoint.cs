using Yourdrs.CrossCutting.CQRS;
using Yourdrs.Reports.API.Features.Reports.GetPracticeCounts;

namespace Yourdrs.Reports.API.Features.Reports.GetAppointmentStatusCounts
{
    public class AppointmentStatusCountEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/Reports/AppointmentStatusCount",
                   async (
                       GetAppointmentStatusCountRequest request,
                       [FromServices] IDispatcher dispatcher,
                       CancellationToken cancellationToken) =>
                   {
                       var command = request.Adapt<GetAppointmentStatusCountCommand>();
                       var result = await dispatcher.Send<GetAppointmentStatusCountCommand, List<AppointmentStatusCountResponse>>(command, cancellationToken);

                       return Results.Ok(result);
                   })
               .WithName("AppointmentStatusCount")
               .Produces<List<PracticeCountResponse>>()
               .ProducesProblem(StatusCodes.Status400BadRequest)
               .WithSummary("Appointment Status Count")
               .WithDescription("Appointment Status Count");
        }
    }
}
