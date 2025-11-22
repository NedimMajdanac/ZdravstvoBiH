using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Zdravstvo.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class TerminModified : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Uputnica",
                table: "Termini");

            migrationBuilder.AddColumn<int>(
                name: "UputnicaId",
                table: "Termini",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Termini_UputnicaId",
                table: "Termini",
                column: "UputnicaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Termini_Uputnice_UputnicaId",
                table: "Termini",
                column: "UputnicaId",
                principalTable: "Uputnice",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Termini_Uputnice_UputnicaId",
                table: "Termini");

            migrationBuilder.DropIndex(
                name: "IX_Termini_UputnicaId",
                table: "Termini");

            migrationBuilder.DropColumn(
                name: "UputnicaId",
                table: "Termini");

            migrationBuilder.AddColumn<bool>(
                name: "Uputnica",
                table: "Termini",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
