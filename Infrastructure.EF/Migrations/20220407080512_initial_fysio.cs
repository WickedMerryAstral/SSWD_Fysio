using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.EF.Migrations
{
    public partial class initial_fysio : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "accounts",
                columns: table => new
                {
                    accountId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    mail = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    accountType = table.Column<int>(type: "int", nullable: false),
                    patientId = table.Column<int>(type: "int", nullable: false),
                    practitionerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_accounts", x => x.accountId);
                });

            migrationBuilder.CreateTable(
                name: "patientFiles",
                columns: table => new
                {
                    patientFileId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    intakeByPractitionerId = table.Column<int>(type: "int", nullable: false),
                    supervisedBypractitionerId = table.Column<int>(type: "int", nullable: false),
                    birthDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    type = table.Column<int>(type: "int", nullable: false),
                    registerDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    entryDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dischargeDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_patientFiles", x => x.patientFileId);
                });

            migrationBuilder.CreateTable(
                name: "practitioners",
                columns: table => new
                {
                    practitionerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    type = table.Column<int>(type: "int", nullable: false),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    mail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    studentNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    employeeNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BIGNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    availableMON = table.Column<bool>(type: "bit", nullable: false),
                    availableTUE = table.Column<bool>(type: "bit", nullable: false),
                    availableWED = table.Column<bool>(type: "bit", nullable: false),
                    availableTHU = table.Column<bool>(type: "bit", nullable: false),
                    availableFRI = table.Column<bool>(type: "bit", nullable: false),
                    availableSAT = table.Column<bool>(type: "bit", nullable: false),
                    availableSUN = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_practitioners", x => x.practitionerId);
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
                    content = table.Column<string>(type: "nvarchar(max)", nullable: false)
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

            migrationBuilder.CreateTable(
                name: "patients",
                columns: table => new
                {
                    patientId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    patientFileId = table.Column<int>(type: "int", nullable: false),
                    studentNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    employeeNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    mail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    photo = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    age = table.Column<int>(type: "int", nullable: false),
                    sex = table.Column<int>(type: "int", nullable: false),
                    type = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_patients", x => x.patientId);
                    table.ForeignKey(
                        name: "FK_patients_patientFiles_patientFileId",
                        column: x => x.patientFileId,
                        principalTable: "patientFiles",
                        principalColumn: "patientFileId",
                        onDelete: ReferentialAction.Cascade);
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
                    table.ForeignKey(
                        name: "FK_treatmentPlans_patientFiles_patientFileId",
                        column: x => x.patientFileId,
                        principalTable: "patientFiles",
                        principalColumn: "patientFileId",
                        onDelete: ReferentialAction.Cascade);
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
                    hasMandatoryExplanation = table.Column<bool>(type: "bit", nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    treatmentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    treatmentEndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
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

            migrationBuilder.CreateIndex(
                name: "IX_accounts_mail",
                table: "accounts",
                column: "mail",
                unique: true);

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
                name: "IX_patients_patientFileId",
                table: "patients",
                column: "patientFileId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_patients_patientId",
                table: "patients",
                column: "patientId");

            migrationBuilder.CreateIndex(
                name: "IX_practitioners_practitionerId",
                table: "practitioners",
                column: "practitionerId");

            migrationBuilder.CreateIndex(
                name: "IX_treatmentPlans_patientFileId",
                table: "treatmentPlans",
                column: "patientFileId",
                unique: true);

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
                name: "accounts");

            migrationBuilder.DropTable(
                name: "comments");

            migrationBuilder.DropTable(
                name: "patients");

            migrationBuilder.DropTable(
                name: "treatments");

            migrationBuilder.DropTable(
                name: "practitioners");

            migrationBuilder.DropTable(
                name: "treatmentPlans");

            migrationBuilder.DropTable(
                name: "patientFiles");
        }
    }
}
