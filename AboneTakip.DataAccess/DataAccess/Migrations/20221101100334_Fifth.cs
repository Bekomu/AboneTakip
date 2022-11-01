using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AboneTakip.DataAccess.DataAccess.Migrations
{
    public partial class Fifth : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsInvoiced",
                table: "Volumetrics",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsInvoiced",
                table: "Volumetrics");
        }
    }
}
