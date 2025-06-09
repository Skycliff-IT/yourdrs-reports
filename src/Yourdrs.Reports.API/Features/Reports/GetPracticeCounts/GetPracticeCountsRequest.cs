namespace Yourdrs.Reports.API.Features.Reports.GetPracticeCounts;

public class GetPracticeCountsRequest
{
    public string PracticeIds { get; init; } = null!;
    public string? LocationIds { get; init; }
    public string? AppointmentTypeIds { get; init; }
    public string? ProviderIds { get; init; }
    public string? StatusIds { get; init; }
    public string? ProcedureIds { get; init; }
    public string? ProcedureTypeIds { get; init; }
    public string? SelectedCaseTypeIds { get; init; }
    public string? SelectedCaseTypeStateIds { get; init; }
    public string? PatientAdvocateIds { get; init; }
    public int? ReferralSourceTypeId { get; init; }
    public int? ReferralSourceMemberId { get; init; }
    public int? ReferralSourcePracticeLocationId { get; init; }
    public int? ReferralSourcePatientId { get; init; }
    public int? LoginMemberId { get; init; }
    public int? ARTypeId { get; init; }
    public byte? BillingTypeId { get; init; }
    public int? LegalRepId { get; init; }
    public DateTime? AppointmentStartDate { get; init; }
    public DateTime? AppointmentEndDate { get; init; }
    public DateTime? PostedStartDate { get; init; }
    public DateTime? PostedEndDate { get; init; }
    public int? TimeZoneOffset { get; init; }
    public string? SurgeryDateOp { get; init; }
}
