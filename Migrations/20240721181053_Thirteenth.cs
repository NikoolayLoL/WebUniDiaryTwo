using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebUniDiaryTwo.Migrations
{
    /// <inheritdoc />
    public partial class Thirteenth : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Grades_Subjects_SubjectId",
                table: "Grades");

            migrationBuilder.DropForeignKey(
                name: "FK_Grades_Users_StudentId",
                table: "Grades");

            migrationBuilder.RenameColumn(
                name: "StudentId",
                table: "Grades",
                newName: "SemesterUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Grades_StudentId",
                table: "Grades",
                newName: "IX_Grades_SemesterUserId");

            migrationBuilder.AlterColumn<decimal>(
                name: "GradeValue",
                table: "Grades",
                type: "decimal(2,2)",
                precision: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "Grades",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Grades",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "SemesterUser",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SemesterId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SemesterUser", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SemesterUser_Semesters_SemesterId",
                        column: x => x.SemesterId,
                        principalTable: "Semesters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SemesterUser_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Grades_UserId",
                table: "Grades",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_SemesterUser_SemesterId",
                table: "SemesterUser",
                column: "SemesterId");

            migrationBuilder.CreateIndex(
                name: "IX_SemesterUser_UserId",
                table: "SemesterUser",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Grades_SemesterUser_SemesterUserId",
                table: "Grades",
                column: "SemesterUserId",
                principalTable: "SemesterUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Grades_Subjects_SubjectId",
                table: "Grades",
                column: "SubjectId",
                principalTable: "Subjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Grades_Users_UserId",
                table: "Grades",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Grades_SemesterUser_SemesterUserId",
                table: "Grades");

            migrationBuilder.DropForeignKey(
                name: "FK_Grades_Subjects_SubjectId",
                table: "Grades");

            migrationBuilder.DropForeignKey(
                name: "FK_Grades_Users_UserId",
                table: "Grades");

            migrationBuilder.DropTable(
                name: "SemesterUser");

            migrationBuilder.DropIndex(
                name: "IX_Grades_UserId",
                table: "Grades");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Grades");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Grades");

            migrationBuilder.RenameColumn(
                name: "SemesterUserId",
                table: "Grades",
                newName: "StudentId");

            migrationBuilder.RenameIndex(
                name: "IX_Grades_SemesterUserId",
                table: "Grades",
                newName: "IX_Grades_StudentId");

            migrationBuilder.AlterColumn<decimal>(
                name: "GradeValue",
                table: "Grades",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(2,2)",
                oldPrecision: 2);

            migrationBuilder.AddForeignKey(
                name: "FK_Grades_Subjects_SubjectId",
                table: "Grades",
                column: "SubjectId",
                principalTable: "Subjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Grades_Users_StudentId",
                table: "Grades",
                column: "StudentId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
