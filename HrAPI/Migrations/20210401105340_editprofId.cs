using Microsoft.EntityFrameworkCore.Migrations;

namespace HrAPI.Migrations
{
    public partial class editprofId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Professions_Employees_ManagerID",
                table: "Professions");

            migrationBuilder.AlterColumn<int>(
                name: "ManagerID",
                table: "Professions",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Professions_Employees_ManagerID",
                table: "Professions",
                column: "ManagerID",
                principalTable: "Employees",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Professions_Employees_ManagerID",
                table: "Professions");

            migrationBuilder.AlterColumn<int>(
                name: "ManagerID",
                table: "Professions",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Professions_Employees_ManagerID",
                table: "Professions",
                column: "ManagerID",
                principalTable: "Employees",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
