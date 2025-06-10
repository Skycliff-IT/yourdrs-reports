namespace Yourdrs.Reports.API.Models;

public partial class PatientAdvocate
{
    public uint Id { get; set; }

    public uint EpisodeId { get; set; }

    public uint MemberId { get; set; }

    public DateTime StartDate { get; set; }

    public DateOnly? EndDate { get; set; }

    public byte IsActive { get; set; }

    public uint ModifiedBy { get; set; }

    public DateTime ModifiedDate { get; set; }

    public uint? PrevDbId { get; set; }
}
