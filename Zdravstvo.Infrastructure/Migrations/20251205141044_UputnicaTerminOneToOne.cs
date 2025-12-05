using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Zdravstvo.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UputnicaTerminOneToOne : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Termini_Uputnice_UputnicaId",
                table: "Termini");

            migrationBuilder.DropIndex(
                name: "IX_Termini_UputnicaId",
                table: "Termini");

            migrationBuilder.CreateIndex(
                name: "IX_Termini_UputnicaId",
                table: "Termini",
                column: "UputnicaId",
                unique: true,
                filter: "[UputnicaId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Termini_Uputnice_UputnicaId",
                table: "Termini",
                column: "UputnicaId",
                principalTable: "Uputnice",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
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
    }
}
