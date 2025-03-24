using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shipping.Api.Data.migration
{
    /// <inheritdoc />
    public partial class UpdateOrderTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PaymentTypeId",
                table: "Orders");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PaymentTypeId",
                table: "Orders",
                type: "int",
                nullable: true);
        }
    }
}
