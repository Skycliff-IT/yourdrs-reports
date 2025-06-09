using System;
using System.Collections.Generic;

namespace Yourdrs.Reports.API.Models;

public partial class RcmClaim
{
    public uint Id { get; set; }

    public string? ClaimNumber { get; set; }

    public uint AppointmentId { get; set; }

    public uint ProviderId { get; set; }

    public ushort PracticeId { get; set; }

    public ushort LocationId { get; set; }

    public uint? PatientPayorId { get; set; }

    public uint? AttorneyId { get; set; }

    public byte? BillingTypeId { get; set; }

    public byte? ClaimTypeId { get; set; }

    public byte? BillingModeId { get; set; }

    public ushort StatusId { get; set; }

    public DateTime? BilledDate { get; set; }

    public byte? PosId { get; set; }

    public byte? TosId { get; set; }

    public byte? FormId { get; set; }

    public sbyte? AcceptsAssignment { get; set; }

    public string? FileName { get; set; }

    public DateTime? SentDate { get; set; }

    public byte? IsActive { get; set; }

    public uint CreatedBy { get; set; }

    public DateTime CreatedDate { get; set; }

    public uint? ModifiedBy { get; set; }

    public DateTime? ModifiedDate { get; set; }
}