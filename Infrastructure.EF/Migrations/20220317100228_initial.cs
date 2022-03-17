using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.EF.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "patients",
                columns: table => new
                {
                    patientId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    patientFileId = table.Column<int>(type: "int", nullable: false),
                    studentNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    employeeNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    mail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    photoURL = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    age = table.Column<int>(type: "int", nullable: false),
                    sex = table.Column<int>(type: "int", nullable: false),
                    type = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_patients", x => x.patientId);
                });

            migrationBuilder.CreateTable(
                name: "practitioners",
                columns: table => new
                {
                    practitionerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    type = table.Column<int>(type: "int", nullable: false),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    mail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    studentNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    employeeNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BIGNumber = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_practitioners", x => x.practitionerId);
                });

            migrationBuilder.CreateTable(
                name: "treatmentPlans",
                columns: table => new
                {
                    treatmentPlanId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    practitionerId = table.Column<int>(type: "int", nullable: false),
                    patientFileId = table.Column<int>(type: "int", nullable: false),
                    diagnosis = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    complaint = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    weeklySessions = table.Column<int>(type: "int", nullable: false),
                    sessionDuration = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_treatmentPlans", x => x.treatmentPlanId);
                });

            migrationBuilder.CreateTable(
                name: "patientFiles",
                columns: table => new
                {
                    patientFileId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    patientId = table.Column<int>(type: "int", nullable: false),
                    patientId1 = table.Column<int>(type: "int", nullable: true),
                    treatmentPlanId = table.Column<int>(type: "int", nullable: false),
                    treatmentPlanId1 = table.Column<int>(type: "int", nullable: true),
                    intakeByPractitionerId = table.Column<int>(type: "int", nullable: false),
                    supervisedBypractitionerId = table.Column<int>(type: "int", nullable: false),
                    birthDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    type = table.Column<int>(type: "int", nullable: false),
                    registerDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dischargeDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_patientFiles", x => x.patientFileId);
                    table.ForeignKey(
                        name: "FK_patientFiles_patients_patientId1",
                        column: x => x.patientId1,
                        principalTable: "patients",
                        principalColumn: "patientId");
                    table.ForeignKey(
                        name: "FK_patientFiles_treatmentPlans_treatmentPlanId1",
                        column: x => x.treatmentPlanId1,
                        principalTable: "treatmentPlans",
                        principalColumn: "treatmentPlanId");
                });

            migrationBuilder.CreateTable(
                name: "treatments",
                columns: table => new
                {
                    treatmentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    practitionerId = table.Column<int>(type: "int", nullable: false),
                    treatmentPlanId = table.Column<int>(type: "int", nullable: false),
                    type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    treatmentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    location = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_treatments", x => x.treatmentId);
                    table.ForeignKey(
                        name: "FK_treatments_practitioners_practitionerId",
                        column: x => x.practitionerId,
                        principalTable: "practitioners",
                        principalColumn: "practitionerId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_treatments_treatmentPlans_treatmentPlanId",
                        column: x => x.treatmentPlanId,
                        principalTable: "treatmentPlans",
                        principalColumn: "treatmentPlanId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "comments",
                columns: table => new
                {
                    commentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    practitionerId = table.Column<int>(type: "int", nullable: false),
                    patientFileId = table.Column<int>(type: "int", nullable: false),
                    postDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    visible = table.Column<bool>(type: "bit", nullable: false),
                    content = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_comments", x => x.commentId);
                    table.ForeignKey(
                        name: "FK_comments_patientFiles_patientFileId",
                        column: x => x.patientFileId,
                        principalTable: "patientFiles",
                        principalColumn: "patientFileId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_comments_commentId",
                table: "comments",
                column: "commentId");

            migrationBuilder.CreateIndex(
                name: "IX_comments_patientFileId",
                table: "comments",
                column: "patientFileId");

            migrationBuilder.CreateIndex(
                name: "IX_patientFiles_patientFileId",
                table: "patientFiles",
                column: "patientFileId");

            migrationBuilder.CreateIndex(
                name: "IX_patientFiles_patientId1",
                table: "patientFiles",
                column: "patientId1");

            migrationBuilder.CreateIndex(
                name: "IX_patientFiles_treatmentPlanId1",
                table: "patientFiles",
                column: "treatmentPlanId1");

            migrationBuilder.CreateIndex(
                name: "IX_patients_patientId",
                table: "patients",
                column: "patientId");

            migrationBuilder.CreateIndex(
                name: "IX_practitioners_practitionerId",
                table: "practitioners",
                column: "practitionerId");

            migrationBuilder.CreateIndex(
                name: "IX_treatmentPlans_treatmentPlanId",
                table: "treatmentPlans",
                column: "treatmentPlanId");

            migrationBuilder.CreateIndex(
                name: "IX_treatments_practitionerId",
                table: "treatments",
                column: "practitionerId");

            migrationBuilder.CreateIndex(
                name: "IX_treatments_treatmentId",
                table: "treatments",
                column: "treatmentId");

            migrationBuilder.CreateIndex(
                name: "IX_treatments_treatmentPlanId",
                table: "treatments",
                column: "treatmentPlanId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "comments");

            migrationBuilder.DropTable(
                name: "treatments");

            migrationBuilder.DropTable(
                name: "patientFiles");

            migrationBuilder.DropTable(
                name: "practitioners");

            migrationBuilder.DropTable(
                name: "patients");

            migrationBuilder.DropTable(
                name: "treatmentPlans");
        }
    }
}
