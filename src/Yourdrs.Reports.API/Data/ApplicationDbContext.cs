using Yourdrs.Reports.API.Features.Reports.GetOfficeAppointmentCounts;
using Yourdrs.Reports.API.Features.Reports.GetPracticeCounts;

namespace Yourdrs.Reports.API.Data;

public partial class ApplicationDbContext : DbContext
{
    public ApplicationDbContext()
    {
    }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    //public DbSet<PracticeCountResponse> PracticeCountResponses { get; set; }
    public DbSet<OfficeAppointmentCountDto> OfficeAppointmentCounts { get; set; } // Optional if using Set<T>()

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Appointment> Appointments { get; set; }

    public virtual DbSet<Location> Locations { get; set; }

    public virtual DbSet<PatientAdvocate> PatientAdvocates { get; set; }

    public virtual DbSet<Practice> Practices { get; set; }

    public virtual DbSet<RcmCheckDetail> RcmCheckDetails { get; set; }

    public virtual DbSet<RcmClaim> RcmClaims { get; set; }

    public virtual DbSet<RcmPayment> RcmPayments { get; set; }

    public virtual DbSet<SurgeryInfoOtherDetail> SurgeryInfoOtherDetails { get; set; }

    public virtual DbSet<AppointmentType> AppointmentTypes { get; set; }

    public virtual DbSet<EpisodeLegalRepresentative> EpisodeLegalRepresentatives { get; set; }

    public virtual DbSet<PartnerMember> PartnerMembers { get; set; }

    public virtual DbSet<PartnerOrganization> PartnerOrganizations { get; set; }

    public virtual DbSet<PartnerOrganizationMember> PartnerOrganizationMembers { get; set; }

    public virtual DbSet<PartnerOrgLocationMapping> PartnerOrgLocationMappings { get; set; }

    public virtual DbSet<Episode> Episodes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseMySql("server=yd-v2-rpt-dev.cluster-custom-csrinnf2oqvq.ap-south-1.rds.amazonaws.com;user=ydrptadm;password=Skyrptadm#1;database=ydv2devhemaheascnew", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.32-mysql"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_unicode_ci")
            .HasCharSet("utf8mb4");

        //modelBuilder.Entity<PracticeCountResponse>().HasNoKey();

        // modelBuilder.Entity<PracticeCountResponse>().ToView(null);

        modelBuilder.Entity<OfficeAppointmentCountDto>().HasNoKey();

        modelBuilder.Entity<Appointment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("appointments");

            entity.HasIndex(e => e.AppointmentTypeId, "fk_appointments_apptype_idx");
            entity.HasIndex(e => e.AuthorizationStatusId, "fk_appointments_authstatus_idx");
            entity.HasIndex(e => e.CreatedBy, "fk_appointments_cmem_idx");
            entity.HasIndex(e => new
            {
                e.EpisodeId,
                e.ProviderId,
                e.PracticeId,
                e.LocationId,
                e.PosPracticeId,
                e.PosLocationId,
                e.AppointmentTypeId,
                e.StatusId,
                e.StartDateTime,
                e.IsActive,
                e.CreatedBy,
                e.CreatedDate,
                e.ModifiedBy,
                e.ModifiedDate
            }, "fk_appointments_custom");

            entity.HasIndex(e => e.EpisodeId, "fk_appointments_epid_idx");
            entity.HasIndex(e => e.LocationId, "fk_appointments_loc_idx");
            entity.HasIndex(e => e.ModifiedBy, "fk_appointments_mmem_idx");
            entity.HasIndex(e => e.PracticeId, "fk_appointments_prac_idx");
            entity.HasIndex(e => e.ProviderId, "fk_appointments_provid_idx");
            entity.HasIndex(e => e.StatusId, "fk_appointments_status_idx");
            entity.HasIndex(e => new { e.PracticeId, e.LocationId, e.StartDateTime, e.StatusId }, "idx_app_custom2");
            entity.HasIndex(e => new { e.PracticeId, e.LocationId }, "idx_appointments_practice_location");

            entity.Property(e => e.Id)
                .HasColumnType("mediumint unsigned")
                .HasColumnName("id");

            entity.Property(e => e.AppointmentTypeId).HasColumnName("appointmenttypeid");
            entity.Property(e => e.AuthorizationStatusId).HasColumnName("authorizationstatusid");
            entity.Property(e => e.CreatedBy)
                .HasColumnType("mediumint unsigned")
                .HasColumnName("createdby");

            entity.Property(e => e.CreatedDate)
                .HasColumnType("datetime")
                .HasColumnName("createddate");

            entity.Property(e => e.EndDateTime)
                .HasColumnType("datetime")
                .HasColumnName("enddatetime");

            entity.Property(e => e.EpisodeId)
                .HasColumnType("mediumint unsigned")
                .HasColumnName("episodeid");

            entity.Property(e => e.ExternalApptId)
                .HasMaxLength(10)
                .HasColumnName("externalapptid");

            entity.Property(e => e.IsActive)
                .HasDefaultValueSql("'1'")
                .HasColumnName("isactive");

            entity.Property(e => e.IsRescheduled).HasColumnName("isrescheduled");
            entity.Property(e => e.LocationId).HasColumnName("locationid");

            entity.Property(e => e.ModifiedBy)
                .HasColumnType("mediumint unsigned")
                .HasColumnName("modifiedby");

            entity.Property(e => e.ModifiedByPatient).HasColumnName("modifiedbypatient");

