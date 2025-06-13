namespace Yourdrs.Reports.API.Features.Reports.GetOfficeAppointmentCounts;

public class OfficeAppointmentCountDto
{
    public int Grp { get; set; }
    public string GrpName { get; set; }
    public string TypeIds { get; set; }
    public int Cnt { get; set; }
    public int Hcnt { get; set; }
}