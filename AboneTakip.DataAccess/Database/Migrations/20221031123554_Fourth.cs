using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AboneTakip.DataAccess.Database.Migrations
{
    public partial class Fourth : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsInvoiced",
                table: "Readings",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsInvoiced",
                table: "Readings");
        }
    }
}
