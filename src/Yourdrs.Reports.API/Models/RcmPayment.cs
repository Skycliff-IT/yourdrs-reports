using System;
using System.Collections.Generic;

namespace Yourdrs.Reports.API.Models;

public partial class RcmPayment
{
    public uint Id { get; set; }

    public uint? EpisodeId { get; set; }

    public uint? ClaimId { get; set; }

    public uint? AppointmentId { get; set; }

    public byte? PaymentTypeId { get; set; }

    public byte? PaymentModeId { get; set; }

    public string? Comments { get; set; }

    public double? Amount { get; set; }

    public ushort? StatusId { get; set; }

    public double? WriteOffAmount { get; set; }

    public double? AdditionalAmount { get; set; }

    public double? BalanceAmount { get; set; }

    public uint? ParentId { get; set; }

    public double? BillToSecondary { get; set; }

    public double? BillToPatient { get; set; }

    public double? PaidByPatient { get; set; }

    public double? PaidByInsurance { get; set; }

    public double? OtherPayments { get; set; }

    public byte? IsEclaimPosting { get; set; }

    public byte? Transferred { get; set; }

    public DateTime? DepositedDate { get; set; }

    public uint? PracticeLocationBanksId { get; set; }

    public DateTime? ActualCreatedDate { get; set; }

    public string? DepositDateReason { get; set; }

    public uint? CheckDetailsId { get; set; }

    public double? Percentage { get; set; }

    public double? EpiPercentage { get; set; }

    public uint? PayorId { get; set; }

    public byte IsActive { get; set; }

    public DateTime CreatedDate { get; set; }

    public uint CreatedBy { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public uint? ModifiedBy { get; set; }
}