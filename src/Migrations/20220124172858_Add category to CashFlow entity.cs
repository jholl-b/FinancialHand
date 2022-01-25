using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinancialHand.Migrations
{
    public partial class AddcategorytoCashFlowentity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Category",
                table: "CashFlows",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Category",
                table: "CashFlows");
        }
    }
}
