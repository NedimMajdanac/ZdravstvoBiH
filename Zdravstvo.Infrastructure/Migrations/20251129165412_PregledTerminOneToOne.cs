using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Zdravstvo.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class PregledTerminOneToOne : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pregledi_Termini_TerminId",
                table: "Pregledi");

            migrationBuilder.DropIndex(
                name: "IX_Pregledi_TerminId",
                table: "Pregledi");

            migrationBuilder.CreateIndex(
                name: "IX_Pregledi_TerminId",
                table: "Pregledi",
                column: "TerminId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Pregledi_Termini_TerminId",
                table: "Pregledi",
                column: "TerminId",
                principalTable: "Termini",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pregledi_Termini_TerminId",
                table: "Pregledi");

            migrationBuilder.DropIndex(
                name: "IX_Pregledi_TerminId",
                table: "Pregledi");

            migrationBuilder.CreateIndex(
                name: "IX_Pregledi_TerminId",
                table: "Pregledi",
                column: "TerminId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pregledi_Termini_TerminId",
                table: "Pregledi",
                column: "TerminId",
                principalTable: "Termini",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
