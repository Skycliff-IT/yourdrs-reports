using System;
using System.Collections.Generic;

namespace Yourdrs.Reports.API.Models;

public partial class Episodelegalrepresentative
{
    public uint Id { get; set; }

    public uint Episodeid { get; set; }

    public ushort Partnerorganizationmemberid { get; set; }

    public byte Isactive { get; set; }

    public uint Createdby { get; set; }

    public DateTime Createddate { get; set; }

    public uint? Modifiedby { get; set; }

    public DateTime? Modifieddate { get; set; }

    public uint? Prevdbid { get; set; }

    public virtual Partnerorganizationmember Partnerorganizationmember { get; set; } = null!;
}
