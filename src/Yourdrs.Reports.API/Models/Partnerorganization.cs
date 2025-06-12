using System;
using System.Collections.Generic;

namespace Yourdrs.Reports.API.Models;

public partial class Partnerorganization
{
    public ushort Id { get; set; }

    public string Partnerorganizationname { get; set; } = null!;

    public byte? Organizationtypeid { get; set; }

    public ushort? Practiceid { get; set; }

    public string? Comments { get; set; }

    public byte Isactive { get; set; }

    public uint Modifiedby { get; set; }

    public DateTime Modifieddate { get; set; }

    public virtual ICollection<Partnerorglocationmapping> Partnerorglocationmappings { get; set; } = new List<Partnerorglocationmapping>();
}
