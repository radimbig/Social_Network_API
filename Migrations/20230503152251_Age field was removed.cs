using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Social_Network_API.Migrations
{
    /// <inheritdoc />
    public partial class Agefieldwasremoved : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Age",
                table: "Users");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<sbyte>(
                name: "Age",
                table: "Users",
                type: "tinyint",
                nullable: false,
                defaultValue: (sbyte)0);
        }
    }
}
