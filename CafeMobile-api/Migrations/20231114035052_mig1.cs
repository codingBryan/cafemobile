using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CafeMobile_api.Migrations
{
    /// <inheritdoc />
    public partial class mig1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "cooking",
                table: "sales",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "pending",
                table: "sales",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "prepared",
                table: "sales",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "cooking",
                table: "sales");

            migrationBuilder.DropColumn(
                name: "pending",
                table: "sales");

            migrationBuilder.DropColumn(
                name: "prepared",
                table: "sales");
        }
    }
}
