using System;
using System.Collections.Generic;

namespace Yourdrs.Reports.API.Models;

public partial class Partnerorganizationmember
{
    public ushort Id { get; set; }

    public ushort Partnerorglocationmappingid { get; set; }

    public ushort Partnermemberid { get; set; }

    public byte Referralsourcetypeid { get; set; }

    public byte Isactive { get; set; }

    public uint Modifiedby { get; set; }

    public DateTime Modifieddate { get; set; }

    public virtual ICollection<Episodelegalrepresentative> Episodelegalrepresentatives { get; set; } = new List<Episodelegalrepresentative>();

    public virtual Partnermember Partnermember { get; set; } = null!;

    public virtual Partnerorglocationmapping Partnerorglocationmapping { get; set; } = null!;
}
