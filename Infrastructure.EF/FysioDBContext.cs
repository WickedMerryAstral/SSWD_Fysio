using System;

using Core.Domain;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure.EF
{
    public class FysioDBContext : DbContext
    {
        public FysioDBContext(DbContextOptions<FysioDBContext> options)
            : base(options) { }

        public DbSet<Comment> comments { get; set; }
        public DbSet<Patient> patients { get; set; }
        public DbSet<PatientFile> patientFiles { get; set; }
        public DbSet<Practitioner> practitioners { get; set; }
        public DbSet<Treatment> treatments { get; set; }
        public DbSet<TreatmentPlan> treatmentPlans { get; set; }

        protected override void OnModelCreating(ModelBuilder model)
        {
            #region UNIQUES
            model.Entity<Comment>().HasIndex(e => e.commentId).IsUnique();
            model.Entity<Patient>().HasIndex(e => e.patientId).IsUnique();
            model.Entity<PatientFile>().HasIndex(e => e.patientFileId).IsUnique();
            model.Entity<Practitioner>().HasIndex(e => e.practitionerId).IsUnique();
            model.Entity<Treatment>().HasIndex(e => e.treatmentId).IsUnique();
            model.Entity<TreatmentPlan>().HasIndex(e => e.treatmentPlanId).IsUnique();
            #endregion

            #region PATIENTS
            // One to One, Patient has one patientfile, patientfile has one patient.
            model.Entity<Patient>()
                .HasOne(p => p.patientFile);
            #endregion

            #region PATIENT FILES
            //model.Entity<PatientFile>()
            //    .HasOne(pf => pf.patient);

            // One to Many, PatientFiles have multiple comments. Comments belong to one patient file.
            model.Entity<PatientFile>()
                .HasMany(pf => pf.comments)
                .WithOne(c => c.patientFile)
                .HasForeignKey(c => c.patientFileId);

            model.Entity<PatientFile>()
                .HasOne(pf => pf.treatmentPlan);
            model.Entity<PatientFile>()
                .HasOne(pf => pf.intakeByPractitioner);
            model.Entity<PatientFile>()
                .HasOne(pf => pf.supervisedByPractitioner);
            #endregion

            #region TREATMENT PLANS
            // TREATMENT PLANS
            model.Entity<TreatmentPlan>()
                .HasMany(tp => tp.treatments)
                .WithOne(t => t.treatmentPlan)
                .HasForeignKey(tp => tp.treatmentPlanId);

            // Head practitioner of a treatmentplan.
            model.Entity<TreatmentPlan>()
                .HasOne(tp => tp.practitioner)
                .WithMany(p => p.treatmentPlans)
                .HasForeignKey(tp => tp.treatmentPlanId);
            #endregion

            #region TREATMENTS
            // TREATMENTS
            model.Entity<Treatment>()
                .HasOne(t => t.practitioner);
            #endregion

            #region COMMENTS
            // COMMENTS
            // Author of comment
            model.Entity<Comment>()
                .HasOne(c => c.practitioner);
            #endregion
        }
    }
}
