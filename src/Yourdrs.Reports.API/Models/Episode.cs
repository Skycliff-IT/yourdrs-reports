namespace Yourdrs.Reports.API.Models;

public partial class Episode
{
    public uint Id { get; set; }

    public uint PatientId { get; set; }

    public byte CaseTypeId { get; set; }

    public byte? CaseTypeStateId { get; set; }

    public string? CaseNumber { get; set; }

    public byte? IncidentTypeId { get; set; }

    public DateOnly? IncidentDate { get; set; }

    public ushort? StatusId { get; set; }

    public ushort? CaseManagementStatusId { get; set; }

    public string? ExternalPatientId { get; set; }

    public string? ExternalBarcode { get; set; }

    public string? CaseMngtStatusComments { get; set; }

    public byte IsActive { get; set; }

    public uint CreatedBy { get; set; }

    public DateTime CreatedDate { get; set; }

    public uint? ModifiedBy { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public uint? PrevDbId { get; set; }

    public uint? V1EpisodeId { get; set; }

    public uint? V2EpisodeId { get; set; }
}
