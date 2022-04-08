using System;

using Core.Domain;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure.EF
{
    public class FysioDBContext : DbContext
    {
        public FysioDBContext(DbContextOptions<FysioDBContext> options) : base(options)
        {
        }

        public FysioDBContext() {
        
        }

        public virtual DbSet<Comment> comments { get; set; }
        public virtual DbSet<Patient> patients { get; set; }
        public virtual DbSet<PatientFile> patientFiles { get; set; }
        public virtual DbSet<Practitioner> practitioners { get; set; }
        public virtual DbSet<Treatment> treatments { get; set; }
        public virtual DbSet<TreatmentPlan> treatmentPlans { get; set; }
        public virtual DbSet<AppAccount> accounts { get; set; }

        protected override void OnModelCreating(ModelBuilder model)
        {
            #region ENTITY SPECIFICATIONS (Keys, Auto-generated properties, etc)
            model.Entity<Comment>(e => {
                e.HasKey(e => e.commentId);
                e.Property(e => e.commentId).ValueGeneratedOnAdd();
                e.HasIndex(e => e.commentId);
            });

            model.Entity<Patient>(e => {
                e.HasKey(e => e.patientId);
                e.Property(e => e.patientId).ValueGeneratedOnAdd();
                e.HasIndex(e => e.patientId);
            });

            model.Entity<PatientFile>(e => {
                e.HasKey(e => e.patientFileId);
                e.Property(e => e.patientFileId).ValueGeneratedOnAdd();
                e.HasIndex(e => e.patientFileId);
            });

            model.Entity<Practitioner>(e => {
                e.HasKey(e => e.practitionerId);
                e.Property(e => e.practitionerId).ValueGeneratedOnAdd();
                e.HasIndex(e => e.practitionerId);
            });

            model.Entity<Treatment>(e => {
                e.HasKey(e => e.treatmentId);
                e.Property(e => e.treatmentId).ValueGeneratedOnAdd();
                e.HasIndex(e => e.treatmentId);
            });

            model.Entity<TreatmentPlan>(e => {
                e.HasKey(e => e.treatmentPlanId);
                e.Property(e => e.treatmentPlanId).ValueGeneratedOnAdd();
                e.HasIndex(e => e.treatmentPlanId);
            });
            #endregion

            #region MANUAL CONSTRAINTS

            #region APP ACCOUNTS
            model.Entity<AppAccount>().HasIndex(a => a.mail).IsUnique();
            #endregion

            #region PATIENTS
            #endregion

            #region PATIENT FILES
            #endregion

            #region TREATMENT PLANS
            #endregion

            #region TREATMENTS
            #endregion

            #region PRACTITIONERS
            #endregion

            #region COMMENTS
            #endregion

            #endregion
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=MAYHEM;Initial Catalog=FysioDB;Integrated Security=True");
            }
        }
    }
}
