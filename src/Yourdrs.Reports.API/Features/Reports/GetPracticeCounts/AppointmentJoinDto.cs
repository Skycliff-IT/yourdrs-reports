namespace Yourdrs.Reports.API.Features.Reports.GetPracticeCounts
{
    public class AppointmentJoinDto
    {
        public Appointment app { get; set; }
        public Practice aprc { get; set; }
        public Location aloc { get; set; }
        public RcmClaim clm { get; set; }
        public RcmPayment pay { get; set; }
        public RcmCheckDetail chk { get; set; }
        public SurgeryInfoOtherDetail surg { get; set; }
    }
}
