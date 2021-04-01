using Microsoft.EntityFrameworkCore.Migrations;

namespace HrAPI.Migrations
{
    public partial class editDeptId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_FacultyDepartments_FacultyDepartmentId",
                table: "Employees");

            migrationBuilder.AlterColumn<int>(
                name: "FacultyDepartmentId",
                table: "Employees",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_FacultyDepartments_FacultyDepartmentId",
                table: "Employees",
                column: "FacultyDepartmentId",
                principalTable: "FacultyDepartments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_FacultyDepartments_FacultyDepartmentId",
                table: "Employees");

            migrationBuilder.AlterColumn<int>(
                name: "FacultyDepartmentId",
                table: "Employees",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_FacultyDepartments_FacultyDepartmentId",
                table: "Employees",
                column: "FacultyDepartmentId",
                principalTable: "FacultyDepartments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
