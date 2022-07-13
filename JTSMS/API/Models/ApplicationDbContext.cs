using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using SharedObjects.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public partial class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
        {

        }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        //Scaffold-DbContext "Data Source=vnhcmm0teapp03;Initial Catalog=jtestsms;User Id=hcm_vui_usr;Password=hcm_vui_usr;" MySql.Data.EntityFrameworkCore -Tables "Master_Approval" -OutputDir Models2


        public virtual DbSet<ScriptDetails> ScriptDetails { get; set; }
        public virtual DbSet<MasterApproval> MasterApproval { get; set; }
        public virtual DbSet<Requestdetail> Requestdetail { get; set; }
        public virtual DbSet<AccessUserRole> AccessUserRole { get; set; }
        public virtual DbSet<Watchdogconfig> Watchdogconfig { get; set; }
        public virtual DbSet<Registration> Registration { get; set; }
        public virtual DbSet<MasterStation> MasterStation { get; set; }
        public virtual DbSet<MasterStatus> MasterStatus { get; set; }
        public virtual DbSet<Assembly> Assembly { get; set; }


        public virtual DbQuery<VCustomer> VCustomer { get; set; }
        public virtual DbQuery<VScriptId> VScriptId { get; set; }
        public virtual DbQuery<VStation> VStation { get; set; }
        public virtual DbQuery<VDetail> VDetail { get; set; }
        public virtual DbQuery<VType> VType { get; set; }
        public virtual DbQuery<VApproval> VApproval { get; set; }
        public virtual DbQuery<VUserRole> VUserRole { get; set; }
        public virtual DbQuery<VRole> VRole { get; set; }
        public virtual DbQuery<VRoute> VRoute { get; set; }
        public virtual DbQuery<VMasterApproval> VMasterApproval { get; set; }
        public virtual DbQuery<VConfig> VConfig { get; set; }
        public virtual DbQuery<VRouteStep> VRouteStep { get; set; }
        public virtual DbQuery<VRegistration> VRegistration { get; set; }
        public virtual DbQuery<VRequest> VRequest { get; set; }

        [Obsolete]
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseMySQL("Data Source=vnhcmm0teapp03;Initial Catalog=jtestsms;User Id=hcm_vui_usr;Password=hcm_vui_usr;")
                    .ConfigureWarnings(warnings => warnings.Throw(RelationalEventId.QueryClientEvaluationWarning)); ;
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ScriptDetails>(entity =>
            {
                entity.HasKey(e => e.IdscriptDetails)
                    .HasName("PRIMARY");

                entity.ToTable("script_details");

                entity.Property(e => e.IdscriptDetails)
                    .HasColumnName("idscript_details")
                    .HasColumnType("int(11)");

                entity.Property(e => e.ApprovalStatus)
                    .HasColumnName("approval_status")
                    .HasMaxLength(100);

                entity.Property(e => e.AssemblyNo)
                    .IsRequired()
                    .HasColumnName("assembly_no")
                    .HasMaxLength(100);

                entity.Property(e => e.AssemblyRev)
                    .IsRequired()
                    .HasColumnName("assembly_rev")
                    .HasMaxLength(100);

                entity.Property(e => e.ChangeType)
                    .HasColumnName("change_type")
                    .HasMaxLength(100);

                entity.Property(e => e.IsActive)
                    .HasColumnName("isActive")
                    .HasColumnType("bit(1)");

                entity.Property(e => e.RequesteddBy)
                    .HasColumnName("requesteddBy")
                    .HasMaxLength(100);

                entity.Property(e => e.TestScriptDesc)
                    .IsRequired()
                    .HasColumnName("test_script_desc")
                    .HasMaxLength(100);

                entity.Property(e => e.TestScriptFileHash)
                    .HasColumnName("test_script_file_hash")
                    .HasMaxLength(100);

                entity.Property(e => e.TestScriptFileLocation)
                    .HasColumnName("test_script_file_location")
                    .HasMaxLength(255);

                entity.Property(e => e.TestScriptId)
                    .IsRequired()
                    .HasColumnName("test_script_id")
                    .HasMaxLength(100);

                entity.Property(e => e.TestScriptName)
                    .IsRequired()
                    .HasColumnName("test_script_name")
                    .HasMaxLength(100);

                entity.Property(e => e.TestScriptRev)
                    .IsRequired()
                    .HasColumnName("test_script_rev")
                    .HasMaxLength(100);

                entity.Property(e => e.TestStationName)
                    .IsRequired()
                    .HasColumnName("test_station_name")
                    .HasMaxLength(100);

                entity.Property(e => e.Workcell)
                    .IsRequired()
                    .HasColumnName("workcell")
                    .HasMaxLength(100);
            });
            modelBuilder.Entity<MasterApproval>(entity =>
            {
                entity.HasKey(e => e.ApprovalId)
                    .HasName("PRIMARY");

                entity.ToTable("master_approval");

                entity.Property(e => e.ApprovalId)
                    .HasColumnName("approvalId")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CreatedBy)
                    .HasColumnName("createdBy")
                    .HasMaxLength(45);

                entity.Property(e => e.CustId)
                    .HasColumnName("custId")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Email).HasMaxLength(100);

                entity.Property(e => e.IsActive)
                    .HasColumnName("isActive")
                    .HasMaxLength(45);

                entity.Property(e => e.Name).HasMaxLength(45);

                entity.Property(e => e.Ntlogin)
                    .HasColumnName("NTLogin")
                    .HasMaxLength(45);

                entity.Property(e => e.RouteId)
                    .HasColumnName("routeId")
                    .HasColumnType("int(11)");
            });
            modelBuilder.Entity<Requestdetail>(entity =>
            {
                entity.HasKey(e => e.ReqId)
                    .HasName("PRIMARY");

                entity.ToTable("requestdetail");

                entity.HasIndex(e => e.Filehash)
                    .HasName("file_hash_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.ReqId)
                    .HasName("reqId_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.ReqNumber)
                    .HasName("reqNumber_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.Scriptid)
                    .HasName("script_id_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.ReqId)
                    .HasColumnName("reqId")
                    .HasColumnType("int(11)");

                entity.Property(e => e.AssemblyNumber)
                    .HasColumnName("assemblyNumber")
                    .HasMaxLength(45);

                entity.Property(e => e.AssemblyRevision)
                    .HasColumnName("assemblyRevision")
                    .HasMaxLength(45);

                entity.Property(e => e.ChangeDetail)
                    .HasColumnName("changeDetail")
                    .HasMaxLength(45);

                entity.Property(e => e.CreatedBy)
                    .HasColumnName("createdBy")
                    .HasMaxLength(10);

                entity.Property(e => e.CreatedEmail)
                    .HasColumnName("createdEmail")
                    .HasMaxLength(100);

                entity.Property(e => e.CreatedName)
                    .HasColumnName("createdName")
                    .HasMaxLength(100);

                entity.Property(e => e.CustId)
                    .HasColumnName("custId")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .HasMaxLength(255);

                entity.Property(e => e.EncriptedFileName)
                    .HasColumnName("encriptedFileName")
                    .HasMaxLength(255);

                entity.Property(e => e.Filehash)
                    .HasColumnName("filehash")
                    .HasMaxLength(255);

                entity.Property(e => e.Firmware)
                    .HasColumnName("firmware")
                    .HasMaxLength(255);

                entity.Property(e => e.FirmwareRevision)
                    .HasColumnName("firmwareRevision")
                    .HasMaxLength(255);

                entity.Property(e => e.IsActive).HasColumnName("isActive");

                entity.Property(e => e.PcnorDevNumber)
                    .HasColumnName("PCNorDevNumber")
                    .HasMaxLength(45);

                entity.Property(e => e.PlatformId)
                    .HasColumnName("platformId")
                    .HasColumnType("int(11)");

                entity.Property(e => e.ReqNumber)
                    .HasColumnName("reqNumber")
                    .HasMaxLength(45);

                entity.Property(e => e.RouteStepId)
                    .HasColumnName("routeStepId")
                    .HasMaxLength(45);

                entity.Property(e => e.ScriptFileName)
                    .HasColumnName("scriptFileName")
                    .HasMaxLength(255);

                entity.Property(e => e.Scriptid)
                    .HasColumnName("scriptid")
                    .HasMaxLength(255);

                entity.Property(e => e.Scriptname)
                    .HasColumnName("scriptname")
                    .HasMaxLength(255);

                entity.Property(e => e.Scriptrev)
                    .HasColumnName("scriptrev")
                    .HasMaxLength(45);

                entity.Property(e => e.StationId)
                    .HasColumnName("stationId")
                    .HasColumnType("int(11)");

                entity.Property(e => e.StatusId)
                    .HasColumnName("statusId")
                    .HasColumnType("int(11)");

                entity.Property(e => e.TypeId)
                    .HasColumnName("typeId")
                    .HasColumnType("int(11)");

                entity.Property(e => e.UpdatedBy)
                    .HasColumnName("updatedBy")
                    .HasMaxLength(45);

                entity.Property(e => e.UpdatedEmail)
                    .HasColumnName("updatedEmail")
                    .HasMaxLength(45);

                entity.Property(e => e.UpdatedName)
                    .HasColumnName("updatedName")
                    .HasMaxLength(45);
            });
            modelBuilder.Entity<AccessUserRole>(entity =>
            {
                entity.HasKey(e => e.UserRoleId);

                entity.ToTable("Access_UserRole");

                entity.Property(e => e.UserRoleId).HasColumnName("userRoleId");

                entity.Property(e => e.CreatedBy)
                    .HasColumnName("createdBy")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedEmail)
                    .HasColumnName("createdEmail")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedName)
                    .HasColumnName("createdName")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.CreationDate)
                    .HasColumnName("creationDate")
                    .HasColumnType("datetime");

                entity.Property(e => e.CustId).HasColumnName("custID");

                entity.Property(e => e.IsActive).HasColumnName("isActive");

                entity.Property(e => e.Ntlogin)
                    .HasColumnName("NTLogin")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.PlantId).HasColumnName("plantID");

                entity.Property(e => e.RoleId).HasColumnName("roleID");

                entity.Property(e => e.UpdateDate)
                    .HasColumnName("updateDate")
                    .HasColumnType("datetime");

                entity.Property(e => e.UpdatedBy)
                    .HasColumnName("updatedBy")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedEmail)
                    .HasColumnName("updatedEmail")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedName)
                    .HasColumnName("updatedName")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.UserName)
                    .HasColumnName("userName")
                    .HasMaxLength(30)
                    .IsUnicode(false);
            });
            modelBuilder.Entity<Watchdogconfig>(entity =>
            {
                entity.HasKey(e => e.WdconfigId)
                    .HasName("PRIMARY");

                entity.ToTable("watchdogconfig");

                entity.Property(e => e.WdconfigId)
                    .HasColumnName("wdconfigId")
                    .HasColumnType("int(11)");

                entity.Property(e => e.AssyNumber)
                    .HasColumnName("assyNumber")
                    .HasMaxLength(45);

                entity.Property(e => e.AssyRev)
                    .HasColumnName("assyRev")
                    .HasMaxLength(45);

                entity.Property(e => e.CustId)
                    .HasColumnName("custId")
                    .HasColumnType("int(11)");

                entity.Property(e => e.EquipmentId)
                    .HasColumnName("equipmentId")
                    .HasColumnType("int(11)");

                entity.Property(e => e.EquipmentName)
                    .HasColumnName("equipmentName")
                    .HasMaxLength(45);

                entity.Property(e => e.IsActive)
                    .HasColumnName("isActive")
                    .HasColumnType("int(11)");

                entity.Property(e => e.IsDmz)
                    .HasColumnName("isDMZ")
                    .HasColumnType("int(11)");

                entity.Property(e => e.IsWatchDogTrigger)
                    .HasColumnName("isWatchDogTrigger")
                    .HasColumnType("int(11)");

                entity.Property(e => e.PlatFormId)
                    .HasColumnName("platFormId")
                    .HasColumnType("int(11)");

                entity.Property(e => e.ProcessStep)
                    .HasColumnName("processStep")
                    .HasMaxLength(45);

                entity.Property(e => e.RouteStep)
                    .HasColumnName("routeStep")
                    .HasMaxLength(45);

                entity.Property(e => e.TestTime)
                    .HasColumnName("testTime")
                    .HasColumnType("int(8)");

                entity.Property(e => e.TesterName)
                    .HasColumnName("testerName")
                    .HasMaxLength(45);

                entity.Property(e => e.TesterPcname)
                    .HasColumnName("testerPCName")
                    .HasMaxLength(45);
            });
            modelBuilder.Entity<Registration>(entity =>
            {
                entity.HasKey(e => e.RegId)
                    .HasName("PRIMARY");

                entity.ToTable("registration");

                entity.HasIndex(e => e.RegId)
                    .HasName("test");

                entity.Property(e => e.RegId)
                    .HasColumnName("regId")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CreatedBy)
                    .HasColumnName("createdBy")
                    .HasMaxLength(45);

                entity.Property(e => e.CreatedEmail)
                    .HasColumnName("createdEmail")
                    .HasMaxLength(100);

                entity.Property(e => e.CreatedName)
                    .HasColumnName("createdName")
                    .HasMaxLength(100);

                entity.Property(e => e.CustId)
                    .HasColumnName("custId")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Family)
                    .HasColumnName("family")
                    .HasMaxLength(45);

                entity.Property(e => e.IsActive).HasColumnName("isActive");

                entity.Property(e => e.PlatformId)
                    .HasColumnName("platformId")
                    .HasColumnType("int(11)");

                entity.Property(e => e.RouteStep)
                    .HasColumnName("routeStep")
                    .HasMaxLength(45);

                entity.Property(e => e.ScriptId)
                    .HasColumnName("scriptId")
                    .HasMaxLength(255);

                entity.Property(e => e.StationId)
                    .HasColumnName("stationId")
                    .HasColumnType("int(11)");

                entity.Property(e => e.StatusId).HasColumnName("statusId");

                entity.Property(e => e.TypeId)
                    .HasColumnName("typeId")
                    .HasColumnType("int(11)");

                entity.Property(e => e.UpdatedBy)
                    .HasColumnName("updatedBy")
                    .HasMaxLength(45);

                entity.Property(e => e.UpdatedEmail)
                    .HasColumnName("updatedEmail")
                    .HasMaxLength(100);

                entity.Property(e => e.UpdatedName)
                    .HasColumnName("updatedName")
                    .HasMaxLength(100);
            });
            modelBuilder.Entity<MasterStation>(entity =>
            {
                entity.HasKey(e => e.StationId)
                    .HasName("PRIMARY");

                entity.ToTable("master_station");

                entity.HasIndex(e => e.StationId)
                    .HasName("stationId_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.StationId)
                    .HasColumnName("stationId")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CreatedBy)
                    .HasColumnName("createdBy")
                    .HasMaxLength(45);

                entity.Property(e => e.CreatedEmail)
                    .HasColumnName("createdEmail")
                    .HasMaxLength(45);

                entity.Property(e => e.CreatedName)
                    .HasColumnName("createdName")
                    .HasMaxLength(45);

                entity.Property(e => e.IsActive)
                    .HasColumnName("isActive")
                    .HasMaxLength(45);

                entity.Property(e => e.Station)
                    .HasColumnName("station")
                    .HasMaxLength(45);

                entity.Property(e => e.StepId)
                    .HasColumnName("stepId")
                    .HasColumnType("int(11)");

                entity.Property(e => e.UpdatedEmail)
                    .HasColumnName("updatedEmail")
                    .HasMaxLength(45);

                entity.Property(e => e.UpdatedName)
                    .HasColumnName("updatedName")
                    .HasMaxLength(45);
            });
            modelBuilder.Entity<MasterStatus>(entity =>
            {
                entity.HasKey(e => e.StatusId)
                    .HasName("PRIMARY");

                entity.ToTable("master_status");

                entity.Property(e => e.StatusId)
                    .HasColumnName("statusId")
                    .HasColumnType("int(11)");

                entity.Property(e => e.IsActive)
                    .HasColumnName("isActive")
                    .HasColumnType("int(11)");

                entity.Property(e => e.StatusColour)
                    .HasColumnName("statusColour")
                    .HasMaxLength(45);

                entity.Property(e => e.StatusName)
                    .HasColumnName("statusName")
                    .HasMaxLength(45);
            });
            modelBuilder.Entity<Assembly>(entity =>
            {
                entity.ToTable("assembly");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.AssemblyNumber)
                    .HasColumnName("assemblyNumber")
                    .HasMaxLength(255);

                entity.Property(e => e.AssemblyRevision)
                    .HasColumnName("assemblyRevision")
                    .HasMaxLength(255);

                entity.Property(e => e.RegId)
                    .HasColumnName("regId")
                    .HasColumnType("int(11)");
            });
            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
