using System;
using System.Collections.Generic;

namespace Yourdrs.Reports.API.Models;

public partial class Partnerorglocationmapping
{
    public ushort Id { get; set; }

    public ushort Partnerorganizationid { get; set; }

    public ushort? Partnerorganizationlocationid { get; set; }

    public byte Isactive { get; set; }

    public uint Modifiedby { get; set; }

    public DateTime Modifieddate { get; set; }

    public virtual Partnerorganization Partnerorganization { get; set; } = null!;

    public virtual ICollection<Partnerorganizationmember> Partnerorganizationmembers { get; set; } = new List<Partnerorganizationmember>();
}
