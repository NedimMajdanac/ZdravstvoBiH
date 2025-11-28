using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Zdravstvo.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class dijagnozaPregledOneToOne : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Dijagnoze_Pregledi_PregledId",
                table: "Dijagnoze");

            migrationBuilder.DropIndex(
                name: "IX_Dijagnoze_PregledId",
                table: "Dijagnoze");

            migrationBuilder.CreateIndex(
                name: "IX_Dijagnoze_PregledId",
                table: "Dijagnoze",
                column: "PregledId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Dijagnoze_Pregledi_PregledId",
                table: "Dijagnoze",
                column: "PregledId",
                principalTable: "Pregledi",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Dijagnoze_Pregledi_PregledId",
                table: "Dijagnoze");

            migrationBuilder.DropIndex(
                name: "IX_Dijagnoze_PregledId",
                table: "Dijagnoze");

            migrationBuilder.CreateIndex(
                name: "IX_Dijagnoze_PregledId",
                table: "Dijagnoze",
                column: "PregledId");

            migrationBuilder.AddForeignKey(
                name: "FK_Dijagnoze_Pregledi_PregledId",
                table: "Dijagnoze",
                column: "PregledId",
                principalTable: "Pregledi",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
