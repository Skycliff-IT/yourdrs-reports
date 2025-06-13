namespace Yourdrs.Reports.API.Models;

public partial class PartnerOrgLocationMapping
{
    public ushort Id { get; set; }

    public ushort PartnerOrganizationId { get; set; }

    public ushort? PartnerOrganizationLocationId { get; set; }

    public byte IsActive { get; set; }

    public uint ModifiedBy { get; set; }

    public DateTime ModifiedDate { get; set; }

    public virtual PartnerOrganization PartnerOrganization { get; set; } = null!;

    public virtual ICollection<PartnerOrganizationMember> PartnerOrganizationMembers { get; set; } = new List<PartnerOrganizationMember>();
}
