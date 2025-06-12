using Yourdrs.CrossCutting.CQRS;

namespace Yourdrs.Reports.API.Features.Reports.GetPracticeCounts;

public class GetPracticeCountEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/Reports/PracticeCounts",
               async (
                   GetPracticeCountsRequest request,
                   [FromServices] IDispatcher dispatcher,
                   CancellationToken cancellationToken) =>
               {
                   var command = request.Adapt<GetPracticeCountsCommand>();
                   var result = await dispatcher.Send<GetPracticeCountsCommand, List<PracticeCountResponse>>(command, cancellationToken);

                   return Results.Ok(result);
               })
           .WithName("GetPracticeCount")
           .Produces<List<PracticeCountResponse>>()
           .ProducesProblem(StatusCodes.Status400BadRequest)
           .WithSummary("Get practice count")
           .WithDescription("Get practice count");
    }
}
