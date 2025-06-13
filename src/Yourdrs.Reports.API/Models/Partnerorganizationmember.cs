namespace Yourdrs.Reports.API.Models;

public partial class PartnerOrganizationMember
{
    public ushort Id { get; set; }

    public ushort PartnerOrgLocationMappingId { get; set; }

    public ushort PartnerMemberId { get; set; }

    public byte ReferralSourceTypeId { get; set; }

    public byte IsActive { get; set; }

    public uint ModifiedBy { get; set; }

    public DateTime ModifiedDate { get; set; }

    public virtual ICollection<EpisodeLegalRepresentative> EpisodeLegalRepresentatives { get; set; } = new List<EpisodeLegalRepresentative>();

    public virtual PartnerMember PartnerMember { get; set; } = null!;

    public virtual PartnerOrgLocationMapping PartnerOrgLocationMapping { get; set; } = null!;
}