            entity.Property(e => e.ModifiedDate)
                .HasColumnType("datetime")
                .HasColumnName("modifieddate");

            entity.Property(e => e.PosLocationId).HasColumnName("poslocationid");
            entity.Property(e => e.PosPracticeId).HasColumnName("pospracticeid");
            entity.Property(e => e.PracticeId).HasColumnName("practiceid");

            entity.Property(e => e.PrevDbId)
                .HasColumnType("mediumint unsigned")
                .HasColumnName("prevdbid");

            entity.Property(e => e.ProviderId)
                .HasColumnType("mediumint unsigned")
                .HasColumnName("providerid");

            entity.Property(e => e.StartDateTime)
                .HasColumnType("datetime")
                .HasColumnName("startdatetime");

            entity.Property(e => e.StatusId).HasColumnName("statusid");

            entity.Property(e => e.StatusLastModifiedDate)
                .HasColumnType("datetime")
                .HasColumnName("statuslastmodifieddate");

            entity.Property(e => e.V1AppointmentId)
                .HasColumnType("mediumint unsigned")
                .HasColumnName("v1appointmentid");

            entity.Property(e => e.V2AppointmentId)
                .HasColumnType("mediumint unsigned")
                .HasColumnName("v2appointmentid");

            entity.HasOne(d => d.Location).WithMany(p => p.Appointments)
                .HasForeignKey(d => d.LocationId)
                .HasConstraintName("fk_appointments_loc");

