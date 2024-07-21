using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebUniDiaryTwo.Migrations
{
    /// <inheritdoc />
    public partial class Fourteenth : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Grades_SemesterUser_SemesterUserId",
                table: "Grades");

            migrationBuilder.DropForeignKey(
                name: "FK_SemesterUser_Semesters_SemesterId",
                table: "SemesterUser");

            migrationBuilder.DropForeignKey(
                name: "FK_SemesterUser_Users_UserId",
                table: "SemesterUser");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SemesterUser",
                table: "SemesterUser");

            migrationBuilder.RenameTable(
                name: "SemesterUser",
                newName: "SemesterUsers");

            migrationBuilder.RenameIndex(
                name: "IX_SemesterUser_UserId",
                table: "SemesterUsers",
                newName: "IX_SemesterUsers_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_SemesterUser_SemesterId",
                table: "SemesterUsers",
                newName: "IX_SemesterUsers_SemesterId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SemesterUsers",
                table: "SemesterUsers",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Grades_SemesterUsers_SemesterUserId",
                table: "Grades",
                column: "SemesterUserId",
                principalTable: "SemesterUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SemesterUsers_Semesters_SemesterId",
                table: "SemesterUsers",
                column: "SemesterId",
                principalTable: "Semesters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SemesterUsers_Users_UserId",
                table: "SemesterUsers",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Grades_SemesterUsers_SemesterUserId",
                table: "Grades");

            migrationBuilder.DropForeignKey(
                name: "FK_SemesterUsers_Semesters_SemesterId",
                table: "SemesterUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_SemesterUsers_Users_UserId",
                table: "SemesterUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SemesterUsers",
                table: "SemesterUsers");

            migrationBuilder.RenameTable(
                name: "SemesterUsers",
                newName: "SemesterUser");

            migrationBuilder.RenameIndex(
                name: "IX_SemesterUsers_UserId",
                table: "SemesterUser",
                newName: "IX_SemesterUser_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_SemesterUsers_SemesterId",
                table: "SemesterUser",
                newName: "IX_SemesterUser_SemesterId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SemesterUser",
                table: "SemesterUser",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Grades_SemesterUser_SemesterUserId",
                table: "Grades",
                column: "SemesterUserId",
                principalTable: "SemesterUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SemesterUser_Semesters_SemesterId",
                table: "SemesterUser",
                column: "SemesterId",
                principalTable: "Semesters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SemesterUser_Users_UserId",
                table: "SemesterUser",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
