//using Yourdrs.CrossCutting.CQRS;
//using Yourdrs.Reports.API.Extensions;

//namespace Yourdrs.Reports.API.Features.Reports.GetOfficeAppointmentCounts
//{
//    public class GetOfficeAppointmentCountsCommandHandler : ICommandHandler<GetOfficeAppointmentCountsRequest, List<OfficeAppointmentCountDto>>
//    {
//        private readonly ApplicationDbContext _context;

//        public GetOfficeAppointmentCountsCommandHandler(ApplicationDbContext context)
//        {
//            _context = context;
//        }

//        public async Task<List<OfficeAppointmentCountDto>> Handle(GetOfficeAppointmentCountsRequest request, CancellationToken cancellationToken = default)
//        {
//            // Parse the CSV strings into integer lists
//            var practiceIds = CsvHelper.ParseCsvToIntList(request.PracticeIds);
//            var locationIds = CsvHelper.ParseCsvToIntList(request.LocationIds);
//            var providerIds = CsvHelper.ParseCsvToIntList(request.ProviderIds);
//            var appointmentTypeIds = CsvHelper.ParseCsvToIntList(request.AppointmentTypeIds);
//            var statusIds = CsvHelper.ParseCsvToIntList(request.StatusIds);
//            var procedureIds = CsvHelper.ParseCsvToIntList(request.ProcedureIds);
//            var procedureTypeIds = CsvHelper.ParseCsvToIntList(request.ProcedureTypeIds);

//            // Define the base query for appointments, joining appointmenttypes, legalrepresentatives, and other related entities
//            var baseQuery = from app in _context.Appointments
//                            //join aty in _context.AppointmentTypes on app.AppointmentTypeId equals aty.Id into atyJoin
//                            //from aty in atyJoin.DefaultIfEmpty()  // Joining appointment types
//                            //join elr in _context.EpisodeLegalRepresentatives on app.EpisodeId equals elr.EpisodeId into elrJoin
//                            //from elr in elrJoin.DefaultIfEmpty()  // Joining legal representatives
//                            //join pom in _context.PartnerOrganizationMembers on elr.PartnerOrganizationMemberId equals pom.Id into pomJoin
//                            //from pom in pomJoin.DefaultIfEmpty()  // Joining partner organization members
//                            //join pol in _context.PartnerOrgLocationMapping on pom.PartnerOrgLocationMappingId equals pol.Id into polJoin
//                            //from pol in polJoin.DefaultIfEmpty()  // Joining partner org location mapping
//                            //join porg in _context.PartnerOrganizations on pol.PartnerOrganizationId equals porg.Id into porgJoin
//                            //from porg in porgJoin.DefaultIfEmpty()  // Joining partner organizations
//                            where app.IsActive == 1
//                            select new { app, 
//                                 };
//            //aty, elr, pom, pol, porg
//            IQueryable<OfficeAppointmentCountDto> query = baseQuery
//                .Select(q => new OfficeAppointmentCountDto
//                {
//                    app = q.app,
//                    //aloc = q.pol,  // Assuming PartnerOrgLocationMapping represents location
//                    //clm = null,  // Initialize as null; will join later if needed
//                    //pay = null,  // Initialize as null; will join later if needed
//                    //chk = null,  // Initialize as null; will join later if needed
//                    //surg = null,  // Initialize as null; will join later if needed
//                    //pom = q.pom,  // Partner organization member details
//                    //pol = q.pol,  // Partner org location mapping details
//                    //porg = q.porg,  // Partner organization details
//                    //elr = q.elr,  // Episode legal representative details
//                    //GroupName = q.aty.Name,  // Group name from appointment types
//                    //GroupDescription = q.aty.Description,  // Description from appointment types
//                    //LegalRepresentative = q.elr != null ? $"{q.elr.FirstName} {q.elr.LastName}" : "N/A",  // Legal representative name
//                    //OrganizationName = q.porg?.PartnerOrganizationName,  // Organization name
//                    //AppointmentTypeIds = string.Join(",", new[] { q.aty.Id.ToString() }),  // Appointment type IDs
//                    //AppointmentCount = 0,  // This will be calculated later
//                    //PreviousYearAppointmentCount = 0  // This will be calculated later
//                });

