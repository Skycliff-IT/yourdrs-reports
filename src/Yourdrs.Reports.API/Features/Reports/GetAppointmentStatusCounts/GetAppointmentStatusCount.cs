using Yourdrs.CrossCutting.CQRS;

namespace Yourdrs.Reports.API.Features.Reports.GetAppointmentStatusCounts
{
    public record AppointmentStatusCountResponse(string ReqSts, List<int> ReqStsIds, int ReqCount, int ReqHistCount,
                                                 string CancelSts, List<int> CancelStsIds, int CancelCount, int CancelHistCount,
                                                 string CheckedOutSts, List<int> CheckedOutStsIds, int CheckedOutCount, int CheckedOutHistCount,
                                                 string ReadyToGoSts, List<int> ReadyToGoStsIds, int ReadyToGoCount, int ReadyToGoHistCount );

    public record GetAppointmentStatusCountCommand(
        string PracticeIds,
        string? LocationIds,
        int? ReferralSourceTypeId,
        int? ReferralSourceMemberId,
        int? ReferralSourcePracticeLocationId,
        int? ReferralSourcePatientId,
        DateTime AppointmentStartDate,
        DateTime AppointmentEndDate,
        string? AppointmentTypeIds,
        string? ProviderIds,
        string? StatusIds,
        int? LoginMemberId,
        int? TimeZoneOffset,
        string? SelectedCaseTypeIds,
        string? SelectedCaseTypeStateIds,
        string? SurgeryDateOp,
        int? ArTypeId,
        DateTime? PostedStartDate,
        DateTime? PostedEndDate,
        string? ProcedureIds,
        string? ProcedureTypeIds,
        byte? BillingTypeId,
        int? LegalRepId,
        string? PatientAdvocateIds
        )
        : ICommand<List<AppointmentStatusCountResponse>>;

}
