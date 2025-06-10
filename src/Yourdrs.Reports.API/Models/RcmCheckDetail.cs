namespace Yourdrs.Reports.API.Models;

public partial class RcmCheckDetail
{
    public uint Id { get; set; }

    public uint? AppointmentId { get; set; }

    public byte? PaymentTypeId { get; set; }

    public byte? PaymentSubTypeId { get; set; }

    public byte? PaymentModeId { get; set; }

    public byte? PostingTypeId { get; set; }

    public string? CheckNumber { get; set; }

    public double? CheckAmount { get; set; }

    public uint? PayorId { get; set; }

    public int? AttorneyId { get; set; }

    public ushort? PayToPracticeId { get; set; }

    public DateTime? CheckDate { get; set; }

    public DateTime? ReceivedDate { get; set; }

    public DateTime? DepositDate { get; set; }

    public DateTime? PostedDate { get; set; }

    public uint? BankId { get; set; }

    public ushort? StatusId { get; set; }

    public ushort? WaitingForSupervisorStatusId { get; set; }

    public string? DepositDateReason { get; set; }

    public string? SupervisorReason { get; set; }

    public string? RejectReason { get; set; }

    public byte IsActive { get; set; }

    public uint CreatedBy { get; set; }

    public DateTime? CreatedDate { get; set; }

    public uint? ModifiedBy { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public virtual Appointment? Appointment { get; set; }

    public virtual ICollection<RcmPayment> RcmPayments { get; set; } = new List<RcmPayment>();
}
