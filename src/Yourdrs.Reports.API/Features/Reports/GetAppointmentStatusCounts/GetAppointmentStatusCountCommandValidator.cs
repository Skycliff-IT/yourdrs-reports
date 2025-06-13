using Yourdrs.Reports.API.Features.Reports.GetPracticeCounts;

namespace Yourdrs.Reports.API.Features.Reports.GetAppointmentStatusCounts
{
    public class GetAppointmentStatusCountCommandValidator : AbstractValidator<GetPracticeCountsCommand>
    {
        public GetAppointmentStatusCountCommandValidator()
        {
            RuleFor(x => x.PracticeIds)
               .NotEmpty().WithMessage("Practice IDs are required.");

            RuleFor(x => x.AppointmentStartDate)
              .NotEmpty().WithMessage("Appointment start date are required.");

            RuleFor(x => x.AppointmentEndDate)
              .NotEmpty().WithMessage("Appointment end date are required.");
        }
    }
}
