namespace Yourdrs.Reports.API.Models;

public partial class PartnerOrganization
{
    public ushort Id { get; set; }

    public string PartnerOrganizationName { get; set; } = null!;

    public byte? OrganizationTypeId { get; set; }

    public ushort? PracticeId { get; set; }

    public string? Comments { get; set; }

    public byte IsActive { get; set; }

    public uint ModifiedBy { get; set; }

    public DateTime ModifiedDate { get; set; }

    public virtual ICollection<PartnerOrgLocationMapping> PartnerOrgLocationMappings { get; set; } = new List<PartnerOrgLocationMapping>();
}
