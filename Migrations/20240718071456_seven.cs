using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebUniDiaryTwo.Migrations
{
    /// <inheritdoc />
    public partial class seven : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Subjects_Specialties_SpecialtyId",
                table: "Subjects");

            migrationBuilder.DropIndex(
                name: "IX_Subjects_SpecialtyId",
                table: "Subjects");

            migrationBuilder.DropColumn(
                name: "SpecialtyId",
                table: "Subjects");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SpecialtyId",
                table: "Subjects",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Subjects_SpecialtyId",
                table: "Subjects",
                column: "SpecialtyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Subjects_Specialties_SpecialtyId",
                table: "Subjects",
                column: "SpecialtyId",
                principalTable: "Specialties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