            entity.HasOne(d => d.Practice).WithMany(p => p.Appointments)
                .HasForeignKey(d => d.PracticeId)
                .HasConstraintName("fk_appointments_prac");
        });


        modelBuilder.Entity<Location>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("locations");

            entity.HasIndex(e => new { e.LocationName, e.ShortName }, "fti_locations_name")
                .HasAnnotation("MySql:FullTextIndex", true);

            entity.HasIndex(e => new { e.Id, e.IsActive }, "idx_locations");

            entity.HasIndex(e => new { e.Id, e.LocationName, e.IsActive }, "idx_locations_name");

            entity.Property(e => e.Id)
                .HasColumnName("id");

            entity.Property(e => e.IsActive)
                .HasDefaultValueSql("'1'")
                .HasColumnName("isactive");

            entity.Property(e => e.LocationName)
                .HasMaxLength(150)
                .HasColumnName("locationname");

            entity.Property(e => e.ModifiedBy)
                .HasColumnType("mediumint unsigned")
                .HasColumnName("modifiedby");

            entity.Property(e => e.ModifiedDate)
                .HasColumnType("datetime")
                .HasColumnName("modifieddate");

            entity.Property(e => e.ShortName)
                .HasMaxLength(150)
                .HasColumnName("shortname");
        });


        modelBuilder.Entity<PatientAdvocate>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("patientadvocates");

            entity.HasIndex(e => e.EpisodeId, "fk_patientadvocates_epid_idx");
            entity.HasIndex(e => e.MemberId, "fk_patientadvocates_memid_idx");

            entity.Property(e => e.Id)
                .HasColumnType("mediumint unsigned")
                .HasColumnName("id");

            entity.Property(e => e.EndDate)
                .HasColumnName("enddate");

            entity.Property(e => e.EpisodeId)
                .HasColumnType("mediumint unsigned")
                .HasColumnName("episodeid");

            entity.Property(e => e.IsActive)
                .HasDefaultValueSql("'1'")
                .HasColumnName("isactive");

            entity.Property(e => e.MemberId)
                .HasColumnType("mediumint unsigned")
                .HasColumnName("memberid");

            entity.Property(e => e.ModifiedBy)
                .HasColumnType("mediumint unsigned")
                .HasColumnName("modifiedby");

            entity.Property(e => e.ModifiedDate)
                .HasColumnType("datetime")
                .HasColumnName("modifieddate");

            entity.Property(e => e.PrevDbId)
                .HasColumnType("mediumint unsigned")
                .HasColumnName("prevdbid");

            entity.Property(e => e.StartDate)
                .HasColumnType("datetime")
                .HasColumnName("startdate");
        });


        modelBuilder.Entity<Practice>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("practices");

            entity.HasIndex(e => e.PracticeName, "fti_practice_name")
                .HasAnnotation("MySql:FullTextIndex", true);

            entity.HasIndex(e => new { e.PracticeName, e.ShortName }, "fti_practices_name")
                .HasAnnotation("MySql:FullTextIndex", true);

            entity.HasIndex(e => new { e.Id, e.IsExternal, e.IsActive }, "idx_practices");

            entity.HasIndex(e => new { e.Id, e.PracticeName, e.IsExternal, e.IsActive }, "idx_practices_name");

            entity.Property(e => e.Id)
                .HasColumnName("id");

            entity.Property(e => e.IsActive)
                .HasDefaultValueSql("'1'")
                .HasColumnName("isactive");

            entity.Property(e => e.IsExternal)
                .HasColumnName("isexternal");

            entity.Property(e => e.IsPrimaryPractice)
                .HasColumnName("isprimarypractice");

            entity.Property(e => e.ModifiedBy)
                .HasColumnType("mediumint unsigned")
                .HasColumnName("modifiedby");

            entity.Property(e => e.ModifiedDate)
                .HasColumnType("datetime")
                .HasColumnName("modifieddate");

            entity.Property(e => e.PracticeName)
                .HasMaxLength(70)
                .HasColumnName("practicename");

            entity.Property(e => e.ShortName)
                .HasMaxLength(70)
                .HasColumnName("shortname");
        });


        modelBuilder.Entity<RcmCheckDetail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("rcmcheckdetails");

            entity.HasIndex(e => e.AppointmentId, "fk_rcmckdtl_appointmentid_idx");
            entity.HasIndex(e => e.CreatedBy, "fk_rcmckdtl_createdby_idx");
            entity.HasIndex(e => e.ModifiedBy, "fk_rcmckdtl_modifiedby_idx");
            entity.HasIndex(e => e.PaymentModeId, "fk_rcmckdtl_paymentmodeid_idx");
            entity.HasIndex(e => e.PaymentTypeId, "fk_rcmckdtl_paymenttypeid_idx");
            entity.HasIndex(e => e.PayorId, "fk_rcmckdtl_payorid_idx");
            entity.HasIndex(e => e.PostingTypeId, "fk_rcmckdtl_postingtypeid_idx");

            entity.Property(e => e.Id)
                .HasColumnType("mediumint unsigned")
                .HasColumnName("id");

            entity.Property(e => e.AppointmentId)
                .HasColumnType("mediumint unsigned")
                .HasColumnName("appointmentid");

            entity.Property(e => e.AttorneyId)
                .HasColumnType("mediumint")
                .HasColumnName("attorneyid");

            entity.Property(e => e.BankId)
                .HasColumnType("mediumint unsigned")
                .HasColumnName("bankid");

            entity.Property(e => e.CheckAmount)
                .HasDefaultValueSql("'0'")
                .HasColumnName("checkamount");

            entity.Property(e => e.CheckDate)
                .HasColumnType("datetime")
                .HasColumnName("checkdate");

            entity.Property(e => e.CheckNumber)
                .HasMaxLength(100)
                .HasColumnName("checknumber");

            entity.Property(e => e.CreatedBy)
                .HasColumnType("mediumint unsigned")
                .HasColumnName("createdby");

            entity.Property(e => e.CreatedDate)
                .HasColumnType("datetime")
                .HasColumnName("createddate");

            entity.Property(e => e.DepositDate)
                .HasColumnType("datetime")
                .HasColumnName("depositdate");

            entity.Property(e => e.DepositDateReason)
                .HasMaxLength(500)
                .HasColumnName("depositdatereason");

            entity.Property(e => e.IsActive)
                .HasDefaultValueSql("'1'")
                .HasColumnName("isactive");

            entity.Property(e => e.ModifiedBy)
                .HasColumnType("mediumint unsigned")
                .HasColumnName("modifiedby");

            entity.Property(e => e.ModifiedDate)
                .HasColumnType("datetime")
                .HasColumnName("modifieddate");

            entity.Property(e => e.PaymentModeId)
                .HasColumnName("paymentmodeid");

            entity.Property(e => e.PaymentSubTypeId)
                .HasColumnName("paymentsubtypeid");

            entity.Property(e => e.PaymentTypeId)
                .HasColumnName("paymenttypeid");

            entity.Property(e => e.PayorId)
                .HasColumnType("mediumint unsigned")
                .HasColumnName("payorid");

            entity.Property(e => e.PayToPracticeId)
                .HasColumnName("paytopracticeid");

            entity.Property(e => e.PostedDate)
                .HasColumnType("datetime")
                .HasColumnName("posteddate");

            entity.Property(e => e.PostingTypeId)
                .HasColumnName("postingtypeid");

            entity.Property(e => e.ReceivedDate)
                .HasColumnType("datetime")
                .HasColumnName("receiveddate");

            entity.Property(e => e.RejectReason)
                .HasMaxLength(1001)
                .HasColumnName("rejectreason");

            entity.Property(e => e.StatusId)
                .HasColumnName("statusid");

            entity.Property(e => e.SupervisorReason)
                .HasMaxLength(1000)
                .HasColumnName("supervisorreason");

            entity.Property(e => e.WaitingForSupervisorStatusId)
                .HasColumnName("waitingforsupervisorstatusid");

            entity.HasOne(d => d.Appointment)
                .WithMany(p => p.RcmCheckDetails)
                .HasForeignKey(d => d.AppointmentId)
                .HasConstraintName("fk_rcmckdtl_appointmentid");
        });


        modelBuilder.Entity<RcmClaim>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("rcmclaim");

            entity.HasIndex(e => e.AppointmentId, "fk_rcmcam_appointmentid_idx");
            entity.HasIndex(e => e.AttorneyId, "fk_rcmcam_attorneyid_idx");
            entity.HasIndex(e => e.BillingModeId, "fk_rcmcam_billingmodeid_idx");
            entity.HasIndex(e => e.BillingTypeId, "fk_rcmcam_billingtypeid_idx");
            entity.HasIndex(e => e.CreatedBy, "fk_rcmcam_createdby_idx");
            entity.HasIndex(e => e.FormId, "fk_rcmcam_formid_idx");
            entity.HasIndex(e => e.LocationId, "fk_rcmcam_locationid_idx");
            entity.HasIndex(e => e.ModifiedBy, "fk_rcmcam_modifiedby_idx");
            entity.HasIndex(e => e.PatientPayorId, "fk_rcmcam_patientpayorid_idx");
            entity.HasIndex(e => e.PosId, "fk_rcmcam_posid_idx");
            entity.HasIndex(e => e.PracticeId, "fk_rcmcam_practiceid_idx");
            entity.HasIndex(e => new { e.PracticeId, e.LocationId }, "fk_rcmcam_prloc_id");
            entity.HasIndex(e => e.ProviderId, "fk_rcmcam_providerid_idx");
            entity.HasIndex(e => e.StatusId, "fk_rcmcam_statusid_idx");
            entity.HasIndex(e => e.TosId, "fk_rcmcam_tosid_idx");
            entity.HasIndex(e => e.ClaimTypeId, "fk_rcmrcmclaim_clalaimtyes_idx");

            entity.Property(e => e.Id)
                .HasColumnType("mediumint unsigned")
                .HasColumnName("id");

            entity.Property(e => e.AcceptsAssignment)
                .HasDefaultValueSql("'1'")
                .HasColumnName("acceptsassignment");

            entity.Property(e => e.AppointmentId)
                .HasColumnType("mediumint unsigned")
                .HasColumnName("appointmentid");

            entity.Property(e => e.AttorneyId)
                .HasColumnType("mediumint unsigned")
                .HasColumnName("attorneyid");

            entity.Property(e => e.BilledDate)
                .HasColumnType("datetime")
                .HasColumnName("billeddate");

            entity.Property(e => e.BillingModeId)
                .HasColumnName("billingmodeid");

            entity.Property(e => e.BillingTypeId)
                .HasColumnName("billingtypeid");

            entity.Property(e => e.ClaimNumber)
                .HasMaxLength(20)
                .HasColumnName("claimnumber");

            entity.Property(e => e.ClaimTypeId)
                .HasColumnName("claimtypeid");

            entity.Property(e => e.CreatedBy)
                .HasColumnType("mediumint unsigned")
                .HasColumnName("createdby");

            entity.Property(e => e.CreatedDate)
                .HasColumnType("datetime")
                .HasColumnName("createddate");

            entity.Property(e => e.FileName)
                .HasMaxLength(200)
                .HasColumnName("filename");

            entity.Property(e => e.FormId)
                .HasColumnName("formid");

            entity.Property(e => e.IsActive)
                .HasDefaultValueSql("'1'")
                .HasColumnName("isactive");

            entity.Property(e => e.LocationId)
                .HasColumnName("locationid");

            entity.Property(e => e.ModifiedBy)
                .HasColumnType("mediumint unsigned")
                .HasColumnName("modifiedby");

            entity.Property(e => e.ModifiedDate)
                .HasColumnType("datetime")
                .HasColumnName("modifieddate");

            entity.Property(e => e.PatientPayorId)
                .HasColumnType("mediumint unsigned")
                .HasColumnName("patientpayorid");

            entity.Property(e => e.PosId)
                .HasColumnName("posid");

            entity.Property(e => e.PracticeId)
                .HasColumnName("practiceid");

            entity.Property(e => e.ProviderId)
                .HasColumnType("mediumint unsigned")
                .HasColumnName("providerid");

            entity.Property(e => e.SentDate)
                .HasColumnType("datetime")
                .HasColumnName("sentdate");

            entity.Property(e => e.StatusId)
                .HasColumnName("statusid");

            entity.Property(e => e.TosId)
                .HasColumnName("tosid");

            entity.HasOne(d => d.Appointment).WithMany(p => p.RcmClaims)
                .HasForeignKey(d => d.AppointmentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_rcmcam_appointmentid");

            entity.HasOne(d => d.Location).WithMany(p => p.RcmClaims)
                .HasForeignKey(d => d.LocationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_rcmcam_locationid");

            entity.HasOne(d => d.Practice).WithMany(p => p.RcmClaims)
                .HasForeignKey(d => d.PracticeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_rcmcam_practiceid");
        });


        modelBuilder.Entity<RcmPayment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("rcmpayments");

            entity.HasIndex(e => e.AppointmentId, "fk_rcmpaymt_appointmentid_idx");
            entity.HasIndex(e => e.CheckDetailsId, "fk_rcmpaymt_checkdetailsid_idx");
            entity.HasIndex(e => e.ClaimId, "fk_rcmpaymt_claimid_idx");
            entity.HasIndex(e => e.CreatedBy, "fk_rcmpaymt_createdby_idx");
            entity.HasIndex(e => e.EpisodeId, "fk_rcmpaymt_episodeid_idx");
            entity.HasIndex(e => e.PaymentModeId, "fk_rcmpaymt_paymentmodeid_idx");
            entity.HasIndex(e => e.PayorId, "fk_rcmpaymt_payorid_idx");
            entity.HasIndex(e => e.PracticeLocationBanksId, "fk_rcmpaymt_practicelocationbanksid_idx");
            entity.HasIndex(e => e.PaymentTypeId, "kf_rcmpaymt_paymenttypeid_idx");

            entity.Property(e => e.Id)
                .HasColumnType("mediumint unsigned")
                .HasColumnName("id");

            entity.Property(e => e.ActualCreatedDate)
                .HasColumnType("datetime")
                .HasColumnName("actualcreateddate");

            entity.Property(e => e.AdditionalAmount)
                .HasDefaultValueSql("'0'")
                .HasColumnName("additionalamount");

            entity.Property(e => e.Amount)
                .HasDefaultValueSql("'0'")
                .HasColumnName("amount");

            entity.Property(e => e.AppointmentId)
                .HasColumnType("mediumint unsigned")
                .HasColumnName("appointmentid");

            entity.Property(e => e.BalanceAmount)
                .HasDefaultValueSql("'0'")
                .HasColumnName("balanceamount");

            entity.Property(e => e.BillToPatient)
                .HasDefaultValueSql("'0'")
                .HasColumnName("billtopatient");

            entity.Property(e => e.BillToSecondary)
                .HasDefaultValueSql("'0'")
                .HasColumnName("billtosecondary");

            entity.Property(e => e.CheckDetailsId)
                .HasColumnType("mediumint unsigned")
                .HasColumnName("checkdetailsid");

            entity.Property(e => e.ClaimId)
                .HasColumnType("mediumint unsigned")
                .HasColumnName("claimid");

            entity.Property(e => e.Comments)
                .HasMaxLength(1000)
                .HasColumnName("comments");

            entity.Property(e => e.CreatedBy)
                .HasColumnType("mediumint unsigned")
                .HasColumnName("createdby");

            entity.Property(e => e.CreatedDate)
                .HasColumnType("datetime")
                .HasColumnName("createddate");

            entity.Property(e => e.DepositDateReason)
                .HasMaxLength(500)
                .HasColumnName("depositdatereason");

            entity.Property(e => e.DepositedDate)
                .HasColumnType("datetime")
                .HasColumnName("depositeddate");

            entity.Property(e => e.EpiPercentage)
                .HasDefaultValueSql("'0'")
                .HasColumnName("epipercentage");

            entity.Property(e => e.EpisodeId)
                .HasColumnType("mediumint unsigned")
                .HasColumnName("episodeid");

            entity.Property(e => e.IsActive)
                .HasDefaultValueSql("'1'")
                .HasColumnName("isactive");

            entity.Property(e => e.IsEclaimPosting)
                .HasColumnName("iseclaimposting");

            entity.Property(e => e.ModifiedBy)
                .HasColumnType("mediumint unsigned")
                .HasColumnName("modifiedby");

            entity.Property(e => e.ModifiedDate)
                .HasColumnType("datetime")
                .HasColumnName("modifieddate");

            entity.Property(e => e.OtherPayments)
                .HasDefaultValueSql("'0'")
                .HasColumnName("otherpayments");

            entity.Property(e => e.PaidByInsurance)
                .HasDefaultValueSql("'0'")
                .HasColumnName("paidbyinsurance");

            entity.Property(e => e.PaidByPatient)
                .HasDefaultValueSql("'0'")
                .HasColumnName("paidbypatient");

            entity.Property(e => e.ParentId)
                .HasColumnType("mediumint unsigned")
                .HasColumnName("parentid");

            entity.Property(e => e.PaymentModeId)
                .HasColumnName("paymentmodeid");

            entity.Property(e => e.PaymentTypeId)
                .HasColumnName("paymenttypeid");

            entity.Property(e => e.PayorId)
                .HasColumnType("mediumint unsigned")
                .HasColumnName("payorid");

            entity.Property(e => e.Percentage)
                .HasDefaultValueSql("'0'")
                .HasColumnName("percentage");

            entity.Property(e => e.PracticeLocationBanksId)
                .HasColumnType("mediumint unsigned")
                .HasColumnName("practicelocationbanksid");

            entity.Property(e => e.StatusId)
                .HasColumnName("statusid");

            entity.Property(e => e.Transferred)
                .HasColumnName("transferred");

            entity.Property(e => e.WriteOffAmount)
                .HasDefaultValueSql("'0'")
                .HasColumnName("writeoffamount");

            entity.HasOne(d => d.Appointment).WithMany(p => p.RcmPayments)
                .HasForeignKey(d => d.AppointmentId)
                .HasConstraintName("fk_rcmpaymt_appointmentid");

            entity.HasOne(d => d.CheckDetails).WithMany(p => p.RcmPayments)
                .HasForeignKey(d => d.CheckDetailsId)
                .HasConstraintName("fk_rcmpaymt_checkdetailsid");

            entity.HasOne(d => d.Claim).WithMany(p => p.RcmPayments)
                .HasForeignKey(d => d.ClaimId)
                .HasConstraintName("fk_rcmpaymt_claimid");
        });


        modelBuilder.Entity<SurgeryInfoOtherDetail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("surgeryinfootherdetails");

            entity.HasIndex(e => e.AppointmentId, "fk_surgeryinfootherdetails_appointmentid_idx");
            entity.HasIndex(e => e.CreatedBy, "fk_surgeryinfootherdetails_createdby_idx");
            entity.HasIndex(e => e.ModifiedBy, "fk_surgeryinfootherdetails_modifiedby_idx");
            entity.HasIndex(e => e.ProcedureId, "fk_surgeryinfootherdetails_procedureid_idx");
            entity.HasIndex(e => e.ProcedureTypeId, "fk_surgeryinfootherdetails_proceduretypeid_idx");

            entity.HasIndex(e => new {
                e.AppointmentId,
                e.ProcedureId,
                e.ProcedureTypeId,
                e.Grade,
                e.IsHardDate,
                e.IsSurgeryUrgent,
                e.IsArchive,
                e.IsMedicalClearanceRequired,
                e.IsMedicalClearanceCompleted,
                e.IsPostOpCall,
                e.CoordinatorId
            }, "idx_surgeryinfootherdetails_custom1");

            entity.Property(e => e.Id)
                .HasColumnType("mediumint unsigned")
                .HasColumnName("id");

            entity.Property(e => e.AppointmentId)
                .HasColumnType("mediumint unsigned")
                .HasColumnName("appointmentid");

            entity.Property(e => e.CoordinatorId)
                .HasColumnType("mediumint unsigned")
                .HasColumnName("coordinatorid");

            entity.Property(e => e.CreatedBy)
                .HasColumnType("mediumint unsigned")
                .HasColumnName("createdby");

            entity.Property(e => e.CreatedDate)
                .HasColumnType("datetime")
                .HasColumnName("createddate");

            entity.Property(e => e.DiagnosisCodes)
                .HasColumnType("json")
                .HasColumnName("diagnosiscodes");

            entity.Property(e => e.Grade)
                .HasMaxLength(5)
                .HasColumnName("grade");

            entity.Property(e => e.IsActive)
                .HasDefaultValueSql("'1'")
                .HasColumnName("isactive");

            entity.Property(e => e.IsArchive)
                .HasColumnName("isarchive");

            entity.Property(e => e.IsHardDate)
                .HasColumnName("isharddate");

            entity.Property(e => e.IsMedicalClearanceCompleted)
                .HasColumnName("ismedicalclearancecompleted");

            entity.Property(e => e.IsMedicalClearanceRequired)
                .HasColumnName("ismedicalclearancerequired");

            entity.Property(e => e.IsPostOpCall)
                .HasColumnName("ispostopcall");

            entity.Property(e => e.IsSurgeryUrgent)
                .HasColumnName("issurgeryurgent");

            entity.Property(e => e.ModifiedBy)
                .HasColumnType("mediumint unsigned")
                .HasColumnName("modifiedby");

            entity.Property(e => e.ModifiedDate)
                .HasColumnType("datetime")
                .HasColumnName("modifieddate");

            entity.Property(e => e.OriginalConfirmedDate)
                .HasColumnType("datetime")
                .HasColumnName("originalconfirmeddate");

            entity.Property(e => e.OtherDataJson)
                .HasComment("[{Notes,Height,Weight,HeightUnit,BodyPartId,BodyPartSideId,ReasonForReject,HardDateComments,DischargeDate,CancelledDateTime,CancelledBy,IsForceAuthorized,\nSurgeryLevelOneId,SurgeryLevelTwoId,PostOPCallBackDate,PostOPCallStatusId,ReasonForCancellation,ReasonForForceAuthorize,\nReasonForUnlockPostOPCall,SpecialSurgicalInstruction,ReasonForPredictedAmountChange,ReasonForReferringToSupervisor}]")
                .HasColumnType("json")
                .HasColumnName("otherdatajson");

            entity.Property(e => e.PredictedAmount)
                .HasColumnType("decimal(10,2) unsigned")
                .HasColumnName("predictedamount");

            entity.Property(e => e.ProcedureId)
                .HasColumnType("mediumint unsigned")
                .HasColumnName("procedureid");

            entity.Property(e => e.ProcedureTypeId)
                .HasColumnName("proceduretypeid");

            entity.Property(e => e.SecondaryProcedure)
                .HasColumnType("json")
                .HasColumnName("secondaryprocedure");

            entity.Property(e => e.SurgeryAssistant)
                .HasColumnType("json")
                .HasColumnName("surgeryassistant");

            entity.Property(e => e.WorkflowStatusId)
                .HasColumnName("workflowstatusid");

            entity.HasOne(d => d.Appointment).WithMany(p => p.SurgeryInfoOtherDetails)
                .HasForeignKey(d => d.AppointmentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_surgeryinfootherdetails_appointmentid");
        });
        modelBuilder.Entity<AppointmentType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("appointmenttypes");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("id");
            entity.Property(e => e.AppointmentTypeName)
                .HasMaxLength(50)
                .HasColumnName("appointmenttypename");
            entity.Property(e => e.AppointmentTypeShortName)
                .HasMaxLength(50)
                .HasColumnName("appointmenttypeshortname");
            entity.Property(e => e.DurationInMinutes)
                .HasDefaultValueSql("'15'")
                .HasColumnName("durationinminutes");
            entity.Property(e => e.GroupId).HasColumnName("groupid");
            entity.Property(e => e.IsActive)
                .HasDefaultValueSql("'1'")
                .HasColumnName("isactive");
            entity.Property(e => e.ModifieBby)
                .HasColumnType("mediumint unsigned")
                .HasColumnName("modifiedby");
            entity.Property(e => e.ModifiedDate)
                .HasColumnType("datetime")
                .HasColumnName("modifieddate");
        });

        modelBuilder.Entity<EpisodeLegalRepresentative>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("episodelegalrepresentatives");

            entity.HasIndex(e => e.EpisodeId, "fk_episodelegalrepresentatives_epid_idx");

            entity.HasIndex(e => e.PartnerOrganizationMemberId, "fk_episodelegalrepresentatives_pomid_idx");

            entity.Property(e => e.Id)
                .HasColumnType("mediumint unsigned")
                .HasColumnName("id");
            entity.Property(e => e.CreateBby)
                .HasColumnType("mediumint unsigned")
                .HasColumnName("createdby");
            entity.Property(e => e.CreatedDate)
                .HasColumnType("datetime")
                .HasColumnName("createddate");
            entity.Property(e => e.EpisodeId)
                .HasColumnType("mediumint unsigned")
                .HasColumnName("episodeid");
            entity.Property(e => e.IsActive)
                .HasDefaultValueSql("'1'")
                .HasColumnName("isactive");
            entity.Property(e => e.ModifiedBy)
                .HasColumnType("mediumint unsigned")
                .HasColumnName("modifiedby");
            entity.Property(e => e.ModifiedDate)
                .HasColumnType("datetime")
                .HasColumnName("modifieddate");
            entity.Property(e => e.PartnerOrganizationMemberId).HasColumnName("partnerorganizationmemberid");
            entity.Property(e => e.PrevDbId)
                .HasColumnType("mediumint unsigned")
                .HasColumnName("prevdbid");

            entity.HasOne(d => d.PartnerOrganizationMember).WithMany(p => p.EpisodeLegalRepresentatives)
                .HasForeignKey(d => d.PartnerOrganizationMemberId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_episodelegalrepresentatives_pomid");
        });

        modelBuilder.Entity<PartnerMember>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("partnermembers");

            entity.HasIndex(e => e.MemberId, "fk_partmem_mem_idx");

            entity.HasIndex(e => e.ModifiedBy, "fk_partmem_modmem_idx");

            entity.HasIndex(e => e.PrefixId, "fk_partmem_prefix_idx");

            entity.HasIndex(e => e.RoleId, "fk_partmem_role_idx");

            entity.HasIndex(e => e.SuffixId, "fk_partmem_suffix_idx");

            entity.HasIndex(e => e.LocationId, "fk_partnermembers_location_idx");

            entity.HasIndex(e => e.PracticeId, "fk_partnermembers_practice_idx");

            entity.HasIndex(e => new { e.FirstName, e.LastName }, "fti_partmem_fnln").HasAnnotation("MySql:FullTextIndex", true);

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.FirstName)
                .HasMaxLength(64)
                .HasColumnName("firstname");
            entity.Property(e => e.IsActive)
                .HasDefaultValueSql("'1'")
                .HasColumnName("isactive");
            entity.Property(e => e.IsArbitrationAttorney).HasColumnName("isarbitrationattorney");
            entity.Property(e => e.IsDefaultMember).HasColumnName("isdefaultmember");
            entity.Property(e => e.IsLitigationAttorney).HasColumnName("islitigationattorney");
            entity.Property(e => e.LastName)
                .HasMaxLength(64)
                .HasColumnName("lastname");
            entity.Property(e => e.LocationId).HasColumnName("locationid");
            entity.Property(e => e.MemberId)
                .HasColumnType("mediumint unsigned")
                .HasColumnName("memberid");
            entity.Property(e => e.MiddleName)
                .HasMaxLength(64)
                .HasColumnName("middlename");
            entity.Property(e => e.ModifiedBy)
                .HasColumnType("mediumint unsigned")
                .HasColumnName("modifiedby");
            entity.Property(e => e.ModifiedDate)
                .HasColumnType("datetime")
                .HasColumnName("modifieddate");
            entity.Property(e => e.PracticeId).HasColumnName("practiceid");
            entity.Property(e => e.PrefixId).HasColumnName("prefixid");
            entity.Property(e => e.RoleId).HasColumnName("roleid");
            entity.Property(e => e.SuffixId).HasColumnName("suffixid");
        });

        modelBuilder.Entity<PartnerOrganization>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("partnerorganizations");

            entity.HasIndex(e => new { e.PartnerOrganizationName, e.OrganizationTypeId, e.IsActive }, "fk_partorg_custom1");

            entity.HasIndex(e => e.ModifiedBy, "fk_partorg_modmem_idx");

            entity.HasIndex(e => e.OrganizationTypeId, "fk_partorg_orgtypes_idx");

            entity.HasIndex(e => e.PracticeId, "fk_partorg_pracid_idx");

            entity.HasIndex(e => e.PartnerOrganizationName, "fti_partnerorganizations").HasAnnotation("MySql:FullTextIndex", true);

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Comments)
                .HasMaxLength(200)
                .HasColumnName("comments");
            entity.Property(e => e.IsActive)
                .HasDefaultValueSql("'1'")
                .HasColumnName("isactive");
            entity.Property(e => e.ModifiedBy)
                .HasColumnType("mediumint unsigned")
                .HasColumnName("modifiedby");
            entity.Property(e => e.ModifiedDate)
                .HasColumnType("datetime")
                .HasColumnName("modifieddate");
            entity.Property(e => e.OrganizationTypeId).HasColumnName("organizationtypeid");
            entity.Property(e => e.PartnerOrganizationName)
                .HasMaxLength(100)
                .HasColumnName("partnerorganizationname");
            entity.Property(e => e.PracticeId).HasColumnName("practiceid");
        });

        modelBuilder.Entity<PartnerOrganizationMember>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("partnerorganizationmembers");

            entity.HasIndex(e => e.PartnerOrgLocationMappingId, "fk_partorgmem_locmapid_idx");

            entity.HasIndex(e => e.ModifiedBy, "fk_partorgmem_modmem_idx");

            entity.HasIndex(e => e.PartnerMemberId, "fk_partorgmem_partmem_idx");

            entity.HasIndex(e => e.ReferralSourceTypeId, "fk_partorgmem_refsourcetype_idx");

            entity.HasIndex(e => new { e.PartnerMemberId, e.IsActive }, "idx_partorgmem_custom1");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.IsActive)
                .HasDefaultValueSql("'1'")
                .HasColumnName("isactive");
            entity.Property(e => e.ModifiedBy)
                .HasColumnType("mediumint unsigned")
                .HasColumnName("modifiedby");
            entity.Property(e => e.ModifiedDate)
                .HasColumnType("datetime")
                .HasColumnName("modifieddate");
            entity.Property(e => e.PartnerMemberId).HasColumnName("partnermemberid");
            entity.Property(e => e.PartnerOrgLocationMappingId).HasColumnName("partnerorglocationmappingid");
            entity.Property(e => e.ReferralSourceTypeId).HasColumnName("referralsourcetypeid");

            entity.HasOne(d => d.PartnerMember).WithMany(p => p.PartnerOrganizationMembers)
                .HasForeignKey(d => d.PartnerMemberId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_partorgmem_partmem");

            entity.HasOne(d => d.PartnerOrgLocationMapping).WithMany(p => p.PartnerOrganizationMembers)
                .HasForeignKey(d => d.PartnerOrgLocationMappingId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_partorgmem_locmapid_idx");
        });

        modelBuilder.Entity<PartnerOrgLocationMapping>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("partnerorglocationmapping");

            entity.HasIndex(e => e.PartnerOrganizationLocationId, "fk_partorglocmap_locsid_idx");

            entity.HasIndex(e => e.PartnerOrganizationId, "fk_partorglocmap_orgid_idx");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.IsActive)
                .HasDefaultValueSql("'1'")
                .HasColumnName("isactive");
            entity.Property(e => e.ModifiedBy)
                .HasColumnType("mediumint unsigned")
                .HasColumnName("modifiedby");
            entity.Property(e => e.ModifiedDate)
                .HasColumnType("datetime")
                .HasColumnName("modifieddate");
            entity.Property(e => e.PartnerOrganizationId).HasColumnName("partnerorganizationid");
            entity.Property(e => e.PartnerOrganizationLocationId).HasColumnName("partnerorganizationlocationid");

            entity.HasOne(d => d.PartnerOrganization).WithMany(p => p.PartnerOrgLocationMappings)
                .HasForeignKey(d => d.PartnerOrganizationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_partorglocmap_orgid");
        });

        modelBuilder.Entity<Episode>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("episodes");

            entity.HasIndex(e => e.CaseTypeId, "fk_episode_casetype_idx");

            entity.HasIndex(e => e.CaseTypeStateId, "fk_episode_casetypestate_idx");

            entity.HasIndex(e => e.CaseManagementStatusId, "fk_episode_cmstatus_idx");

            entity.HasIndex(e => e.IncidentTypeId, "fk_episode_incidenttype_idx");

            entity.HasIndex(e => e.PatientId, "fk_episode_patient_idx");

            entity.HasIndex(e => e.StatusId, "fk_episode_status_idx");

            entity.HasIndex(e => e.CaseNumber, "fti_episode_casenumber").HasAnnotation("MySql:FullTextIndex", true);

            entity.HasIndex(e => new { e.Id, e.IsActive }, "fti_episode_isactive");

            entity.HasIndex(e => new { e.PatientId, e.CaseTypeId, e.CaseTypeStateId, e.CaseNumber, e.StatusId, e.IsActive, e.IncidentTypeId, e.IncidentDate }, "idx_episode_custom1");

            entity.Property(e => e.Id)
                .HasColumnType("mediumint unsigned")
                .HasColumnName("id");
            entity.Property(e => e.CaseManagementStatusId).HasColumnName("casemanagementstatusid");
            entity.Property(e => e.CaseMngtStatusComments)
                .HasMaxLength(200)
                .HasColumnName("casemngtstatuscomments");
            entity.Property(e => e.CaseNumber)
                .HasMaxLength(20)
                .HasColumnName("casenumber");
            entity.Property(e => e.CaseTypeId).HasColumnName("casetypeid");
            entity.Property(e => e.CaseTypeStateId).HasColumnName("casetypestateid");
            entity.Property(e => e.CreatedBy)
                .HasColumnType("mediumint unsigned")
                .HasColumnName("createdby");
            entity.Property(e => e.CreatedDate)
                .HasColumnType("datetime")
                .HasColumnName("createddate");
            entity.Property(e => e.ExternalBarcode)
                .HasMaxLength(10)
                .HasColumnName("externalbarcode");
            entity.Property(e => e.ExternalPatientId)
                .HasMaxLength(20)
                .HasColumnName("externalpatientid");
            entity.Property(e => e.IncidentDate).HasColumnName("incidentdate");
            entity.Property(e => e.IncidentTypeId).HasColumnName("incidenttypeid");
            entity.Property(e => e.IsActive)
                .HasDefaultValueSql("'1'")
                .HasColumnName("isactive");
            entity.Property(e => e.ModifiedBy)
                .HasColumnType("mediumint unsigned")
                .HasColumnName("modifiedby");
            entity.Property(e => e.ModifiedDate)
                .HasColumnType("datetime")
                .HasColumnName("modifieddate");
            entity.Property(e => e.PatientId)
                .HasColumnType("mediumint unsigned")
                .HasColumnName("patientid");
            entity.Property(e => e.PrevDbId)
                .HasColumnType("mediumint unsigned")
                .HasColumnName("prevdbid");
            entity.Property(e => e.StatusId).HasColumnName("statusid");
            entity.Property(e => e.V1EpisodeId)
                .HasColumnType("mediumint unsigned")
                .HasColumnName("v1episodeid");
            entity.Property(e => e.V2EpisodeId)
                .HasColumnType("mediumint unsigned")
                .HasColumnName("v2episodeid");
        });


        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
