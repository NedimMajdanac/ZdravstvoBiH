using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Zdravstvo.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class MedKartonModified : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pacijenti_MedicinskiKartoni_MedicinskiKartonId",
                table: "Pacijenti");

            migrationBuilder.DropIndex(
                name: "IX_Pacijenti_MedicinskiKartonId",
                table: "Pacijenti");

            migrationBuilder.CreateIndex(
                name: "IX_Pacijenti_MedicinskiKartonId",
                table: "Pacijenti",
                column: "MedicinskiKartonId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Pacijenti_MedicinskiKartoni_MedicinskiKartonId",
                table: "Pacijenti",
                column: "MedicinskiKartonId",
                principalTable: "MedicinskiKartoni",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pacijenti_MedicinskiKartoni_MedicinskiKartonId",
                table: "Pacijenti");

            migrationBuilder.DropIndex(
                name: "IX_Pacijenti_MedicinskiKartonId",
                table: "Pacijenti");

            migrationBuilder.CreateIndex(
                name: "IX_Pacijenti_MedicinskiKartonId",
                table: "Pacijenti",
                column: "MedicinskiKartonId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pacijenti_MedicinskiKartoni_MedicinskiKartonId",
                table: "Pacijenti",
                column: "MedicinskiKartonId",
                principalTable: "MedicinskiKartoni",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
