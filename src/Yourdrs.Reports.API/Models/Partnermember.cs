namespace Yourdrs.Reports.API.Models;

public partial class PartnerMember
{
    public ushort Id { get; set; }

    public string FirstName { get; set; } = null!;

    public string? MiddleName { get; set; }

    public string? LastName { get; set; }

    public byte? SuffixId { get; set; }

    public byte? PrefixId { get; set; }

    public byte? IsArbitrationAttorney { get; set; }

    public byte? IsLitigationAttorney { get; set; }

    public byte? RoleId { get; set; }

    public uint? MemberId { get; set; }

    public ushort? PracticeId { get; set; }

    public ushort? LocationId { get; set; }

    public sbyte IsDefaultMember { get; set; }

    public byte IsActive { get; set; }

    public uint ModifiedBy { get; set; }

    public DateTime ModifiedDate { get; set; }

    public virtual ICollection<PartnerOrganizationMember> PartnerOrganizationMembers { get; set; } = new List<PartnerOrganizationMember>();
}