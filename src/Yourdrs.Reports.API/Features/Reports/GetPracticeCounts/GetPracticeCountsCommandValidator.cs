namespace Yourdrs.Reports.API.Features.Reports.GetPracticeCounts;

public class GetPracticeCountsCommandValidator : AbstractValidator<GetPracticeCountsCommand>
{
    public GetPracticeCountsCommandValidator()
    {
        RuleFor(x => x.PracticeIds)
           .NotEmpty().WithMessage("Practice IDs are required.");
    }
}
