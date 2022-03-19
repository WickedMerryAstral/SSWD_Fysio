﻿// <auto-generated />
using System;
using Infrastructure.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Infrastructure.EF.Migrations
{
    [DbContext(typeof(FysioDBContext))]
    [Migration("20220318094747_no-passwords-in-ef")]
    partial class nopasswordsinef
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Core.Domain.AppAccount", b =>
                {
                    b.Property<int>("accountId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("accountId"), 1L, 1);

                    b.Property<int>("accountType")
                        .HasColumnType("int");

                    b.Property<string>("mail")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("accountId");

                    b.ToTable("accounts");
                });

            modelBuilder.Entity("Core.Domain.Comment", b =>
                {
                    b.Property<int>("commentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("commentId"), 1L, 1);

                    b.Property<string>("content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("patientFileId")
                        .HasColumnType("int");

                    b.Property<DateTime>("postDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("practitionerId")
                        .HasColumnType("int");

                    b.Property<bool>("visible")
                        .HasColumnType("bit");

                    b.HasKey("commentId");

                    b.HasIndex("commentId");

                    b.HasIndex("patientFileId");

                    b.ToTable("comments");
                });

            modelBuilder.Entity("Core.Domain.Patient", b =>
                {
                    b.Property<int>("patientId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("patientId"), 1L, 1);

                    b.Property<int>("age")
                        .HasColumnType("int");

                    b.Property<string>("employeeNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("mail")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("patientFileId")
                        .HasColumnType("int");

                    b.Property<string>("phone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("photoURL")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("sex")
                        .HasColumnType("int");

                    b.Property<string>("studentNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("type")
                        .HasColumnType("int");

                    b.HasKey("patientId");

                    b.HasIndex("patientId");

                    b.ToTable("patients");
                });

            modelBuilder.Entity("Core.Domain.PatientFile", b =>
                {
                    b.Property<int>("patientFileId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("patientFileId"), 1L, 1);

                    b.Property<DateTime>("birthDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("dischargeDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("intakeByPractitionerId")
                        .HasColumnType("int");

                    b.Property<int>("patientId")
                        .HasColumnType("int");

                    b.Property<int?>("patientId1")
                        .HasColumnType("int");

                    b.Property<DateTime>("registerDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("supervisedBypractitionerId")
                        .HasColumnType("int");

                    b.Property<int>("treatmentPlanId")
                        .HasColumnType("int");

                    b.Property<int?>("treatmentPlanId1")
                        .HasColumnType("int");

                    b.Property<int>("type")
                        .HasColumnType("int");

                    b.HasKey("patientFileId");

                    b.HasIndex("patientFileId");

                    b.HasIndex("patientId1");

                    b.HasIndex("treatmentPlanId1");

                    b.ToTable("patientFiles");
                });

            modelBuilder.Entity("Core.Domain.Practitioner", b =>
                {
                    b.Property<int>("practitionerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("practitionerId"), 1L, 1);

                    b.Property<string>("BIGNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("employeeNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("mail")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("phone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("studentNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("type")
                        .HasColumnType("int");

                    b.HasKey("practitionerId");

                    b.HasIndex("practitionerId");

                    b.ToTable("practitioners");
                });

            modelBuilder.Entity("Core.Domain.Treatment", b =>
                {
                    b.Property<int>("treatmentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("treatmentId"), 1L, 1);

                    b.Property<string>("location")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("practitionerId")
                        .HasColumnType("int");

                    b.Property<DateTime>("treatmentDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("treatmentPlanId")
                        .HasColumnType("int");

                    b.Property<string>("type")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("treatmentId");

                    b.HasIndex("practitionerId");

                    b.HasIndex("treatmentId");

                    b.HasIndex("treatmentPlanId");

                    b.ToTable("treatments");
                });

            modelBuilder.Entity("Core.Domain.TreatmentPlan", b =>
                {
                    b.Property<int>("treatmentPlanId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("treatmentPlanId"), 1L, 1);

                    b.Property<string>("complaint")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("diagnosis")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("patientFileId")
                        .HasColumnType("int");

                    b.Property<int>("practitionerId")
                        .HasColumnType("int");

                    b.Property<int>("sessionDuration")
                        .HasColumnType("int");

                    b.Property<int>("weeklySessions")
                        .HasColumnType("int");

                    b.HasKey("treatmentPlanId");

                    b.HasIndex("treatmentPlanId");

                    b.ToTable("treatmentPlans");
                });

            modelBuilder.Entity("Core.Domain.Comment", b =>
                {
                    b.HasOne("Core.Domain.PatientFile", null)
                        .WithMany("comments")
                        .HasForeignKey("patientFileId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Core.Domain.PatientFile", b =>
                {
                    b.HasOne("Core.Domain.Patient", "patient")
                        .WithMany()
                        .HasForeignKey("patientId1");

                    b.HasOne("Core.Domain.TreatmentPlan", "treatmentPlan")
                        .WithMany()
                        .HasForeignKey("treatmentPlanId1");

                    b.Navigation("patient");

                    b.Navigation("treatmentPlan");
                });

            modelBuilder.Entity("Core.Domain.Treatment", b =>
                {
                    b.HasOne("Core.Domain.Practitioner", null)
                        .WithMany("treatments")
                        .HasForeignKey("practitionerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Core.Domain.TreatmentPlan", null)
                        .WithMany("treatments")
                        .HasForeignKey("treatmentPlanId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Core.Domain.PatientFile", b =>
                {
                    b.Navigation("comments");
                });

            modelBuilder.Entity("Core.Domain.Practitioner", b =>
                {
                    b.Navigation("treatments");
                });

            modelBuilder.Entity("Core.Domain.TreatmentPlan", b =>
                {
                    b.Navigation("treatments");
                });
#pragma warning restore 612, 618
        }
    }
}
