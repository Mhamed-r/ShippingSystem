using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shipping.Api.Data.migration
{
    /// <inheritdoc />
    public partial class updatephone : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CustomerPhone2",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CustomerPhone2",
                table: "Orders");
        }
    }
}