//            // Join RcmClaims if BillingTypeId or PostedDate is passed
//            if (request.BillingTypeId != null || request.PostedStartDate.HasValue || request.PostedEndDate.HasValue)
//            {
//                query = from q in query
//                        join clmRaw in _context.RcmClaims on q.app.Id equals clmRaw.AppointmentId into clmJoin
//                        from clm in clmJoin.DefaultIfEmpty()
//                        select new OfficeAppointmentCountDto
//                        {
//                            app = q.app,
//                            aloc = q.aloc,
//                            clm = clm,
//                            pay = q.pay,
//                            chk = q.chk,
//                            surg = q.surg,
//                            //pom = q.pom,
//                            //pol = q.pol,
//                            //porg = q.porg,
//                            //elr = q.elr,
//                            //GroupName = q.GroupName,
//                            //GroupDescription = q.GroupDescription,
//                            //LegalRepresentative = q.LegalRepresentative,
//                            //OrganizationName = q.OrganizationName,
//                            //AppointmentTypeIds = q.AppointmentTypeIds,
//                            //AppointmentCount = q.AppointmentCount,
//                            //PreviousYearAppointmentCount = q.PreviousYearAppointmentCount
//                        };
//            }

//            // Join Payments and Surgery Info if applicable
//            if ((procedureIds != null && procedureIds.Any()) || (procedureTypeIds != null && procedureTypeIds.Any()))
//            {
//                query = from q in query
//                        join surgRaw in _context.SurgeryInfoOtherDetails on q.app.Id equals surgRaw.AppointmentId into surgJoin
//                        from surg in surgJoin.DefaultIfEmpty()
//                        select new OfficeAppointmentCountDto
//                        {
//                            app = q.app,
//                            aloc = q.aloc,
//                            clm = q.clm,
//                            pay = q.pay,
//                            chk = q.chk,
//                            surg = surg,
//                            //pom = q.pom,
//                            //pol = q.pol,
//                            //porg = q.porg,
//                            //elr = q.elr,
//                            //GroupName = q.GroupName,
//                            //GroupDescription = q.GroupDescription,
//                            //LegalRepresentative = q.LegalRepresentative,
//                            //OrganizationName = q.OrganizationName,
//                            //AppointmentTypeIds = q.AppointmentTypeIds,
//                            //AppointmentCount = q.AppointmentCount,
//                            //PreviousYearAppointmentCount = q.PreviousYearAppointmentCount
//                        };
//            }

//            // Apply filters based on command parameters
//            //if (practiceIds != null && practiceIds.Any())
//            //    query = query.Where(x => practiceIds.Contains(x.app.PracticeId));

//            //if (locationIds != null && locationIds.Any())
//            //    query = query.Where(x => locationIds.Contains(x.app.LocationId));

//            //if (providerIds != null && providerIds.Any())
//            //    query = query.Where(x => providerIds.Contains(x.app.ProviderId));

//            //if (appointmentTypeIds != null && appointmentTypeIds.Any())
//            //    query = query.Where(x => appointmentTypeIds.Contains(x.app.AppointmentTypeId));

//            //if (statusIds != null && statusIds.Any())
//            //    query = query.Where(x => statusIds.Contains(x.app.StatusId));

//            //if (procedureIds != null && procedureIds.Any())
//            //    query = query.Where(x => procedureIds.Contains(x.surg.ProcedureId));

//            if (procedureTypeIds != null && procedureTypeIds.Any())
//                query = query.Where(x => procedureTypeIds.Contains(x.surg.ProcedureTypeId));

//            // Handle appointment date filters
//            if (request.AppointmentStartDate.HasValue && request.AppointmentEndDate.HasValue)
//            {
//                query = query.Where(x => x.app.StartDateTime >= request.AppointmentStartDate &&
//                                         x.app.StartDateTime <= request.AppointmentEndDate);
//            }
//            else if (request.AppointmentStartDate.HasValue)
//            {
//                query = query.Where(x => x.app.StartDateTime >= request.AppointmentStartDate);
//            }
//            else if (request.AppointmentEndDate.HasValue)
//            {
//                query = query.Where(x => x.app.StartDateTime <= request.AppointmentEndDate);
//            }

//            // Calculate previous year counts by adjusting the dates
//            var previousYearStart = request.AppointmentStartDate?.AddYears(-1);
//            var previousYearEnd = request.AppointmentEndDate?.AddYears(-1);


//            // Group by practice and location, and count the appointments
//            //var result = await query
//            //    .GroupBy(x => new { x.app.PracticeId, x.app.LocationId })
//            //    .Select(g => new OfficeAppointmentCountResponse
//            //    {
//            //        Grp = g.First().PracticeId,
//            //        GrpName = g.First().GroupDescription,
//            //        TypeIds = string.Join(",", g.Select(x => x.AppointmentTypeIds).Distinct()),
//            //        Cnt = g.Select(x => x.app.Id).Distinct().Count()  // Adjust for previous year logic
//            //    })
//            //    .ToListAsync();

//            //return result.OrderBy(x => x.AppointmentCount).ToList();
//        }
//    }
//}
