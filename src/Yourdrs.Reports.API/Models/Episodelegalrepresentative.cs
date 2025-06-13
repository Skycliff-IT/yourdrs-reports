namespace Yourdrs.Reports.API.Models;

public partial class EpisodeLegalRepresentative
{
    public uint Id { get; set; }

    public uint EpisodeId { get; set; }

    public ushort PartnerOrganizationMemberId { get; set; }

    public byte IsActive { get; set; }

    public uint CreateBby { get; set; }

    public DateTime CreatedDate { get; set; }

    public uint? ModifiedBy { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public uint? PrevDbId { get; set; }

    public virtual PartnerOrganizationMember PartnerOrganizationMember { get; set; } = null!;
}
