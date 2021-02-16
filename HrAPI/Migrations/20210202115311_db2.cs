using Microsoft.EntityFrameworkCore.Migrations;

namespace HrAPI.Migrations
{
    public partial class db2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FileName",
                table: "EmployeeDocuments",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "FacultyDepartmentDTO",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FacultyDepartmentName = table.Column<string>(nullable: true),
                    FacultyId = table.Column<int>(nullable: false),
                    FacultyName = table.Column<string>(nullable: true),
                    UniversityId = table.Column<int>(nullable: false),
                    UniversityName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FacultyDepartmentDTO", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FacultyDTO",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FacultyName = table.Column<string>(nullable: true),
                    UniversityID = table.Column<int>(nullable: false),
                    UniversityName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FacultyDTO", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FacultyDepartmentDTO");

            migrationBuilder.DropTable(
                name: "FacultyDTO");

            migrationBuilder.DropColumn(
                name: "FileName",
                table: "EmployeeDocuments");
        }
    }
}
