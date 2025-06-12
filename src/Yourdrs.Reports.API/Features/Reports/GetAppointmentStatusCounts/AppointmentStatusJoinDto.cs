namespace Yourdrs.Reports.API.Features.Reports.GetAppointmentStatusCounts;

public class AppointmentStatusJoinDto
{
    public Appointment app { get; set; }
    public Episode eps { get; set; }
    public Practice aprc { get; set; }
    public Location aloc { get; set; }
    public RcmClaim clm { get; set; }
    public RcmPayment pay { get; set; }
    public RcmCheckDetail chk { get; set; }
    public SurgeryInfoOtherDetail surg { get; set; }
}
