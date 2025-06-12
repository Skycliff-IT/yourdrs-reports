using System.Linq;
using Yourdrs.CrossCutting.CQRS;
using Yourdrs.Reports.API.Extensions;

namespace Yourdrs.Reports.API.Features.Reports.GetAppointmentStatusCounts
{
    public class GetAppointmentStatusCountCommandHandler(ApplicationDbContext context) : ICommandHandler<GetAppointmentStatusCountCommand, List<AppointmentStatusCountResponse>>
    {
        public async Task<List<AppointmentStatusCountResponse>> Handle(GetAppointmentStatusCountCommand command, CancellationToken cancellationToken = default)
        {
            var practiceIds = CsvHelper.ParseCsvToIntList(command.PracticeIds);
            var locationIds = CsvHelper.ParseCsvToIntList(command.LocationIds);
            var providerIds = CsvHelper.ParseCsvToIntList(command.ProviderIds);
            var appointmentTypeIds = CsvHelper.ParseCsvToIntList(command.AppointmentTypeIds);
            var statusIds = CsvHelper.ParseCsvToIntList(command.StatusIds);
            var procedureIds = CsvHelper.ParseCsvToIntList(command.ProcedureIds);
            var procedureTypeIds = CsvHelper.ParseCsvToIntList(command.ProcedureTypeIds);
            DateTime? hStartDate = command.AppointmentStartDate.AddYears(-1);
            DateTime? hEndDate = command.AppointmentEndDate.AddYears(-1);

            var baseQuery = from app in context.Appointments
                            join eps in context.Episodes
                            on new { Id = app.EpisodeId, IsActive = (byte)1 } equals new { Id = eps.Id, IsActive = eps.IsActive }
                            where app.IsActive == 1 
                            select new { app, eps };

            IQueryable<AppointmentStatusJoinDto> query = baseQuery.Select(q => new AppointmentStatusJoinDto
            {
                app = q.app,
                eps = q.eps,
                aprc = null,
                aloc = null,
                clm = null,
                pay = null,
                chk = null,
                surg = null
            });

            if ((procedureIds != null && procedureIds.Any()) || (procedureTypeIds != null && procedureTypeIds.Any()))
            {
                query = from q in query
                        join surgRaw in context.SurgeryInfoOtherDetails on new { Id = q.app.Id,IsActive = (byte)1 } equals new { Id = surgRaw.AppointmentId, IsActive = surgRaw.IsActive } into surgJoin
                        from surg in surgJoin.DefaultIfEmpty()
                        select new AppointmentStatusJoinDto
                        {
                            app = q.app,
                            aprc = q.aprc,
                            aloc = q.aloc,
                            clm = q.clm,
                            pay = q.pay,
                            chk = q.chk,
                            surg = surg,
                            eps = q.eps
                        };

            }

            if (command.BillingTypeId.HasValue || command.PostedStartDate.HasValue || command.PostedEndDate.HasValue)
            {
                query = from q in query
                        join clmRaw in context.RcmClaims on new { Id = q.app.Id, IsActive = (byte?)1 } equals new { Id = clmRaw.AppointmentId, clmRaw.IsActive } into clmJoin
                        from clm in clmJoin.DefaultIfEmpty()

                        select new AppointmentStatusJoinDto
                        {
                            app = q.app,
                            aprc = q.aprc,
                            aloc = q.aloc,
                            clm = clm,
                            pay = q.pay,
                            chk = q.chk,
                            surg = q.surg,
                            eps = q.eps
                        };
            }

            if (command.PostedStartDate.HasValue || command.PostedEndDate.HasValue)
            {
                query = from q in query
                        join payRaw in context.RcmPayments on new { Id = q.clm.Id, IsActive = (byte)1 } equals new { Id = (uint)(payRaw.ClaimId ?? 0), payRaw.IsActive } into payJoin
                        from pay in payJoin.DefaultIfEmpty()
                        join chkRaw in context.RcmCheckDetails on new { Id = pay.CheckDetailsId, IsActive = (byte)1 } equals new { Id = (uint?)chkRaw.Id, chkRaw.IsActive } into chkJoin
                        from chk in chkJoin.DefaultIfEmpty()
                        select new AppointmentStatusJoinDto
                        {
                            app = q.app,
                            aprc = q.aprc,
                            aloc = q.aloc,
                            clm = q.clm,
                            pay = pay,
                            chk = chk,
                            surg = q.surg,
                            eps = q.eps
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

            query = query.Where(x => (x.app.StartDateTime >= command.AppointmentStartDate &&
                                        x.app.StartDateTime <= command.AppointmentEndDate) || 
                                        (x.app.StartDateTime >= hStartDate &&
                                        x.app.StartDateTime <= hEndDate));

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

            if (command.ArTypeId.HasValue)
            {
                if(command.ArTypeId.Value == 1)
                {
                    query = query.Where(x => x.app.AppointmentTypeId == 7);
                }
                else if(command.ArTypeId.Value == 2)
                {
                    query = query.Where(x => x.app.AppointmentTypeId != 7);
                }
            }

           

            var result = await query
                 .GroupBy(x => 1) 
                 .Select(g => new AppointmentStatusCountResponse(
                     "Requested",
                     new List<int>{0},
                     g.Count(x => x.app.StatusId != 5 && x.app.StartDateTime >= command.AppointmentStartDate && x.app.StartDateTime <= command.AppointmentEndDate),
                     g.Count(x => x.app.StatusId != 5 && x.app.StartDateTime >= hStartDate && x.app.StartDateTime <= hEndDate),

                     "Cancelled",
                     new List<int> { 8127 },
                     g.Count(x => new ushort?[] { 18, 127 }.Contains(x.app.StatusId) && x.app.StartDateTime >= command.AppointmentStartDate && x.app.StartDateTime <= command.AppointmentEndDate),
                     g.Count(x => new ushort?[] { 18, 127 }.Contains(x.app.StatusId) && x.app.StartDateTime >= hStartDate && x.app.StartDateTime <= hEndDate),

                     "Checked Out",
                      new List<int> { 55, 54, 28, 9, 111, 86, 88, 87, 92, 91, 93, 94, 95, 90, 96, 89, 39 },
                     g.Count(app => new ushort?[] { 55, 54, 28, 9, 111, 86, 88, 87, 92, 91, 93, 94, 95, 90, 96, 89, 39 }.Contains(app.app.StatusId) && app.app.StartDateTime >= command.AppointmentStartDate && app.app.StartDateTime <= command.AppointmentEndDate),
                     g.Count(app => new ushort?[] { 55, 54, 28, 9, 111, 86, 88, 87, 92, 91, 93, 94, 95, 90, 96, 89, 39 }.Contains(app.app.StatusId) && app.app.StartDateTime >= hStartDate && app.app.StartDateTime <= hEndDate),

                     "Ready To Go",
                      new List<int> { 128, 100, 104, 114, 108, 38, 45, 110, 46, 109, 49, 50, 51, 53, 129, 17 },
                     g.Count(app => new ushort?[] { 128, 100, 104, 114, 108, 38, 45, 110, 46, 109, 49, 50, 51, 53, 129, 17 }.Contains(app.app.StatusId) && app.app.StartDateTime >= command.AppointmentStartDate && app.app.StartDateTime <= command.AppointmentEndDate),
                     g.Count(app => new ushort?[] { 128, 100, 104, 114, 108, 38, 45, 110, 46, 109, 49, 50, 51, 53, 129, 17 }.Contains(app.app.StatusId) && app.app.StartDateTime >= hStartDate && app.app.StartDateTime <= hEndDate)
                 ))
                 .ToListAsync();

            return result;
        }
    }
}
