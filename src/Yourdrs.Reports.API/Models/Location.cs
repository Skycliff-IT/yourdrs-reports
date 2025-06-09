namespace Yourdrs.Reports.API.Models;

public partial class Location
{
    public ushort Id { get; set; }

    public string LocationName { get; set; } = null!;

    public string? ShortName { get; set; }

    public byte IsActive { get; set; }

    public uint ModifiedBy { get; set; }

    public DateTime ModifiedDate { get; set; }
}
