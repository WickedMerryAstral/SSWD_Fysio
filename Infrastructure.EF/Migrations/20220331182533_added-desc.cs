using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.EF.Migrations
{
    public partial class addeddesc : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "description",
                table: "treatments",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "hasMandatoryExplanation",
                table: "treatments",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "description",
                table: "treatments");

            migrationBuilder.DropColumn(
                name: "hasMandatoryExplanation",
                table: "treatments");
        }
    }
}
