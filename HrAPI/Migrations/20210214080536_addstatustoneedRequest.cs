using Microsoft.EntityFrameworkCore.Migrations;

namespace HrAPI.Migrations
{
    public partial class addstatustoneedRequest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "NeedsRequests",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "EmployeeDocumentsDTO",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DocumentName = table.Column<string>(nullable: true),
                    FileName = table.Column<string>(nullable: true),
                    EmployeeID = table.Column<int>(nullable: false),
                    EmployeeName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeDocumentsDTO", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmployeeDocumentsDTO");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "NeedsRequests");
        }
    }
}
