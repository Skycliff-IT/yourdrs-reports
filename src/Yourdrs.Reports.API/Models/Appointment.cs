namespace Yourdrs.Reports.API.Models;

public partial class Appointment
{
    public uint Id { get; set; }

    public uint EpisodeId { get; set; }

    public uint ProviderId { get; set; }

    public ushort? PracticeId { get; set; }

    public ushort? LocationId { get; set; }

    public ushort? PosPracticeId { get; set; }

    public ushort? PosLocationId { get; set; }

    public byte AppointmentTypeId { get; set; }

    public ushort? StatusId { get; set; }

    public ushort? AuthorizationStatusId { get; set; }

    public byte IsRescheduled { get; set; }

    public DateTime StartDateTime { get; set; }

    public DateTime EndDateTime { get; set; }

    public DateTime StatusLastModifiedDate { get; set; }

    public string? ExternalApptId { get; set; }

    public byte IsActive { get; set; }

    public uint CreatedBy { get; set; }

    public DateTime CreatedDate { get; set; }

    public uint? ModifiedBy { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public byte ModifiedByPatient { get; set; }

    public uint? PrevDbId { get; set; }

    public uint? V1AppointmentId { get; set; }

    public uint? V2AppointmentId { get; set; }

    public virtual Location? Location { get; set; }

    public virtual Practice? Practice { get; set; }

    public virtual ICollection<RcmCheckDetail> RcmCheckDetails { get; set; } = new List<RcmCheckDetail>();

    public virtual ICollection<RcmClaim> RcmClaims { get; set; } = new List<RcmClaim>();

    public virtual ICollection<RcmPayment> RcmPayments { get; set; } = new List<RcmPayment>();

    public virtual ICollection<SurgeryInfoOtherDetail> SurgeryInfoOtherDetails{ get; set; } = new List<SurgeryInfoOtherDetail>();
}
