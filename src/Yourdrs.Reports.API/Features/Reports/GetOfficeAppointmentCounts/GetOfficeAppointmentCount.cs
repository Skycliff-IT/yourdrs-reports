using Yourdrs.CrossCutting.CQRS;

namespace Yourdrs.Reports.API.Features.Reports.GetOfficeAppointmentCounts;

public record OfficeAppointmentCountResponse(int Grp,string GrpName,string TypeIds,int Cnt,int Hcnt);
public record GetOfficeAppointmentCountsCommand(
    string PracticeIds,
    string? LocationIds,
    string? AppointmentTypeIds,
    string? ProviderIds,
    string? StatusIds,
    string? ProcedureIds,
    string? ProcedureTypeIds,
    int? ARTypeId,
    byte? BillingTypeId,
    DateTime? AppointmentStartDate,
    DateTime? AppointmentEndDate,
    DateTime? PostedStartDate,
    DateTime? PostedEndDate,
    int? TimeZoneOffset,
    string? SurgeryDateOp) : ICommand<List<OfficeAppointmentCountResponse>>;
