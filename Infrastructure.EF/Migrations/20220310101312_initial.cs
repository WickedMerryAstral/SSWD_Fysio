using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.EF.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "practitioners",
                columns: table => new
                {
                    practitionerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
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
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_treatmentPlans", x => x.treatmentPlanId);
                    table.ForeignKey(
                        name: "FK_treatmentPlans_practitioners_treatmentPlanId",
                        column: x => x.treatmentPlanId,
                        principalTable: "practitioners",
                        principalColumn: "practitionerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "patientFiles",
                columns: table => new
                {
                    patientFileId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    intakeByPractitionerpractitionerId = table.Column<int>(type: "int", nullable: true),
                    supervisedByPractitionerpractitionerId = table.Column<int>(type: "int", nullable: true),
                    treatmentPlanId1 = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_patientFiles", x => x.patientFileId);
                    table.ForeignKey(
                        name: "FK_patientFiles_practitioners_intakeByPractitionerpractitionerId",
                        column: x => x.intakeByPractitionerpractitionerId,
                        principalTable: "practitioners",
                        principalColumn: "practitionerId");
                    table.ForeignKey(
                        name: "FK_patientFiles_practitioners_supervisedByPractitionerpractitionerId",
                        column: x => x.supervisedByPractitionerpractitionerId,
                        principalTable: "practitioners",
                        principalColumn: "practitionerId");
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
                    practitionerId1 = table.Column<int>(type: "int", nullable: true),
                    treatmentPlanId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_treatments", x => x.treatmentId);
                    table.ForeignKey(
                        name: "FK_treatments_practitioners_practitionerId1",
                        column: x => x.practitionerId1,
                        principalTable: "practitioners",
                        principalColumn: "practitionerId");
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
                    patientFileId = table.Column<int>(type: "int", nullable: false),
                    practitionerId1 = table.Column<int>(type: "int", nullable: true)
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
                    table.ForeignKey(
                        name: "FK_comments_practitioners_practitionerId1",
                        column: x => x.practitionerId1,
                        principalTable: "practitioners",
                        principalColumn: "practitionerId");
                });

            migrationBuilder.CreateTable(
                name: "patients",
                columns: table => new
                {
                    patientId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    patientFileId = table.Column<int>(type: "int", nullable: false)
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

            migrationBuilder.CreateIndex(
                name: "IX_comments_commentId",
                table: "comments",
                column: "commentId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_comments_patientFileId",
                table: "comments",
                column: "patientFileId");

            migrationBuilder.CreateIndex(
                name: "IX_comments_practitionerId1",
                table: "comments",
                column: "practitionerId1");

            migrationBuilder.CreateIndex(
                name: "IX_patientFiles_intakeByPractitionerpractitionerId",
                table: "patientFiles",
                column: "intakeByPractitionerpractitionerId");

            migrationBuilder.CreateIndex(
                name: "IX_patientFiles_patientFileId",
                table: "patientFiles",
                column: "patientFileId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_patientFiles_supervisedByPractitionerpractitionerId",
                table: "patientFiles",
                column: "supervisedByPractitionerpractitionerId");

            migrationBuilder.CreateIndex(
                name: "IX_patientFiles_treatmentPlanId1",
                table: "patientFiles",
                column: "treatmentPlanId1");

            migrationBuilder.CreateIndex(
                name: "IX_patients_patientFileId",
                table: "patients",
                column: "patientFileId");

            migrationBuilder.CreateIndex(
                name: "IX_patients_patientId",
                table: "patients",
                column: "patientId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_practitioners_practitionerId",
                table: "practitioners",
                column: "practitionerId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_treatmentPlans_treatmentPlanId",
                table: "treatmentPlans",
                column: "treatmentPlanId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_treatments_practitionerId1",
                table: "treatments",
                column: "practitionerId1");

            migrationBuilder.CreateIndex(
                name: "IX_treatments_treatmentId",
                table: "treatments",
                column: "treatmentId",
                unique: true);

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
                name: "patients");

            migrationBuilder.DropTable(
                name: "treatments");

            migrationBuilder.DropTable(
                name: "patientFiles");

            migrationBuilder.DropTable(
                name: "treatmentPlans");

            migrationBuilder.DropTable(
                name: "practitioners");
        }
    }
}
