using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CsKmsBackend.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class ModifyLogTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PostTitle",
                table: "Logs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "Logs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PostTitle",
                table: "Logs");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "Logs");
        }
    }
}
