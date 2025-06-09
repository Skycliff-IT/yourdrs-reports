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

        var query = from app in context.Appointments
                    join aprc in context.Practices on app.PracticeId equals aprc.Id
                    join aloc in context.Locations on app.LocationId equals aloc.Id
                    join clm in context.RcmClaims.Where(c => c.IsActive == 1)
                        on app.Id equals clm.AppointmentId into clmJoin
                    from clm in clmJoin.DefaultIfEmpty()
                    join pay in context.RcmPayments.Where(p => p.IsActive == 1)
                        on clm.Id equals pay.ClaimId into payJoin
                    from pay in payJoin.DefaultIfEmpty()
                    join chk in context.RcmCheckDetails.Where(c => c.IsActive == 1)
                        on pay.CheckDetailsId equals chk.Id into chkJoin
                    from chk in chkJoin.DefaultIfEmpty()
                    join ptadv in context.PatientAdvocates.Where(p => p.IsActive == 1)
                        on app.EpisodeId equals ptadv.EpisodeId into ptadvJoin
                    from ptadv in ptadvJoin.DefaultIfEmpty()
                    join surg in context.SurgeryInfoOtherDetails.Where(s => s.IsActive == 1)
                        on app.Id equals surg.AppointmentId into surgJoin
                    from surg in surgJoin.DefaultIfEmpty()
                    select new { app, aprc, aloc, clm, pay, chk, ptadv, surg };

        if(practiceIds!=null && practiceIds.Any())
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
        {
            query = query.Where(x => x.clm != null && x.clm.BillingTypeId == command.BillingTypeId);
        }

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
