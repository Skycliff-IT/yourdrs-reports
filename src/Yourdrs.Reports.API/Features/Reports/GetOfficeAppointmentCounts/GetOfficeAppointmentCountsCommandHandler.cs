using MySqlConnector;
using Yourdrs.CrossCutting.CQRS;

namespace Yourdrs.Reports.API.Features.Reports.GetOfficeAppointmentCounts;

public class GetOfficeAppointmentCountsCommandHandler : ICommandHandler<GetOfficeAppointmentCountsCommand, List<OfficeAppointmentCountDto>>
{
    private readonly ApplicationDbContext _context;

    public GetOfficeAppointmentCountsCommandHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<OfficeAppointmentCountDto>> Handle(GetOfficeAppointmentCountsCommand request, CancellationToken cancellationToken = default)
    {
        var hStartDate = request.AppointmentStartDate.AddYears(-1);
        var hEndDate = request.AppointmentEndDate.AddYears(-1);

        var locationCondition = string.IsNullOrEmpty(request.LocationIds) ? "" : "AND FIND_IN_SET(app.locationid, @_LocationIds) > 0";
        var providerCondition = string.IsNullOrEmpty(request.ProviderIds) ? "" : "AND FIND_IN_SET(app.providerid, @_ProviderIds) > 0";
        var appointmentTypeCondition = string.IsNullOrEmpty(request.AppointmentTypeIds) ? "" : "AND FIND_IN_SET(app.appointmenttypeid, @_AppointmentTypeIds) > 0";
        var statusCondition = string.IsNullOrEmpty(request.StatusIds) ? "" : "AND FIND_IN_SET(app.statusid, @_StatusIds) > 0";
        var procedureCondition = string.IsNullOrEmpty(request.ProcedureIds) ? "" : "AND FIND_IN_SET(surg.procedureid, @_ProcedureIds) > 0";
        var procedureTypeCondition = string.IsNullOrEmpty(request.ProcedureTypeIds) ? "" : "AND FIND_IN_SET(surg.proceduretypeid, @_ProcedureTypeIds) > 0";
        var billingTypeCondition = request.BillingTypeId.HasValue ? "AND clm.billingtypeid = @_BillingTypeId" : "";

        var postedDateCondition = @"
                AND (
                    (@_PostedStartDate IS NULL AND @_PostedEndDate IS NULL)
                    OR (@_PostedStartDate IS NOT NULL AND @_PostedEndDate IS NULL AND DATE(chk.posteddate) >= @_PostedStartDate)
                    OR (@_PostedStartDate IS NULL AND @_PostedEndDate IS NOT NULL AND DATE(chk.posteddate) <= @_PostedEndDate)
                    OR (DATE(chk.posteddate) BETWEEN @_PostedStartDate AND @_PostedEndDate)
                )";

        var arTypeCondition = @"
                AND (
                    @_ARTypeId IS NULL 
                    OR (@_ARTypeId = 1 AND app.appointmenttypeid = 7) 
                    OR (@_ARTypeId = 2 AND app.appointmenttypeid != 7)
                )";

        var surgeryDateOpCondition = @"
                AND (
                    (@_SurgeryDateOp = 1 AND DATE(app.startdatetime) BETWEEN @_HAppointmentstartdate AND @_Appointmentenddate)
                    OR (@_SurgeryDateOp = 2 AND DATE(DATE_ADD(app.createddate, INTERVAL (-1 * @_TimeZoneOffset) MINUTE)) BETWEEN @_HAppointmentstartdate AND @_Appointmentenddate)
                    OR (@_SurgeryDateOp = 3 AND DATE(app.startdatetime) BETWEEN @_HAppointmentstartdate AND @_Appointmentenddate AND app.isrescheduled = 1)
                )";

        var whereClause = $@"
                WHERE app.statusid NOT IN (5)
                AND FIND_IN_SET(app.practiceid, @_PracticeIds) > 0
                {locationCondition}
                {providerCondition}
                {appointmentTypeCondition}
                {statusCondition}
                {procedureCondition}
                {procedureTypeCondition}
                {billingTypeCondition}
                {postedDateCondition}
                {arTypeCondition}
                {surgeryDateOpCondition}
            ";

        var sqlQuery = $@"
                WITH apptype AS (
                                SELECT 1 AS grp, 'Office Visits' AS grpname, id AS appttypid 
                                FROM appointmenttypes 
                                WHERE id IN (1,4,5,17,18,19,22,23,26,28,29,30,31,34,35,36,37,38,39,41,46,47,49,51,52,53,54,55,56,57,58,59,60,61,62,63,64,65,66,67,68,69,70,71,72,73)
    
                                UNION ALL SELECT 2, 'IE / Consultation', id 
                                FROM appointmenttypes 
                                WHERE id IN (6, 32)
    
                                UNION ALL SELECT 3, 'PT', id 
                                FROM appointmenttypes 
                                WHERE id IN (9, 11, 33)
    
                                UNION ALL SELECT 4, 'EMG / NCV', id 
                                FROM appointmenttypes 
                                WHERE id IN (13,14,15,16,42,43,44,45,50)
    
                                UNION ALL SELECT 5, 'In office procedure', id 
                                FROM appointmenttypes 
                                WHERE id IN (10, 25)
    
                                UNION ALL SELECT 6, 'Diagnostic', id 
                                FROM appointmenttypes 
                                WHERE id IN (20, 21, 27)
    
                                UNION ALL SELECT 7, 'Post Op Visit', id 
                                FROM appointmenttypes 
                                WHERE id IN (24)
                            )
                SELECT aty.grp, aty.grpname, GROUP_CONCAT(DISTINCT aty.appttypid) AS typeids,
                       COUNT(DISTINCT CASE 
                           WHEN (@_SurgeryDateOp = 1 OR @_SurgeryDateOp = 3) AND DATE(app.startdatetime) BETWEEN @_Appointmentstartdate AND @_Appointmentenddate THEN app.id
                           WHEN @_SurgeryDateOp = 2 AND DATE(app.createddate) BETWEEN @_Appointmentstartdate AND @_Appointmentenddate THEN app.id
                       END) AS cnt,
                       COUNT(DISTINCT CASE 
                           WHEN (@_SurgeryDateOp = 1 OR @_SurgeryDateOp = 3) AND DATE(app.startdatetime) BETWEEN @_HAppointmentstartdate AND @_HAppointmentenddate THEN app.id
                           WHEN @_SurgeryDateOp = 2 AND DATE(app.createddate) BETWEEN @_HAppointmentstartdate AND @_HAppointmentenddate THEN app.id
                       END) AS Hcnt
                FROM appointments app
                INNER JOIN apptype aty ON app.appointmenttypeid = aty.appttypid
                INNER JOIN episodes e ON e.id = app.episodeid AND e.isactive = 1
                LEFT JOIN rcmclaim clm ON clm.appointmentid = app.id AND clm.isactive = 1
                LEFT JOIN rcmpayments pay ON pay.claimid = clm.id AND pay.isactive = 1
                LEFT JOIN rcmcheckdetails chk ON chk.id = pay.checkdetailsid AND chk.isactive = 1
                LEFT JOIN surgeryinfootherdetails surg ON surg.appointmentid = app.id
                {whereClause}
                GROUP BY aty.grp, aty.grpname;
            ";

        var parameters = new List<MySqlParameter>
        {
            new("@_PracticeIds", request.PracticeIds),
            new("@_LocationIds", request.LocationIds ?? ""),
            new("@_ProviderIds", request.ProviderIds ?? ""),
            new("@_AppointmentTypeIds", request.AppointmentTypeIds ?? ""),
            new("@_StatusIds", request.StatusIds ?? ""),
            new("@_ProcedureIds", request.ProcedureIds ?? ""),
            new("@_ProcedureTypeIds", request.ProcedureTypeIds ?? ""),
            new("@_BillingTypeId", request.BillingTypeId ?? (object)DBNull.Value),
            new("@_PostedStartDate", request.PostedStartDate ?? (object)DBNull.Value),
            new("@_PostedEndDate", request.PostedEndDate ?? (object)DBNull.Value),
            new("@_ARTypeId", request.ArTypeId ?? (object)DBNull.Value),
            new("@_SurgeryDateOp", request.SurgeryDateOp ?? (object)DBNull.Value),
            new("@_TimeZoneOffset", request.TimeZoneOffset ?? 0),
            new("@_Appointmentstartdate", request.AppointmentStartDate),
            new("@_Appointmentenddate", request.AppointmentEndDate),
            new("@_HAppointmentstartdate", hStartDate),
            new("@_HAppointmentenddate", hEndDate)
        };

        var result = await _context
                    .Set<OfficeAppointmentCountDto>()
                    .FromSqlRaw(sqlQuery, parameters.ToArray())
                    .ToListAsync(cancellationToken);

        return result;
    }
}
