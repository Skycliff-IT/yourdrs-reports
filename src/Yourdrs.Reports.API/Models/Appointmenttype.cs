using System;
using System.Collections.Generic;

namespace Yourdrs.Reports.API.Models;

public partial class AppointmentType
{
    public byte Id { get; set; }

    public string AppointmentTypeName { get; set; } = null!;

    public string AppointmentTypeShortName { get; set; } = null!;

    public byte DurationInMinutes { get; set; }

    public sbyte? GroupId { get; set; }

    public sbyte IsActive { get; set; }

    public DateTime ModifiedDate { get; set; }

    public uint ModifieBby { get; set; }
}
