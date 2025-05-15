using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Test.Entity.Migrations
{
    /// <inheritdoc />
    public partial class OccupiedSeatColumnAdd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OccupiedSeat",
                table: "Concerts",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OccupiedSeat",
                table: "Concerts");
        }
    }
}
