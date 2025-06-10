namespace Yourdrs.Reports.API.Models;

public partial class Practice
{
    public ushort Id { get; set; }

    public string PracticeName { get; set; } = null!;

    public string? ShortName { get; set; }

    public sbyte IsPrimaryPractice { get; set; }

    public byte IsExternal { get; set; }

    public byte IsActive { get; set; }

    public uint ModifiedBy { get; set; }

    public DateTime ModifiedDate { get; set; }

    public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();

    public virtual ICollection<RcmClaim> RcmClaims { get; set; } = new List<RcmClaim>();
}
