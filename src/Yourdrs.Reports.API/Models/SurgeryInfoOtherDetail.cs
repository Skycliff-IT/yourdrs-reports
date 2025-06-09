using System;
using System.Collections.Generic;

namespace Yourdrs.Reports.API.Models;

public partial class SurgeryInfoOtherDetail
{
    public uint Id { get; set; }

    public uint AppointmentId { get; set; }

    public uint ProcedureId { get; set; }

    public byte ProcedureTypeId { get; set; }

    public string? Grade { get; set; }

    public byte IsHardDate { get; set; }

    public byte IsSurgeryUrgent { get; set; }

    public byte IsArchive { get; set; }

    public byte IsMedicalClearanceRequired { get; set; }

    public byte IsMedicalClearanceCompleted { get; set; }

    public byte? IsPostOpCall { get; set; }

    public DateTime OriginalConfirmedDate { get; set; }

    public ushort? WorkflowStatusId { get; set; }

    public uint? CoordinatorId { get; set; }

    public string? SecondaryProcedure { get; set; }

    public string? DiagnosisCodes { get; set; }

    public string? OtherDataJson { get; set; }

    public string? SurgeryAssistant { get; set; }

    public decimal? PredictedAmount { get; set; }

    public byte IsActive { get; set; }

    public uint CreatedBy { get; set; }

    public DateTime CreatedDate { get; set; }

    public uint? ModifiedBy { get; set; }

    public DateTime? ModifiedDate { get; set; }
}