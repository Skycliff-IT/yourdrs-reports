namespace Yourdrs.Reports.API.Features.Reports.GetOfficeAppointmentCounts;

public class OfficeAppointmentCountDto
{
    public Appointment app { get; set; }
    public Location aloc { get; set; }
    public RcmClaim clm { get; set; }
    public RcmPayment pay { get; set; }
    public RcmCheckDetail chk { get; set; }
    public SurgeryInfoOtherDetail surg { get; set; }
    //public PartnerOrganization pom { get; set; } 
    //public PartnerOrgLocationMapping pol { get; set; }
    //public PartnerOrganization porg { get; set; }  
    //public EpisodeLegalRepresentative elr { get; set; }
    
}

