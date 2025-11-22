using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Zdravstvo.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UputnicaAdd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Uputnice",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PacijentId = table.Column<int>(type: "int", nullable: false),
                    DoktorId = table.Column<int>(type: "int", nullable: false),
                    SpecijalizacijaId = table.Column<int>(type: "int", nullable: false),
                    DatumIzdavanja = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SifraUputnice = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsKoristena = table.Column<bool>(type: "bit", nullable: false),
                    DatumKoristenja = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Uputnice", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Uputnice_Doktori_DoktorId",
                        column: x => x.DoktorId,
                        principalTable: "Doktori",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Uputnice_Pacijenti_PacijentId",
                        column: x => x.PacijentId,
                        principalTable: "Pacijenti",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Uputnice_Specijalizacija_SpecijalizacijaId",
                        column: x => x.SpecijalizacijaId,
                        principalTable: "Specijalizacija",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Uputnice_DoktorId",
                table: "Uputnice",
                column: "DoktorId");

            migrationBuilder.CreateIndex(
                name: "IX_Uputnice_PacijentId",
                table: "Uputnice",
                column: "PacijentId");

            migrationBuilder.CreateIndex(
                name: "IX_Uputnice_SpecijalizacijaId",
                table: "Uputnice",
                column: "SpecijalizacijaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Uputnice");
        }
    }
}
