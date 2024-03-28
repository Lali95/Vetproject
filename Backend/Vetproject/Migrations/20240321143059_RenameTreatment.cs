using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vetproject.Migrations
{
    /// <inheritdoc />
    public partial class RenameTreatment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MedicalIntervention",
                table: "MedicalRecords",
                newName: "MedicalTreatment");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MedicalTreatment",
                table: "MedicalRecords",
                newName: "MedicalIntervention");
        }
    }
}
