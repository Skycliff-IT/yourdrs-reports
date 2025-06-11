using Yourdrs.CrossCutting.CQRS;
using Yourdrs.Reports.API.Extensions;

namespace Yourdrs.Reports.API.Features.Reports.GetPracticeCounts;

public class GetPracticeCountsCommandHandler(ApplicationDbContext context) : ICommandHandler<GetPracticeCountsCommand, List<PracticeCountResponse>>
{
    public async Task<List<PracticeCountResponse>> Handle(GetPracticeCountsCommand command, CancellationToken cancellationToken = default)
    {
        var practiceIds = CsvHelper.ParseCsvToIntList(command.PracticeIds);
        var locationIds = CsvHelper.ParseCsvToIntList(command.LocationIds);
        var providerIds = CsvHelper.ParseCsvToIntList(command.ProviderIds);
        var appointmentTypeIds = CsvHelper.ParseCsvToIntList(command.AppointmentTypeIds);
        var statusIds = CsvHelper.ParseCsvToIntList(command.StatusIds);
        var procedureIds = CsvHelper.ParseCsvToIntList(command.ProcedureIds);
        var procedureTypeIds = CsvHelper.ParseCsvToIntList(command.ProcedureTypeIds);
        var patientAdvocateIds = CsvHelper.ParseCsvToIntList(command.PatientAdvocateIds);

        var baseQuery = from app in context.Appointments                         
                        join aprc in context.Practices on new { Id = app.PracticeId, IsActive = (byte)1 } equals new { Id = (ushort?)aprc.Id, aprc.IsActive }   
                        join aloc in context.Locations on new { Id = app.LocationId, IsActive = (byte)1 } equals new { Id = (ushort?)aloc.Id, aloc.IsActive }
                        where app.IsActive == 1
                        select new { app, aprc, aloc };

        IQueryable<AppointmentJoinDto> query = baseQuery
             .Select(q => new AppointmentJoinDto
             {
                 app = q.app,
                 aprc = q.aprc,
                 aloc = q.aloc,
                 clm = null,
                 pay = null,
                 chk = null,
                 surg = null
             });

        if (command.BillingTypeId.HasValue || command.PostedStartDate.HasValue || command.PostedEndDate.HasValue)
        {
            query = from q in query
                    join clmRaw in context.RcmClaims on new {Id = q.app.Id, IsActive = (byte?)1 } equals new {Id = clmRaw.AppointmentId , clmRaw.IsActive } into clmJoin
                    from clm in clmJoin.DefaultIfEmpty()

                    select new AppointmentJoinDto
                    {
                        app = q.app,
                        aprc = q.aprc,
                        aloc = q.aloc,
                        clm = clm,
                        pay = q.pay,
                        chk = q.chk,
                        surg = q.surg
                    };
        }

        if (command.PostedStartDate.HasValue || command.PostedEndDate.HasValue)
        {
            query = from q in query
                    join payRaw in context.RcmPayments on new { Id = q.clm.Id , IsActive = (byte)1 } equals new { Id =(uint)(payRaw.ClaimId ?? 0), payRaw.IsActive }  into payJoin
                    from pay in payJoin.DefaultIfEmpty()
                    join chkRaw in context.RcmCheckDetails on new { Id = pay.CheckDetailsId, IsActive = (byte)1 } equals new { Id = (uint?)chkRaw.Id, chkRaw.IsActive } into chkJoin
                    from chk in chkJoin.DefaultIfEmpty()
                    select new AppointmentJoinDto
                    {
                        app = q.app,
                        aprc = q.aprc,
                        aloc = q.aloc,
                        clm = q.clm,
                        pay = pay,
                        chk = chk,
                        surg = q.surg
                    };
        }

        if ((procedureIds != null && procedureIds.Any()) || (procedureTypeIds != null && procedureTypeIds.Any()))
        {
            query = from q in query
                    join surgRaw in context.SurgeryInfoOtherDetails on new {Id = q.app.Id, IsActive = (byte)1 } equals new { Id = surgRaw.AppointmentId, surgRaw.IsActive}  into surgJoin
                    from surg in surgJoin.DefaultIfEmpty()
                    select new AppointmentJoinDto
                    {
                        app = q.app,
                        aprc = q.aprc,
                        aloc = q.aloc,
                        clm = q.clm,
                        pay = q.pay,
                        chk = q.chk,
                        surg = surg
                    };
        }

        if (practiceIds != null && practiceIds.Any())
            query = query.Where(x => practiceIds.Contains((int)x.app.PracticeId));

        if (locationIds != null && locationIds.Any())
            query = query.Where(x => locationIds.Contains((int)x.app.LocationId));

        if (providerIds != null && providerIds.Any())
            query = query.Where(x => providerIds.Contains((int)x.app.ProviderId));

        if (appointmentTypeIds != null && appointmentTypeIds.Any())
            query = query.Where(x => appointmentTypeIds.Contains((int)x.app.AppointmentTypeId));

        if (statusIds != null && statusIds.Any())
            query = query.Where(x => statusIds.Contains((int)x.app.StatusId));

        if (procedureIds != null && procedureIds.Any())
            query = query.Where(x => procedureIds.Contains((int)x.surg.ProcedureId));

        if (procedureTypeIds != null && procedureTypeIds.Any())
            query = query.Where(x => procedureTypeIds.Contains((int)x.surg.ProcedureTypeId));

        if (command.BillingTypeId.HasValue)
            query = query.Where(x => x.clm != null && x.clm.BillingTypeId == command.BillingTypeId);

        if (command.AppointmentStartDate.HasValue && command.AppointmentEndDate.HasValue)
        {
            query = query.Where(x => x.app.StartDateTime >= command.AppointmentStartDate &&
                                     x.app.StartDateTime <= command.AppointmentEndDate);
        }
        else if (command.AppointmentStartDate.HasValue)
        {
            query = query.Where(x => x.app.StartDateTime >= command.AppointmentStartDate);
        }
        else if (command.AppointmentEndDate.HasValue)
        {
            query = query.Where(x => x.app.StartDateTime <= command.AppointmentEndDate);
        }

        if (command.PostedStartDate.HasValue && command.PostedEndDate.HasValue)
        {
            query = query.Where(x => x.chk.PostedDate >= command.AppointmentStartDate &&
                                    x.chk.PostedDate <= command.AppointmentEndDate);
        }
        else if (command.PostedStartDate.HasValue)
        {
            query = query.Where(x => x.chk.PostedDate >= command.AppointmentStartDate);
        }
        else if (command.PostedEndDate.HasValue)
        {
            query = query.Where(x => x.chk.PostedDate <= command.AppointmentEndDate);
        }

        var result = await query
             .GroupBy(x => new { x.aprc.PracticeName, x.aloc.LocationName })
             .Select(g => new PracticeCountResponse(
                 g.Key.PracticeName,
                 g.Key.LocationName,
                 g.Select(x => x.app.Id).Distinct().Count()
             ))
             .ToListAsync();

        return result.OrderBy(x => x.AppointmentCount).ToList();
    }

}
