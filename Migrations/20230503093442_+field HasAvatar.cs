using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Social_Network_API.Migrations
{
    /// <inheritdoc />
    public partial class fieldHasAvatar : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "HasAvatar",
                table: "Users",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HasAvatar",
                table: "Users");
        }
    }
}
