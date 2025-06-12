using System;
using System.Collections.Generic;

namespace Yourdrs.Reports.API.Models;

public partial class Partnermember
{
    public ushort Id { get; set; }

    public string Firstname { get; set; } = null!;

    public string? Middlename { get; set; }

    public string? Lastname { get; set; }

    public byte? Suffixid { get; set; }

    public byte? Prefixid { get; set; }

    public byte? Isarbitrationattorney { get; set; }

    public byte? Islitigationattorney { get; set; }

    public byte? Roleid { get; set; }

    public uint? Memberid { get; set; }

    public ushort? Practiceid { get; set; }

    public ushort? Locationid { get; set; }

    public sbyte Isdefaultmember { get; set; }

    public byte Isactive { get; set; }

    public uint Modifiedby { get; set; }

    public DateTime Modifieddate { get; set; }

    public virtual ICollection<Partnerorganizationmember> Partnerorganizationmembers { get; set; } = new List<Partnerorganizationmember>();
}
