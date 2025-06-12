namespace Yourdrs.Reports.API.Features.Reports.GetOfficeAppointmentCounts;

public class GetOfficeAppointmentCountsRequest
{
    public string PracticeIds { get; set; }
    public string LocationIds { get; set; }
    public string AppointmentTypeIds { get; set; }
    public string ProviderIds { get; set; }
    public string StatusIds { get; set; }
    public DateTime? AppointmentStartDate { get; set; }
    public DateTime? AppointmentEndDate { get; set; }
    public DateTime? PostedStartDate { get; set; }
    public DateTime? PostedEndDate { get; set; }
    public int? ARTypeId { get; set; }
    public string ProcedureIds { get; set; }
    public string ProcedureTypeIds { get; set; }
    public string BillingTypeId { get; set; }
}
