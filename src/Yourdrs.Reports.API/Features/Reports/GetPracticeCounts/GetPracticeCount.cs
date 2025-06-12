using Yourdrs.CrossCutting.CQRS;

namespace Yourdrs.Reports.API.Features.Reports.GetPracticeCounts;

public record PracticeCountResponse(string PracticeName, string LocationName, int AppointmentCount);

public record GetPracticeCountsCommand(
    string PracticeIds, 
    string? LocationIds,
    string? AppointmentTypeIds,
    string? ProviderIds,
    string? StatusIds,
    string? ProcedureIds,
    string? ProcedureTypeIds,
    string? SelectedCaseTypeIds,
    string? SelectedCaseTypeStateIds,
    string? PatientAdvocateIds,
    int? ReferralSourceTypeId,
    int? ReferralSourceMemberId,
    int? ReferralSourcePracticeLocationId,
    int? ReferralSourcePatientId,
    int? LoginMemberId,
    int? ArTypeId,
    byte? BillingTypeId,
    int? LegalRepId,
    DateTime? AppointmentStartDate,
    DateTime? AppointmentEndDate,
    DateTime? PostedStartDate,
    DateTime? PostedEndDate,
    int? TimeZoneOffset,
    string? SurgeryDateOp) : ICommand<List<PracticeCountResponse>>;
