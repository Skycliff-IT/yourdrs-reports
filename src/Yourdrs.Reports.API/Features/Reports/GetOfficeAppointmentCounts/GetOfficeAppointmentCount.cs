using Yourdrs.CrossCutting.CQRS;

namespace Yourdrs.Reports.API.Features.Reports.GetOfficeAppointmentCounts;
public record GetOfficeAppointmentCountsCommand(
    string PracticeIds,
    string? LocationIds,
    string? AppointmentTypeIds,
    string? ProviderIds,
    string? StatusIds,
    string? ProcedureIds,
    string? ProcedureTypeIds,
    int? ArTypeId,
    byte? BillingTypeId,
    DateTime AppointmentStartDate,
    DateTime AppointmentEndDate,
    DateTime? PostedStartDate,
    DateTime? PostedEndDate,
    int? TimeZoneOffset,
    int? SurgeryDateOp) : ICommand<List<OfficeAppointmentCountDto>>;
