using Yourdrs.CrossCutting.CQRS;

namespace Yourdrs.Reports.API.Features.Reports.GetPracticeCounts;

public class GetPrcticeCountEndpoint : ICarterModule
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
           .WithName("GetPrcticeCount")
           .Produces<List<PracticeCountResponse>>(StatusCodes.Status200OK)
           .ProducesProblem(StatusCodes.Status400BadRequest)
           .WithSummary("Get prctice count")
           .WithDescription("Get prctice count");
    }
}
