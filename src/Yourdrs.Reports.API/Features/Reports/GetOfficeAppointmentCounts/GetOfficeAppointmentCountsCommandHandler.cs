using MySqlConnector;
using Yourdrs.CrossCutting.CQRS;
using Yourdrs.Reports.API.Extensions;

namespace Yourdrs.Reports.API.Features.Reports.GetOfficeAppointmentCounts;

public class GetOfficeAppointmentCountsCommandHandler(ApplicationDbContext _context) : ICommandHandler<GetOfficeAppointmentCountsCommand, List<OfficeAppointmentCountDto>>
{
    

    public async Task<List<OfficeAppointmentCountDto>> Handle(GetOfficeAppointmentCountsCommand request, CancellationToken cancellationToken = default)
    {
        var hStartDate = request.AppointmentStartDate.AddYears(-1);
        var hEndDate = request.AppointmentEndDate.AddYears(-1);

        // Conditional JOIN flags
        var includeSurgJoin = CsvHelper.ParseCsvToIntList(request.ProcedureIds).Any() || CsvHelper.ParseCsvToIntList(request.ProcedureTypeIds).Any();
        var includeClaimJoin = request.BillingTypeId.HasValue || request.PostedStartDate.HasValue || request.PostedEndDate.HasValue;

        // Conditional JOINs
        var joinSurg = includeSurgJoin ? "LEFT JOIN surgeryinfootherdetails surg ON surg.appointmentid = app.id" : "";
        var joinClaim = includeClaimJoin ? "LEFT JOIN rcmclaim clm ON clm.appointmentid = app.id AND clm.isactive = 1" : "";
        var joinPay = includeClaimJoin ? "LEFT JOIN rcmpayments pay ON pay.claimid = clm.id AND pay.isactive = 1" : "";
        var joinCheck = includeClaimJoin ? "LEFT JOIN rcmpayments pay ON pay.claimid = clm.id AND pay.isactive = 1 LEFT JOIN rcmcheckdetails chk ON chk.id = pay.checkdetailsid AND chk.isactive = 1"
                 : "";
        // Dynamic WHERE conditions
        string BuildInClause(string column, List<int> values, string paramPrefix)
        {
            if (values == null || !values.Any()) return "";
            var inClause = string.Join(",", values);
            return $"AND {column} IN ({inClause})";
        }

        var practiceCondition = BuildInClause("app.practiceid", CsvHelper.ParseCsvToIntList(request.PracticeIds), "_prac");
        var locationCondition = BuildInClause("app.locationid", CsvHelper.ParseCsvToIntList(request.LocationIds), "_loc");
        var providerCondition = BuildInClause("app.providerid", CsvHelper.ParseCsvToIntList(request.ProviderIds), "_prov");
        var appointmentTypeCondition = BuildInClause("app.appointmenttypeid", CsvHelper.ParseCsvToIntList(request.AppointmentTypeIds), "_appt");
        var statusCondition = BuildInClause("app.statusid", CsvHelper.ParseCsvToIntList(request.StatusIds), "_stat");
        var procedureCondition = includeSurgJoin ? BuildInClause("surg.procedureid", CsvHelper.ParseCsvToIntList(request.ProcedureIds), "_proc") : "";
        var procedureTypeCondition = includeSurgJoin ? BuildInClause("surg.proceduretypeid", CsvHelper.ParseCsvToIntList(request.ProcedureTypeIds), "_proctype") : "";

        var billingTypeCondition = includeClaimJoin && request.BillingTypeId.HasValue ? "AND clm.billingtypeid = @_BillingTypeId" : "";

        var postedDateCondition = includeClaimJoin && (request.PostedStartDate.HasValue || request.PostedEndDate.HasValue)
            ? @"AND ((@_PostedStartDate IS NULL AND @_PostedEndDate IS NULL)
                    OR (@_PostedStartDate IS NOT NULL AND @_PostedEndDate IS NULL AND DATE(chk.posteddate) >= @_PostedStartDate)
                    OR (@_PostedStartDate IS NULL AND @_PostedEndDate IS NOT NULL AND DATE(chk.posteddate) <= @_PostedEndDate)
                    OR (DATE(chk.posteddate) BETWEEN @_PostedStartDate AND @_PostedEndDate))"
            : "";

        var arTypeCondition = request.ArTypeId.HasValue
            ? @"AND ((@_ARTypeId = 1 AND app.appointmenttypeid = 7)
                     OR (@_ARTypeId = 2 AND app.appointmenttypeid != 7))"
            : "";

        var surgeryDateOpCondition = request.SurgeryDateOp.HasValue
            ? @"AND ((@_SurgeryDateOp = 1 AND DATE(app.startdatetime) BETWEEN @_HAppointmentstartdate AND @_Appointmentenddate)
                     OR (@_SurgeryDateOp = 2 AND DATE(DATE_ADD(app.createddate, INTERVAL (-1 * @_TimeZoneOffset) MINUTE)) BETWEEN @_HAppointmentstartdate AND @_Appointmentenddate)
                     OR (@_SurgeryDateOp = 3 AND DATE(app.startdatetime) BETWEEN @_HAppointmentstartdate AND @_Appointmentenddate AND app.isrescheduled = 1))"
            : "";

        var whereClause = $@"
            WHERE app.statusid NOT IN (5)
            {practiceCondition}
            {locationCondition}
            {providerCondition}
            {appointmentTypeCondition}
            {statusCondition}
            {procedureCondition}
            {procedureTypeCondition}
            {billingTypeCondition}
            {postedDateCondition}
            {arTypeCondition}
            {surgeryDateOpCondition}";

        var sqlQuery = $@"
            WITH apptype AS (
                SELECT 1 AS grp, 'Office Visits' AS grpname, id AS appttypid FROM appointmenttypes WHERE id IN (1,4,5,17,18,19,22,23,28,30,31,36,38,41,47,49,51,52,53,55,56,57,58,59,61,63,64,66,69,72)
                UNION ALL SELECT 2, 'IE / Consultation', id FROM appointmenttypes WHERE id IN (6, 32)
                UNION ALL SELECT 3, 'PT', id FROM appointmenttypes WHERE id IN (9, 11, 33)
                UNION ALL SELECT 4, 'EMG / NCV', id FROM appointmenttypes WHERE id IN (13,14,15,16,42,43,44,50)
                UNION ALL SELECT 5, 'In office procedure', id FROM appointmenttypes WHERE id IN (10, 25)
                UNION ALL SELECT 6, 'Diagnostic', id FROM appointmenttypes WHERE id IN (20, 21, 27)
                UNION ALL SELECT 7, 'Post Op Visit', id FROM appointmenttypes WHERE id IN (24)
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
            {joinClaim}
            {joinPay}
            {joinCheck}
            {joinSurg}
            {whereClause}
            GROUP BY aty.grp, aty.grpname;";

        var parameters = new List<MySqlParameter>
        {
            new("@_PracticeIds", request.PracticeIds ?? ""),
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
